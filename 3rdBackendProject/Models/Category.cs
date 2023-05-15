using _3rdBackendProject.Models;

namespace _3rdBackendProject.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
