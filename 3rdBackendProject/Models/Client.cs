using _3rdBackendProject.Models.Base;

namespace _3rdBackendProject.Models
{
    public class Client:BaseEntity
    {
        public string Name { get; set; }
        public string? Surname { get; set; }
        public Profession? Profession { get; set; }
        public int ProfessionId { get; set; }
    }
}
