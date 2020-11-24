using ExercisesDAL;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace ExercisesViewModels
{
    public class StudentViewModel
    {

        readonly private StudentDAO _dao;

        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Timer { get; set; }
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public int id { get; set; }
        public string Picture64 { get; set; }
        public StudentViewModel()
        {
            _dao = new StudentDAO();
        }

        public void GetByLastName()
        {
            try
            {
                Students stu = _dao.GetByLastName(LastName);
                Title = stu.Title;
                FirstName = stu.FirstName;
                LastName = stu.LastName;
                PhoneNo = stu.PhoneNo;
                Email = stu.Email;
                id = stu.Id;
                DivisionId = stu.DivisionId;

                if (stu.Picture != null)
                {
                    Picture64 = Convert.ToBase64String(stu.Picture);
                }
                if (Picture64 != null)
                {
                    stu.Picture = Convert.FromBase64String(Picture64);
                }
                Timer = Convert.ToBase64String(stu.Timer);
            }
            catch (NullReferenceException nex)
            {
                Debug.WriteLine(nex.Message);
                LastName = "not found";
            }
            catch (Exception ex)
            {
                LastName = "not found";
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        public void GetById()
        {
            try
            {
                Students stu = _dao.GetById(id);
                Title = stu.Title;
                FirstName = stu.FirstName;
                LastName = stu.LastName;
                PhoneNo = stu.PhoneNo;
                Email = stu.Email;
                id = stu.Id;
                DivisionId = stu.DivisionId;

                if (stu.Picture != null)
                {
                    Picture64 = Convert.ToBase64String(stu.Picture);
                }
                Timer = Convert.ToBase64String(stu.Timer);
            }
            catch (NullReferenceException nex)
            {
                Debug.WriteLine(nex.Message);
                LastName = "not found";
            }
            catch (Exception ex)
            {
                LastName = "not found";
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        public List<StudentViewModel> GetAll()
        {
            List<StudentViewModel> allVms = new List<StudentViewModel>();
            try
            {
                List<Students> allStudents = _dao.GetAll();
                foreach (Students stu in allStudents)
                {
                    StudentViewModel stuVm = new StudentViewModel();
                    stuVm.Title = stu.Title;
                    stuVm.FirstName = stu.FirstName;
                    stuVm.LastName = stu.LastName;
                    stuVm.PhoneNo = stu.PhoneNo;
                    stuVm.Email = stu.Email;
                    stuVm.id = stu.Id;
                    stuVm.DivisionId = stu.DivisionId;
                    stuVm.DivisionName = stu.Division.Name;
                    stuVm.Timer = Convert.ToBase64String(stu.Timer);
                    allVms.Add(stuVm);
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

        public void Add()
        {
            id = -1;
            try
            {
                Students stu = new Students
                {
                    Title = Title,
                    FirstName = FirstName,
                    LastName = LastName,
                    PhoneNo = PhoneNo,
                    Email = Email,
                    DivisionId = DivisionId
                };
                id = _dao.Add(stu);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        public int Update()
        {
            UpdateStatus studentsUpdated = UpdateStatus.Failed;
            try
            {
                Students stu = new Students();

                stu.Title = Title;
                stu.FirstName = FirstName;
                stu.LastName = LastName;
                stu.PhoneNo = PhoneNo;
                stu.Email = Email;
                stu.Id = id;
                stu.DivisionId = DivisionId;
                

                if (Picture64 != null)
                {
                    stu.Picture = Convert.FromBase64String(Picture64);
                }

                stu.Timer = Convert.FromBase64String(Timer);
                studentsUpdated = _dao.Update(stu);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                   MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return Convert.ToInt16(studentsUpdated);
        }

        public int Delete()
        {
            int studentsDeleted = -1;

            try
            {
                studentsDeleted = _dao.Delete(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                  MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return studentsDeleted;
        }

    }
}
