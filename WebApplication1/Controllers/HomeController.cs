using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new
            {
                Employees=_context.Employees.OrderBy(s=>s.Order)

            };
            return View();
        }
    }
}
