using K14Forum.CodeHelper;
using K14Forum.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace K14Forum.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? page)
        {
            CurrentAction.currentAction = "Home";
            var model = db.ApplicationArticles
            .OrderByDescending(x => x.DateCreated);

            int pageSize = int.Parse(WebConfigurationManager.AppSettings["pageSize"]);
            int pageNumber = (page ?? 1);

            return View(model.ToPagedList(pageNumber, pageSize));
        }

        [ChildActionOnly]
        public PartialViewResult _MenuPartail()
        {
            ViewBag.Comments = db.ApplicationComments
                .OrderByDescending(x => x.DateCreated)
                .Take(5)
                .ToList();

            ViewBag.Tags = db.ApplicationTags
                .Where(x => x.Articles.Count() > 0)
                .ToList();

            ViewBag.Articles = db.ApplicationArticles
               .OrderByDescending(x => x.DateCreated)
               .Take(5)
               .ToList();

            return PartialView();
        }


        public JsonResult GetDataForSearch()
        {
            var model = db.ApplicationArticles
                .Select(x => x.Title);
            if (model != null)
            {
                return Json(new { status = true, data = model }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchResult(string keyword = "")
        {
            CurrentAction.currentAction = "Search";
            ViewBag.Message = keyword;
            var model = db.ApplicationArticles
                .Where(x => x.Title.Contains(keyword) || keyword.Contains(x.Title))
                .ToList();
            return View(model);
        }
    }
}