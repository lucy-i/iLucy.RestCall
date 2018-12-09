using System.Net.Http;

namespace iLucy.RestCall
{
    public interface IResponse<TResult> 
    {
        HttpResponseMessage Response { get; set; }
        TResult Result { get; set; }
    }

    internal interface IAction
    {
        IResponse<object> Get();
        IResponse<object> Get(string[] urlParams);
        IResponse<object> Post(HttpContent content);
        IResponse<object> Post(HttpContent content, string[] urlParams);
    }

    internal interface IAction<TResult>
    {
        IResponse<TResult> Get();
        IResponse<TResult> Get(string[] urlParams);
        IResponse<TResult> Post(HttpContent content);
        IResponse<TResult> Post(HttpContent content, string[] urlParams);
    }

    internal interface IAction<TResult, TQModel>: IAction<TResult> 
    {
        IResponse<TResult> Get(TQModel queryParam);
        IResponse<TResult> Post(TQModel queryParam, HttpContent content);
    }

    public class Action
    {
        private HttpClient cli;
        public void get()
        {
            //cli.GetAsync()
        }
    }

    public class Action<Model>
    {
    }

}
