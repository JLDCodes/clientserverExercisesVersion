using Xunit;
using ExercisesViewModels;
using System.Collections.Generic;

namespace ExerciseTests
{
    public class ViewModelTests
    {
        [Fact]
        public void Student_GetByLastNameTest()
        {
            StudentViewModel vm = new StudentViewModel { LastName = "Pet " };
            vm.GetByLastName();
            Assert.NotNull(vm.FirstName);
        }

        [Fact]
        public void Student_GetById()
        {
            StudentViewModel vm = new StudentViewModel { LastName = "Pet" };
            vm.GetByLastName();
            vm.GetById();
            Assert.NotNull(vm.FirstName);
        }

        [Fact]
        public void Student_GetAllTest()
        {
            StudentViewModel vm = new StudentViewModel();
            List<StudentViewModel> allStudents = vm.GetAll();
            Assert.True(allStudents.Count > 0);
        }

        [Fact]
        public void Student_AddTest()
        {
            StudentViewModel vm = new StudentViewModel
            {
                FirstName = "Jean-Luc",
                LastName = "Desjardins",
                PhoneNo = "(555)555-1234",
                Title = "Mr.",
                DivisionId = 10,
                Email = "jld@somemail.com"
            };
            vm.Add();
            Assert.True(vm.id > 0);
        }

        [Fact]
        public void Student_UpdateTest()
        {
            StudentViewModel vm = new StudentViewModel { LastName = "Desjardins" };
            vm.GetByLastName(); // student just added
            vm.PhoneNo = vm.PhoneNo == "(555)555-5551" ? "(555)555-5552" : "(555)555-5551";
            int studentsUpdated = vm.Update();
            Assert.True(studentsUpdated > 0);
        }

        [Fact]
        public void Student_DeleteTest()
        {
            StudentViewModel vm = new StudentViewModel { LastName = "Desjardins" };
            vm.GetByLastName();
            int studentsDeleted = vm.Delete();
            Assert.True(studentsDeleted == 1);
        }
  
    }
}
