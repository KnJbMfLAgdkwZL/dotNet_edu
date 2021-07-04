using System;

namespace WebApplication.models
{
    public class Order
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreate { get; set; }
        public long ClientId { get; set; }

        public Order()
        {
        }

        public Order(long id, string name, string description, DateTime dateCreate)
        {
            Id = id;
            Name = name;
            Description = description;
            DateCreate = dateCreate;
        }
    }
}