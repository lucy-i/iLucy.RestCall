using System;
using System.Collections.Generic;
using System.Text;

namespace iLucy.RestCall.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RestCallAttribute : Attribute
    {   
        public string Name { get; set; }        
        public string URL { get; private set; }
        public bool Authorized { get; set; }
        public string[] Headers { get; set; }

        public RestCallAttribute(string url)
        {
            URL = url;
        }

        public RestCallAttribute()
        {
            if(string.IsNullOrEmpty(URL))
            {   
                throw new Exception("URL of RestCall can Never be Empty");
            }
        }
    }
}
