using Application.Models.CustomerModels.Response;
using MediatR;

namespace Application.Models.CustomerModels.Request
{
    public class DeleteCustomerByIdRequest : IRequest<DeleteCustomerByIdResponse>
    {
        public int CustomerId { get; set; }
    }
}
