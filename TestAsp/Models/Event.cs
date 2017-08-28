using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TestAsp.Models
{
    public class Event
    {
        public int ID{get;set;}
        public DateTime DateOf { get; set; }
        public string Name { get; set; }
    }
}