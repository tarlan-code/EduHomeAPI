﻿namespace EduHome.Business.DTOs.Courses;
public class CourseUpdateDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
}
