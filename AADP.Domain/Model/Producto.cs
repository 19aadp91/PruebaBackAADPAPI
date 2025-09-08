namespace AADP.Domain.Model
{
    public class Producto
    {
        public int CodigoProducto { get;  set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get;  set; } = null!;
        public string ReferenciaInterna { get;  set; } = null!;
        public decimal PrecioUnitario { get;  set; }
        public string Estado { get;  set; } = null!;
        public string UnidadMedida { get;  set; } = null!;
        public DateTime FechaCreacion { get;  set; }
    }
}
