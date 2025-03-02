namespace Trellix.Dtos.Response.Attachment;

using System.Text.Json.Serialization;

public record class DeleteAttachmentResponse
(
    [property: JsonPropertyName("success")]
    bool Success
);