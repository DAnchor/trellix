namespace Trellix.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Trellix.DataAccess.Exceptions;
using Trellix.Dtos.Payload.Attachment;
using Trellix.Dtos.Request.Attachment;
using Trellix.Services.Container;
using Trellix.Services.Exceptions;
using Trellix.Validators;

[Route("api/[controller]")]
public class AttachmentController
(
    IAttachmentService attachmentService,
    ILogger<AttachmentController> logger
) : ControllerBase
{
    private readonly IAttachmentService _attachmentService = attachmentService;
    private readonly ILogger<AttachmentController> _logger = logger;

    [HttpPost("CreateAttachment")]
    public async Task<IActionResult> CreateAttachment([FromForm] CreateAttachmentDto request)
    {
        try
        {
            var attachmentValidation = new AttachmentValidator();
            var validationResult = attachmentValidation.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.First());
            }

            var response = await _attachmentService.CreateAttachment(request);

            return new JsonResult(response);
        }
        catch (ArgumentNullException ane)
        {
            _logger.LogError(ane, ane.Message);
            return BadRequest(ane.Message);
        }
        catch (ServiceException se)
        {
            _logger.LogError(se, se.Message);
            return BadRequest(se.Message);
        }
        catch (DataAccessException dae)
        {
            _logger.LogError(dae, dae.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError, dae.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    [HttpGet("DownloadAttachment")]
    public async Task<IActionResult> DownloadAttachment(string id)
    {
        try
        {
            var guidValidation = new GuidValidator();
            var validationResult = guidValidation.Validate(id);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors[0]);
            }

            var response = await _attachmentService.DownloadAttachment(new DownloadAttachmentRequest
            (
                Id: id
            ));

            return File(response.AttachmentData.ToArray(), "application/pdf", $"{response.AttachmentName}");
        }
        catch (ArgumentNullException ane)
        {
            _logger.LogError(ane, ane.Message);
            return BadRequest(ane.Message);
        }
        catch (ServiceException se)
        {
            _logger.LogError(se, se.Message);
            return BadRequest(se.Message);
        }
        catch (DataAccessException dae)
        {
            _logger.LogError(dae, dae.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError, dae.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    [HttpGet("GetAttachments")]
    public async Task<IActionResult> GetAttachments()
    {
        try
        {
            var response = await _attachmentService.GetAttachments();

            return new JsonResult(response);
        }
        catch (ArgumentNullException ane)
        {
            _logger.LogError(ane, ane.Message);
            return BadRequest(ane.Message);
        }
        catch (ServiceException se)
        {
            _logger.LogError(se, se.Message);
            return BadRequest(se.Message);
        }
        catch (DataAccessException dae)
        {
            _logger.LogError(dae, dae.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError, dae.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }
}

