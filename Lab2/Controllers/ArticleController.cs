using Microsoft.AspNetCore.Mvc;
using Lab2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab2.Controllers
{
    public class ArticleController : Controller
    {
        private readonly Database _dbcontext;

        public ArticleController(Database dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IActionResult Index()
        {
            return View(_dbcontext.Articles!.ToList());   
        }
        public IActionResult Add()
        {
            var tempListAuthors = _dbcontext.Authors.ToList();
            var listAuthors = new List<SelectListItem>();
            foreach (var temp in tempListAuthors)
            {
                string id = temp.AuthorId.ToString();
                string name = temp.FirstName.ToString() + " " + temp.LastName.ToString();
                listAuthors.Add(new SelectListItem(name, id));
            }
            ViewBag.ListAuthors = listAuthors;

            var tempListCategories = _dbcontext.Categories.ToList();
            var listCategories = new List<SelectListItem>();
            foreach (var category in tempListCategories)
            {
                string id = category.CategoryId.ToString();
                string name = category.Name.ToString();
                listCategories.Add(new SelectListItem(name, id));
            }
            ViewBag.ListCategories = listCategories;

            var tempListTags = _dbcontext.Tags.ToList();
            var listTags = new List<SelectListItem>();
            foreach(var tag in tempListTags)
            {
                string id = tag.TagId.ToString();
                string name = tag.Name.ToString();
                listTags.Add(new SelectListItem(name, id));
            }
            ViewBag.ListTags = listTags;
            return View();
        }
        [HttpPost]
        public IActionResult Added(Article article, List<int> Tags)
        {

            var tagList = _dbcontext.Tags.Where(x => Tags.Contains(x.TagId)).ToList();
            
            Article articl1e = new()
            {
                Title = article.Title,
                Lead = article.Lead,
                Content = article.Content,
                AuthorId = article.AuthorId,
                CategoryId = article.CategoryId,
                CreationDate = DateTime.Now,
                Tags = tagList
            };

            _dbcontext.Articles.Add(articl1e);
            _dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var tempListAuthors = _dbcontext.Authors.ToList();
            var listAuthors = new List<SelectListItem>();
            foreach (var temp in tempListAuthors)
            {
                string tempId = temp.AuthorId.ToString();
                string name = temp.FirstName.ToString() + " " + temp.LastName.ToString();
                listAuthors.Add(new SelectListItem(name, tempId));
            }
            ViewBag.ListAuthors = listAuthors;

            var tempListCategories = _dbcontext.Categories.ToList();
            var listCategories = new List<SelectListItem>();
            foreach (var category in tempListCategories)
            {
                string tempId = category.CategoryId.ToString();
                string name = category.Name.ToString();
                listCategories.Add(new SelectListItem(name, tempId));
            }
            ViewBag.ListCategories = listCategories;

            var tempListTags = _dbcontext.Tags.ToList();
            var listTags = new List<SelectListItem>();
            foreach (var tag in tempListTags)
            {
                string tempId = tag.TagId.ToString();
                string name = tag.Name.ToString();
                listTags.Add(new SelectListItem(name, tempId));
            }
            ViewBag.ListTags = listTags;
            return View(_dbcontext.Articles.FirstOrDefault(x => x.ArticleId == id));
        }
        [HttpPost]
        public IActionResult Edited(Article artcile, List<int> Tags)
        {

            if(_dbcontext.Articles.Find(artcile.ArticleId) != null)
            {
                var tempArtcile = _dbcontext.Articles.FirstOrDefault(x => x.ArticleId == artcile.ArticleId);
                var tagsList = _dbcontext.Tags.Where(x => Tags.Contains(x.TagId)).ToList();

                if (tempArtcile != null)
                {
                    tempArtcile.Title = artcile.Title.ToString();
                    tempArtcile.Lead = artcile.Lead.ToString();
                    tempArtcile.Content = artcile.Content.ToString();
                    tempArtcile.AuthorId = artcile.AuthorId;
                    tempArtcile.CategoryId = artcile.CategoryId;

                    foreach (var tag in tempArtcile.Tags!)
                    {
                        tempArtcile.Tags.Remove(tag);
                    }
                    tempArtcile.Tags = tagsList;
                    _dbcontext.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            return View(_dbcontext.Articles.FirstOrDefault(x => x.ArticleId == id));
        }
        [HttpPost]
        public IActionResult Deleted(Article article)
        {
            var articleToDelete = _dbcontext.Articles.FirstOrDefault(x => x.ArticleId == article.ArticleId);
            
            if(articleToDelete != null)
            {
                _dbcontext.Articles.Remove(articleToDelete);
                _dbcontext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public IActionResult Show(int id)
        {
            var article = _dbcontext.Articles.FirstOrDefault(x => x.ArticleId == id);
            ViewBag.Comments = article!.Comments!.OrderByDescending(x => x.CommentId);
            return View(article);
        }
    }
}
