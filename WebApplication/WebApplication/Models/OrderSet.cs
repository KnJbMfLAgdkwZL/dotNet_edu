using System;

namespace WebApplication.models
{
    public class OrderSet : Order
    {
        private new long id { set; get; }
        public new string name { set; get; }
        public new string description { set; get; }
        private new DateTime dateCreate { set; get; }

        public OrderSet(string name, string description) : base(name, description)
        {
        }
    }
}