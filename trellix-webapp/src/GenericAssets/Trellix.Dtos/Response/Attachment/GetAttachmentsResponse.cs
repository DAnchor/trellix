namespace Trellix.Dtos.Response.Attachment;

using Trellix.Dtos.Payload.Attachment;
using System.Text.Json.Serialization;

public record class GetAttachmentsResponse
(
    [property: JsonPropertyName("attachments")]
    IEnumerable<AttachmentBasicDto> Attachments,
    [property: JsonPropertyName("success")]
    bool Success
);