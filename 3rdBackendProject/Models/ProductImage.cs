using _3rdBackendProject.Models;

namespace _3rdBackendProject.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public bool? IsPrimary { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
