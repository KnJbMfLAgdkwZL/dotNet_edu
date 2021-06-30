namespace WebApplication.models
{
    public class OrderSet
    {
        public string Name { set; get; }
        public string Description { set; get; }

        public OrderSet(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}