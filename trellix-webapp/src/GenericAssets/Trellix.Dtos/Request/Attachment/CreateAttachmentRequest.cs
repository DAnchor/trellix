namespace Trellix.Dtos.Request.Attachment;

using System.Text.Json.Serialization;
using Trellix.Dtos.Payload.Attachment;

public record class CreateAttachmentRequest
(
    [property: JsonPropertyName("createAttachment")]
    CreateAttachmentDto CreateAttachment
);