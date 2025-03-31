using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatikaLMSCoreProject.Business.Operations.Course;
using PatikaLMSCoreProject.Business.Operations.Course.Dtos;
using PatikaLMSCoreProject.WebApi.Filters;
using PatikaLMSCoreProject.WebApi.Models;

namespace PatikaLMSCoreProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCourse(AddCourseRequest request)
        {
            var addCourseDto = new AddCourseDto
            {
                Name = request.Name,
                Stars = request.Stars,
                EducationType = request.EducationType,
                FeatureIds = request.FeatureIds
            };

            var result = await _courseService.AddCourse(addCourseDto);

            if (!result.IsSucceed)
                return BadRequest(result.Message);
            else
                return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _courseService.GetCourse(id);

            if (course is null)
                return NotFound();
            else
                return Ok(course);
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _courseService.GetCourses();

            return Ok(courses);
        }

        [HttpPatch("{id}/stars")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdjustCourseStars(int id, int changeTo)
        {
            var result = await _courseService.AdjustCourseStars(id, changeTo);

            if (!result.IsSucceed)
                return NotFound(result.Message);
            else
                return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var result = await _courseService.DeleteCourse(id);

            if (!result.IsSucceed)
                return NotFound(result.Message);
            else
                return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [TimeControlFilter]
        public async Task<IActionResult> UpdateCourse(int id, UpdateCourseRequest request)
        {
            var updateCourseDto = new UpdateCourseDto
            {
                Id = id,
                Name = request.Name,
                Stars = request.Stars,
                EducationType = request.EducationType,
                FeatureIds = request.FeatureIds
            };

            var result = await _courseService.UpdateCourse(updateCourseDto);

            if (!result.IsSucceed)
                return NotFound(result.Message);
            else
                return await GetCourse(id);
        }
    }
}