using System.Diagnostics;
using BTL_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL_WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly PetCareHubContext _context;

        public HomeController(PetCareHubContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.FeaturedServices = await _context.Services
                .Include(x => x.Category)
                .Where(x => x.Status == "Active")
                .OrderBy(x => x.ServiceName)
                .Take(3)
                .ToListAsync();

            ViewBag.FeaturedPets = await _context.Pets
                .Include(x => x.PetImages)
                .Where(x => x.Status == "Active" && x.AdoptionStatus == "Available")
                .OrderByDescending(x => x.CreatedAt)
                .Take(6)
                .ToListAsync();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
