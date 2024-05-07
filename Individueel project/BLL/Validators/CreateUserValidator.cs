using BLL.Classes;
using FluentValidation;
using Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BLL.Validators
{
    public class CreateUserValidator : AbstractValidator<User>
    {
        IUserDal _userDal;
        public CreateUserValidator(IUserDal userDal)
        {
            _userDal = userDal;

            RuleFor(user => user.Email).NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Invalid email address format.")
                .DependentRules(() =>
                { RuleFor(u => _userDal.IsValueUnique("Email", u.Email, null)).Equal(true).WithMessage("Email is already used!"); }
                );

            RuleFor(user => user.UserName).NotEmpty().WithMessage("Username is required.")
                .MinimumLength(4).WithMessage("Username is too short.")
                .MaximumLength(25).WithMessage("Username is too long.")
                .Matches("^[a-zA-Z0-9.,]*$").WithMessage("Special characters are not allowed.")
                .DependentRules(() =>
                {
                    RuleFor(u => _userDal.IsValueUnique("UserName", u.UserName, null)).Equal(true).WithMessage("Username is already used!");
                });

            RuleFor(user => user.Password).NotEmpty().WithMessage("Please enter a password.").MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$")
                .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, and one number.");

            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Firstname is required.")
            .Matches("^[a-zA-Z0-9]*$").WithMessage("Special characters in a first name are not allowed.")
            .Must(s => !string.IsNullOrWhiteSpace(s)).WithMessage("First name cannot contain only spaces.");

            RuleFor(x => x.LastName)
           .NotEmpty().WithMessage("Lastname is required.")
           .Matches("^[a-zA-Z0-9-]*$").WithMessage("Special characters in a last name are not allowed.")
           .Must(s => !string.IsNullOrWhiteSpace(s)).WithMessage("Last name cannot contain only spaces.");


            RuleFor(user => user.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+(?:[0-9] ?){6,14}[0-9]$").WithMessage("Invalid phone number format.");

            RuleFor(user => user.Gender).NotNull().WithMessage("Please enter a gender");
        }

    }
}
