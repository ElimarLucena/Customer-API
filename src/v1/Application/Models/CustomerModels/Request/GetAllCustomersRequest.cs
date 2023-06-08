using Domain.Entities;
using MediatR;

namespace Application.Models.CustomerModels.Request
{
    public class GetAllCustomersRequest : IRequest<List<Customer>>
    {
    }
}
