using EduHome.Core.Entities.Common;

namespace EduHome.Core.Entities;
public class Course:BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
}
