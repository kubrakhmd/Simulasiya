using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;
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
        { HomeVM homeVM = new HomeVM
        {

            Employees=_context.Employees.OrderBy(e=>e.Id).Take(2).ToList()

        };
          
            return View(homeVM);
        }
    }
}
