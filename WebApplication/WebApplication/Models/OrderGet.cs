using System;

namespace WebApplication.models
{
    public class OrderGet : Order
    {
        public long id { set; get; }
        public string name { set; get; }
        public string description { set; get; }
        public DateTime dateCreate { set; get; }
    }
}