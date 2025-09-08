using AADP.API.Dtos.UserDto;
using AADP.Application.Port.In;
using AADP.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AADP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController(IUserServices userServices, ILogger<UserController> logger) : ControllerBase
    {
        private readonly IUserServices _userServices = userServices;
        private readonly ILogger<UserController> _logger = logger;

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAllAsync()
        {
            try
            {
                return Ok(await _userServices.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener usuarios en GetAllAsync");
                return Problem("Ha ocurrido un error al procesar la solicitud");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Usuario?>> GetByIdAsync(int id)
        {
            try
            {
                return Ok(await _userServices.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener usuarios en GetByIdAsync");
                return Problem("Ha ocurrido un error al procesar la solicitud");
            }
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<int>> InsertAsync([FromBody] CreateUsuario entity)
        {
            try
            {
                var newId = await _userServices.InsertAsync(
                    new() 
                    { 
                        CorreoElectronico = entity.CorreoElectronico,
                        ClaveHash = entity.ClaveHash,
                        NombreUsuario = entity.NombreUsuario,
                        Rol = entity.Rol,
                        IdUsuario = 0,
                        Estado = true,
                        FechaCreacion = DateTime.UtcNow,
                    }
                );
                return Ok("El usuario fue creado con exito");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al insertar usuario en InsertAsync");
                return Problem("Ha ocurrido un error al procesar la solicitud");
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateUsuario entity)
        {
            try
            {
                var updated = await _userServices.UpdateAsync(
                    new() 
                    {
                        IdUsuario = entity.IdUsuario,
                        CorreoElectronico = entity.CorreoElectronico,
                        ClaveHash = entity.ClaveHash,
                        NombreUsuario = entity.NombreUsuario,
                        Rol = entity.Rol,
                        Estado = entity.Estado,
                        FechaCreacion = DateTime.UtcNow,   
                    });
                if (updated)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar usuario en UpdateAsync");
                return Problem("Ha ocurrido un error al procesar la solicitud");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var deleted = await _userServices.DeleteAsync(id);
                if (deleted)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar usuario en DeleteAsync");
                return Problem("Ha ocurrido un error al procesar la solicitud");
            }
        }
    }
}
