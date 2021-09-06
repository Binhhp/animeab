using AnimeAB.Core.ChatHubs;
using AnimeAB.Reponsitories.DTO;
using AnimeAB.Reponsitories.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
