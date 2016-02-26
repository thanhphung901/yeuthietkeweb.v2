using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Cart_result_entity
    {
        public int Basket_quantity { get; set; }
        public decimal Basket_Price { get; set; }
        public decimal BasketSum_Price { get; set; }
        public int BASKET_ID { get; set; }
        public int NEWS_ID { get; set; }
        public string NEWS_SEO_URL { get; set; }
        public string NEWS_URL { get; set; }
        public string NEWS_IMAGE3 { get; set; }
        public string NEWS_TITLE { get; set; }
        public string CAT_SEO_URL { get; set; }
    }
}
