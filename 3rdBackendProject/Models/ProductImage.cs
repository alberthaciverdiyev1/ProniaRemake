using _3rdBackendProject.Models;
using _3rdBackendProject.Models.Base;

namespace _3rdBackendProject.Models
{
    public class ProductImage:BaseEntity
    {
 
        public string Image { get; set; }
        public bool? IsPrimary { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
