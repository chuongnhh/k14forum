using K14Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using K14Forum.CodeHelper;
using PagedList;
using System.Web.Configuration;

namespace K14Forum.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        // GET: Topic
        public ActionResult Index(int? page)
        {
            CurrentAction.currentAction = "Article";
            var model = db.ApplicationArticles
                .OrderByDescending(x => x.DateCreated);

            int pageSize = int.Parse(WebConfigurationManager.AppSettings["pageSize"]);
            int pageNumber = (page ?? 1);

            return View(model.ToPagedList(pageNumber, pageSize));
        }

        [AllowAnonymous]
        // GET: Topic/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            CurrentAction.currentAction = "Article";
            var model = db.ApplicationArticles.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            model.Views++;
            await db.SaveChangesAsync();
            return View(model);
        }

        // GET: Topic/Create
        public ActionResult Create()
        {
            CurrentAction.currentAction = "Manage-Article-Create";
            //ViewBag.CategoryId = new SelectList(db.ApplicationCategories, "Id", "Category");
            return View();
        }

        // POST: Topic/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateArticleViewModel model)
        {
            CurrentAction.currentAction = "Manage-Article-Create";
            if (ModelState.IsValid)
            {
                var Article = new ApplicationArticle
                {
                    Title = model.Title,
                    ArticleContent = model.ArticleContent,
                    UserId = User.Identity.GetUserId()
                };

                string[] tags = model.Tags == null ? new string[0] : model.Tags.Split(',');
                foreach (var item in tags)
                {
                    string sitem = item.Trim();
                    var t = db.ApplicationTags.Where(x => x.Tag == sitem).FirstOrDefault();
                    // not this tag in database
                    if (t == null)
                    {
                        var tag = new ApplicationTag
                        {
                            Tag = sitem
                        };
                        db.ApplicationTags.Add(tag);
                        Article.Tags.Add(tag);
                    }
                    else
                    {
                        Article.Tags.Add(t);
                    }
                }

                db.ApplicationArticles.Add(Article);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Article", new { id = Article.Id });
            }
            //ViewBag.CategoryId = new SelectList(db.ApplicationCategories, "Id", "Category", model.CategoryId);
            return View(model);
        }

        // GET: Topic/Edit/5
        public ActionResult Edit(Guid? id)
        {
            CurrentAction.currentAction = "Manage-Article-Edit";
            var Article = db.ApplicationArticles.Find(id);
            if (Article == null)
            {
                return HttpNotFound();
            }
            string tags = "";
            foreach (var item in Article.Tags)
            {
                tags += (item.Tag + ",");
            }
            if (tags.Length > 0)
            {
                tags.Remove(tags.Length - 1, 1);
            }
            var model = new EditArticleViewModel
            {
                Id = Article.Id,
                Title = Article.Title,
                ArticleContent = Article.ArticleContent,
                Tags = tags
            };
            return View(model);
        }

        // POST: Topic/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditArticleViewModel model)
        {
            CurrentAction.currentAction = "Manage-Article-Edit";
            if (ModelState.IsValid)
            {
                var Article = db.ApplicationArticles.Find(model.Id);

                Article.Title = model.Title;
                Article.ArticleContent = model.ArticleContent;
                Article.DateEdited = DateTime.Now;

                string[] tags = model.Tags == null ? new string[0] : model.Tags.Split(',');
                foreach (var item in tags)
                {
                    string sitem = item.Trim();
                    var t = db.ApplicationTags.Where(x => x.Tag == sitem).FirstOrDefault();
                    // not this tag in database
                    if (t == null)
                    {
                        var tag = new ApplicationTag
                        {
                            Tag = sitem
                        };
                        db.ApplicationTags.Add(tag);
                        Article.Tags.Add(tag);
                    }
                    else
                    {
                        if (!Article.Tags.Any(x => x.Tag == sitem))
                            Article.Tags.Add(t);
                    }
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Article", new { id = model.Id });
            }
            return View(model);
        }

        // GET: Topic/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(Guid? id)
        {
            CurrentAction.currentAction = "Article";
            try
            {
                var model = db.ApplicationArticles.Find(id);
                db.ApplicationComments.RemoveRange(model.Comments);
                db.ApplicationArticles.Remove(model);
                await db.SaveChangesAsync();
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
            }
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }

        // POST: Comment/Create
        [HttpPost]
        public async Task<ActionResult> CreateComment(CreateCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var comment = new ApplicationComment
                {
                    UserId = User.Identity.GetUserId(),
                    Comment = model.Comment,
                    ArticleId = model.ArticleId,
                };

                db.ApplicationComments.Add(comment);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Details", "Article", new { id = model.ArticleId });
        }

        // POST: Comment/Edit/5
        [HttpPost]
        public ActionResult EditComment(EditCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var comment = db.ApplicationComments.Find(model.Id);
                comment.Comment = model.Comment;
                comment.ArticleId = model.ArticleId;
                comment.UserId = User.Identity.GetUserId();
            }
            return View(model);
        }

        // POST: Comment/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteComment(Guid? id)
        {
            var model = await db.ApplicationComments.FindAsync(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.ApplicationComments.Remove(model);
            await db.SaveChangesAsync();
            return View();
        }

        public ActionResult Tags(Guid? id)
        {
            CurrentAction.currentAction = "Article";

            var tag = db.ApplicationTags.Find(id);

            List<TagViewModel> model = new List<TagViewModel>();
            if (tag == null)
            {
                return HttpNotFound();
            }
            ViewBag.Title = "Bài viết được gắn thẻ " + tag.Tag;
            //Articles
            foreach (var item in tag.Articles)
            {
                model.Add(new TagViewModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    DateCreated = item.DateCreated,
                    Tags = item.Tags,
                    Controller = "Article",
                    User = item.User
                });
            }
            return View(model.OrderByDescending(x => x.DateCreated));
        }

        public JsonResult GetTags()
        {
            List<string> data = new List<string>();
            data.AddRange(db.ApplicationTags.Select(x => x.Tag));
            // var data = db.ApplicationTags.ToList();
            return Json(new
            {
                data = data
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
