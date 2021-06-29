using System;

namespace WebApplication.models
{
    public class OrderGet : Order
    {
        public new long id { set; get; }
        public new string name { set; get; }
        public new string description { set; get; }
        public new DateTime dateCreate { set; get; }
    }
}