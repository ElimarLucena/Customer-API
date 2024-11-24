namespace UnitTests.util.Models.JwtModels
{
    public class JwtToken
    {
        public string SigningAlgorithm { get; set; } = string.Empty;
        public string TokenType { get; set; } = string.Empty;
        public Guid CustomerId { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
