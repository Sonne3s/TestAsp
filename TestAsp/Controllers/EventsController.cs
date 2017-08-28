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
    public class EventsController : Controller
    {
        // GET: Events
        private EFDbContext db = new EFDbContext();

        public ActionResult Index()
        {
            List<BlogModel> models = new List<BlogModel>();
            List<EventsContent> content;
            foreach (Event n in db.Events.ToList())
            {
                content = db.EventsContents.Where(x => x.EventID == n.ID).ToList();
                models.Add(new BlogModel(n, content));
            }
            List<string> str = db.Pictures.Select(x => x.Name).ToList();
            List<Picture> str11 = db.Pictures.ToList();
            List<string> str12 = str11.Select(x => x.Name).ToList();
            List<string> str1 = db.EventsContents.Select(x => x.Content).ToList();
            List<string> str2 = db.EventsContents.Where(x => x.Content.Contains("img")).Select(x => x.Content).ToList();

            return View(models);
        }
        public ActionResult View(int id)
        {
            BlogModel Events = new BlogModel(db.Events.FirstOrDefault(n => n.ID == id), db.EventsContents.Where(c => c.EventID == id).ToList());
            return View(Events);
        }

        public ActionResult Delete(int idmodels)
        {
            string topic = db.Events.Find(idmodels).Name;
            List<int> pictures = db.Pictures.Where(x => x.Topic == topic).Select(x => x.ID).ToList();
            db.Pictures.RemoveRange(db.Pictures.Where(x => x.Topic == topic));
            foreach (int p in pictures) System.IO.File.Delete(Server.MapPath("~/Content/img/" + p + ".jpg"));
            db.EventsContents.RemoveRange(db.EventsContents.Where(x => x.EventID == idmodels));
            db.Events.Remove(db.Events.Find(idmodels));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}