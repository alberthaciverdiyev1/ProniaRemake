using _3rdBackendProject.Models.Base;

namespace _3rdBackendProject.Models
{
    public class Profession:BaseEntity
    {
        public string Name { get; set; }
        public List<Client>? Clients { get; set; }
    }
}
