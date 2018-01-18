using DS.Core.Domain;

namespace DS.Core.Domain.Configuration
{
    public partial class Setting : BaseEntity
    {
        public Setting() { }

        public Setting(string name, string value) {
            this.Name = name;
            this.Value = value;
        }
        
        public int ID_Setting { get; set; }
        public string Name { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
