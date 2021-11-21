using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using DataAccessDemo.Data;
using DataAccessDemo.Data.Dto;
using Xunit;
using Xunit.Abstractions;

namespace DataAccessDemo.Tests
{
    public class BasicIntegrationTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public BasicIntegrationTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void CreateDeleteAndVerifyTheCompanyRepo()
        {
            try
            {
                //create an instance of the repo
                var repo = new CompanyRepo();

                Guid theId = Guid.NewGuid();

                //create a company to save
                var company = new Company
                {
                    Id = theId,
                    Name = "TheCompany",
                    CreateDate = DateTimeOffset.Now
                };

                //save the company
                repo.Save(company);

                //Load the company to make sure it saved
                var companyFromGet = repo.Get(theId);
                Assert.Equal("TheCompany", companyFromGet.Name);

                //delete the company
                repo.Delete(theId);

                var companyToVerifyDelete = repo.Get(theId);
                Assert.Null(companyToVerifyDelete);
            }
            catch (Exception e)
            {
                _testOutputHelper.WriteLine(e.ToString());
                throw;
            }
        }

        [Fact]
        public void CreateDeleteAndVerifyTheStudentRepo()
        {
            try
            {
                //create an instance of the repo
                var repo = new StudentRepo();

                Guid theId = Guid.NewGuid();

                //create a object to save
                var student = new Student
                {
                    Id = theId,
                    Name = "Billy",
                    Age = 10
                };

                //save the object
                repo.Save(student);

                //Load the object to make sure it saved
                var studentFromGet = repo.Get(theId);
                Assert.Equal("Billy", studentFromGet.Name);
                Assert.Equal(10, student.Age);

                //delete the object
                repo.Delete(theId);

                var studentToVerifyDelete = repo.Get(theId);
                Assert.Null(studentToVerifyDelete);
            }
            catch (Exception e)
            {
                _testOutputHelper.WriteLine(e.ToString());
                throw;
            }
        }

        #region Fake Repo Tests

        [Fact]
        public void UseFakeRepo()
        {
            var repo = new FakeStudentRepo();
            IEnumerable<Student> allStudents = repo.GetAllStudents();
            IEnumerable<Student> allBoyStudents = repo.GetAllStudents(Enums.Sex.Male);
            IEnumerable<Student> allGirlStudents = repo.GetAllStudents(Enums.Sex.Female);

            Assert.Equal(5, allStudents.Count());
            Assert.Equal(3, allGirlStudents.Count());
            Assert.Equal(2, allBoyStudents.Count());

            //do this instead of a Console.WriteLine
            _testOutputHelper.WriteLine($"allStudents has {allStudents.Count()} students in the collection");
            _testOutputHelper.WriteLine($"allGirlStudents has {allGirlStudents.Count()} students in the collection");
            _testOutputHelper.WriteLine($"allBoyStudents has {allBoyStudents.Count()} students in the collection");
        }

        [Fact]
        public void UseFakeRepoMore()
        {
            var repo = new FakeStudentRepo();
            IEnumerable<Student> students = repo.GetAllStudents();

            //we now have a list of students from the repo. now lets query the IEnumerable.

            IEnumerable<Student> under17 = students.Where(student => student.Age < 17);
            IEnumerable<Student> studentsWithS = students.Where(s => s.Name.StartsWith("S"));

            _testOutputHelper.WriteLine("Here are the students that are under 17:");
            foreach (var student in under17)
            {
                _testOutputHelper.WriteLine(student.Name);
            }

            _testOutputHelper.WriteLine("Here are the students that have names that start with 's'");
            foreach (var student in studentsWithS)
            {
                _testOutputHelper.WriteLine(student.Name);
            }
        }

        [Fact]
        public void BetweenFunction()
        {
            var repo = new FakeStudentRepo();

            IEnumerable<Student> students = repo.GetAllStudents();
            
            IEnumerable<Student> studentsBetween10And15 = students.Where(s => s.Age is <= 15 and >= 10);

            _testOutputHelper.WriteLine("Here are the students between ages 10 and 15:");

            foreach (var student in studentsBetween10And15)
            {
                _testOutputHelper.WriteLine(student.Name);
            }
        }

        [Fact]
        public void BetweenFunctionUsingArgs()
        {
            var repo = new FakeStudentRepo();

            IEnumerable<Student> students = repo.GetAllStudents();

            //for the purpose of the test lets pretend that the lower and upper ages were input
            //into a form by a user. we take the values from the form and put them into integers:
            int lowerAgeFromUserInput = 10; //when I change this value to 3, an exception occurs... which is what is supposed to happen.
            int upperAgeFromUserInput = 15;
            
            IEnumerable<Student> studentsAge10To15 = repo.GetStudents10Thru15(lowerAgeFromUserInput, upperAgeFromUserInput);
            
            
            //Now we reuse the integer values to write out your message. Out string output now used
            //the same values that it passed to the function so it will be accurate.
            //another way to say this is... your message is dynaminc; based on the user input:
            
            _testOutputHelper.WriteLine($"Here are the students between ages {lowerAgeFromUserInput} and {upperAgeFromUserInput}:");

            foreach (var student in studentsAge10To15)
            {
                _testOutputHelper.WriteLine(student.Name);
            }
        }


        /// ///////////////// ///////////////// //////////////

       public  enum Belts
        {
            White,
            Blue,
            Purple,
            Brown,
            Black
        }

        public void DoThingsByBelt(Belts belts)
        {
            switch (belts)
            {
                case Belts.White:
                    //white belt code
                    break;
                case Belts.Blue:
                    break;
                case Belts.Purple:
                    break;
                case Belts.Brown:
                    break;
                case Belts.Black:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(belts), belts, null);
            }
        }

        public void DoThingsByBelt(string belts)
        {
            if (string.IsNullOrWhiteSpace(belts))
            {
                if (belts.ToLower().Trim() == "white")
                {
                    
                }
            }
        }
        
        /// ///////////////// ///////////////// //////////////
        
        
        
        
        
        // [Fact]
        // public void CreateAndSaveStudents()
        // {;
        //     var repo = new StudentRepo();
        //
        //     var studentlist = new List<Student>
        //     {
        //         new Student {Age = 15, Name = "Katie", Sex = Enums.Sex.Female},
        //         new Student {Age = 10, Name = "Lucy", Sex = Enums.Sex.Female},
        //         new Student {Age = 13, Name = "Billy", Sex = Enums.Sex.Male},
        //         new Student {Age = 16, Name = "Alex", Sex = Enums.Sex.Male},
        //         new Student {Age = 15, Name = "Sally", Sex = Enums.Sex.Female}
        //     };   
        //     
        //
        //     foreach (var Student in studentlist)
        //     {
        //         repo.Save(Student);
        //     }
        // }
    }
}



#endregion Fake Repo Tests

