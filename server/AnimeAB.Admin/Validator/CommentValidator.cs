
using AnimeAB.Domain.DTOs;
using FluentValidation;

namespace AnimeAB.Core.Validator
{
    public class CommentValidator : AbstractValidator<CommentDto>
    {
        public CommentValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.message)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.photo_url)
                .NotEmpty()
                .NotNull();
        }
    }
}
