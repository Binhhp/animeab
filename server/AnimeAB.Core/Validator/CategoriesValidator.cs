using AnimeAB.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeAB.AppAdmin.Validator
{
    public class CategoriesValidator : AbstractValidator<Categories>
    {
        public CategoriesValidator()
        {
            RuleFor(x => x.Key)
                .NotEmpty()
                .WithMessage("Bạn cần nhập key.")
                .NotNull();

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Bạn cần nhập title.")
                .NotNull();

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Bạn cần nhập mô tả.")
                .NotNull();
        }
    }
}
