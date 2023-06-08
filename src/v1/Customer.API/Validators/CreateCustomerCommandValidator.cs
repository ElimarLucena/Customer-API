//using Application.Models.CommandModel;
//using FluentValidation;

/*namespace Customer.API.Validators
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator() 
        {
            RuleFor(customer => customer.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Name cannot be emppty.");
            RuleFor(customer => customer.Email)
                .EmailAddress()
                .WithMessage("Please specify a valid email address");
            RuleFor(customer => customer.Age)
                .NotEmpty()
                .WithMessage("Age cannot be emppty.");
            RuleFor(customer => customer.Phone)
                .NotEmpty()
                .WithMessage("Phone cannot be emppty.");
            RuleFor(customer => customer.Document)
                .NotNull()
                .NotEmpty()
                .WithMessage("Document cannot be emppty.");
            RuleFor(customer => customer.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Password cannot be emppty.");
        }
    }
}*/
