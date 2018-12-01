using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace iLucy.RestCall
{
    public abstract class RestClient
    {
        internal readonly HttpClient httpClient;
        public RestClient()
        {
            httpClient = new HttpClient();            
        }

        //public List<string> GetMethod(Type type)
        //{
        //    return type.GetMethods().Select(t => t.Name).ToList();
        //}
    }
}
