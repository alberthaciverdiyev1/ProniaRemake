using _3rdBackendProject.Models;

namespace _3rdBackendProject.Models
{
    public class ProductColor
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public Product Product { get; set; }
        public Color Color { get; set; }
    }
}
