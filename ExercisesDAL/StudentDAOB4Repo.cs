//using System;
//using System.Diagnostics;
//using System.Linq;
//using System.Reflection;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;

//namespace ExercisesDAL
//{
//    public class StudentDAO
//    {
//        public Students GetByLastName(string name)
//        {
//            Students selectedStudent = null;

//            try
//            {
//                SomeSchoolContext _db = new SomeSchoolContext();
//                selectedStudent = _db.Students.FirstOrDefault(stu => stu.LastName == name);
//            }
//            catch(Exception ex)
//            {
//                Debug.WriteLine("Problem in " + GetType().Name + " " +
//                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
//                throw ex; 
//            }

//            return selectedStudent; 
//        }

//        public Students GetById(int id)
//        {
//            Students selectedStudent = null;
//            try
//            {
//                SomeSchoolContext _db = new SomeSchoolContext();
//                selectedStudent = _db.Students.FirstOrDefault(stu => stu.Id == id);
//            }
//            catch(Exception ex)
//            {
//                Debug.WriteLine("Problem in " + GetType().Name + " " +
//                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
//                throw ex;
//            }
//            return selectedStudent;
//        }
//        public List<Students> GetAll()
//        {
//            List<Students> allStudents = new List<Students>();
//            try
//            {
//                SomeSchoolContext _db = new SomeSchoolContext();
//                allStudents = _db.Students.ToList(); 

//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine("Problem in " + GetType().Name + " " +
//                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
//                throw ex;
//            }
//            return allStudents;
//        }
//        public int Add(Students newStudent)
//        {
//            try
//            {
//                SomeSchoolContext _db = new SomeSchoolContext();
//                _db.Students.Add(newStudent);
//                _db.SaveChanges();
//            }
//            catch(Exception ex)
//            {
//                Debug.WriteLine("Problem in " + GetType().Name + " " +
//                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
//                throw ex;
//            }

//            return newStudent.Id; 
//        }

//        public UpdateStatus Update(Students updatedStudent)
//        {
//            UpdateStatus operationStatus = UpdateStatus.Failed;

//            try
//            {
//                SomeSchoolContext _db = new SomeSchoolContext();
//                Students currentStudent = _db.Students.FirstOrDefault(stu => stu.Id == updatedStudent.Id);
//                _db.Entry(currentStudent).OriginalValues["Timer"] = updatedStudent.Timer;
//                _db.Entry(currentStudent).CurrentValues.SetValues(updatedStudent);
//                if (_db.SaveChanges() == 1)
//                {
//                    operationStatus = UpdateStatus.Ok;
//                }
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                operationStatus = UpdateStatus.Stale;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Problem in " + GetType().Name + " " +
//                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
//                throw ex;
//            }
//            return operationStatus;
//        }
//        public int Delete(int id)
//        {
//            int studentsDeleted = -1; 
//            try
//            {
//                SomeSchoolContext _db = new SomeSchoolContext();
//                Students selectedStudent = _db.Students.FirstOrDefault(stu => stu.Id == id);
//                _db.Students.Remove(selectedStudent);
//                studentsDeleted = _db.SaveChanges(); // returns # of rows removed
//            }
//            catch(Exception ex)
//            {
//                Debug.WriteLine("Problem in " + GetType().Name + " " +
//                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
//                throw ex;
//            }
//            return studentsDeleted;
//        }
//    }
//}
