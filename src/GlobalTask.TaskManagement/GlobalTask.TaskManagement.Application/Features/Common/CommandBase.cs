using FluentValidation;
using GlobalTask.TaskManagement.Application.Contracts.Validation;

namespace GlobalTask.TaskManagement.Application.Features.Common
{
    public class CommandBase<T> : IValidableEntity where T : CommandBase<T>
    {
        IEnumerable<string> _erros = new List<string>();
        readonly AbstractValidator<T> _validator;
        public ISet<string> Errors => new HashSet<string>(_erros);

        public CommandBase(AbstractValidator<T> validator)
        {
            _validator = validator;
        }

        public bool Validate()
        {
            var result = _validator.Validate((T)this);
            _erros = result.Errors.Select(x => x.ErrorMessage);

            return result.IsValid;
        }
    }
}
