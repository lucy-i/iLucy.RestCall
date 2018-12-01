using System;
using System.Collections.Generic;
using System.Linq;
using iLucy.RestCall.Helpers;

namespace iLucy.RestCall.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            RestClientRepo client = new RestClientRepo();

            Type tt = client.GetType();
            var df=client.ExecuteFunc<RestClientRepo,string[]>(t => t.model);
            df.Wait();

            string[] str = df.Result;
            Console.WriteLine(df.Result);
            for (int i = 0; i < str.Length; i++)
            {
                Console.WriteLine(str[i]);
            }
            Console.ReadLine();
        }
    }
}
