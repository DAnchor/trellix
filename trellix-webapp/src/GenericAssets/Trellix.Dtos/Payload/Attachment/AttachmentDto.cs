namespace Trellix.Dtos.Payload.Attachment;

using System.Text.Json.Serialization;

public record class AttachmentDto
(
    [property: JsonPropertyName("name")]
    string Name,
    [property: JsonPropertyName("data")]
    byte[] Data
);