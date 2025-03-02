namespace Trellix.Validators;

using FluentValidation;
using System.Text.RegularExpressions;

public class GuidValidator : AbstractValidator<string>
{
    public GuidValidator()
    {
        RuleFor(attachment => attachment.ToString()).Custom((str, context) =>
        {
            if (str.Length == 0)
            {
                context.AddFailure("Id field should not be empty");
            }
            else if (!Regex.Match(str, @"(^[A-Za-z0-9]+)[\-A-Za-z0-9]+[A-Za-z0-9]+$", RegexOptions.IgnoreCase).Success)
            {
                context.AddFailure("Allowed Id characters are: Abc123-");
            }
            else if (str.Length > 36)
            {
                context.AddFailure("Id should not exceed maximum of 36 characters");
            }
        });
    }
}