using System;

namespace WebApplication.models
{
    public class Order
    {
        public long id { set; get; }
        public string name { set; get; }
        public string description { set; get; }
        public DateTime dateCreate { set; get; }

        public Order(long id, string name, string description, DateTime dateCreate)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.dateCreate = dateCreate;
        }
    }
}