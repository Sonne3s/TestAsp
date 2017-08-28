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
    public class ActivityController : Controller
    {

        // GET: Activity
        private EFDbContext db = new EFDbContext();

        public ActionResult Index()
        {
            return View(new ScheduleWithGroup(db.Groups.ToList(),db.Schedules.ToList()));
        }
        [HttpPost]
        public ActionResult CreateSchedule(string dayweek, string stime, string groupid)
        {
            db.Schedules.Add(new Schedule 
            {
                Day = (DayWeek)Enum.Parse(typeof(DayWeek),dayweek), 
                STime = DateTime.Parse(stime), 
                GroupID=Convert.ToInt32(groupid) 
            });
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteSchedule(string id)
        {
            db.Schedules.Remove(db.Schedules.Find(Convert.ToInt32(id)));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student model)
        {
            db.Students.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Students()
        {
            return View(db.Students.ToList());
        }
        public ActionResult DeleteStudent(string id)
        {
            db.Students.Remove(db.Students.Find(Convert.ToInt32(id)));
            db.SaveChanges();
            return RedirectToAction("Students");
        }
        public ActionResult Parents()
        {
            return View(new ParentsPageModel(db.Parents.ToList(),db.Students.ToList()));
        }
        public ActionResult DeleteParent(string id)
        {
            db.Parents.Remove(db.Parents.Find(Convert.ToInt32(id)));
            db.SaveChanges();
            return RedirectToAction("Parents");
        }
        public ActionResult Trainers()
        {
            return View(new TrainersWithGroups{ Trainers = db.Trainers.ToList(), Groups = db.Groups.ToList() });
        }
        public ActionResult CreateTrainer()
        {
            return View(new Trainer());
        }
        [HttpPost]
        public ActionResult CreateTrainer(Trainer model)
        {
            db.Trainers.Add(model);
            db.SaveChanges();
            return RedirectToAction("Trainers");
        }
        [HttpPost]
        public ActionResult DeleteTrainer(string id)
        {
            try
                {
                    db.Trainers.Remove(db.Trainers.Find(Convert.ToInt32(id)));
                    db.SaveChanges();
                }
            catch (System.Data.Entity.Infrastructure.DbUpdateException) { }
            return RedirectToAction("Trainers");
        }
        [HttpPost]
        public ActionResult CreateGroup(string sort, string postfix)
        {
            db.Groups.Add(new Group { Postfix = postfix, Sort = (Sort)Enum.Parse(typeof(Sort), sort), TrainerID=null });
            db.SaveChanges();
            return RedirectToAction("Trainers");
        }
        [HttpPost]
        public ActionResult AppointGroup(string idTrainer, string idGroup)
        {
            db.Groups.Find(Convert.ToInt32(idGroup)).TrainerID = Convert.ToInt32(idTrainer);
            db.SaveChanges();
            return RedirectToAction("Trainers");
        }
        public ActionResult UnAppointGroup(string id)
        {
            db.Groups.Find(Convert.ToInt32(id)).TrainerID = null;
            db.SaveChanges();
            return RedirectToAction("Trainers");
        }
        public ActionResult DeleteGroup(string id)
        {
            db.Groups.Remove(db.Groups.Find(Convert.ToInt32(id)));
            db.SaveChanges();
            return RedirectToAction("Trainers");
        }
       
        public ActionResult CreateParent()
        {
            return View(new ParentCreateModel(db.Students.ToList()));
        }
        [HttpPost]
        public ActionResult CreateParent(ParentCreateModel model)
        {
            db.Parents.Add(new Parent() 
            {
                First = model.First,
                Middle = model.Middle,
                Last = model.Last,
                Phone = model.Phone,
                Degree = model.Degree,
                ChildID = model.ChildID
            });
            db.SaveChanges();
            return RedirectToAction("Parents");
        }


        public JsonResult GetPeopleDataJson(string category)
        {
            switch (category)
            {
                case "students":
                var data = db.Students.AsEnumerable().Select(
                       s => new
                       {
                           First = s.First,
                           Middle = s.Middle,
                           Last = s.Last,
                           Phone = s.Phone,
                           Rank = Enum.GetName(typeof(Ranks), s.Rank),
                           BirthDay = s.BirthDay.Date.ToString("dd.MM.yyyy"),
                           Weight = s.Weight
                       });

                return Json(data, JsonRequestBehavior.AllowGet);
                case "parents":
                var data2 = db.Parents.AsEnumerable().Select(
                       s => new
                       {
                           First = s.First,
                           Middle = s.Middle,
                           Last = s.Last,
                           Phone = s.Phone,
                           Degree = Enum.GetName(typeof(Degree), s.Degree),
                           Child = s.Child.Last,
                       });

                return Json(data2, JsonRequestBehavior.AllowGet);
                default:
                    data = db.Students.AsEnumerable().Select(
                       s => new
                       {
                           First = s.First,
                           Middle = s.Middle,
                           Last = s.Last,
                           Phone = s.Phone,
                           Rank = Enum.GetName(typeof(Ranks), s.Rank).ToString(),
                           BirthDay = s.BirthDay.Date.ToString("dd.MM.yyyy"),
                           Weight = s.Weight
                       });

                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }
    }
}