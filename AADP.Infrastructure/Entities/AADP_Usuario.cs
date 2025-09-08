namespace AADP.Infrastructure.Entities
{
    public class AADP_Usuario
    {
        public int IdUsuario { get; set; }

        public string NombreUsuario { get; set; } = string.Empty;

        public string CorreoElectronico { get; set; } = string.Empty;

        public string ClaveHash { get; set; } = string.Empty;

        public string Rol { get; set; } = "Usuario";

        public bool Estado { get; set; } = true;

        public DateTime FechaCreacion { get; set; }
    }
}