using System;

namespace WebApplication.models
{
    public class Order
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public DateTime DateCreate { set; get; }

        public Order(long id, string name, string description, DateTime dateCreate)
        {
            Id = id;
            Name = name;
            Description = description;
            DateCreate = dateCreate;
        }
    }
}