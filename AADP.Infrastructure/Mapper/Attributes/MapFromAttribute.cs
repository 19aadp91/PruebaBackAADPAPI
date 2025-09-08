namespace AADP.Infrastructure.Mapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MapFromAttribute(string sourceName) : Attribute
    {
        public string SourceName { get; } = sourceName;
    }
}
