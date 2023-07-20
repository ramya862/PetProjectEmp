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
using MiNET.Plugins;
using Moq;
using NUnit.Framework;
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
        [Xunit.Theory]

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
            var empHttpTriggers = new Httptriggers(cosmosClientMock.Object);
            var expectedActionResult = new OkObjectResult("Successfully created the new employee record");
            var mydomainMock = new Mock<DomainInterface>();
            mydomainMock.Setup(e => e.CreateItem(httpRequestMock, mock_id, documentsOutMock)).ReturnsAsync(expectedActionResult);
            //Act
           //documentsOutMock.SetUp(m => m.AddAsync(It.IsAny<dynamic>())).Returns(Task.CompletedTask);

            var result = await Httptriggers.Registeremployee(req.Object,documentsOutMock,loggerMock,mock_id);

            //Assert
            // Assert.IsType<OkObjectResult>(result);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedActionResult, result);
        }

    }

}