using AADP.Domain.Model;
using AADP.Infrastructure.Entities;

namespace AADP.Infrastructure.Registry
{
    public static class InfraRegistry
    {
        private static readonly Dictionary<Type, Type> _registry = new()
        {
            {typeof(Producto), typeof(AADP_Producto)},
            {typeof(Usuario), typeof(AADP_Usuario)},
        };

        public static Type GetInfraestructuraType<TDominio>() where TDominio : class => GetInfraestructuraType(typeof(TDominio));

        public static Type GetInfraestructuraType(Type dominioType)
        {
            if (_registry.TryGetValue(dominioType, out var infraType)) return infraType;

            throw new InvalidOperationException($"No se ha registrado infraestructura para {dominioType.Name}");
        }
    }
}
