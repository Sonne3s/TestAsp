using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Util;

namespace TestAsp
{
    public class CustomRequestValidation:RequestValidator
    {
        public CustomRequestValidation() { }
        protected override bool IsValidRequestString(HttpContext context, string value, RequestValidationSource requestValidationSource, string collectionKey, out int validationFailureIndex)
        {
            string valueUpd=value;
            validationFailureIndex = -1;
            /*if (requestValidationSource == RequestValidationSource.RawUrl)
                return true;*/
            if ((requestValidationSource == RequestValidationSource.Form) && (collectionKey == "Contents"))
            {
                valueUpd = valueUpd.Replace("<a>", "").Replace("</a>", "");
                valueUpd = valueUpd.Replace("<b>", "").Replace("</b>", "");
                valueUpd = valueUpd.Replace("<i>", "").Replace("</i>", "");
                valueUpd = valueUpd.Replace("<em>", "").Replace("</em>", "");
                valueUpd = valueUpd.Replace("<strong", "").Replace("</strong>", "");
                valueUpd = valueUpd.Replace("<pre>", "").Replace("</pre>", "");
                valueUpd = valueUpd.Replace("<sup>", "").Replace("</sup>", "");
                valueUpd = valueUpd.Replace("<sub>", "").Replace("</sub>", "");
                valueUpd = valueUpd.Replace("<p>", "").Replace("</p>", "");
                valueUpd = valueUpd.Replace("<blockquote>", "").Replace("</blockquote>", "");

                if (valueUpd.Contains("<img="))
                {
                    int index1, index2;
                    while (valueUpd.Contains("<img="))
                    {
                        index1 = valueUpd.IndexOf("<img=");
                        index2 = valueUpd.IndexOf(">", index1);                        
                        valueUpd = valueUpd.Remove(index1, index2 - index1);
                    }
                }
            }
            if ((requestValidationSource == RequestValidationSource.Form) && (collectionKey == "HeadImg"))
            {                
                if (valueUpd.Contains("<img="))
                {
                    int index1, index2;
                    while (valueUpd.Contains("<img="))
                    {
                        index1 = valueUpd.IndexOf("<img=");
                        index2 = valueUpd.IndexOf(">", index1);
                        valueUpd = valueUpd.Remove(index1, index2 - index1);
                    }
                }
            }
            return base.IsValidRequestString(context, valueUpd, requestValidationSource, collectionKey, out validationFailureIndex);
        }
    }
}