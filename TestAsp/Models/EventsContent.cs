using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAsp.Models
{
    public class EventsContent
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Event")]
        public int EventID { get; set; }
        [Key, Column(Order = 2)]
        public int ContentID { get; set; }
        public string Content { get; set; }

        public virtual Event Event { get; set; }
    }
}