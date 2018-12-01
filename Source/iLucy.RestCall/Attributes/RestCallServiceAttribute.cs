using System;
using System.Collections.Generic;
using System.Text;

namespace iLucy.RestCall.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RestCallServiceAttribute : Attribute
    {
        public string HostURL { get; private set; }

        public RestCallServiceAttribute(string hostURL)
        {
            HostURL = hostURL;
        }
    }
}
