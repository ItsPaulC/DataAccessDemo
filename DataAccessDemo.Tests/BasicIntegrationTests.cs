using System;
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
        public void CreateDeleteAndVerifyTheRepo()
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
                Assert.Equal("TheCompany",companyFromGet.Name);
                
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
    }
}
