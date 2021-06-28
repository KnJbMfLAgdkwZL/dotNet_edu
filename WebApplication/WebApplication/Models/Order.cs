using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using SQLitePCL;

namespace WebApplication.models
{
    public class Order
    {
        public long id { set; get; }
        public string name { set; get; }
        public string description { set; get; }
        [JsonIgnore] public DateTime dateCreate { set; get; }

        public Order()
        {
        }

        public Order(long id, string name, string description, DateTime dateCreate)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.dateCreate = dateCreate;
        }

        public Order(string name, string description)
        {
            this.id = 0;
            this.name = name;
            this.description = description;
            this.description = null;
            //this.dateCreate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}