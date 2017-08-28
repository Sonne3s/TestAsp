using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestAsp.Models
{
    public enum Sort
    {
        Дзюдо,
        Самбо
    }
    public class Group
    {
        public int ID { get; set; }
        public Sort Sort { get; set; }
        public string Postfix { get; set; }
        public int? TrainerID { get; set; }

        public Trainer Trainer { get; set; }
    }
}