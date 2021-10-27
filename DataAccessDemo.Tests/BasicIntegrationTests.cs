using System;
using System.Collections.Generic;
using System.Linq;
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
    }

    #endregion Fake Repo Tests
}


