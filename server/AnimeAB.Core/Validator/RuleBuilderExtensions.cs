using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeAB.Core.Validator
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                          .NotEmpty()
                          .WithMessage("Bạn cần nhập mật khẩu.")
                          .NotNull()
                          .WithMessage("Bạn cần nhập mật khẩu.")
                          .MinimumLength(8)
                          .MaximumLength(16)
                          .WithMessage(string.Format(@"Mật khẩu phải dài ít nhất {1} và tối đa {0} ký tự.", 16, 8));
                          //.Matches("^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$").WithMessage("regex error");

            return options;
        }
    }
}
