namespace Trellix.Validators;

using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

public class AttachmentValidator : AbstractValidator<IFormFile>
{
    public AttachmentValidator()
    {
        RuleFor(attachment => attachment.Name).Custom((str, context) =>
        {
            if (str.Length == 0)
            {
                context.AddFailure("Filename field should not be empty");
            }
            else if (!Regex.Match(str, @"([A-Za-z0-9\-_.]+)", RegexOptions.IgnoreCase).Success)
            {
                context.AddFailure("Allowed characters for filename are: abc123._-");
            }
            else if (str.Length > 100)
            {
                context.AddFailure("Filename should not exceed maximum of 100 characters");
            }
        });
        
        RuleFor(attachment => attachment).Custom((file, context) =>
        {
            var fileExtension = Path.GetExtension(file.FileName);

            if (!fileExtension.Equals(".pdf", StringComparison.CurrentCultureIgnoreCase))
            {
                context.AddFailure("Only PDF format is allowed");
            }
        });
    }
}