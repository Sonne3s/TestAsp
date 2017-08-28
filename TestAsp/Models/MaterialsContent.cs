using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAsp.Models
{
    public class MaterialsContent
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Material")]
        public int MaterialID { get; set; }
        [Key, Column(Order = 2)]
        public int ContentID { get; set; }
        public string Content { get; set; }

        public virtual Material Material { get; set; }
    }
}