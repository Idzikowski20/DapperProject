using DapperProject.Dtos;
using FluentValidation;

namespace DapperProject.Validators
{
    public class CreateVideoGameDtoValidator : AbstractValidator<CreateVideoGameDto>
    {
        public CreateVideoGameDtoValidator() 
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Publisher)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Developer)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Platform)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.ReleaseDate)
                .LessThanOrEqualTo(DateTime.Now);
        }
    }
}
