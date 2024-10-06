using Application.Models.CustomerModels.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.CustomerModels.Request
{
    public class GetCustomerByIdRequest : IRequest<GetCustomerByIdResponse>
    {
        [Required(ErrorMessage = "The customerId is required.")]
        public Guid CustomerId { get; set; }
    }
}
