using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace TPL_AsyncAwait_Demo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                GetToDo todoRequest = new GetToDo();

                var response =  await todoRequest.GetToDoAsynchronous();
                Console.WriteLine(response);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
           
        }
    }

    public class GetToDo
    {
        public readonly HttpClient client;

        public GetToDo()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(Consts.BaseUrl)
            };
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Consts.MediaType));
        }
       

        public async Task<string> GetToDoAsynchronous()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(Consts.ToDo);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
            
        }
    }

     public static class Consts
    {
        public const string BaseUrl = "https://jsonplaceholder.typicode.com";
        public const string ToDo = "/todos";
        public const string MediaType = "application/json";
    }
}
