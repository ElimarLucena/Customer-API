using Application.Models.LoginModels.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.LoginModels.Request
{
    public class LoginCustomerRequest : IRequest<LoginCustomerResponse>
    {
        [Required(ErrorMessage = "The email is required.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "The email is invalid.", MatchTimeoutInMilliseconds = 250)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "The password is required.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "The name needs to be longer than 4 characters.")]
        public string Password { get; set; } = string.Empty;
    }
}
