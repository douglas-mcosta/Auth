using DMC.Core.Messages;
using FluentValidation;
using System;
using birtDate = DMC.Core.DomainObject;

namespace DMC.Auth.API.Application.Commands
{
    public class RegisterUserCommand : Command
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public int Education { get; private set; }

        public RegisterUserCommand(Guid id, string firstName, string lastName, string email, DateTime birthDate, int education)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;
            Education = education;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class CreateUserCommandValidation : AbstractValidator<RegisterUserCommand>
        {
            public CreateUserCommandValidation()
            {
                RuleFor(u => u.FirstName)
                    .NotEmpty().WithMessage("O Primimeiro Nome é obrigátorio.")
                    .Length(2, 100).WithMessage("O Primeiro nome deve ter entre {MinLength} e {MaxLength}");

                RuleFor(u => u.LastName)
                    .NotEmpty().WithMessage("O Primimeiro Nome é obrigátorio.")
                    .Length(2, 150).WithMessage("O Primeiro nome deve ter entre {MinLength} e {MaxLength}");

                RuleFor(u => u.Email)
                    .NotEmpty().WithMessage("O E-mail é obrigátorio.")
                    .Must(HasEmailValid).WithMessage("E-mail inválido.");

                RuleFor(u => u.BirthDate)
                    .NotEmpty().WithMessage("A Data de Nascimento é obrigatoria.")
                    .Must(HasBirthDateValid).WithMessage("Data de Nascimento inválida.");

                RuleFor(u => u.Education)
                    .GreaterThan(0).WithMessage("Educação inválida.")
                    .LessThanOrEqualTo(4).WithMessage("Educação inválida.");
            }

            private static bool HasBirthDateValid(DateTime birthDate)
            {
                return birtDate.BirthDate.IsValid(birthDate);
            }

            private static bool HasEmailValid(string email)
            {
                return birtDate.Email.IsValid(email);
            }
        }
    }
}