using DMC.Core.Data;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace DMC.Core.Messages
{
    public abstract class CommandHandler
    {
        public ValidationResult ValidationResult { get; protected set; }
        public CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }
        protected void AddError(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        protected void AddError(string type, string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(type, message));
        }

        protected async Task<ValidationResult> SaveChanges(IUnitOfWork unitOfWork)
        {
            var success = await unitOfWork.Commit();

            if (!success)
            {
                AddError("Houve um erro ao persistir os dados.");
            }

            return ValidationResult;
        }
    }
}