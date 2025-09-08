using AADP.Application.Port.In;
using AADP.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AADP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductoController(IProductoServices productoServices, ILogger<ProductoController> logger) : ControllerBase
    {
        private readonly IProductoServices _productoServices = productoServices;
        private readonly ILogger<ProductoController> _logger = logger;

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetAllAsync()
        {
            try
            {
                return Ok(await _productoServices.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener usuarios en GetAllAsync");
                return Problem("Ha ocurrido un error al procesar la solicitud");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Producto?>> GetByIdAsync(int id)
        {
            try
            {
                return Ok(await _productoServices.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener usuarios en GetByIdAsync");
                return Problem("Ha ocurrido un error al procesar la solicitud");
            }
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<string>> InsertAsync([FromBody] Producto entity)
        {
            try
            {
                var newId = await _productoServices.InsertAsync(entity);
                return Ok(new { mensaje = "El usuario fue creado con éxito" });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al insertar usuario en InsertAsync");
                return Problem("Ha ocurrido un error al procesar la solicitud");
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult> UpdateAsync([FromBody] Producto entity)
        {
            try
            {
                var updated = await _productoServices.UpdateAsync(entity);
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
                var deleted = await _productoServices.DeleteAsync(id);
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
