using _3rdBackendProject.Models.Base;

namespace _3rdBackendProject.Models
{
    public class Users:BaseEntity
    {   public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public string Comment { get; set; }
        public string ImageURL { get; set; }
    }
}
