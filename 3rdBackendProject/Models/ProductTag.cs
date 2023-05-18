using _3rdBackendProject.Models;
using _3rdBackendProject.Models.Base;

namespace _3rdBackendProject.Models
{
    public class ProductTag:BaseEntity
    {

        public int ProductId { get; set; }
        public int TagId { get; set; }

        public Product Product { get; set; }
        public Tag Tag { get; set; }
    }
}
