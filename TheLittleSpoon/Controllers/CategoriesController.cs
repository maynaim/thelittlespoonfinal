using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheLittleSpoon.Models;
using TheLittleSpoon.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


namespace TheLittleSpoon.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly LittleSpoonDBContext _context;

        public CategoriesController(LittleSpoonDBContext context)
        {
            _context = context;
        }

        // GET: Categories
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        public IActionResult CategoriesError()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        // GET: Categories/Details/1
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["Articles"] = await _context.Articles
                .Where(x => x.CategoryId == id).ToListAsync();

            if (id == null)
            {
                return RedirectToAction("CategoriesError");
            }

            var categoryModel = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (categoryModel == null)
            {
                return RedirectToAction("CategoriesError");
            }

            return View(categoryModel);
        }

        [Authorize]
        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("CategoryId, Name, Description, Color")] Category categoryModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryModel);
        }

        // GET: Categories/Edit/1
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("CategoriesError");
            }

            var categoryModel = await _context.Categories.FindAsync(id);
            if (categoryModel == null)
            {
                return RedirectToAction("CategoriesError");
            }
            return View(categoryModel);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId, Name, Description, Color")] Category categoryModel)
        {
            if (id != categoryModel.CategoryId)
            {
                return RedirectToAction("CategoriesError");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryModelExists(categoryModel.CategoryId))
                    {
                        return RedirectToAction("CategoriesError");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoryModel);
        }

        // GET: Categories/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("CategoriesError");
            }

            var categoryModel = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (categoryModel == null)
            {
                return RedirectToAction("CategoriesError");
            }

            return View(categoryModel);
        }

        // POST: Categories/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Remove articles of the selected category
            foreach (Article article in _context.Articles.Where(article => article.CategoryId == id))
            {
                _context.Articles.Remove(article);
            }

            var categoryModel = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(categoryModel);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryModelExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}