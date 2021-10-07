using AnimeAB.Domain.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeAB.Core.Validator
{
    public class AccountLoginValidator : AbstractValidator<AccountLoginDto>
    {
        public AccountLoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Bạn cần nhập email.")
                .NotNull()
                .EmailAddress().WithMessage("Email không hợp lệ.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Bạn cần nhập mật khẩu.").NotNull();
        }
    }

    public class AccountValidator : AbstractValidator<AccountSignUpDto>
    {
        public AccountValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Bạn cần nhập email.")
                .NotNull()
                .EmailAddress().WithMessage("Email không hợp lệ.");

            RuleFor(x => x.Password).Password();

            RuleFor(x => x.ConfirmPassword)
                .Password().Equal(x => x.Password).WithMessage("Mật khẩu nhập lại không đúng.");

            RuleFor(x => x.FullName).NotEmpty().WithMessage("Bạn cần nhập tên của bạn.").NotNull();
        }
    }
}
