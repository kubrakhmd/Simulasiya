using mamba.Utilities.Enums;
using mamba.Utilities.Extensions.mamba.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.ViewModels.Employee;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<GetEmployeeVM> employeeVM = await _context.Employees.Include(e => e.Department).Select(
               e => new GetEmployeeVM
               {
                   Id = e.Id,
                   Name = e.Name,
                   DepatmentName = e.Department.Name,
                   Image = e.Image
               }).ToListAsync();

            return View(employeeVM);
        }
        public async Task<IActionResult> Create()
        {

            CreateEmployeeVM employeeVM = new CreateEmployeeVM
            {
                Departments = await _context.Departments.ToListAsync(),
            };
            return View(employeeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVM employeeVM)
        {
            employeeVM.Departments = await _context.Departments.ToListAsync();
            if (!ModelState.IsValid) return View(employeeVM);
            if (!employeeVM.MainPhoto.ValidateType("image/"))
            {
                ModelState.AddModelError(nameof(employeeVM.MainPhoto), "File type is incorrect");
                return View(employeeVM);
            }
            if (!employeeVM.MainPhoto.ValidateSize(FileSize.MB, 1))
            {
                ModelState.AddModelError(nameof(employeeVM.MainPhoto), "File size is incorrect");
                return View(employeeVM);
            }
            bool result = await _context.Departments.AnyAsync(c => c.Id == employeeVM.DepartmentId);
            if (!result)
            {
                ModelState.AddModelError(nameof(employeeVM.DepartmentId), "Wrong Department!");
                return View(employeeVM);
            }
            Employee employee = new()
            {
                Name=employeeVM.Name,
                DepartmentId=employeeVM.DepartmentId.Value

            };
            bool exist = await _context.Departments.AnyAsync(c => c.Id == employeeVM.DepartmentId);
            if (!result)
            {
                ModelState.AddModelError(nameof(employeeVM.DepartmentId), "Wrong Department!");
                return View(employeeVM);
            }


            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id < 1) return BadRequest();
            Employee employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            UpdateEmployeeVM employeeVM = new UpdateEmployeeVM
            {
                Name = employee.Name,
                DepartmentId = employee.DepartmentId,
                Departments = await _context.Departments.ToListAsync()

            };
            return View(employeeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateEmployeeVM employeeVM,int? id)
        {
            if (id is null || id < 1) return BadRequest();
            Employee existed = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (existed is null) return NotFound();

            employeeVM.Departments = await _context.Departments.ToListAsync();
            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }

            if (employeeVM.MainnPhoto != null)
            {
                if (!employeeVM.MainnPhoto.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(UpdateEmployeeVM.MainnPhoto), "Wrong Format!");
                    return View(employeeVM);
                }
                if (!employeeVM.MainnPhoto.ValidateSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(UpdateEmployeeVM.MainnPhoto), "Wrong Size!");
                    return View(employeeVM);
                }
            }
            if (existed.DepartmentId != employeeVM.DepartmentId)
            {
                bool result = await _context.Departments.AnyAsync(e => e.Id == employeeVM.DepartmentId);
                if (!result)
                {
                    ModelState.AddModelError(nameof(Department.Id), "This category does not exist!");
                    return View(employeeVM);
                }
            }

            if (employeeVM.ImageIds is null)
            {
                employeeVM.ImageIds = new List<int>();
            }
            existed.Name = employeeVM.Name;

            existed.DepartmentId = employeeVM.DepartmentId.Value;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }
    }
}