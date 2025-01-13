namespace WebApplication1.Models
{
    public class Employees:BaseEntity
    {
        public  string  Name{ get; set; }
        public string  Surname { get; set; }
        public string  FbLink { get; set; }
        public string TwitterLink { get; set; }
        public string Image { get; set; }
        public int DepartmentId { get; set; }
        public Departments Department { get; set; } 
    }
}
