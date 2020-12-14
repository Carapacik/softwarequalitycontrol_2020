namespace Api
{
    public class JsonProduct
    {
        public int id { get; set; }
        public int category_id { get; set; } // 1 - 15
        public string title { get; set; }
        public string alias { get; set; }
        public string content { get; set; }
        public double price { get; set; }
        public double old_price { get; set; }
        public int status { get; set; } // 0 - 1
        public string keywords { get; set; }
        public string description { get; set; }
        public string img { get; set; }
        public int hit { get; set; } // 0 - 1
        public string cat { get; set; }

        public JsonProduct()
        {
            category_id = 1;
            title = "some test";
            content = "<p>some huge text for test this field\n test description</p>";
            price = 10;
            old_price = 1;
            status = 0;
            keywords = "test";
            description = "это какое то описание на русском языке ";
            hit = 1;
        }

        public bool CheckEqualProducts(ref JsonProduct product)
        {
            if (id != product.id)
                return false;

            if (category_id != product.category_id)
                return false;

            if (title != product.title)
                return false;

            if (content != product.content)
                return false;

            if (price != product.price)
                return false;

            if (old_price != product.old_price)
                return false;

            if (status != product.status)
                return false;

            if (keywords != product.keywords)
                return false;

            if (description != product.description)
                return false;

            if (img != product.img)
                return false;

            return hit == product.hit;
        }
    }
}