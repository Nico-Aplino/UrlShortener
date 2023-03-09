using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.Models;

namespace UrlShortener.BLL.Validations
{
    public class UrlValidator : AbstractValidator<UrlModel>
    {
        public UrlValidator()
        {
            RuleFor(x => x.OriginalUrl)
                .NotEmpty().WithMessage("The URL must not be empty.")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
                             (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                .WithMessage("The URL must be a valid HTTP or HTTPS URL.");
        }
    }
}
