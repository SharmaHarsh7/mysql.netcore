using DS.Core.Enums;

namespace DS.Core.Domain
{
    public class ModelMetaData
    {
        public string Description { get; set; } = "";
        public object DefaultValue { get; set; } = null;
        public DataType Type { get; set; } = DataType.Custom;
        public bool Required { get; set; } = false;
        public int MinLength { get; set; } = -1;
        public int MaxLength { get; set; } = -1;
    }
}
