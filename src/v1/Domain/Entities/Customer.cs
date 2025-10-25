namespace Domain.Entities
{
    public class Customer
    {
        public Guid CustomerId { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public long Phone { get; set; }
        public string Document { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public DateTime UdatedAt { get; set; }
    }
}

