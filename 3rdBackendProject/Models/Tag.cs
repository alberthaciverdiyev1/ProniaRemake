using _3rdBackendProject.Models.Base;

namespace _3rdBackendProject.Models
{
    public class Tag:BaseEntity
    {
        public string Name { get; set; }

        public List<ProductTag> ProductTags { get; set; }
    }
}
