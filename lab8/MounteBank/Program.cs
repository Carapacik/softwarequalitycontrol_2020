using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MbDotNet;
using MbDotNet.Enums;

namespace MounteBank
{
    internal static class Program
    {
        private static async Task Request(string key)
        {
            var request = "http://localhost:7777/rate/" + key;
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var text = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"\r\nRequest: {request} \r\nStatus code: {response.StatusCode} \r\nContent: {text}");
            }
            else
            {
                Console.WriteLine($"\r\nRequest: {request} \r\nStatus code: {response.StatusCode}");
            }
        }

        private static async Task Main()
        {
            var client = new MountebankClient();
            var imposter = client.CreateHttpImposter(7777);
            var usd = new TestObj {Key = "usd", Rate = 36.96};
            var euro = new TestObj {Key = "eur", Rate = 44.88};
            var yen = new TestObj {Key = "yen", Rate = 1.36};

            imposter.AddStub().ReturnsJson(HttpStatusCode.OK, usd)
                .OnPathAndMethodEqual($"/rate/{usd.Key}", Method.Get);
            imposter.AddStub().ReturnsJson(HttpStatusCode.OK, euro)
                .OnPathAndMethodEqual($"/rate/{euro.Key}", Method.Get);
            imposter.AddStub().ReturnsJson(HttpStatusCode.OK, yen)
                .OnPathAndMethodEqual($"/rate/{yen.Key}", Method.Get);

            imposter.AddStub().ReturnsStatus(HttpStatusCode.BadRequest);

            await client.SubmitAsync(imposter);

            await Request("usd");
            await Request("eur");
            await Request("yen");
            await Request("rub");
            await client.DeleteAllImpostersAsync();
        }

        private struct TestObj
        {
            public string Key { get; set; }
            public double Rate { private get; set; }
        }
    }
}