using ExercisesDAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace ExercisesViewModels
{
    public class CourseViewModel
    {
        public int courseId { get; set; }
        public string Name { get; set; }
        public int DivisionId { get; set; }


        readonly private CourseDAO _dao;
        public CourseViewModel()
        {
            _dao = new CourseDAO();
        }

        public List<CourseViewModel> GetAll()
        {
            List<CourseViewModel> allVms = new List<CourseViewModel>();
            try
            {
                List<Courses> allCourses = _dao.GetAll();
                foreach (Courses course in allCourses)
                {
                    CourseViewModel divVm = new CourseViewModel
                    {
                        courseId = course.Id,
                        Name = course.Name,
                        DivisionId =  course.DivisionId
                    };
                    allVms.Add(divVm);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allVms;
        }


    }
}
