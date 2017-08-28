using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestAsp.Models
{
    public enum Ranks
    {
        КЮ6,
        КЮ5,
        КЮ4,
        КЮ3,
        КЮ2,
        КЮ1,
        ДАН1,
        ДАН2,
        ДАН3,
        ДАН4,
        ДАН5,
        ДАН6,
        ДАН7,
        ДАН8
    }
    public class Student
    {
        public int ID { get; set; }
        [Display(Name = "Имя")]
        public string First { get; set; }
        [Display(Name = "Отчество")]
        public string Middle { get; set; }
        [Display(Name = "Фамилия")]
        public string Last { get; set;}
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }
        [Display(Name = "Вес")]
        public int Weight{get;set;}
        [Display(Name = "Разряд")]
        public Ranks Rank{get;set;}
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }
    }
}