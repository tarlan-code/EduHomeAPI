using EduHome.Business.DTOs.Courses;
using EduHome.Core.Entities;
using System.Linq.Expressions;

namespace EduHome.Business.Interfaces;
public interface ICourseService
{
    Task<IEnumerable<CourseDto>> GetAllCourseAsync();

    Task<IEnumerable<CourseDto>> FindByConditionAsync(Expression<Func<Course, bool>> expression);
    Task<CourseDto?> FindByIdAsync(Guid id);
    Task CreateAsync(CoursePostDto course);
    Task UpdateAsync(Guid? id,CourseUpdateDto course);
    Task DeleteAsync(Guid id);

}
