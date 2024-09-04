using FluentValidation;
using Tasks.Common.DTOs.Task;

namespace Tasks_BE.Validators.Task
{
    public class UpdateTaskValidator : AbstractValidator<UpdateTaskDTO>
    {
        public UpdateTaskValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.Status)
                .IsInEnum();

            RuleFor(x => x.Priority)
                .IsInEnum();
        }
    }
}
