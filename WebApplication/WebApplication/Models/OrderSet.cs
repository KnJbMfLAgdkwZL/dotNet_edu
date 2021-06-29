using System;

namespace WebApplication.models
{
    public class OrderSet
    {
        public string name { set; get; }
        public string description { set; get; }

        public OrderSet(string name, string description)
        {
            this.name = name;
            this.description = description;
        }
    }
}