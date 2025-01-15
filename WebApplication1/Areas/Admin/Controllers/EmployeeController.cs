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
            if (!employeeVM.SecondaryPhoto.ValidateType("image/"))
            {
                ModelState.AddModelError(nameof(employeeVM.SecondaryPhoto), "File type is incorrect");
                return View(employeeVM);
            }
            if (!employeeVM.SecondaryPhoto.ValidateSize(FileSize.MB, 1))
            {
                ModelState.AddModelError(nameof(employeeVM.SecondaryPhoto), "File size is incorrect");
                return View(employeeVM);
            }
        }
    }
}