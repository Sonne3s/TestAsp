using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.IO;

using TestAsp.DAL;
using TestAsp.Models;
using TestAsp.Infrastructure;
using System.Text.RegularExpressions;
using TestAsp.ViewModels;

namespace TestAsp.Controllers
{
    public class NewsController : Controller
    {
        private EFDbContext db = new EFDbContext();
        // GET: News
        public ActionResult Index()
        {
            List<BlogModel> news = new List<BlogModel>();
            List<NewsContent> content;
            foreach(New n in db.News.ToList())
            {
                content = db.NewsContents.Where(x => x.NewID == n.ID).ToList();
                news.Add(new BlogModel(n, content));
            }
            List<string> str = db.Pictures.Select(x => x.Name).ToList();
            List<Picture> str11 = db.Pictures.ToList();
            List<string> str12 = str11.Select(x => x.Name).ToList();
            List<string> str1 = db.NewsContents.Select(x => x.Content).ToList();
            List<string> str2=db.NewsContents.Where(x=>x.Content.Contains("img")).Select(x=>x.Content).ToList();

            return View(news);
        }
        public ActionResult View(int id)
        {
            BlogModel news = new BlogModel(db.News.FirstOrDefault(n=>n.ID==id),db.NewsContents.Where(c=>c.NewID==id).ToList());
            return View(news);
        }
        
        public ActionResult Delete(int idnews)
        {
            string topic = db.News.Find(idnews).Name;
            List<int> pictures = db.Pictures.Where(x => x.Topic == topic).Select(x => x.ID).ToList();
            db.Pictures.RemoveRange(db.Pictures.Where(x => x.Topic == topic));
              
            foreach (int p in pictures) System.IO.File.Delete(Server.MapPath("~/Content/img/" + p + ".jpg"));
        
            db.NewsContents.RemoveRange(db.NewsContents.Where(x => x.NewID == idnews));
            db.News.Remove(db.News.Find(idnews));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
/* public ActionResult Create()
  {
      return View();
  }
  [HttpPost]
  public ActionResult Create(NewsToView news)
  {
      if(TryUpdateModel<NewsToView>(news))
      {
          string newsname = news.Name.Replace("\r", "").Replace("\n", "");
          db.News.Add(new New() { Name = newsname });
          db.SaveChanges();
          string[] s;
          s = news.Contents.FirstOrDefault().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
          int i = 1;

          db.NewsContents.Add(new NewsContent()
          {
              NewID = db.News.Where(nc => nc.Name == newsname).OrderByDescending(nc => nc.ID).FirstOrDefault().ID,
              Content = news.HeadImg.Replace("\r","").Replace("\n",""),
              ContentID = i++
          });
          foreach(string c in s)
          {
              db.NewsContents.Add(new NewsContent()
              {
                  NewID = db.News.Where(nc => nc.Name == news.Name).OrderByDescending(nc => nc.ID).FirstOrDefault().ID,
                  Content = c.Replace("\r",""),
                  ContentID = i++
              });
          }
          db.NewsContents.Add(new NewsContent()
          {
              NewID = db.News.Where(nc => nc.Name == news.Name).OrderByDescending(nc => nc.ID).FirstOrDefault().ID,
              Content = ".",
              ContentID = i
          });
          db.SaveChanges();
      }
      return RedirectToAction("Index");
      //return View(news);
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
  public PartialViewResult GetPictures(string filter="none")
  {           
      return PartialView(Pictures(filter));
  }
  public JsonResult GetPicturesJson(string filter="none")
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
              ID=++i,
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
  }*/
