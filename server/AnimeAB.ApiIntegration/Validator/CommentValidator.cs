
using AnimeAB.Domain.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeAB.ApiIntegration.Validator
{
    public class CommentValidator : AbstractValidator<CommentRequest>
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
