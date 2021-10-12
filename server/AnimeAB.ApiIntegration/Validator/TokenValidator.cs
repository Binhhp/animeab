using AnimeAB.Domain.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeAB.ApiIntegration.Validator
{
    public class TokenValidator : AbstractValidator<RefreshTokenDto>
    {
        public TokenValidator()
        {
            RuleFor(x => x.refresh_token)
                .NotEmpty()
                .NotNull();
        }
    }
}
