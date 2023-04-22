using AutoMapper;
using EduHome.Business.DTOs.Courses;
using EduHome.Business.Exceptions;
using EduHome.Business.Interfaces;
using EduHome.Core.Entities;
using EduHome.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EduHome.Business.Implementations;
public class CourseService : ICourseService
{
    readonly ICourseRepository _courseRepository;
    readonly IMapper _mapper;

    public CourseService(ICourseRepository courseRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CourseDto>> GetAllCourseAsync()
    {
        var courses = await _courseRepository.GetAll().ToListAsync();
        var result = _mapper.Map<IEnumerable<CourseDto>>(courses);
        return result;
    }

    public async Task<CourseDto?> FindByIdAsync(Guid id)
    {
        var course = await _courseRepository.FindByIdAsync(id);
        if (course is null) throw new NotFoundException("Not Found");
        return _mapper.Map<CourseDto>(course);
    }

    public async Task<IEnumerable<CourseDto>> FindByConditionAsync(Expression<Func<Course, bool>> expression)
    {
        var courses = await _courseRepository.FindByCondition(expression).ToListAsync();
        if(courses is null)
        {
            throw new NotFoundException("Not Found Courses");
        }

        return _mapper.Map<List<CourseDto>>(courses);
    }

    public async Task CreateAsync(CoursePostDto course)
    {
        if(course is null) throw new ArgumentNullException(nameof(course));
        var newCourse = _mapper.Map<Course>(course);
        await _courseRepository.CreateAsync(newCourse);
        await _courseRepository.SaveAsync();
    }
    public async Task UpdateAsync(Guid? id, CourseUpdateDto course)
    {
        if (id is null || id != course.Id) throw new BadRequestException("Please enter valid Id");

        var baseCourse = await _courseRepository.FindByIdAsync((Guid)id);

        if (baseCourse is null) throw new NotFoundException("Not found");

        
        _mapper.Map(course, baseCourse);
        _courseRepository.Update(baseCourse);
        await _courseRepository.SaveAsync();
        
    }

    public async Task DeleteAsync(Guid id)
    {
        Course? course = await _courseRepository.FindByIdAsync(id);
        if (course is null) throw new NotFoundException("Course not found!");
        _courseRepository.Delete(course);
        await _courseRepository.SaveAsync();
    }

}
