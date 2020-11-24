using Xunit;
using ExercisesDAL;
using System.Collections.Generic;
using NuGet.Frameworks;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Diagnostics;
using System.ComponentModel;
using System;
using ExercisesViewModels;

namespace ExerciseTests
{
    public class DAOTests
    {
        [Fact]
        public void Student_GetByLastNameTest()
        {
            StudentDAO dao = new StudentDAO();
            Students selectedStudent = dao.GetByLastName("Pet");
            Assert.NotNull(selectedStudent);
        }
        [Fact]
        public void Student_GetById()
        {
            StudentDAO dao = new StudentDAO();
            Students selectedStudent = dao.GetById(2);
            Assert.NotNull(selectedStudent);
        }
        [Fact]
        public void Student_GetAll()
        {
            StudentDAO dao = new StudentDAO();
            List<Students> allStudents = dao.GetAll();
            Assert.NotNull(allStudents);
        }
        [Fact]
        public void Student_AddTest()
        {
            StudentDAO dao = new StudentDAO();
            Students newStudent = new Students
            {
                FirstName = "Jean-Luc",
                LastName = "Desjardins",
                PhoneNo = "(555)555-1234",
                Title = "Mr.",
                DivisionId = 10,
                Email = "js@someemail.ca"
            };
            dao.Add(newStudent);
            Assert.NotNull(newStudent);
        }
        [Fact]
        public void Student_UpdateTest()
        {
            StudentDAO dao = new StudentDAO();
            Students studentForUpdate = dao.GetByLastName("Smith");

            if (studentForUpdate != null)
            {
                string oldPhoneNo = studentForUpdate.PhoneNo;
                string newPhoneNo = oldPhoneNo == "519-555-1234" ? "555-555-555" : "519-555-1234";
                studentForUpdate.PhoneNo = newPhoneNo;
            }

            Assert.True(dao.Update(studentForUpdate) == UpdateStatus.Ok);
        }

        [Fact]
        public void Student_DeleteTest()
        {
           StudentDAO dao = new StudentDAO();
           int studentsDeleted = dao.Delete(dao.GetByLastName("Desjardins").Id);
           Assert.True(studentsDeleted != -1);
        }
        [Fact]
        public void Student_ConcurrencyTest()
        {
            StudentViewModel vm1 = new StudentViewModel();
            StudentViewModel vm2 = new StudentViewModel();
            vm1.LastName = "Desjardins";
            vm2.LastName = "Desjardins";
            vm1.GetByLastName();
            vm2.GetByLastName();
            vm1.Email = (vm1.Email.IndexOf(".ca") > 0) ? " jld@abc.com" : "jld@abc.ca";


            if (vm1.Update() == 1)
            {
                vm2.Email = "something@different.com";
                Assert.True(vm2.Update() == -2);
            }
            else
            {
                Assert.True(false);
            }
            
        }
        [Fact]
        public void Student_LoadPicsTest()
        {
            DALUtil util = new DALUtil();
            Assert.True(util.AddStudentPicsToDb());
        }
    }
}
