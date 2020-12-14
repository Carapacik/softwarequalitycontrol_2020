using System;
using System.Text;
using Newtonsoft.Json;

namespace Api
{
    internal static class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            var json = new JsonProduct();
            var temp = JsonConvert.SerializeObject(json);

            Console.WriteLine(temp);

            var api = new ApiRequest();
            var createResponse = api.CreateProduct(ref json);
            if (createResponse.statusCode == 200)
                Console.WriteLine("woooo, it's created");

            Console.WriteLine(createResponse.id);
        }
    }
}