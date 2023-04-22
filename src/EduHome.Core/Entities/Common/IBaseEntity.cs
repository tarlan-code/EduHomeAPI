namespace EduHome.Core.Entities.Common;
public interface IBaseEntity
{
     Guid Id { get; set; }
    bool IsDeleted { get; set; }
}
