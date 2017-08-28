using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TestAsp.ViewModels;

namespace TestAsp.Models
{
    public class NewsContent
    {
        [Key, Column(Order=1)]
        [ForeignKey("New")]
        public int NewID { get; set; }
        [Key, Column(Order=2)]
        public int ContentID { get; set; }
        public string Content { get; set; }

        public virtual New New {get; set;}
              
    }
}