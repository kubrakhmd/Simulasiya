using WebApplication1.Models;

namespace WebApplication1.Areas.ViewModels.Employee
{
    public class CreateEmployeeVM
    { public string Name { get; set; }
        public int ?DepartmentId { get; set; }   
        public List<Department>?Departments { get; set; }
        public IFormFile MainPhoto { get; set; }
        public IFormFile SecondaryPhoto { get; set; }

    }
}
