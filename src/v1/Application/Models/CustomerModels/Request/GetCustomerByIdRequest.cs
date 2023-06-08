using Application.Models.CustomerModels.Response;
using MediatR;

namespace Application.Models.CustomerModels.Request
{
    public class GetCustomerByIdRequest : IRequest<GetCustomerByIdResponse>
    {
        public int CustomerId { get; set; }
    }
}
