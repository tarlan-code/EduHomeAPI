using Microsoft.AspNetCore.Http;

namespace EduHome.Business.DTOs.Courses;
public class CoursePostDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public IFormFile? Image { get; set; }
}
