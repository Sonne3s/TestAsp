using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAsp.DAL;
using TestAsp.Models;
using TestAsp.ViewModels;

namespace TestAsp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private EFDbContext db = new EFDbContext();

        public ActionResult Index()
        {
            List<BlogModel> models = new List<BlogModel>();
            List<MaterialsContent> contentM;
            List<NewsContent> contentN;
            List<EventsContent> contentE;
            foreach (Material n in db.Materials.ToList())
            {
                contentM = db.MaterialsContents.Where(x => x.MaterialID == n.ID).ToList();
                models.Add(new BlogModel(n, contentM));
            }
            foreach (New n in db.News.ToList())
            {
                contentN = db.NewsContents.Where(x => x.NewID == n.ID).ToList();
                models.Add(new BlogModel(n, contentN));
            }
            foreach (Event n in db.Events.ToList())
            {
                contentE = db.EventsContents.Where(x => x.EventID == n.ID).ToList();
                models.Add(new BlogModel(n, contentE));
            }
            return View(models);
        }
    }
}