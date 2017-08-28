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
    public class MaterialsController : Controller
    {
        // GET: Materials
        private EFDbContext db = new EFDbContext();

        public ActionResult Index()
        {
            List<BlogModel> models = new List<BlogModel>();
            List<MaterialsContent> content;
            foreach (Material n in db.Materials.ToList())
            {
                content = db.MaterialsContents.Where(x => x.MaterialID == n.ID).ToList();
                models.Add(new BlogModel(n, content));
            }
            return View(models);
        }
        public ActionResult View(int id)
        {
            BlogModel Materials = new BlogModel(db.Materials.FirstOrDefault(n => n.ID == id), db.MaterialsContents.Where(c => c.MaterialID == id).ToList());
            return View(Materials);
        }

        public ActionResult Delete(int idmodels)
        {
            string topic = db.Materials.Find(idmodels).Name;
            List<int> pictures = db.Pictures.Where(x => x.Topic == topic).Select(x => x.ID).ToList();
            db.Pictures.RemoveRange(db.Pictures.Where(x => x.Topic == topic));
            foreach (int p in pictures) System.IO.File.Delete(Server.MapPath("~/Content/img/" + p + ".jpg"));
            db.MaterialsContents.RemoveRange(db.MaterialsContents.Where(x => x.MaterialID == idmodels));
            db.Materials.Remove(db.Materials.Find(idmodels));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}