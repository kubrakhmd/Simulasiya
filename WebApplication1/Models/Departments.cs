namespace WebApplication1.Models
{
    public class Departments:BaseEntity
    {
        public string Name{ get; set; }
        public List<Employees>? Employees { get; set; }
    }
}
