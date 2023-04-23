using EduHome.Business.DTOs.Courses;
using EduHome.Business.Exceptions;
using EduHome.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EduHome.API.Controllers;
[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class CoursesController : ControllerBase
{
    readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var courses = await _courseService.GetAllCourseAsync();
            return Ok(courses);
            //return StatusCode((int)HttpStatusCode.OK, courses); 
        }
        catch (NotFoundException ex)
        {

            return NotFound(ex.Message);
        }

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            return Ok(await _courseService.FindByIdAsync(id));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (FormatException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {

            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpGet("getByName/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        try
        {
            IEnumerable<CourseDto> courses = await _courseService.FindByConditionAsync(c => c.Name != null ? c.Name.Contains(name) : true);
            return Ok(courses);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (FormatException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {

            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromForm]CoursePostDto course)
    {
        try
        {
            await _courseService.CreateAsync(course);
            return StatusCode((int)HttpStatusCode.Created);

        }
        catch (Exception ex)
        {

            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id,CourseUpdateDto course)
    {
        try
        {

        await _courseService.UpdateAsync(id, course);
        return StatusCode((int)HttpStatusCode.NoContent);

        }
        catch (NotFoundException ex)
        {

            return NotFound(ex.Message);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpDelete("delete/{id}")]

    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _courseService.DeleteAsync(id);
            return StatusCode((int)HttpStatusCode.NoContent);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (FormatException ex)
        {
            return BadRequest(ex.Message); 
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
       
    }



}
