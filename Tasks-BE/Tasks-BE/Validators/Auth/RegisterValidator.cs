﻿using FluentValidation;
using Tasks.Common.DTOs;

namespace Tasks_BE.Validators.Auth
{
    public class RegisterValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterValidator()
        {
            RuleFor(dto => dto.Username)
                .NotEmpty()
                .WithMessage("Username is required");

            RuleFor(dto => dto.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Your email is invalid");

            RuleFor(dto => dto.Password)
                .NotEmpty()
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$")
                .WithMessage("Your password must be 8 minimum length and must contain at least one uppercase and lowercase letter, one number and one special symbol");
        }
    }
}
