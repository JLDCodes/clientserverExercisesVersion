using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection; // compiler figures out method name using meta data 
using Microsoft.EntityFrameworkCore;
namespace ExercisesDAL
{
    public class CourseDAO
    {
        readonly IRepository<Courses> repository;

        public CourseDAO()
        {
            repository = new SomeSchoolRepository<Courses>();
        }


        public List<Courses> GetAll()
        {

            try
            {
                repository.GetAll();
                return repository.GetAll();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }


        }

    }
}