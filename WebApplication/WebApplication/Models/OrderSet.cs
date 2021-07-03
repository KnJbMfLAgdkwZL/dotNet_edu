namespace WebApplication.models
{
    public class OrderSet
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long ClientId { get; set; }

        public OrderSet(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}