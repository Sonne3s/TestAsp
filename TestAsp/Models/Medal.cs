using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAsp.Models
{
    public enum Par
    {
        Золото,
        Серебро,
        Бронза
    }
    public class Medal
    {
        public int ID{get;set;}
        public Par Par { get; set; }
        public int StudentID { get; set; }
        public int EventID { get; set; }

        public virtual Student Student { get; set; }
        public virtual Event Event { get; set; }
    }
}