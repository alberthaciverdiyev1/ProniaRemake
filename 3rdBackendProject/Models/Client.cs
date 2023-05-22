using _3rdBackendProject.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3rdBackendProject.Models
{
    public class Client:BaseEntity
    {
        public string Name { get; set; }
        public string? Surname { get; set; }
        public Profession? Profession { get; set; }
        public int ProfessionId { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
