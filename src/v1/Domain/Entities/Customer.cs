namespace Domain.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public long Phone { get; set; }
        public string Document { get; set; } = string.Empty;
    }
}

