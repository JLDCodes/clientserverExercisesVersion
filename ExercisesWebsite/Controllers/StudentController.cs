using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ExercisesViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExercisesWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet("{LastName}")]

        public IActionResult GetByLastName(string LastName)
        {
            try
            {
                StudentViewModel viewmodel = new StudentViewModel();
                viewmodel.LastName = LastName;
                viewmodel.GetByLastName();
                return Ok(viewmodel);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    try
        //    {
        //        StudentViewModel vm = new StudentViewModel();
        //        var allStus = vm.GetAll();
        //        return Ok(allStus);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);//something went wrong 
        //    }
        //}

        [HttpPut]
        public IActionResult Put([FromBody] StudentViewModel viewModel)
        {
            try
            {
                int retVal = viewModel.Update();
                switch (retVal)
                {
                    case 1:
                        return Ok(new { msg = "Student " + viewModel.LastName + " updated!" });
                    case -2:
                        return Ok(new { msg = "Student " + viewModel.LastName + " not updated, stale dataa" });
                    default:
                        return Ok(new { msg = "Student " + viewModel.LastName + " not updated!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);//something went wrong 
            }
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                StudentViewModel viewModel = new StudentViewModel();
                List<StudentViewModel> allStudents = viewModel.GetAll();
                return Ok(allStudents);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);//something went wrong
            }
        }

    }
}
