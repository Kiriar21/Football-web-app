using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Lab2.Models;

namespace Lab2.Controllers
{
    public class CommentController : Controller
    {
        private readonly Database _dbcontext;
        public CommentController(Database dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add(int id)
        {
            ViewBag.Article = _dbcontext.Articles.FirstOrDefault(x => x.ArticleId == id);
            return View();
        }
        [HttpPost]
        public IActionResult Added(Comment comment, int ArticleId)
        {
            var article = _dbcontext.Articles.FirstOrDefault(x => x.ArticleId == ArticleId);
            if (article != null)
            {
                Comment tempComment = new()
                {
                    Title = comment.Title,
                    Content = comment.Content,
                    ArticleId = ArticleId
                };

                _dbcontext.Add(tempComment);
                _dbcontext.SaveChanges();
            } 
            else
            {
                return RedirectToAction("Homepage","Index");
            }
            return RedirectToAction("Show", "Article", new { id = ArticleId });
        }
        public IActionResult Edit(int id)
        {

            var comment = _dbcontext.Comments.FirstOrDefault(x => x.CommentId == id);
            if (comment != null)
            {
                var aid = comment.ArticleId;
                var article = _dbcontext.Articles.FirstOrDefault(x => x.ArticleId == aid);
                ViewBag.Article = article;
                return View(comment);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edited(Comment comment, int ArticleId)
        {
            var tempComment= _dbcontext.Comments!.FirstOrDefault(x => x.CommentId == comment.CommentId);
            tempComment!.Title = comment.Title;
            tempComment!.Content = comment.Content;
            _dbcontext.SaveChanges();
            return RedirectToAction("Show", "Article", new { id = ArticleId });
        }
        public IActionResult Delete(int id, int ArticleId)
        {
            var article = _dbcontext.Articles.FirstOrDefault(x => x.ArticleId == ArticleId);
            if (article != null)
            {
                var comment = article.Comments!.FirstOrDefault(x => x.CommentId == id);
                if(comment != null)
                {
                    article.Comments!.Remove(comment);
                    _dbcontext.SaveChanges();
                }
            }
            return RedirectToAction("Show", "Article", new { id = ArticleId });
        }
    }
}
