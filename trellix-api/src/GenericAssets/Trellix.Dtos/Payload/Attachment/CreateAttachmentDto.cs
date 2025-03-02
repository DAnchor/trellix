namespace Trellix.Dtos.Payload.Attachment;

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

public record class CreateAttachmentDto
(
    [property: JsonPropertyName("name")]
    string Name,
    [property: JsonPropertyName("data")]
    IFormFile Data
);