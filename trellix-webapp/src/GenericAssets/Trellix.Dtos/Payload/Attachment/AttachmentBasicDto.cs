namespace Trellix.Dtos.Payload.Attachment;

using System.Text.Json.Serialization;

public record class AttachmentBasicDto
(
    [property: JsonPropertyName("id")]
    string Id,
    [property: JsonPropertyName("name")]
    string Name,
    [property: JsonPropertyName("createdOn")]
    DateTime CreatedOn,
    [property: JsonPropertyName("modifiedOn")]
    DateTime ModifiedOn
);