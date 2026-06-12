using System.ComponentModel.DataAnnotations;

namespace Sparrow.Application.Validations
{
    public class HttpHttpsUrlAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var url = value.ToString();

            if (string.IsNullOrWhiteSpace(url))
                return ValidationResult.Success;

            if (Uri.TryCreate(url, UriKind.Absolute, out Uri uri) &&
                (uri.Scheme == Uri.UriSchemeHttp ||
                 uri.Scheme == Uri.UriSchemeHttps))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Only valid HTTP or HTTPS URLs are allowed.");
        }
    }
}
