using System;
using System.Collections.Generic;
using System.Linq;
using TestAsp.Models;
using TestAsp.Infrastructure;

namespace TestAsp.Infrastructure
{
    public class PicturesModel
    {
        public List<int> ID { get; set; }
        public List<string> Name{get;set;}
        public List<string> Picture { get; set; }
        public List<string> Topics{get;set;}
        public String Filter { get; set; }
        public int MaxId {get;set;}
        public PicturesModel(List<Picture> _pictures, List<string> _topics, string _filter, int _MaxId=0)
        {
            ID = _pictures.Select(x => x.ID).ToList();
            Name = _pictures.Select(x => x.Name).ToList();
            Picture = _pictures.Select(x=>x.Name).ToList();
            for (int i = 0; i < Picture.Count; i++) Picture[i] = InterpredNews.Interpred(Picture[i]);
            Topics = _topics;
            Filter = _filter;
            MaxId = _MaxId;
        }
        public PicturesModel(List<Picture> _pictures, string _filter)
        {
            ID = _pictures.Select(x => x.ID).ToList();
            Name = _pictures.Select(x => x.Name).ToList();
            Picture = _pictures.Select(x => x.Name).ToList();
            for (int i = 0; i < Picture.Count; i++) Picture[i] = InterpredNews.Interpred(Picture[i]);
            Topics = new List<string>();
            Filter = _filter;
            MaxId = 0;
        }
    }
}