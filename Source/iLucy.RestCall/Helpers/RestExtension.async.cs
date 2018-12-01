using iLucy.RestCall.Attributes;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;

namespace iLucy.RestCall.Helpers
{
    public delegate D OnSuccess<D>(HttpResponseMessage message, D model);
    public static class RestExtension
    {
        //public static Task<D> Execute<T, D>(this T client, OnSuccess<D> func) where T : RestClient
        //{
        //    return ExecuteAsync(client, func);
        //}
        //Execute<T, D>(this T client, Func<HttpResponseMessage, D, D> func) where T : RestClient
        public static async Task<D> ExecuteFunc<T, D>(this T client, Func<T, Func<HttpResponseMessage, Task<D>>> expression) where T : RestClient
        {
            HttpClient httpClient = new HttpClient();
            Func<HttpResponseMessage, Task<D>> ff = expression(client);
            RestCallAttribute[] attributes = ff.Method.GetCustomAttributes(typeof(RestCallAttribute), true) as RestCallAttribute[];
            RestCallServiceAttribute[] serviceAttributes = client.GetType().GetCustomAttributes(typeof(RestCallServiceAttribute), true) as RestCallServiceAttribute[];
            if (serviceAttributes == null)
                throw new Exception();
            if (serviceAttributes.Length == 0)
                throw new Exception();
            if (attributes == null)
                throw new Exception();
            if (attributes.Length == 0)
                throw new Exception();

            RestCallAttribute actualAttribute = attributes[0];
            RestCallServiceAttribute actualServiceAttribute = serviceAttributes[0];

            httpClient.BaseAddress = new Uri(actualServiceAttribute.HostURL);
            var responseMessage = await httpClient.GetAsync(actualAttribute.URL);

            return await ff(responseMessage);
        }
        public static async Task<D> Execute<T, D>(this T client, Func<HttpResponseMessage, D, D> func) where T : RestClient
        {
            Type tt = func.GetType();
            RestCallAttribute[] attribute = tt.GetCustomAttributes(typeof(RestCallAttribute), true) as RestCallAttribute[];
            if (attribute == null)
                throw new Exception();
            if (attribute.Length == 0)
                throw new Exception();

            RestCallAttribute actualAttribute = attribute[0];

            var responseMessage = await client.httpClient.GetAsync(actualAttribute.URL);
            var response = await responseMessage.Content.ReadAsAsync<D>();
            return func(responseMessage, response);
        }

        public static async Task<D> Execute<T, D>(this T client, Func<T, OnSuccess<D>> func) where T : RestClient
        {
            OnSuccess<D> ff = func(client);
            Type tt = ff.GetType();
            RestCallAttribute[] attribute = tt.GetCustomAttributes(typeof(RestCallAttribute), true) as RestCallAttribute[];
            if (attribute == null)
                throw new Exception();
            if (attribute.Length == 0)
                throw new Exception();

            RestCallAttribute actualAttribute = attribute[0];

            var responseMessage = await client.httpClient.GetAsync(actualAttribute.URL);
            var response = await responseMessage.Content.ReadAsAsync<D>();
            return ff(responseMessage, response);
        }

        public static async Task<object> Execute<T>(this T client, Func<T, OnSuccess<object>> func, Type type) where T : RestClient
        {
            OnSuccess<object> ff = func(client);
            var responseMessage = await client.httpClient.GetAsync("");
            var response = await responseMessage.Content.ReadAsAsync(type);
            return ff(responseMessage, response);
        }


        public static async Task<D> ExecuteAsync<T, D>(this T client, OnSuccess<D> func) where T : RestClient
        {
            var responseMessage = await client.httpClient.GetAsync("");
            var response = responseMessage.Content.ReadAsStringAsync();
            try
            {
                D model = await responseMessage.Content.ReadAsAsync<D>();
                return func(responseMessage, model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static async Task<D> ExecuteAsync<D>(this RestClient client, OnSuccess<D> func)
        {
            var responseMessage = await client.httpClient.GetAsync("");
            var response = responseMessage.Content.ReadAsStringAsync();
            try
            {
                D model = await responseMessage.Content.ReadAsAsync<D>();
                return func(responseMessage, model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
