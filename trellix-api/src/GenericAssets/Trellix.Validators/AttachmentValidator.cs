namespace Trellix.Validators;

using FluentValidation;
using System.Text.RegularExpressions;
using Trellix.Dtos.Payload.Attachment;

public class AttachmentValidator : AbstractValidator<CreateAttachmentDto>
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
                context.AddFailure("Allowed characters for filename are: abc123._-abc123");
            }
            else if (str.Length > 100)
            {
                context.AddFailure("Filename should not exceed maximum of 100 characters");
            }
        });
        
        RuleFor(attachment => attachment.Data).Custom((file, context) =>
        {
            var fileExtension = Path.GetExtension(file.FileName);

            if (!fileExtension.Equals(".pdf", StringComparison.CurrentCultureIgnoreCase))
            {
                context.AddFailure("Only PDF format is allowed");
            }
        });
    }
}