using iLucy.RestCall.Attributes;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace iLucy.RestCall.TestApiIntegration
{   
    [RestCallService("http://localhost:51525")]
    public class SimpleApiIntegration : RestClient
    {
        public SimpleApiIntegration():base()
        {

        } 

        [RestCall("/api/values")]
        public async Task<string[]> model(HttpResponseMessage rep)
        {
            return await rep.Content.ReadAsAsync<string[]>();
        }
    }
}
