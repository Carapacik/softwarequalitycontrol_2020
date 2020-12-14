using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Api
{
    public class ApiRequest
    {
        private const string BaseUrl = "http://91.210.252.240:9010/api/";
        private static readonly HttpClient client = new HttpClient();

        public HttpResponseMessage GetAllProducts()
        {
            var response = client.GetAsync(BaseUrl + "products").Result;
            return response;
        }

        public int DeleteProduct(int id)
        {
            var response = client.GetAsync(BaseUrl + "deleteproduct?id=" + id).Result;
            return response.StatusCode.GetHashCode();
        }

        public ApiCreateResponse CreateProduct(ref JsonProduct product)
        {
            var json = JsonSerializer.Serialize(product);
            var data = new StringContent(json, Encoding.UTF8);

            Console.WriteLine(data.ReadAsStringAsync().Result);

            var response = client.PostAsync(BaseUrl + "addproduct", data).Result;

            var createResponse =
                JsonSerializer.Deserialize<ApiCreateResponse>(response.Content.ReadAsStringAsync().Result);
            createResponse.statusCode = response.StatusCode.GetHashCode();
            return createResponse;
        }

        public int EditProduct(ref JsonProduct product)
        {
            var json = JsonSerializer.Serialize(product);
            var data = new StringContent(json, Encoding.UTF8);

            Console.WriteLine(data.ReadAsStringAsync().Result);

            var response = client.PostAsync(BaseUrl + "editproduct", data).Result;

            return response.StatusCode.GetHashCode();
        }
    }

    public class ApiCreateResponse
    {
        public int id { get; set; }
        public int statusCode { get; set; }
    }
}