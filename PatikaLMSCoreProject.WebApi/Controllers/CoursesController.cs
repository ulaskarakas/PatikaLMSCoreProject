﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatikaLMSCoreProject.Business.Operations.Course;
using PatikaLMSCoreProject.Business.Operations.Course.Dtos;
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
    }
}