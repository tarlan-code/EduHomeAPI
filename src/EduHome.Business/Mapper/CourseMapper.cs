using AutoMapper;
using EduHome.Business.DTOs.Courses;
using EduHome.Core.Entities;

namespace EduHome.Business.Mapper;
public class CourseMapper:Profile
{
	public CourseMapper()
	{
		CreateMap<Course, CourseDto>().ReverseMap();
		CreateMap<Course, CoursePostDto>().ReverseMap()
			.ForMember(c=>c.Image,c=>c.Ignore());
		CreateMap<Course, CourseUpdateDto>().ReverseMap();
	}
}
