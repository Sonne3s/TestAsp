using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAsp.Controllers;
using TestAsp.Models;

namespace TestAsp.ViewModels
{
    public class BlogModel
    {        
        public int ID { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Изображение иконки")]
        public string HeadImg { get; set; }
        [Display(Name = "Содержание")]
        public List<string> Contents { get; set; }
        [HiddenInput(DisplayValue=false)]
        public string Type { get; set; }
        public BlogModel(Material _model, List<MaterialsContent> _modelContent)
        {
            ID = _model.ID;
            Name = _model.Name;

            List<MaterialsContent> modelContent = _modelContent;
            Contents = new List<string>();

            foreach (MaterialsContent c in modelContent)
            {
                if (c.ContentID != 1) Contents.Add(c.Content);
                else HeadImg = Convert.ToString(c.Content);
            }

            Type = "Material";
        }
        public BlogModel(New _model, List<NewsContent> _modelContent)
        {
            ID = _model.ID;
            Name = _model.Name;

            List<NewsContent> modelContent = _modelContent;
            Contents = new List<string>();

            foreach (NewsContent c in modelContent)
            {
                if (c.ContentID != 1) Contents.Add(c.Content);
                else HeadImg = Convert.ToString(c.Content);
            }

            Type = "New";
        }
        public BlogModel(Event _model, List<EventsContent> _modelContent)
        {
            ID = _model.ID;
            Name = _model.Name;

            List<EventsContent> modelContent = _modelContent;
            Contents = new List<string>();

            foreach (EventsContent c in modelContent)
            {
                if (c.ContentID != 1) Contents.Add(c.Content);
                else HeadImg = Convert.ToString(c.Content);
            }

            Type = "Event";
        }
        public BlogModel() { }
    }
}