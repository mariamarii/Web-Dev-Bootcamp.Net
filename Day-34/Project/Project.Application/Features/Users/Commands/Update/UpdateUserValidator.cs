using FluentValidation;
using Project.Domain.Models.Users;

namespace Project.Application.Features.Users.Commands.Update;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User ID is required.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .MaximumLength(UserConstants.MaxFirstNameLength)
            .WithMessage($"First name cannot exceed {UserConstants.MaxFirstNameLength} characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .MaximumLength(UserConstants.MaxLastNameLength)
            .WithMessage($"Last name cannot exceed {UserConstants.MaxLastNameLength} characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email must be a valid email address.")
            .MaximumLength(UserConstants.MaxEmailLength)
            .WithMessage($"Email cannot exceed {UserConstants.MaxEmailLength} characters.");

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(UserConstants.MaxPhoneNumberLength)
            .WithMessage($"Phone number cannot exceed {UserConstants.MaxPhoneNumberLength} characters.")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
    }
}