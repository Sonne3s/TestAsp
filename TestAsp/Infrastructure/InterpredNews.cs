using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestAsp.Infrastructure
{
    public static class InterpredNews
    {
        public static bool Check(string call)
        {
             if (call.Contains("<"))return true;
             else return false;
        }
       
        public static MvcHtmlString Interpred(this HtmlHelper html, List<string> calls)
        {
            //-----------------indicator
            TagBuilder tag = new TagBuilder("div");
            TagBuilder tagIndOl = new TagBuilder("ol");
            TagBuilder tagContDiv=new TagBuilder("div");

            int idCarousel=0;
            int imageRow = 0;
            foreach(string call in calls)
            {
                if (call.Contains("<img"))
                {
                    if (imageRow++ == 0) idCarousel++;
                    TagBuilder tagIndLi = new TagBuilder("li");
                    tagIndLi.MergeAttributes(new Dictionary<string, string>
                        {
                            {"class",imageRow>1?"":"active"},
                            {"target-data","#myCarousel"+idCarousel},
                            {"data-slide-to",Convert.ToString(imageRow-1)}
                        });

                    //------------------------------------------------content



                    TagBuilder tagImg = new TagBuilder("img");
                    string[] split = call.Split(new string[] { "=" }, StringSplitOptions.None);
                    tagImg.MergeAttributes(new Dictionary<string, string> 
                    {
                        {"src", "/Content/img/" + (split[1].Replace(">",""))},
                        {"class", "center-block img-responsive"},
                        {"height","400"}
                    });

                    //TagBuilder tagCaptionDiv = new TagBuilder("div");
//подпись под картинкой//tagCaptionDiv.AddCssClass("carousel-caption");
                    //tagCaptionDiv.SetInnerText("<p>Текст (описание) 1 слайда...</p>");

                    TagBuilder tagItemDiv = new TagBuilder("div");
                    tagItemDiv.AddCssClass((imageRow>1?"":"active") + " item");
                    tagItemDiv.InnerHtml += (tagImg.ToString()/* + tagCaptionDiv.ToString()*/);

                    if (imageRow == 1)
                    {
                        tagContDiv = new TagBuilder("div");
                        tagContDiv.AddCssClass("carousel-inner");
                    }
                    tagContDiv.InnerHtml += tagItemDiv.ToString();

                    //-------
                    if (imageRow == 1)
                    {
                        tagIndOl = new TagBuilder("ol");
                        tagIndOl.AddCssClass("carousel-indicators");
                    }
                    tagIndOl.InnerHtml += tagIndLi.ToString();
                }

                    //------------------------------------------------buttons
                else if(imageRow>0)
                {
                    TagBuilder tagLeftSpan = new TagBuilder("span");
                    tagLeftSpan.AddCssClass("glyphicon glyphicon-chevron-left");

                    TagBuilder tagLeft = new TagBuilder("a");
                    tagLeft.MergeAttributes(new Dictionary<string, string>
                        {
                            {"class","carousel-control left"},
                            {"href","#myCarousel"+idCarousel},
                            {"data-slide","prev"}
                        });
                    tagLeft.InnerHtml += tagLeftSpan.ToString();

                    TagBuilder tagRightSpan = new TagBuilder("span");
                    tagRightSpan.AddCssClass("glyphicon glyphicon-chevron-right");

                    TagBuilder tagRight = new TagBuilder("a");
                    tagRight.MergeAttributes(new Dictionary<string, string>
                        {
                            {"class","carousel-control right"},
                            {"href","#myCarousel"+idCarousel},
                            {"data-slide","next"}
                        });
                    tagRight.InnerHtml += tagRightSpan.ToString();

                    //--------------------------container
                    TagBuilder tagCarousel = new TagBuilder("div");
                    tagCarousel.MergeAttributes(new Dictionary<string, string> 
                        { 
                            {"id","myCarousel"+idCarousel},
                            {"class", "carousel slide"}, 
                            {"data-interval", "3000"}, 
                            {"data-ride", "carousel"},
                            {"data-pause", "hover"}
                        });
                    tagCarousel.InnerHtml += tagIndOl.ToString() + tagContDiv.ToString() + tagLeft.ToString() + tagRight.ToString();

                    tag.InnerHtml += tagCarousel.ToString();
                    imageRow = 0;
                }
                if (!call.Contains("<img")) 
                { 
                    tag.InnerHtml += "<p>" + call + "</p>";                    
                }
            }
            return new MvcHtmlString(tag.ToString());
        }

        public static string Interpred(string call)
        {
            if (call.Contains("<img="))
            {
                string[] split = call.Split(new string[] { "<img=" }, StringSplitOptions.None);
                return split[1].Replace(">","");
            }
            else return call;
        }
    }
}


/*if (split[0].Contains(":")) 
{
    string[] attrs = split[0].Split(new string[]{","},StringSplitOptions.None);
    foreach(string attr in attrs)
    {
        if(attr.Contains("width"))
        {
            string[] width = attr.Split(new string[] { ":" }, StringSplitOptions.None);
            tag.MergeAttribute("width", width[1]);
        }
        if (attr.Contains("height"))
        {
            string[] height = attr.Split(new string[] { ":" }, StringSplitOptions.None);
            tag.MergeAttribute("height", height[1]);
        }
    }
}*/

            /*if (call.Contains("<img"))
            {
                TagBuilder tag = new TagBuilder("img");
                string[] split = call.Split(new string[] { "=" }, StringSplitOptions.None);

                tag.MergeAttribute("src", "/Content/img/" + split[1]);
                tag.MergeAttribute("class", "center-block img-responsive");
                return new MvcHtmlString(tag.ToString());
            }
            else return(new MvcHtmlString(new TagBuilder("br").ToString()));*/

/*private static TagBuilder TextFormat(string call)
{
    List<List<int>> index=new List<List<int>>();
    List<string> formated = new List<string>();
    List<string> substrings = new List<string>();
    //int[] index=new int[2];
    TagBuilder tag = new TagBuilder("p");
    TagBuilder subtag;
    string subcall=call;
    while (call.Contains("<b>"))            
    {
        index.Add(new List<int>(){call.IndexOf("<b>")});
        index.Last().Add(call.IndexOf("</b>"));
        formated.Add(subcall.Substring(index.Last()[0], (index.Last()[1] - index.Last()[0])));
        subcall = subcall.Replace(formated.Last(), "<>");
    }
    substrings = subcall.Split(new string[] {"<>"}, StringSplitOptions.None).ToList();
            
    foreach(string s in substrings)
    {
        subtag = new TagBuilder("b");
        subtag.SetInnerText(formated.FirstOrDefault().Replace("<b>","").Replace("</b>",""));
        formated.Remove(formated.FirstOrDefault());
        tag.InnerHtml += (" " + s + " " + subtag.ToString());

    }
 }*/