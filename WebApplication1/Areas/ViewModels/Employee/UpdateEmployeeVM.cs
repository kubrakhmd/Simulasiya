using WebApplication1.Models;

namespace WebApplication1.Areas.ViewModels.Employee
{
    public class UpdateEmployeeVM
    {
        public string Name { get; set; }
        public int? DepartmentId { get; set; }
        public List<Department>? Departments { get; set; }
        public IFormFile MainnPhoto { get; set; }
        public List<int> ImageIds { get; set; }
    }
}
