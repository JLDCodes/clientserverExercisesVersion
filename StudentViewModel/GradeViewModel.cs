using ExercisesDAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;


namespace ExercisesViewModels
{
    public class GradeViewModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Mark { get; set; }
        public string Comments { get; set; }
      

        readonly private GradeDAO _dao;
        public GradeViewModel()
        {
            _dao = new GradeDAO();
        }

        public List<GradeViewModel> GetAll()
        {
            List<GradeViewModel> allVms = new List<GradeViewModel>();
            try
            {
                List<Grades> allGrades = _dao.GetAll();
                foreach (Grades grade in allGrades)
                {
                    GradeViewModel GradeVM = new GradeViewModel
                    {
                        Id = grade.Id,
                        StudentId = grade.StudentId,
                        CourseId = grade.CourseId,
                        CourseName = grade.Course.ToString(),
                        Mark = grade.Mark,
                        Comments = grade.Comments
                    };
                    allVms.Add(GradeVM);
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
