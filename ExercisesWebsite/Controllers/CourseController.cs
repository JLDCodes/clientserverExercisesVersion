using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using ExercisesDAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExercisesViewModels;

namespace ExercisesWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetAll(int id)
        {
            try
            {
                CourseViewModel viewModel = new CourseViewModel();
                List<CourseViewModel> allCourses = viewModel.GetAll();
                return Ok(allCourses);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
            }
        }
    }
}