using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheLittleSpoon.Models;
using TheLittleSpoon.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace TheLittleSpoon.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly LittleSpoonDBContext _context;
        private readonly IHostingEnvironment _env;

        private readonly string _articlesTransitionDataPath;

        public ArticlesController(LittleSpoonDBContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;

            // Set path to file that holds the article transitions for training the ML
            // note - make sure that the "Copy to Output Directory" property of the file is set to "Copy Always"
            _articlesTransitionDataPath = System.IO.Path.Combine(_env.WebRootPath, "ml", "articles-transition-data.txt");
        }

        [Authorize]
        // GET: Articles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Articles.ToListAsync());
        }

        public IActionResult ArticleError()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        // GET: Articles/Details/?
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ArticleError");
            }

            var article = await _context.Articles.Include(a => a.Category).FirstOrDefaultAsync(m => m.ArticleId == id);

            if (article == null)
            {
                return RedirectToAction("ArticleError");
            }

            return View(article);
        }

        // GET: Articles/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Articles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Header,Summary,Content,HomeImageUrl,CategoryId,Location,IsShowMap")] Article articleModel)
        {
            if (ModelState.IsValid)
            {
                articleModel.DateCreated = DateTime.Now;
                _context.Add(articleModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(articleModel);
        }

        // GET: Articles/Edit/?
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ArticleError");
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return RedirectToAction("ArticleError");
            }

            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name");

            return View(article);
        }

        // POST: Articles/Edit/?
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleId,Header,Summary,Content,HomeImageUrl,CategoryId,Location,IsShowMap")] Article article)
        {
            if (id != article.ArticleId)
            {
                return RedirectToAction("ArticleError");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Articles.Attach(article);
                    _context.Entry(article).Property(x => x.Content).IsModified = true;
                    _context.Entry(article).Property(x => x.Header).IsModified = true;
                    _context.Entry(article).Property(x => x.Summary).IsModified = true;
                    _context.Entry(article).Property(x => x.HomeImageUrl).IsModified = true;
                    _context.Entry(article).Property(x => x.CategoryId).IsModified = true;
                    _context.Entry(article).Property(x => x.Location).IsModified = true;
                    _context.Entry(article).Property(x => x.IsShowMap).IsModified = true;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.ArticleId))
                    {
                        return RedirectToAction("ArticleError");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: Articles/Delete/?
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ArticleError");
            }

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return RedirectToAction("ArticleError");
            }

            return View(article);
        }

        // POST: Articles/Delete/?
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.ArticleId == id);
        }

        public async Task<IActionResult> Search(int? category, string header, string summary, DateTime? date)
        {
            var result = _context.Articles.AsQueryable();

            if (category != null)
                result = result.Where(x => x.CategoryId == category);
            if (!String.IsNullOrWhiteSpace(header))
                result = result.Where(x => x.Header.Contains(header));
            if (!String.IsNullOrWhiteSpace(summary))
                result = result.Where(x => x.Summary.Contains(summary));
            if (date.HasValue)
                result = result.Where(x => x.DateCreated.Value.Date == date.Value.Date);

            return Json(result);
        }

        public async Task<IActionResult> Comments(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ArticleError");
            }

            var comments = await _context.Comments.Where(c => c.ArticleId == id.Value).ToListAsync();

            if (comments == null)
            {
                return RedirectToAction("ArticleError");
            }

            return Json(comments);
        }

        // This function is calles onlt when you "transition" between articles.
        // When a user presses an article in the "related" section, we first save the transition
        // and only then really redirect him to his requested article page
        public async Task<IActionResult> MoveRelated(int? prevId, int? newId)
        {
            if (prevId == null || newId == null)
            {
                return RedirectToAction("ArticleError");
            }

            // Insert to training-set only if the article id's are differnet
            if (prevId.Value != newId.Value)
            {
                // Create a new line for the training data file in format "0.0,0.0" (we need a float num for the ML model)
                string line = ((float)prevId).ToString("0.0") + ',' + ((float)newId).ToString("0.0");

                // Add new line to training data file
                System.IO.File.AppendAllText(_articlesTransitionDataPath, line + Environment.NewLine);
            }

            // Now really redirect to the requested article
            return RedirectToAction(nameof(Details), new { id = newId });
        }

        // This function is called via javascript in the "Details" view of an article
        public async Task<IActionResult> GetRelatedArticles(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ArticleError");
            }

            const int NUM_OF_RELATED_ARTICLES = 3;
            List<Article> relatedArticles = new List<Article>();

            try
            {
                // Get the related article to the viewed article
                int mlRelatedArticle = MachineLearning.GetRelatedArticle(_articlesTransitionDataPath, id.Value);

                // Make sure we don't recommend the same article as the viewed article
                if (mlRelatedArticle != id.Value)
                {
                    var article = await _context.Articles.FirstAsync(c => c.ArticleId == mlRelatedArticle);

                    if (article != null)
                    {
                        relatedArticles.Add(article);

                        // Get the related article to the related article
                        int mlRelatedToRelatedArticle = MachineLearning.GetRelatedArticle(_articlesTransitionDataPath, mlRelatedArticle);

                        // Check that the recommended article isn't itself or the previous recommended article
                        if (mlRelatedToRelatedArticle != mlRelatedArticle && mlRelatedToRelatedArticle != id.Value)
                        {
                            article = await _context.Articles.FirstAsync(c => c.ArticleId == mlRelatedToRelatedArticle);

                            if (article != null)
                            {
                                relatedArticles.Add(article);
                            }
                        }
                    }
                }

            }
            catch (Exception)
            {
                // We don't do anything since it is OK for the algorithm to fail is there isn't any data for the viewed article
            }

            int articlesCount = _context.Articles.Count();
            Random random = new Random();

            // Fill the rest of the articles with random articles (in case the ML returned the same article and for better training of the model)
            while (relatedArticles.Count() < NUM_OF_RELATED_ARTICLES)
            {
                int randomArticleId = random.Next(1, articlesCount);

                if (randomArticleId != id.Value && !relatedArticles.Exists(c => c.ArticleId == randomArticleId))
                {
                    var randomArticle = await _context.Articles.FirstAsync(c => c.ArticleId == randomArticleId);

                    if (randomArticle != null)
                    {
                        relatedArticles.Add(randomArticle);
                    }
                }
            }

            return Json(relatedArticles);
        }
    }
    }
