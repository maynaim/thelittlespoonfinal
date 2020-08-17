using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheLittleSpoon.Models;
using TheLittleSpoon.Data;

namespace TheLittleSpoon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : Controller
    {
        private readonly LittleSpoonDBContext _context;

        public CommentsController(LittleSpoonDBContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        [HttpGet]
        public IEnumerable<Comment> GetComments()
        {
            return _context.Comments;
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // PUT: api/Comments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment([FromRoute] int id, [FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.CommentId)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Comments
        [HttpPost]
        public async Task<IActionResult> PostComment([FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(comment.Author) || string.IsNullOrWhiteSpace(comment.Content))
            {
                return BadRequest();
            }

            comment.Sequence = await _context.Comments.Where(c => c.ArticleId == comment.ArticleId).CountAsync() + 1;
            comment.DatePosted = DateTime.Now;

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.CommentId }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return Ok(comment);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search(string Author, string Content, DateTime? DatePosted)
        {
            var result = _context.Comments.AsQueryable();

            if (!String.IsNullOrWhiteSpace(Author))
                result = result.Where(x => x.Author.Contains(Author));
            if (!String.IsNullOrWhiteSpace(Content))
                result = result.Where(x => x.Content.Contains(Content));
            if (DatePosted.HasValue)
                result = result.Where(x => x.DatePosted.Date == DatePosted.Value.Date);

            return Json(result);
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.CommentId == id);
        }
    }
}