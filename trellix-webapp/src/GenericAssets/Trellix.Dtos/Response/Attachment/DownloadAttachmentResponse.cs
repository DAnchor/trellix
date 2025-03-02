namespace Trellix.Dtos.Response.Attachment;

using System.Text.Json.Serialization;

public record class DownloadAttachmentResponse
(
    [property: JsonPropertyName("attachmentName")]
    string AttachmentName,
    [property: JsonPropertyName("attachmentData")]
    MemoryStream AttachmentData,
    [property: JsonPropertyName("success")]
    bool Success
);