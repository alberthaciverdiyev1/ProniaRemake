using _3rdBackendProject.Models.Base;

namespace _3rdBackendProject.Models
{
    public class Setting:BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
