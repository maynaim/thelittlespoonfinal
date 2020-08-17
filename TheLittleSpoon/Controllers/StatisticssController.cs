using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheLittleSpoon.Data;
using TheLittleSpoon.Models;
using Microsoft.EntityFrameworkCore;


namespace TheLittleSpoon.Controllers
{
    public class StatisticssController : Controller
    {

        private readonly LittleSpoonDBContext _context;

        public StatisticssController(LittleSpoonDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Articles()
        {
            var articlesPerCategory = await _context.Articles.Join(_context.Categories,
                category => category.CategoryId,
                article => article.CategoryId,
                (article, category) => new Article()
                {
                    ArticleId = article.ArticleId,
                    CategoryId = category.CategoryId,
                    Category = category
                }).GroupBy(x => x.CategoryId).ToListAsync();

            var statistics = articlesPerCategory.Select(categoryArticles => new
            {
                label = categoryArticles.First().Category.Name,
                color = categoryArticles.First().Category.HexColor,
                count = categoryArticles.Count()
            }).ToList();

            return Json(statistics);
        }

        public async Task<IActionResult> ArticlesCreation()
        {
            var monthlyCreatedArtical = await _context.Articles.GroupBy(x => new { x.DateCreated.Value.Year, x.DateCreated.Value.Month, x.DateCreated.Value.Day }).ToListAsync();

            var statistics = monthlyCreatedArtical.Select(monthGroup => new {
                date = new DateTime(monthGroup.First().DateCreated.Value.Year, monthGroup.First().DateCreated.Value.Month, monthGroup.First().DateCreated.Value.Day),
                dateDate = new
                {
                    year = monthGroup.First().DateCreated.Value.Year,
                    month = monthGroup.First().DateCreated.Value.Month,
                    day = monthGroup.First().DateCreated.Value.Day
                },
                nps = monthGroup.Count()
            }).ToList();

            return Json(statistics);
        }

        public async Task<IActionResult> Comments()
        {
            var CommentsPerArticale = await _context.Comments.Join(_context.Articles,
               article => article.ArticleId,
               comment => comment.ArticleId,
               (comment, article) => new Comment()
               {
                   CommentId = comment.ArticleId,
                   ArticleId = article.ArticleId,
                   Article = article
               }).GroupBy(x => x.Article.CategoryId).ToListAsync();

            var statistics = CommentsPerArticale.Select(commentArticle => new
            {
                label = _context.Categories.Find(commentArticle.First().Article.CategoryId).Name,
                color = _context.Categories.Find(commentArticle.First().Article.CategoryId).HexColor,
                count = commentArticle.Count()
            }).ToList();

            return Json(statistics);
        }
    }
}
