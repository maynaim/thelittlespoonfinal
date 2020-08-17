using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheLittleSpoon.Models;
using TheLittleSpoon.Data;

namespace TheLittleSpoon.Controllers
{
    public class SearchController : Controller
    {
        private readonly LittleSpoonDBContext _context;

        public SearchController(LittleSpoonDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Categories = _context.Categories.ToList<Category>();
            return View();
        }
    }
}
