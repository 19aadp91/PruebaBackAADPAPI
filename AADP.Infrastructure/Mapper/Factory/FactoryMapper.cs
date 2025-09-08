using System.Collections.Concurrent;

namespace AADP.Infrastructure.Mapper.Factory
{
    internal readonly struct TypePair(Type source, Type target) : IEquatable<TypePair>
    {
        public Type Source { get; } = source;
        public Type Target { get; } = target;

        public bool Equals(TypePair other) => Source == other.Source && Target == other.Target;

        public override bool Equals(object? obj) => obj is TypePair other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Source, Target);
    }

    public class FactoryMapper : IFactoryMapper
    {
        private static readonly ConcurrentDictionary<TypePair, object> _mappers = new();

        public IEntityMapper<TSrc, TDest> Mapper<TSrc, TDest>() where TSrc : class, new() where TDest : class, new()
        {
            var key = new TypePair(typeof(TSrc), typeof(TDest));

            return (IEntityMapper<TSrc, TDest>)_mappers.GetOrAdd(key, _ =>
            {
                var mapperType = typeof(EntityMapperr<,>).MakeGenericType(typeof(TSrc), typeof(TDest));
                return Activator.CreateInstance(mapperType)!;
            });
        }
    }
}