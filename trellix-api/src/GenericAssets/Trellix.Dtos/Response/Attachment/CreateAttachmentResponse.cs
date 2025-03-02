namespace Trellix.Dtos.Response.Attachment;

using System.Text.Json.Serialization;
using Trellix.Dtos.Payload.Attachment;

public record class CreateAttachmentResponse
(
    [property: JsonPropertyName("attachment")]
    AttachmentDto Attachment,
    [property: JsonPropertyName("success")]
    bool Success
);