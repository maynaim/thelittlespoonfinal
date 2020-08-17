using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheLittleSpoon.Models;
using TheLittleSpoon.Data;
using Microsoft.EntityFrameworkCore;

namespace TheLittleSpoon.Controllers
{
    public class HomeController : Controller
    {

        private readonly LittleSpoonDBContext _context;

        public HomeController(LittleSpoonDBContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            Dictionary<Category, List<Article>> categories = new Dictionary<Category, List<Article>>();

            foreach (Category c in _context.Categories)
            {
                categories.Add(c, await GetFeatured(c).ToListAsync());
            }

            ViewData["Categories"] = categories;

            ViewData["MainArticle"] = null;
            ViewData["MainArticleCategory"] = null;

            if (_context.Articles.Any())
            {
                // Set the main article (last one added to site)
                Article mainArticle = await _context.Articles.OrderByDescending(x => x.DateCreated).FirstAsync();
                ViewData["MainArticle"] = mainArticle;
                ViewData["MainArticleCategory"] = await _context.Categories
                    .FirstOrDefaultAsync(c => c.CategoryId == mainArticle.CategoryId);

                // Set all articles
                List<Article> allArticles = await _context.Articles.ToListAsync();
                ViewData["Articles"] = allArticles;

                // Set featured articles (randomly choose 2 articles that aren't the main article)
                List<Article> featuredArticles = new List<Article>();
                Random r = new Random();
                int articlesAdded = 0, totalArticles = _context.Articles.Count();
                const int NUM_ARTICLES_TO_ADD = 2;

                while (articlesAdded < NUM_ARTICLES_TO_ADD && totalArticles >= NUM_ARTICLES_TO_ADD)
                {
                    int articleIndex = r.Next(1, totalArticles);
                    Article article = allArticles[articleIndex];
                    if (article.ArticleId != mainArticle.ArticleId && !featuredArticles.Contains(article))
                    {
                        featuredArticles.Add(article);
                        articlesAdded++;
                    }
                }

                ViewData["Featured"] = featuredArticles;

                // Set recent articles (the last 3 added before the main article)
                ViewData["Recent"] = await _context.Articles.OrderByDescending(x => x.DateCreated).Skip(1).Take(3).ToListAsync();
            }

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult BMI()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Index(Models.BMIFormData formData)
        {
            formData.BMIReport = BMICalculator.GetBMIValue(formData.UserWeight, formData.UserHieght);

            ViewBag.BmiRes = formData.BMIReport.BmiValue;
            return View();
        }
    

     

        public IQueryable<Article> GetFeatured(Category category)
        {
            return _context.Entry(category).Collection(p => p.Articles).Query().Take(4);
        }
    }
}
