using AutoMapper;
using EduHome.Business.DTOs.Courses;
using EduHome.Core.Entities;

namespace EduHome.Business.Mapper;
public class CourseMapper:Profile
{
	public CourseMapper()
	{
		CreateMap<Course, CourseDto>().ReverseMap();
		CreateMap<Course, CoursePostDto>().ReverseMap();
		CreateMap<Course, CourseUpdateDto>().ReverseMap();
	}
}
