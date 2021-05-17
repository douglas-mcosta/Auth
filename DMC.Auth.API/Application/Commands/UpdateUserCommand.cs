using DMC.Core.Messages;
using FluentValidation;
using System;
using birthDate = DMC.Core.DomainObject; 

namespace DMC.Auth.API.Application.Commands
{
    public class UpdateUserCommand : Command
    {
        public Guid Id { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public int Education { get; private set; }

        public UpdateUserCommand(Guid id, string firstName, string lastName, DateTime birthDate, int education)
        {
            AggregateId = id;
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Education = education;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class UpdateUserCommandValidation : AbstractValidator<UpdateUserCommand>
        {
            public UpdateUserCommandValidation()
            {
                RuleFor(u => u.FirstName)
                    .NotEmpty().WithMessage("O Primimeiro Nome é obrigátorio.")
                    .Length(2, 100).WithMessage("O Primeiro nome deve ter entre {MinLength} e {MaxLength}");

                RuleFor(u => u.LastName)
                    .NotEmpty().WithMessage("O Primimeiro Nome é obrigátorio.")
                    .Length(2, 150).WithMessage("O Primeiro nome deve ter entre {MinLength} e {MaxLength}");


                RuleFor(u => u.BirthDate)
                    .NotEmpty().WithMessage("A Data de Nascimento é obrigatoria.")
                    .Must(HasBirthDateValid).WithMessage("Data de Nascimento inválida.");

                RuleFor(u => u.Education)
                    .GreaterThan(0).WithMessage("Educação inválida.")
                    .LessThanOrEqualTo(4).WithMessage("Educação inválida.");
            }

            private static bool HasBirthDateValid(DateTime date)
            {
                return birthDate.BirthDate.IsValid(date);
            }
          
        }
    }
}