namespace EduHome.Core.Entities.Common;
public abstract class BaseEntity:IBaseEntity
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; } = false;
}
