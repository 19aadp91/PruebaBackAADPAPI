namespace AADP.API.Dtos.UserDto
{
    public class UpdateUsuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;
        public string ClaveHash { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public bool Estado { get; set; }
    }
}
