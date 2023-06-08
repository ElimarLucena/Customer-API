using Application.Models.CustomerModels.Response;
using MediatR;

namespace Application.Models.CustomerModels.Request
{
    public class CreateCustomerRequest : IRequest<CreateCustomerResponse>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public long Phone { get; set; }
        public string Document { get; set; } = string.Empty;
    }
}
