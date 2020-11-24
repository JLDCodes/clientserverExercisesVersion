using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using ExercisesDAL;
using ExercisesViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExercisesWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                DivisionViewModel viewModel = new DivisionViewModel();
                List<DivisionViewModel> allStudents = viewModel.GetAll();
                return Ok(allStudents);
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
