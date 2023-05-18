
using _3rdBackendProject.Models.Base;

namespace _3rdBackendProject.Models
{
    public class Category:BaseEntity
    {
  
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
