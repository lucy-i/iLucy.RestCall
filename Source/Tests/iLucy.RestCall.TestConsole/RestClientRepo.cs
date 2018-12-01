using iLucy.RestCall.Attributes;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace iLucy.RestCall.TestConsole
{
    [RestCallService("http://localhost:51525")]
    internal class RestClientRepo : RestClient
    {
        public RestClientRepo() : base()
        {
        } 

        [RestCall("/api/values")]
        public async Task<string[]> model(HttpResponseMessage rep)
        {
            return await rep.Content.ReadAsAsync<string[]>();
            //return "";
        }
    }
}
