namespace Trellix.Dtos.Request.Attachment;

using System.Text.Json.Serialization;

public record class DownloadAttachmentRequest
(
    [property: JsonPropertyName("attachmentId")]
    string AttachmentId
);