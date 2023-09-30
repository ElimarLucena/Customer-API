using Application.Models.CustomerModels.Response;
using MediatR;

namespace Application.Models.CustomerModels.Request
{
    public class GetAllCustomersRequest : IRequest<List<GetAllCustomerResponse>>
    {
    }
}
