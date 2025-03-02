namespace Trellix.Services.Container;

using Trellix.Dtos.Response.Attachment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public interface IAttachmentService
{
    Task<CreateAttachmentResponse> CreateAttachment(HttpContent httpContent);
    Task<MemoryStream> DownloadAttachment(string id);
    Task<GetAttachmentsResponse> GetAttachments();
}

public class AttachmentService
(
    IHttpClientFactory httpClientFactory,
    IConfiguration configuration,
    ILogger<AttachmentService> logger
) : BaseService(httpClientFactory, configuration, logger), IAttachmentService
{
    public async Task<CreateAttachmentResponse> CreateAttachment(HttpContent httpContent)
    {
        return await PostFromForm<CreateAttachmentResponse>
        (
            Api
                .Routes
                .Attachment
                .CreateAttachment, 
                httpContent
        );
    }

    public async Task<MemoryStream> DownloadAttachment(string id)
    {
        return await DownloadRequest<MemoryStream>
        (
            Api
                .Routes
                .Attachment
                .DownloadAttachment
                .Replace("{id}", id)
        );
    }

    public async Task<GetAttachmentsResponse> GetAttachments()
    {
        return await GetRequest<GetAttachmentsResponse>
        (
            Api
                .Routes
                .Attachment
                .GetAttachments
        );
    }
}
