namespace Trellix.WebApp.Controllers;

using Trellix.Services.Container;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using Trellix.Services.Exceptions;
using Trellix.Validators;

public class AttachmentController
(
    ILogger<AttachmentController> logger,
    IAttachmentService service,
    IResponseHeaderService responseHeaderService
) : Controller
{
    private readonly ILogger<AttachmentController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IAttachmentService _service = service ?? throw new ArgumentNullException(nameof(service));
    private readonly IResponseHeaderService _responseHeaderService = responseHeaderService ?? throw new ArgumentNullException(nameof(responseHeaderService));

    public async Task<IActionResult> ListAttachments()
    {
        _responseHeaderService.SetCspHeader(ViewBag, Response);

        return await Task.FromResult(View());
    }

    [HttpPost("Attachment/CreateAttachment")]
    public async Task<IActionResult> CreateAttachment()
    {
        try
        {
            _responseHeaderService.SetCspHeader(ViewBag, Response);

            var form = await Request.ReadFormAsync();
            var formToDictionary = form.ToDictionary(x => x.Key, x => x.Value);

            var attachmentValidation = new AttachmentValidator();
            var validationResult = attachmentValidation.Validate(Request.Form.Files[0]);

            if (!validationResult.IsValid)
            {
                return Json
                    (
                        new 
                        { 
                            StatusCode = 400, 
                            Message = validationResult.Errors[0].ErrorMessage 
                        }
                    );
            }

            var streamContent = new StreamContent(Request.Form.Files[0].OpenReadStream())
            {
                Headers =
                {
                    ContentLength = Request.Form.Files[0].Length,
                    ContentType = new MediaTypeHeaderValue(Request.Form.Files[0].ContentType)
                }
            };

            var multipartFormDataContent = new MultipartFormDataContent
            {
                { new StringContent(WebUtility.HtmlEncode(formToDictionary["Name"])), "Name" },
                { streamContent, "Data", Request.Form.Files[0].FileName }
            };

            var response = await _service.CreateAttachment(multipartFormDataContent);

            return Json(new { StatusCode = 201, Message = "Successfully added Attachment" });
        }
        catch (ApiException ae)
        {
            _logger.LogError(ae, ae.BadRequestMessage);

            return Json(new {StatusCode = (int)ae.StatusCode, Message = ae.BadRequestMessage});
        }
    }

    [HttpGet("Attachment/DownloadAttachment/{id?}")]
    public async Task<IActionResult> DownloadAttachment(string id)
    {
        try
        {
            _responseHeaderService.SetCspHeader(ViewBag, Response);

            var guidValidation = new GuidValidator();
            var validationResult = guidValidation.Validate(id);

            if (!validationResult.IsValid)
            {
                return Json
                    (
                        new
                        {
                            StatusCode = 400,
                            Message = validationResult.Errors[0].ErrorMessage
                        }
                    );
            }

            var response = await _service.DownloadAttachment(id);

            return response.Length == 0 ? Ok() : File(response, "application/pdf", id);
        }
        catch (ApiException ae)
        {
            _logger.LogError(ae, ae.BadRequestMessage);

            return Json(new {StatusCode = (int)ae.StatusCode, Message = ae.BadRequestMessage});
        }
    }

    [HttpGet("Attachment/GetAttachments")]
    public async Task<IActionResult> GetAttachments()
    {
        try
        {
            _responseHeaderService.SetCspHeader(ViewBag, Response);

            var response = await _service.GetAttachments();

            return Json(response);
        }
        catch (ApiException ae)
        {
            _logger.LogError(ae, ae.BadRequestMessage);

            return Json(new {StatusCode = (int)ae.StatusCode, Message = ae.BadRequestMessage});
        }
    }
}
