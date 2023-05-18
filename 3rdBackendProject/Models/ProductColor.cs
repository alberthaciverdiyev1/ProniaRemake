using _3rdBackendProject.Models;
using _3rdBackendProject.Models.Base;

namespace _3rdBackendProject.Models
{
    public class ProductColor:BaseEntity
    {

        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public Product Product { get; set; }
        public Color Color { get; set; }
    }
}
