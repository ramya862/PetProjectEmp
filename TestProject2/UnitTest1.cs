using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
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
using Microsoft.VisualStudio.TestPlatform.Common;
using Moq;
using Newtonsoft.Json;
using TestProject1;
using TTACRUD.Domain;
using Xunit;
using Xunit.Sdk;

public class HttptriggersTests
{


    [Fact]
    public async Task RegisterEmployee_ReturnsBadRequestObjectResult()
    {
        var cosmosClientMock = new Mock<CosmosClient>();
        var documentContainerMock = new Mock<Microsoft.Azure.Cosmos.Container>();
        cosmosClientMock.Setup(c => c.GetContainer(It.IsAny<string>(), It.IsAny<string>())).Returns(documentContainerMock.Object);

        var req = Testfactory.CreateHttpRequest("id", "");
        var documentsOutMock = new Mock<IAsyncCollector<dynamic>>().Object;
        var httpRequestMock = new Mock<HttpRequest>().Object;
        var loggerMock = new Mock<ILogger>().Object;
        var empdomain = new Mock<EmpDomain>();
        var mock_id = "";
        var empDomainMock = new Mock<EmpDomain>();
        var empDalMock = new Mock<EmpDAL>();
        var empHttpTriggers = new Httptriggers(cosmosClientMock.Object, empDomainMock.Object);
        var expectedActionResult = new BadRequestObjectResult("Failed to register the employee");
        var mydomainMock = new Mock<DomainInterface>();
        var httptriggers = new Httptriggers(cosmosClientMock.Object, empDomainMock.Object);
        //Act
        mydomainMock.Setup(e => e.CreateItem(req, mock_id, documentsOutMock)).ReturnsAsync(expectedActionResult);

        var result = await httptriggers.RegisterEmployee(req, documentsOutMock, loggerMock, mock_id);
        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task DeleteEmployee_ReturnsOkObjectResult()
    {
        //Arrange
        var cosmosClientMock = new Mock<CosmosClient>();
        var documentContainerMock = new Mock<Microsoft.Azure.Cosmos.Container>();
        cosmosClientMock.Setup(c => c.GetContainer(It.IsAny<string>(), It.IsAny<string>())).Returns(documentContainerMock.Object);

        var req = Testfactory.CreateHttpRequest("id", "1");
        var documentsOutMock = new Mock<IAsyncCollector<dynamic>>().Object;
        var httpRequestMock = new Mock<HttpRequest>().Object;
        var loggerMock = new Mock<ILogger>().Object;
        var empdomain = new Mock<EmpDomain>();
        var mock_id = "1";
        var empDomainMock = new Mock<EmpDomain>();
        var empDalMock = new Mock<EmpDAL>();
        var empHttpTriggers = new Httptriggers(cosmosClientMock.Object, empDomainMock.Object);
        var expectedActionResult = new OkObjectResult("Deleted employee record sucessfully");
        var mydomainMock = new Mock<DomainInterface>();
        var httptriggers = new Httptriggers(cosmosClientMock.Object, empDomainMock.Object);
        //Act
        mydomainMock.Setup(e => e.DeleteItem(req, mock_id, documentContainerMock.Object)).ReturnsAsync(expectedActionResult);

        var result = await httptriggers.DeleteEmployee(req, loggerMock, mock_id);
        // Assert
        Assert.IsType<OkObjectResult>(result);

    }
    [Fact]
    public async Task DeleteEmployee_ReturnsBadrequest()
    {
        //Arrange
        var cosmosClientMock = new Mock<CosmosClient>();
        var documentContainerMock = new Mock<Microsoft.Azure.Cosmos.Container>();
        cosmosClientMock.Setup(c => c.GetContainer(It.IsAny<string>(), It.IsAny<string>())).Returns(documentContainerMock.Object);

        var req = Testfactory.CreateHttpRequest("id", "");
        var documentsOutMock = new Mock<IAsyncCollector<dynamic>>().Object;
        var httpRequestMock = new Mock<HttpRequest>().Object;
        var loggerMock = new Mock<ILogger>().Object;
        var empdomain = new Mock<EmpDomain>();
        var mock_id = "";
        var empDomainMock = new Mock<EmpDomain>();
        var empDalMock = new Mock<EmpDAL>();
        var empHttpTriggers = new Httptriggers(cosmosClientMock.Object, empDomainMock.Object);
        var expectedActionResult = new BadRequestObjectResult("The id parameter is missing or invalid");
        var mydomainMock = new Mock<DomainInterface>();
        var httptriggers = new Httptriggers(cosmosClientMock.Object, empDomainMock.Object);
        //Act
        mydomainMock.Setup(e => e.DeleteItem(req, mock_id, documentContainerMock.Object)).ReturnsAsync(expectedActionResult);

        var result = await httptriggers.DeleteEmployee(req, loggerMock, mock_id);
        // Assert
        Assert.IsType<BadRequestObjectResult>(result);

    }
    [Fact]
    public async Task ReadEmployeebyId_ReturnsBadrequestObjectResult()
    {
        //Arrange
        var cosmosClientMock = new Mock<CosmosClient>();
        var documentContainerMock = new Mock<Microsoft.Azure.Cosmos.Container>();
        cosmosClientMock.Setup(c => c.GetContainer(It.IsAny<string>(), It.IsAny<string>())).Returns(documentContainerMock.Object);

        var req = Testfactory.CreateHttpRequest("id", "");
        var documentsOutMock = new Mock<IAsyncCollector<dynamic>>().Object;
        var httpRequestMock = new Mock<HttpRequest>().Object;
        var loggerMock = new Mock<ILogger>().Object;
        var empdomain = new Mock<EmpDomain>();
        var mock_id = "";
        var empDomainMock = new Mock<EmpDomain>();
        var empDalMock = new Mock<EmpDAL>();
        var empHttpTriggers = new Httptriggers(cosmosClientMock.Object, empDomainMock.Object);
        var expectedActionResult = new BadRequestObjectResult("The id parameter is missing or invalid");
        var mydomainMock = new Mock<DomainInterface>();
        var httptriggers = new Httptriggers(cosmosClientMock.Object, empDomainMock.Object);
        //Act
        mydomainMock.Setup(e => e.ReadItemById(req, mock_id, documentContainerMock.Object)).ReturnsAsync(expectedActionResult);

        var result = await httptriggers.GetEmployeeById(req, loggerMock, mock_id);
        // Assert
        Assert.IsType<BadRequestObjectResult>(result);

    }
   
    [Fact]
    public async Task ReadEmployee_ReturnsOkObjectResult()
    {
        //Arrange
        var mockdata = new Model { id = "1", name = "Ruth", Gender = "Female", DOB = "2002-06-09", Email = "ruth@gmail.com", phone = "9678675645", Age = "21"
        };
        var cosmosClientMock = new Mock<CosmosClient>();
        var documentContainerMock = new Mock<Microsoft.Azure.Cosmos.Container>();
        cosmosClientMock.Setup(c => c.GetContainer(It.IsAny<string>(), It.IsAny<string>())).Returns(documentContainerMock.Object);
        var context = new DefaultHttpContext();
        var Request = context.Request;
        var req = Testfactory.CreateHttpRequest("id", "1");
        //var Request = new DefaultHttpContext();
        var documentsOutMock = new Mock<IAsyncCollector<dynamic>>().Object;
        var docs = new Mock<IEnumerable<dynamic>>().Object;
        var httpRequestMock = new Mock<HttpRequest>().Object;
        var loggerMock = new Mock<ILogger>().Object;
        var empdomain = new Mock<EmpDomain>();
        var mock_id = "1";
        var empDomainMock = new Mock<EmpDomain>();
        var empDalMock = new Mock<EmpDAL>();
        var empHttpTriggers = new Httptriggers(cosmosClientMock.Object, empDomainMock.Object);
        var expectedActionResult = new OkObjectResult(mockdata);
        var mydomainMock = new Mock<DomainInterface>();
        var httptriggers = new Httptriggers(cosmosClientMock.Object, empDomainMock.Object);
        //Act
        mydomainMock.Setup(e => e.ReadItem(Request, docs)).ReturnsAsync(expectedActionResult);

        var result = await empHttpTriggers.GetEmployeeInfo(Request, docs, loggerMock);
        // Assert
        Assert.IsType<OkObjectResult>(result);

    }


[Fact]
public async Task ReadEmployeebyId_ReturnsOkObjectResult()
{
    // Arrange
    var mock_id = "1";
    var mockdata = new Model
    {
        id = "1",
        name = "Ruth",
        Gender = "Female",
        DOB = "2002-06-09",
        Email = "ruth@gmail.com",
        phone = "9678675645",
        Age = "21"
    };

    var cosmosClientMock = new Mock<CosmosClient>();
    var documentContainerMock = new Mock<Microsoft.Azure.Cosmos.Container>();
    cosmosClientMock.Setup(c => c.GetContainer(It.IsAny<string>(), It.IsAny<string>())).Returns(documentContainerMock.Object);

    var itemResponseMock = new Mock<ItemResponse<Model>>();
    itemResponseMock.SetupGet(r => r.Resource).Returns(mockdata);
    itemResponseMock.SetupGet(r => r.StatusCode).Returns(HttpStatusCode.OK);

        var requestOptions = new ItemRequestOptions();

        documentContainerMock.Setup(c => c.ReadItemAsync<Model>(It.IsAny<string>(), It.IsAny<PartitionKey>(), It.IsAny<ItemRequestOptions>(), default))
    .ReturnsAsync(itemResponseMock.Object);
        var req = Testfactory.CreateHttpRequest("id", mock_id);
    var loggerMock = Mock.Of<ILogger>();
    var empDomainMock = new Mock<DomainInterface>();
        var mydomain = new Mock<EmpDomain>();
    var expectedActionResult = new OkObjectResult(mockdata);
    empDomainMock.Setup(e => e.ReadItemById(req, mock_id, documentContainerMock.Object))
                 .ReturnsAsync(expectedActionResult);

    var httpTriggers = new Httptriggers(cosmosClientMock.Object, mydomain.Object);

    // Act
    var result = await httpTriggers.GetEmployeeById(req, loggerMock, mock_id);

    // Assert
    Assert.IsType<OkObjectResult>(result);
    Assert.Equal(expectedActionResult.Value, (result as OkObjectResult)?.Value);
}

[Fact]
    public async Task UpdateEmployee_ReturnsBadRequestObjectResult()
    {
        var cosmosClientMock = new Mock<CosmosClient>();
        var documentContainerMock = new Mock<Microsoft.Azure.Cosmos.Container>();
        cosmosClientMock.Setup(c => c.GetContainer(It.IsAny<string>(), It.IsAny<string>())).Returns(documentContainerMock.Object);

        var req = Testfactory.CreateHttpRequest("id", "");
        var documentsOutMock = new Mock<IAsyncCollector<dynamic>>().Object;
        var httpRequestMock = new Mock<HttpRequest>().Object;
        var loggerMock = new Mock<ILogger>().Object;
        var empdomain = new Mock<EmpDomain>();
        var mock_id = "";
        var empDomainMock = new Mock<EmpDomain>();
        var empDalMock = new Mock<EmpDAL>();
        var empHttpTriggers = new Httptriggers(cosmosClientMock.Object, empDomainMock.Object);
        var expectedActionResult = new BadRequestObjectResult("The id parameter is missing or invalid");
        var mydomainMock = new Mock<DomainInterface>();
        var httptriggers = new Httptriggers(cosmosClientMock.Object, empDomainMock.Object);
        //Act
        mydomainMock.Setup(e => e.UpdateItem(req, mock_id, documentContainerMock.Object)).ReturnsAsync(expectedActionResult);

        var result = await httptriggers.UpdateEmployee(req, loggerMock, mock_id);
        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
        // Assert.Equal(expectedActionResult, result);
    }
   



public class ItemResponseWrapper<T>
{
    public T Resource { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}








[Fact]
public async Task UpdateEmployee_ReturnsOkObjectResult()
{
    // Arrange
    var mock_id = "1";
    var mockdata = new Model
    {
        id = "1",
        name = "Ruth",
        Gender = "Female",
        DOB = "2002-06-09",
        Email = "ruth@gmail.com",
        phone = "9678675645",
        Age = "21"
    };

    var cosmosClientMock = new Mock<CosmosClient>();
    var documentContainerMock = new Mock<Microsoft.Azure.Cosmos.Container>();
    cosmosClientMock.Setup(c => c.GetContainer(It.IsAny<string>(), It.IsAny<string>())).Returns(documentContainerMock.Object);

    var requestOptions = new ItemRequestOptions();
    var itemResponseMock = new Mock<ItemResponse<Model>>();
    itemResponseMock.SetupGet(r => r.Resource).Returns(mockdata);
    itemResponseMock.SetupGet(r => r.StatusCode).Returns(HttpStatusCode.OK);

    documentContainerMock
        .Setup(c => c.ReadItemAsync<Model>(It.IsAny<string>(), It.IsAny<PartitionKey>(), It.IsAny<ItemRequestOptions>(), default))
        .ReturnsAsync(itemResponseMock.Object);


        var req = Testfactory.CreateHttpRequest("id", mock_id);
    var loggerMock = Mock.Of<ILogger>();
    var empDomainMock = new Mock<EmpDomain>();
    var mydomain = new Mock<DomainInterface>();

    var expectedActionResult = new OkObjectResult("Updated employee data successfully");

    mydomain.Setup(e => e.UpdateItem(req, mock_id, documentContainerMock.Object))
            .ReturnsAsync(expectedActionResult);
        var requestData = JsonConvert.SerializeObject(new Updatedoc
        {
            name = "Ruth",
            Gender = "Female",
            Email = "ruth@gmail.com",
            DOB = "2002-06-09",
            Age = "21",
            phone = "9678675645"
        });
        req.Body = new MemoryStream(Encoding.UTF8.GetBytes(requestData));
        var httpTriggers = new Httptriggers(cosmosClientMock.Object, empDomainMock.Object);

    // Act
    var result = await httpTriggers.UpdateEmployee(req, loggerMock, mock_id);

    // Assert
    Assert.IsType<OkObjectResult>(result);
}


    [Fact]
    public async Task CreateEmployee_ReturnsOkObjectResult()
    {
        // Arrange
        var mock_id = "1";
        var mockdata = new Model
        {
            id = "1",
            name = "Ruth",
            Gender = "Female",
            DOB = "2002-06-09",
            Email = "ruth@gmail.com",
            phone = "9678675645",
            Age = "21"
        };

        var cosmosClientMock = new Mock<CosmosClient>();
        var documentsOutMock = new Mock<IAsyncCollector<dynamic>>().Object;

        var documentContainerMock = new Mock<Microsoft.Azure.Cosmos.Container>();
        cosmosClientMock.Setup(c => c.GetContainer(It.IsAny<string>(), It.IsAny<string>())).Returns(documentContainerMock.Object);

        var requestOptions = new ItemRequestOptions();
        var itemResponseMock = new Mock<ItemResponse<Model>>();
        itemResponseMock.SetupGet(r => r.Resource).Returns(mockdata);
        itemResponseMock.SetupGet(r => r.StatusCode).Returns(HttpStatusCode.OK);

        documentContainerMock
            .Setup(c => c.ReadItemAsync<Model>(It.IsAny<string>(), It.IsAny<PartitionKey>(), It.IsAny<ItemRequestOptions>(), default))
            .ReturnsAsync(itemResponseMock.Object);


        var req = Testfactory.CreateHttpRequest("id", mock_id);
        var loggerMock = Mock.Of<ILogger>();
        var empDomainMock = new Mock<EmpDomain>();
        var mydomain = new Mock<DomainInterface>();

        var expectedActionResult = new OkObjectResult("Updated employee data successfully");

        mydomain.Setup(e => e.CreateItem(req, mock_id, documentsOutMock))
                .ReturnsAsync(expectedActionResult);
        var requestData = JsonConvert.SerializeObject(new Updatedoc
        {
            name = "Ruth",
            Gender = "Female",
            Email = "ruth@gmail.com",
            DOB = "2002-06-09",
            Age = "21",
            phone = "9678675645"
        });
        req.Body = new MemoryStream(Encoding.UTF8.GetBytes(requestData));
        var httpTriggers = new Httptriggers(cosmosClientMock.Object, empDomainMock.Object);

        // Act
        var result = await httpTriggers.RegisterEmployee(req,documentsOutMock, loggerMock, mock_id);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
















}
