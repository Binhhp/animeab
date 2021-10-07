using AnimeAB.Core.Controllers;
using AnimeAB.Domain.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeAB.Core.Validator
{
    public class CollectionValidator : AbstractValidator<CollectionDto>
    {
        public CollectionValidator()
        {
            RuleFor(x => x.Key)
                .NotEmpty()
                .WithMessage("Bạn cần nhập key.")
                .NotNull();

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Bạn cần nhập title.")
                .NotNull();

            RuleFor(x => x.FileUpload).SetValidator(new FileValidator());
        }
    }
}
