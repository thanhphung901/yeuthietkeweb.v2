using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class News_details_entity
    {
        public int NEWS_ID { get; set; }
        public string NEWS_TITLE { get; set; }
        public string NEWS_IMAGE3 { get; set; }
        public string NEWS_DESC { get; set; }
        public string NEWS_SEO_URL { get; set; }
        public string NEWS_URL { get; set; }
        public int NEWS_ORDER { get; set; }
        public int NEWS_ORDER_PERIOD { get; set; }
        public DateTime NEWS_PUBLISHDATE { get; set; }
        public string NEWS_CODE { get; set; }
        public string CAT_SEO_URL { get; set; }
    }
}
