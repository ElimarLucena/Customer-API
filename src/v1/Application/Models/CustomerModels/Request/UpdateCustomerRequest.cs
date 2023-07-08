using Application.Models.CustomerModels.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.CustomerModels.Request
{
    public class UpdateCustomerRequest : IRequest<UpdateCustomerResponse>
    {
        [Required(ErrorMessage = "The customerId is required.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "The name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The name needs to be longer than 3 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The email is required.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(@)(.+)$", ErrorMessage = "The email is invalid.", MatchTimeoutInMilliseconds = 250)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "The age is required.")]
        [Range(0, 100)]
        public int Age { get; set; }

        [Required(ErrorMessage = "The phone is required.")]
        [DataType(DataType.PhoneNumber)]
        public long Phone { get; set; }

        [Required(ErrorMessage = "The document is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The document needs to be longer than 3 characters.")]
        public string Document { get; set; } = string.Empty;
    }
}
