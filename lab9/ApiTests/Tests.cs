using System;
using System.Collections.Generic;
using System.Linq;
using Api;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ApiTests
{
    public class Tests
    {
        private readonly ApiRequest api = new ApiRequest();
        private readonly Dump dump = new Dump();
        private readonly List<int> addedID = new List<int>();

        [Test]
        public void GetAllProductsTest()
        {
            var response = api.GetAllProducts();
            var products = JsonConvert.DeserializeObject<JsonProduct[]>(response.Content.ReadAsStringAsync().Result);

            var answer = dump.CheckEqualsLists(ref products);

            Assert.AreEqual(answer, true);
            Assert.AreEqual(response.StatusCode.GetHashCode(), 200);
        }

        [TestCase(new[] {"1", "testTitle1", "10,25", "8,5", "0", "0"}, 200)]
        [TestCase(new[] {"0", "testTitle2", "1000", "850", "0", "0"}, 412)]
        [TestCase(new[] {"1", "testTitle3", "1000", "850", "0", "0"}, 200)]
        [TestCase(new[] {"15", "testTitle4", "1000", "850", "0", "0"}, 200)]
        [TestCase(new[] {"16", "testTitle5", "1000", "850", "0", "0"}, 412)]
        [TestCase(new[] {"7", "testTitle6", "-120", "850", "0", "0"}, 412)]
        [TestCase(new[] {"7", "testTitle7", "1200", "850", "0", "0"}, 200)]
        [TestCase(new[] {"7", "testTitle8", "1000", "850", "0", "1"}, 200)]
        [TestCase(new[] {"7", "testTitle9", "1020", "850", "0", "2"}, 412)]
        [TestCase(new[] {"7", "testTitle10", "1200", "850", "1", "0"}, 200)]
        [TestCase(new[] {"7", "testTitle11", "1200", "850", "2", "0"}, 412)]
        [TestCase(new[] {"7", "Casio MRP-700-1AVEF", "1200", "850", "1", "0"}, 200)]
        public void AddProductTest(string[] productParams, int statusCode)
        {
            var product = new JsonProduct
            {
                category_id = Convert.ToInt32(productParams[0]),
                title = productParams[1],
                price = Convert.ToDouble(productParams[2]),
                old_price = Convert.ToDouble(productParams[3]),
                status = Convert.ToInt32(productParams[4]),
                hit = Convert.ToInt32(productParams[5])
            };

            var createResponse = api.CreateProduct(ref product);

            Assert.AreEqual(createResponse.statusCode, statusCode);
            api.DeleteProduct(createResponse.id);
        }

        [TestCase(0, false)]
        [TestCase(15, true)]
        [TestCase(1, true)]
        [TestCase(16, false)]
        public void EditCategoryProductTest(int newCategory, bool answer)
        {
            var product = new JsonProduct();
            var createResponse = api.CreateProduct(ref product);
            var response = api.GetAllProducts();
            var products = JsonConvert.DeserializeObject<JsonProduct[]>(response.Content.ReadAsStringAsync().Result);
            var editedProduct = products[FindProductById(ref products, createResponse.id)];

            editedProduct.category_id = newCategory;

            api.EditProduct(ref editedProduct);

            response = api.GetAllProducts();
            products = JsonConvert.DeserializeObject<JsonProduct[]>(response.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(
                products[FindProductById(ref products, createResponse.id)].CheckEqualProducts(ref editedProduct),
                answer);
            api.DeleteProduct(createResponse.id);
        }

        [TestCase(2, false)]
        [TestCase(0, true)]
        [TestCase(-1, false)]
        [TestCase(1, true)]
        public void EditStatusTest(int newStatus, bool answer)
        {
            var product = new JsonProduct();
            var createResponse = api.CreateProduct(ref product);
            var response = api.GetAllProducts();
            var products = JsonConvert.DeserializeObject<JsonProduct[]>(response.Content.ReadAsStringAsync().Result);
            var editedProduct = products[FindProductById(ref products, createResponse.id)];

            editedProduct.status = newStatus;

            api.EditProduct(ref editedProduct);

            response = api.GetAllProducts();
            products = JsonConvert.DeserializeObject<JsonProduct[]>(response.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(
                products[FindProductById(ref products, createResponse.id)].CheckEqualProducts(ref editedProduct),
                answer);
            api.DeleteProduct(createResponse.id);
        }

        [TestCase(2, false)]
        [TestCase(0, true)]
        [TestCase(-1, false)]
        [TestCase(1, true)]
        public void EditHitTest(int newStatus, bool answer)
        {
            var product = new JsonProduct();
            var createResponse = api.CreateProduct(ref product);
            var response = api.GetAllProducts();
            var products = JsonConvert.DeserializeObject<JsonProduct[]>(response.Content.ReadAsStringAsync().Result);
            var editedProduct = products[FindProductById(ref products, createResponse.id)];

            editedProduct.status = newStatus;

            api.EditProduct(ref editedProduct);

            response = api.GetAllProducts();
            products = JsonConvert.DeserializeObject<JsonProduct[]>(response.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(
                products[FindProductById(ref products, createResponse.id)].CheckEqualProducts(ref editedProduct),
                answer);
            api.DeleteProduct(createResponse.id);
        }

        [TestCase("", true)]
        [TestCase(null, false)]
        [TestCase("Title", true)]
        [TestCase("Title123", true)]
        [TestCase("Новый заголовок", true)]
        public void EditTitleTest(string newTitle, bool answer)
        {
            var product = new JsonProduct();
            var createResponse = api.CreateProduct(ref product);
            var response = api.GetAllProducts();
            var products = JsonConvert.DeserializeObject<JsonProduct[]>(response.Content.ReadAsStringAsync().Result);
            var editedProduct = products[FindProductById(ref products, createResponse.id)];

            editedProduct.title = newTitle;

            api.EditProduct(ref editedProduct);

            response = api.GetAllProducts();
            products = JsonConvert.DeserializeObject<JsonProduct[]>(response.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(
                products[FindProductById(ref products, createResponse.id)].CheckEqualProducts(ref editedProduct),
                answer);
            api.DeleteProduct(createResponse.id);
        }

        [TestCase("", true)]
        [TestCase(null, true)]
        [TestCase("Description", true)]
        [TestCase("Some New Product's Description 1 2 3", true)]
        [TestCase("Это описание на другом языке", true)]
        public void EditDescriptionTest(string newDescription, bool answer)
        {
            var product = new JsonProduct();
            var createResponse = api.CreateProduct(ref product);
            var response = api.GetAllProducts();
            var products = JsonConvert.DeserializeObject<JsonProduct[]>(response.Content.ReadAsStringAsync().Result);
            var editedProduct = products[FindProductById(ref products, createResponse.id)];

            editedProduct.description = newDescription;

            api.EditProduct(ref editedProduct);

            response = api.GetAllProducts();
            products = JsonConvert.DeserializeObject<JsonProduct[]>(response.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(
                products[FindProductById(ref products, createResponse.id)].CheckEqualProducts(ref editedProduct),
                answer);
            api.DeleteProduct(createResponse.id);
        }

        [TestCase("", true)]
        [TestCase(null, true)]
        [TestCase("Keyword", true)]
        [TestCase("Casio 3", true)]
        [TestCase("Часы", true)]
        public void EditKeywordsTest(string newKeywords, bool answer)
        {
            var product = new JsonProduct();
            var createResponse = api.CreateProduct(ref product);
            var response = api.GetAllProducts();
            var products = JsonConvert.DeserializeObject<JsonProduct[]>(response.Content.ReadAsStringAsync().Result);
            var editedProduct = products[FindProductById(ref products, createResponse.id)];

            editedProduct.keywords = newKeywords;

            api.EditProduct(ref editedProduct);

            response = api.GetAllProducts();
            products = JsonConvert.DeserializeObject<JsonProduct[]>(response.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(
                products[FindProductById(ref products, createResponse.id)].CheckEqualProducts(ref editedProduct),
                answer);
            api.DeleteProduct(createResponse.id);
        }

        [TestCase("", true)]
        [TestCase(null, true)]
        [TestCase("<p>Test Content\n In This String</p>", true)]
        [TestCase("<p>Какой то контент на русском языке</p>", true)]
        public void EditContentTest(string newContent, bool answer)
        {
            var product = new JsonProduct();
            var createResponse = api.CreateProduct(ref product);
            var response = api.GetAllProducts();
            var products = JsonConvert.DeserializeObject<JsonProduct[]>(response.Content.ReadAsStringAsync().Result);
            var editedProduct = products[FindProductById(ref products, createResponse.id)];

            editedProduct.content = newContent;

            api.EditProduct(ref editedProduct);

            response = api.GetAllProducts();
            products = JsonConvert.DeserializeObject<JsonProduct[]>(response.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(
                products[FindProductById(ref products, createResponse.id)].CheckEqualProducts(ref editedProduct),
                answer);
            api.DeleteProduct(createResponse.id);
        }

        [TestCase(20, true)]
        [TestCase(0, true)]
        [TestCase(-1, false)]
        [TestCase(1000, true)]
        public void EditPriceTest(int newPrice, bool answer)
        {
            var product = new JsonProduct();
            var createResponse = api.CreateProduct(ref product);
            var response = api.GetAllProducts();
            var products = JsonConvert.DeserializeObject<JsonProduct[]>(response.Content.ReadAsStringAsync().Result);
            var editedProduct = products[FindProductById(ref products, createResponse.id)];

            editedProduct.old_price = editedProduct.price;
            editedProduct.price = newPrice;

            api.EditProduct(ref editedProduct);

            response = api.GetAllProducts();
            products = JsonConvert.DeserializeObject<JsonProduct[]>(response.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(
                products[FindProductById(ref products, createResponse.id)].CheckEqualProducts(ref editedProduct),
                answer);

            api.DeleteProduct(createResponse.id);
        }


        [Test]
        public void DeleteProduct()
        {
            var product = new JsonProduct();

            var createResponse = api.CreateProduct(ref product);

            Assert.AreEqual(api.DeleteProduct(createResponse.id), 200);
        }

        [Test]
        public void DeleteAllProducts()
        {
            var response = api.GetAllProducts();
            var products = JsonConvert.DeserializeObject<JsonProduct[]>(response.Content.ReadAsStringAsync().Result);

            for (var i = Dump.ProductCount; i < products.Length; ++i) addedID.Add(products[i].id);

            foreach (var code in addedID.Select(t => api.DeleteProduct(t))) Assert.AreEqual(code, 200);

            addedID.Clear();
        }

        private int FindProductById(ref JsonProduct[] products, int id)
        {
            for (var i = 0; i < products.Length; ++i)
                if (products[i].id == id)
                    return i;
            return 10000;
        }
    }
}