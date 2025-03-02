namespace Trellix.Core.Models;

public class BaseModel
{
    public virtual Guid Id { get; set; }
    public virtual bool IsActive { get; set; }
    public virtual DateTime CreatedOn { get; set; }
    public virtual DateTime ModifiedOn { get; set; }
}