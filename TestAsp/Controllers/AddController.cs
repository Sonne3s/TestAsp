using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAsp.DAL;
using TestAsp.Infrastructure;
using TestAsp.Models;
using TestAsp.ViewModels;

namespace TestAsp.Controllers
{
    public class AddController : Controller
    {
        private EFDbContext db = new EFDbContext();
        // GET: Add
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create(string type)
        {
            return View(new BlogModel(){Type=type});
        }
        [HttpPost]
        public ActionResult Create(BlogModel model)
        {
            if (TryUpdateModel<BlogModel>(model))
            {
                string name = model.Name.Replace("\r", "").Replace("\n", "");
                ModelAdd(model.Type, name);
                db.SaveChanges();
                
                string[] s;

                s = model.Contents.FirstOrDefault().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                int id= FindID(model.Type,name);
                int i = 1;

                ContentAdd(model.Type, id, model.HeadImg, i++);
                foreach (string c in s)
                {
                    ContentAdd(model.Type, id, c, i++);
                }
                ContentAdd(model.Type, id, ".", i);

                db.SaveChanges();
            }
            /*if (images != null && images.FirstOrDefault() != null)
                foreach (HttpPostedFileBase img in images)
                    img.SaveAs(Server.MapPath("~/Content/img/" + img.FileName));*/
            //return RedirectToAction("Index", model.Type+"s");
            return View(model);
        }
        public PicturesModel Pictures(string filter)
        {
            List<Picture> pictures = filter == " " ? db.Pictures.ToList() : db.Pictures.Where(x => x.Topic == filter).ToList();
            PicturesModel model;
            try
            {
                model = new PicturesModel(pictures, db.Pictures.Select(x => x.Topic).Distinct().ToList(), filter, db.Pictures.OrderByDescending(x => x.ID).FirstOrDefault().ID);
            }
            catch (NullReferenceException)
            {
                model = new PicturesModel(pictures, filter);
            }
            return model;
        }
        public PartialViewResult GetPictures(string filter = "none")
        {
            return PartialView(Pictures(filter));
        }
        public JsonResult GetPicturesJson(string filter = "none")
        {
            return Json(Pictures(filter), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Pictures()
        {
            int i;
            try
            {
                i = db.Pictures.OrderByDescending(x => x.ID).FirstOrDefault().ID;
            }
            catch (NullReferenceException)
            {
                i = 0;
            }
            foreach (string file in Request.Files)
            {
                db.Pictures.Add(new Picture()
                {
                    ID = ++i,
                    Name = "<img=" + i + ".jpg>",
                    Topic = Request.Form["topic"]
                });
                var upload = Request.Files[file];
                if (upload != null)
                {
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileName(upload.FileName);
                    // сохраняем файл в папку Files в проекте
                    upload.SaveAs(Server.MapPath("~/Content/img/" + i + ".jpg"));
                    db.SaveChanges();
                }
            }
            return Json("файл загружен");
        }

        /////
        public int FindID(string type, string name)
        {
            switch (type)
            {
                case "New":
                    return db.News.Where(nc => nc.Name == name).OrderByDescending(nc => nc.ID).FirstOrDefault().ID;
                case "Material":
                    return db.Materials.Where(nc => nc.Name == name).OrderByDescending(nc => nc.ID).FirstOrDefault().ID;
                case "Event":
                    return db.Events.Where(nc => nc.Name == name).OrderByDescending(nc => nc.ID).FirstOrDefault().ID;
                default: return 0;
            }
        }
        public void ModelAdd(string type, string name)
        {
            switch (type)
            {
                case "New":
                    db.News.Add(new New() { Name = name });
                    break;

                case "Material":
                    db.Materials.Add(new Material() { Name = name });
                    break;

                case "Event":
                    db.Events.Add(new Event() { Name = name, DateOf=new DateTime(1900,01,01) });
                    break;
            }
        }
        public void ContentAdd(string type, int id, string content, int contentId)
        {
            switch (type)
            {
                case "New":
                    db.NewsContents.Add(new NewsContent()
                    {
                        NewID = id,
                        Content = content.Replace("\r", "").Replace("\n", ""),
                        ContentID = contentId
                    });
                    break;

                case "Material":
                    db.MaterialsContents.Add(new MaterialsContent()
                    {
                        MaterialID = id,
                        Content = content.Replace("\r", "").Replace("\n", ""),
                        ContentID = contentId
                    });
                    break;

                case "Event":
                    db.EventsContents.Add(new EventsContent()
                    {
                        EventID = id,
                        Content = content.Replace("\r", "").Replace("\n", ""),
                        ContentID = contentId
                    });
                    break;
            }
        }
    }
}