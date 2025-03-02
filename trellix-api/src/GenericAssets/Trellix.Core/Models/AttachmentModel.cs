namespace Trellix.Core.Models;

public class AttachmentModel : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public byte[] Data { get; set; } = [];

    public AttachmentModel() {}
}