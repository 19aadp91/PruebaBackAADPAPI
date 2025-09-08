using AADP.Infrastructure.Mapper.Attributes;
using System.Collections;
using System.Reflection;

namespace AADP.Infrastructure.Mapper
{
    public class EntityMapperr<TSrc, TDest> : IEntityMapper<TSrc, TDest> where TSrc : class, new() where TDest : class, new()
    {
        private readonly Dictionary<object, object> _visited = [];

        public TDest MapTo(TSrc src)
        {
            if (src == null) return null!;
            return (TDest)MapObject(src, typeof(TDest))!;
        }

        private object? MapObject(object? src, Type destType)
        {
            if (src is null) return null;

            if (_visited.TryGetValue(src, out var existing))
                return existing;

            var dest = Activator.CreateInstance(destType)!;
            _visited[src] = dest;

            foreach (var destProp in destType.GetProperties().Where(p => p.CanWrite))
            {
                var srcName = destProp.GetCustomAttribute<MapFromAttribute>()?.SourceName ?? destProp.Name;
                var srcProp = src.GetType().GetProperty(srcName);

                if (srcProp is null || !srcProp.CanRead || srcProp.GetIndexParameters().Length > 0)
                    continue;

                var srcValue = srcProp.GetValue(src);
                if (srcValue is null) continue;

                if (IsListType(srcProp.PropertyType, out var srcItemType) &&
                    IsListType(destProp.PropertyType, out var destItemType))
                {
                    var list = ((IEnumerable)srcValue).Cast<object>();
                    var mappedList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(destItemType))!;
                    foreach (var item in list)
                        mappedList.Add(MapObject(item, destItemType));

                    destProp.SetValue(dest, mappedList);
                    continue;
                }

                if (IsComplexType(srcProp.PropertyType) && IsComplexType(destProp.PropertyType))
                {
                    destProp.SetValue(dest, MapObject(srcValue, destProp.PropertyType));
                    continue;
                }

                destProp.SetValue(dest, srcValue);
            }

            return dest;
        }

        public static List<TDestItem> MapList<TSrcItem, TDestItem>(List<TSrcItem> source)
            where TSrcItem : class, new()
            where TDestItem : class, new()
        {
            if (source == null) return [];
            var mapper = new EntityMapperr<TSrcItem, TDestItem>();
            return [.. source.Select(mapper.MapTo)];
        }

        private static bool IsListType(Type type, out Type itemType)
        {
            if (type == typeof(string))
            {
                itemType = null!;
                return false;
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                itemType = type.GetGenericArguments()[0];
                return true;
            }

            var ienum = type.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));

            if (ienum != null)
            {
                itemType = ienum.GetGenericArguments()[0];
                return true;
            }

            itemType = null!;
            return false;
        }


        private static bool IsComplexType(Type type) => type.IsClass && type != typeof(string);
    }
}
