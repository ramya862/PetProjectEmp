using Cosmosex;
using CRUD.Dataaccess;
using CRUD.Logic;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections;
using System.Threading.Tasks;
using TestProject1;
using TTACRUD.Domain;
using Xunit;

namespace TestProject
{
    public class UnitTest1
    {
        private class InpuData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new Model[] { new Model { name = "TEST", Gender = "M", Age = "21", Email = "test@gmail.com", phone = "9898978675", DOB = "2002-06-08" } };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        [Theory]

        [ClassData(typeof(InpuData))]
        public async Task RegisterEmployee_Return200OkSuccess(Model input)
        {
            var req = Testfactory.GenerateHttpRequest(input);
            var cosmosClientMock = new Mock<CosmosClient>();
            var documentsOutMock = new Mock<IAsyncCollector<dynamic>>().Object;
            var httpRequestMock = new Mock<HttpRequest>().Object;
            var loggerMock = new Mock<ILogger>().Object;
            var modelValidatorMock = new Mock<IValidator<Model>>();
            var empdomain = new Mock<EmpDomain>();
            var mock_id = "1";
            var empDomainMock = new Mock<EmpDomain>();
            var empDalMock = new Mock<EmpDAL>();
            var empHttpTriggers = new Httptriggers(cosmosClientMock.Object,empDomainMock.Object);
            var expectedActionResult = new OkObjectResult("Successfully created the new employee record");
            var mydomainMock = new Mock<DomainInterface>();
            mydomainMock.Setup(e => e.CreateItem(req.Object, mock_id, documentsOutMock)).ReturnsAsync(expectedActionResult);
            //Act

           //var result = await Httptriggers.Registeremployee(req.Object, documentsOutMock, loggerMock, mock_id);
            
            //Assert
            //Assert.NotNull(result);
           // Assert.Equal(expectedActionResult, result);
        }

    }

}