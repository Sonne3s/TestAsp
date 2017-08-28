using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestAsp.Models
{
   public enum Degree
    {
        Папа,
        Мама,
        Дедушка,
        Бабушка,
        Родственник
    }
    public class Parent
    {
        public int ID { get; set; }
        [Display(Name = "Имя")]
        public string First { get; set; }
        [Display(Name = "Отчество")]
        public string Middle { get; set; }
        [Display(Name = "Фамилия")]
        public string Last { get; set; }
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }
        [Display(Name = "Спортсмен")]
        public int ChildID { get; set; }
        [Display(Name = "Степерь родства")]
        public Degree Degree { get; set; }

        public virtual Student Child { get; set; }
    }
}