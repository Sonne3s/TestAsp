using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestAsp.Models;

namespace TestAsp.ViewModels
{
    public class ParentCreateModel
    {
        [Display(Name = "Имя")]
        public string First { get; set; }
        [Display(Name = "Отчество")]
        public string Middle { get; set; }
        [Display(Name = "Фамилия")]
        public string Last { get; set; }
         [Display(Name = "Номер телефона")]
        public string Phone { get; set; }
        [Display(Name = "Степерь родства")]
        public Degree Degree { get; set; }
        [Display(Name = "Спортсмен")]
        public int ChildID { get; set; }
        public List<Student> Students { get; set; }
        public ParentCreateModel(List<Student> students)
        {
            Students = students;
        }
        public ParentCreateModel()
        {
        }
    }
    public class ParentPageModel
    {
        public int ID { get;set; }
        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }
        public string Phone { get; set; }
        public Degree Degree { get; set; }
        public Student Student { get; set; }
        public ParentPageModel(Parent parent, Student student)
        {
            ID = parent.ID;
            First = parent.First;
            Middle = parent.Middle;
            Last = parent.Last;
            Phone = parent.Phone;
            Degree = parent.Degree;
            Student = student;
        }
    }
    public class ParentsPageModel
    {
        public List<ParentPageModel> Parents { get; set; }

       public ParentsPageModel(List<Parent> parents, List<Student> students)
        {
            Parents = new List<ParentPageModel>();
           foreach(Parent p in parents)
           {
                Parents.Add(new ParentPageModel(p,students.Find(s=>s.ID==p.ChildID)));
           }
        }
    }
}