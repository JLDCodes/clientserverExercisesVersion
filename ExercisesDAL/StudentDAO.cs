// I commented out my old code (you don't have to include that in the new StudentDAO, I just wanted to keep it)



using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection; // compiler figures out method name using meta data 
using System.Collections.Generic; // for list 
using Microsoft.EntityFrameworkCore;
using System.Buffers;

namespace ExercisesDAL
{
    public class StudentDAO
    {
        readonly IRepository<Students> repository;

        public StudentDAO()
        {
            repository = new SomeSchoolRepository<Students>();
        }


        public Students GetByLastName(string name)
        {
      

            try
            {
                return repository.GetByExpression(stu => stu.LastName == name).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            /*
            try
            {
                SomeSchoolContext _db = new SomeSchoolContext();
                selectedStudent = _db.Students.FirstOrDefault(Students => Students.LastName == name);
            }
            catch (Exception ex)
            {
                // instead of hardcoding the actual method name, 
                // letting compiler figure it out via the System.Reflection library
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return selectedStudent;
            */
        }

        // searching on a passed Id which is an int 
        public Students GetById(int id)
        {
            Students selectedStudent = null;

            try
            {
                return repository.GetByExpression(stu => stu.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            /*
            try
            {
                SomeSchoolContext _db = new SomeSchoolContext();
                selectedStudent = _db.Students.FirstOrDefault(stu => stu.Id == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return selectedStudent;
            */
        }

        // retrieving all the students info by returning a List
        // containing a series of Students instances
        public List<Students> GetAll()
        {

            try
            {
                return repository.GetAll();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            /*
            List<Students> allStudents = new List<Students>();

            try
            {
                SomeSchoolContext _db = new SomeSchoolContext();
                allStudents = _db.Students.ToList(); 
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return allStudents;
            */
        }

        // add a student to the database and return their newly generated student Id
        public int Add(Students newStudent)
        {

            try
            {
                newStudent = repository.Add(newStudent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return newStudent.Id;

            /*
            try
            {
                SomeSchoolContext _db = new SomeSchoolContext();
                _db.Students.Add(newStudent);
                _db.SaveChanges(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return newStudent.Id;
            */
        }


        // update function that returns the rows updated if successful, or -1 if unsuccessful
        public UpdateStatus Update(Students updatedStudent)
        {
            UpdateStatus operationStatus = UpdateStatus.Failed;

            try
            {
                operationStatus = repository.Update(updatedStudent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                  MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return operationStatus;
        }

        // delete function that returns # of students deleted or -1
        public int Delete(int id)
        {
            int studentsDeleted = -1;

            try
            {
                studentsDeleted = repository.Delete(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return studentsDeleted;

            /*
            int studentsDeleted = -1;

            try
            {
                SomeSchoolContext _db = new SomeSchoolContext();
                Students selectedStudent = _db.Students.FirstOrDefault(stu => stu.Id == id);
                _db.Students.Remove(selectedStudent);
                studentsDeleted = _db.SaveChanges(); // returns # of rows removed
                SomeSchoolContext _db2 = new SomeSchoolContext();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
             return studentsDeleted; 
            */
        }
    }
}
