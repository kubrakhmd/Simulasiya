using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.ViewModels;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {

            List<GetDepartmentVM> departmentVMs = await _context.Departments.Where(d => !d.IsDeleted)
                .Include(d => d.Employees).Select(d =>
                new GetDepartmentVM
                {
                    Id = d.Id,
                    Name = d.Name,

                }

                ).ToListAsync();

            return View(departmentVMs);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentVM departmentVM)
        {
            if (!ModelState.IsValid) return View();
            bool result = await _context.Departments.AnyAsync(d => d.Name.Trim() == departmentVM.Name.Trim());
            if (result)
            {
                ModelState.AddModelError("Name", "name Already Exist");
                return View();
            }
            Department department = new()
            {
                Name = departmentVM.Name
            };
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {

            if (id == null || id < 1) return BadRequest();
            Department existed = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
            if (existed == null) return NotFound();
            if (!ModelState.IsValid) return View();



            return View(existed);

        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Department departmentVM)
        {

            if (id == null || id < 1) return BadRequest();
            Department existed = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
            if (departmentVM is null) return NotFound();
            if (!ModelState.IsValid) return View();
            bool result = await _context.Departments.AnyAsync(d => d.Id == id && d.Name == departmentVM.Name);
            if (result)
            {
                ModelState.AddModelError(nameof(Department.Name), "Department already exists");
                return View();
            }



            existed.Name = departmentVM.Name;

            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));

        }
        public async Task <IActionResult> Delete(int id)
        {

            return RedirectToAction(nameof(Index));
        }
    }
}