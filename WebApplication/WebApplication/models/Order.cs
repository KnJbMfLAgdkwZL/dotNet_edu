using System;
using System.Collections.Generic;
using System.Text.Json;
using SQLitePCL;

namespace WebApplication.models
{
    public class Order
    {
        public int id { set; get; }
        public string name { set; get; }
        public string description { set; get; }
        public string dateCreate { set; get; }

        public Order()
        {
        }

        public Order(object id, object name, object description, object dateCreate)
        {
            this.id = Int32.Parse(id.ToString() ?? string.Empty);
            this.name = name.ToString();
            this.description = description.ToString();
            this.dateCreate = dateCreate.ToString();
        }

        public Order(string name, string description)
        {
            this.id = 0;
            this.name = name;
            this.description = description;
            this.dateCreate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}