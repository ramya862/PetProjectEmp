using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System.Dynamic;
using System.IO;
using System.Diagnostics.Eventing.Reader;
using CRUD.Logic;
using Microsoft.AspNetCore.Cors;

namespace Cosmosex
{
    public class Httptriggers
    {
        private const string DataBaseName = "Employee";
        private const string CollectionName = "Details";
        private readonly CosmosClient _cosmosClient;
        private Microsoft.Azure.Cosmos.Container documentContainer;
        private readonly EmpDomain _empDomain;


        public Httptriggers(CosmosClient cosmosClient, EmpDomain empDomain)
        {
            _cosmosClient = cosmosClient;
            documentContainer = _cosmosClient.GetContainer("Employee", "Details");
            _empDomain = empDomain;

        }

        [FunctionName("RegisterNewEmployee")]
        [EnableCors]

        public  async Task<IActionResult> RegisterEmployee(
    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "registeremployee/{id}")] HttpRequest req,
    [CosmosDB(DataBaseName, CollectionName, Connection = "CosmosDbConnectionString")] IAsyncCollector<dynamic> documentsOut,
    ILogger log, string id)
        {
            log.LogInformation("creating doc in cosmos db");
            try
            {
                EmpDomain e = new EmpDomain();
                req.EnableBuffering();
                req.Body.Seek(0, SeekOrigin.Begin);

                return await e.CreateItem(req, id, documentsOut);

            }
            catch (CosmosException e)
            {
                string responseMessage = "Failed to register the employee";
                return new BadRequestObjectResult(responseMessage);
            }

        }


        [FunctionName("GetAllempInfo")]
        [EnableCors]

        public  async Task<IActionResult> GetEmployeeInfo(
     [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getallempinfo")] HttpRequest req,
     [CosmosDB(DataBaseName,CollectionName,Connection= "CosmosDbConnectionString",
        SqlQuery = "SELECT * FROM c")]
        IEnumerable<dynamic> documents,
     ILogger log)
        {
            try
            {
                EmpDomain e = new EmpDomain();

                return await e.ReadItem(req, documents);

            }
            catch (CosmosException e)
            {
                string responseMessage = "Failed to get all the employees information";
                return new NotFoundObjectResult(responseMessage);
            }
        }

        [FunctionName("GetemployeeById")]
        [EnableCors]

        public async Task<IActionResult> GetEmployeeById(
                [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getempbyid/{id}")]
             HttpRequest req, ILogger log, string id)

        {
            log.LogInformation($"Reading with ID: {id}");
            try
            {
                EmpDomain e = new EmpDomain();

                return await e.ReadItemById(req, id, documentContainer);
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                string responseMessage = "Invalid input params,Please check";
                return new NotFoundObjectResult(responseMessage);
            }
        }
        [FunctionName("UpdateemployeeInfo")]
        [EnableCors]

        public async Task<IActionResult> UpdateEmployee(
          [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "updateemployee/{id}")] HttpRequest req,
          ILogger log, string id)
        {
            log.LogInformation($"Updating doc with ID: {id}");
            try
            {
                EmpDomain e = new EmpDomain();

                return await e.UpdateItem(req, id, documentContainer);
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                string responseMessage = "There is no employee record with the mentioned id";
                return new NotFoundObjectResult(responseMessage);
            }
        }

        [FunctionName("DeleteempInfo")]
        [EnableCors]

        public async Task<IActionResult> DeleteEmployee(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "delemp/{id}")] HttpRequest req,
            ILogger log, string id)
        {
            log.LogInformation($"Deleting employee with ID: {id}");

            try
            {
                EmpDomain e = new EmpDomain();

                return await e.DeleteItem(req, id, documentContainer);
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                string responseMessage = "There is no employee record with the mentioned id";
                return new NotFoundObjectResult(responseMessage);
            }
        }



    }
}