using Application.Models.CustomerModels.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.CustomerModels.Request
{
    public class DeleteCustomerByIdRequest : IRequest<DeleteCustomerByIdResponse>
    {
        [Required(ErrorMessage = "The customerId is required.")]
        public Guid CustomerId { get; set; }
    }
}
