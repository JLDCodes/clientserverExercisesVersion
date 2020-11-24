using ExercisesDAL;
using System.Linq;
using System;
using Xunit;

namespace ExerciseTests
{
    public class LinqTests
    {
        [Fact]
        public void Test1()
        {
            SomeSchoolContext _db = new SomeSchoolContext();
            var selectedStudents = from stu in _db.Students
                                   where stu.Id == 2
                                   select stu;
            Assert.True(selectedStudents.Count() > 0);
        }
        [Fact]
        public void Test2()
        {
            SomeSchoolContext _db = new SomeSchoolContext();
            var selectedStudents = from stu in _db.Students
                                   where stu.Title == "Ms." || stu.Title == "Mrs"
                                   select stu;

            Assert.True(selectedStudents.Count() > 0);
        }
        [Fact]
        public void Test3()
        {
            SomeSchoolContext _db = new SomeSchoolContext();
            var selectedStudents = _db.Students.Where(stu => stu.Division.Name == "Design");

            Assert.True(selectedStudents.Count() > 0);
        }
        [Fact]
        public void Test4()
        {
            SomeSchoolContext _db = new SomeSchoolContext();
            var selectedStudents = _db.Students.FirstOrDefault(Students => Students.Id == 2);
            Assert.True(selectedStudents.FirstName == "Teachers");
        }
        [Fact]
        public void Test5()
        {
            SomeSchoolContext _db = new SomeSchoolContext();
            var selectedStudents = _db.Students.Where(stu => stu.Title == "Ms." || stu.Title == "Mrs.");
            Assert.True(selectedStudents.Count() > 0);
        }
        [Fact]
        public void Test6()
        {
            SomeSchoolContext _db = new SomeSchoolContext();
            var selectedStudents = _db.Students.Where(stu => stu.Division.Name == "Design");
            Assert.True(selectedStudents.Count() > 0);
        }
        [Fact]
        public void Test7()
        {
            SomeSchoolContext _db = new SomeSchoolContext();
            Students selectedStudent = _db.Students.FirstOrDefault(stu => stu.Id == 1005);
            if (selectedStudent != null)
            {
                string oldEmail = selectedStudent.Email;
                string newEmail = oldEmail == "jsd@someschool.com" ? "jld@someschool.com" : "JLD@someschool.com";
                selectedStudent.Email = newEmail;
                _db.Entry(selectedStudent).CurrentValues.SetValues(selectedStudent);
            }
            Assert.True(_db.SaveChanges() == 1);
        }
        [Fact]
        public void Test8()
        {
            SomeSchoolContext _db = new SomeSchoolContext();
            Students newStudent = new Students
            {
                FirstName = "Jean-Luc",
                LastName = "Desjardins",
                PhoneNo = "(555)555-1234",
                Title = "Mr.",
                DivisionId = 10,
                Email = "jsd@someschool.com"
            };
            _db.Students.Add(newStudent);
            _db.SaveChanges();
            Assert.True(newStudent.Id > 1);
        }
        [Fact]
        public void Test9()
        {
            SomeSchoolContext _db = new SomeSchoolContext();
            Students selectedstudent = _db.Students.FirstOrDefault(stu => stu.FirstName == "Jean-Luc");
            if (selectedstudent != null)
            {
                _db.Students.Remove(selectedstudent);
                Assert.True(_db.SaveChanges() == 1);
            }
            else
            {
                Assert.True(true);
            }
        }
    }
}
