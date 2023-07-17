using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using TTACRUD.Domain;
using FluentValidation;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Serilog;
using CRUD.Dataaccess;
using System.Linq;

namespace CRUD.Logic
{
    public class EmpDomain
    {

        public static async Task<IActionResult> CreateItem(HttpRequest req,string id, IAsyncCollector<dynamic> documentsOut)
        {
            string name = req.Query["name"];
            string Gender = req.Query["Gender"];
            string Age = req.Query["Age"];
            string Email = req.Query["Email"];
            string DOB = req.Query["DOB"];
            string phone= req.Query["phone"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Model data = null;
            if (!string.IsNullOrEmpty(requestBody))
            {
                data = JsonConvert.DeserializeObject<Model>(requestBody);
            }
            else
            {
                data = new Model();
            }
            var validator = new ModelValidator();
            var validationResult = await validator.ValidateAsync(data);
            if (!validationResult.IsValid)
            {
                return new BadRequestObjectResult(validationResult.Errors);
            }
            name = name ?? data?.name;
            Gender = Gender ?? data?.Gender;
            Age = Age ?? data?.Age;
            Email = Email ?? data?.Email;
            phone = phone ?? data?.phone;
            DOB = DOB ?? data?.DOB;

            return await EmpDAL.CreateItem(req, id,name, Gender,Age,Email,phone,DOB ,documentsOut);
        }
        public static async Task<IActionResult> ReadItem(HttpRequest req, IEnumerable<dynamic> documents)
        {
            Log.Information("Getting all the items");
            return await EmpDAL.ReadItem(req, documents);
        }

        public static async Task<IActionResult> ReadItemById(HttpRequest req, string id, Microsoft.Azure.Cosmos.Container documentContainer)
        {
            if (string.IsNullOrEmpty(id))
            {
                string responsemessage = "The id parameter is missing or invalid ";
                return new BadRequestObjectResult(responsemessage);
            }
            Log.Information("Getting doc by id");
            return await EmpDAL.ReadItemById(req, id, documentContainer);

        }

        public static async Task<IActionResult> UpdateItem(HttpRequest req, string id, Microsoft.Azure.Cosmos.Container documentContainer)
        {
            if (string.IsNullOrEmpty(id))
            {
                string responsemessage = "The id parameter is missing or invalid ";
                return new BadRequestObjectResult(responsemessage);
            }
            Log.Information("Updating doc by Id");
            return await EmpDAL.UpdateItem(req, id, documentContainer);
        }

        public static async Task<IActionResult> DeleteItem(HttpRequest req, string id, Microsoft.Azure.Cosmos.Container documentContainer)
        {
            if (string.IsNullOrEmpty(id))
            {
                string responsemessage = "The id parameter is missing or invalid ";
                return new BadRequestObjectResult(responsemessage);
            }
            return await EmpDAL.DeleteItem(req, id, documentContainer);

        }
    }
}

