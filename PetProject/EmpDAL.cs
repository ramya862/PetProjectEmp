using CSharpVitamins;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;
using TTACRUD.Domain;

namespace CRUD.Dataaccess
{
    public class EmpDAL

    {

       public static async Task<IActionResult> CreateItem(HttpRequest req,string id, string name, string Gender,string Age,string Email,string phone, string DOB,IAsyncCollector<dynamic> documentsOut)
        {
            //var shortGuid = ShortGuid.NewGuid().ToString();
 

            if (!string.IsNullOrEmpty(id))
            {
                //var longid = System.Guid.NewGuid().ToString();
               // id= ShortGuid.NewGuid().ToString();

                //var shortGuid = ShortGuid.NewGuid().ToString();
                await documentsOut.AddAsync(new
                {
                    //id = shortGuid,
                    id = id,
                    name = name,
                    Gender = Gender,
                    Age = Age,
                    Email = Email,
                    phone = phone,
                    DOB = DOB
                }); ;
            }

            string responseMessage = string.IsNullOrEmpty(name)
               ? "This HTTP create function executed successfully. Pass a name in the query string or in the request body to create document in cosmos db"
                 : $"Successfully created the record with {name} and {Email} {id}";

            return new OkObjectResult(responseMessage);
        }

        public static async Task<IActionResult> ReadItem(HttpRequest req, IEnumerable<dynamic> documents)
        {
            //string responsemessage = "Retrived all students information successfully";
            //dynamic gmydata = new ExpandoObject();
            //gmydata.message = responsemessage;
            //gmydata.Data = documents;
            //string json = Newtonsoft.Json.JsonConvert.SerializeObject(gmydata);
            return new OkObjectResult(documents);
        }
        public static async Task<IActionResult> ReadItemById(HttpRequest req, string id, Microsoft.Azure.Cosmos.Container documentContainer)
        {
            var item = await documentContainer.ReadItemAsync<Model>(id, new Microsoft.Azure.Cosmos.PartitionKey(id));
            string getmessage = "Retrived  the employee entry successfully by Id";
            dynamic gmydata = new ExpandoObject();
            gmydata.message = getmessage;
            gmydata.Data = item.Resource;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(gmydata);
            return new OkObjectResult(json);
        }

        public static async Task<IActionResult> UpdateItem(HttpRequest req, string id, Microsoft.Azure.Cosmos.Container documentContainer)
        {
            string requestData = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Updatedoc>(requestData);
            var item = await documentContainer.ReadItemAsync<Model>(id, new Microsoft.Azure.Cosmos.PartitionKey(id));
            item.Resource.Gender = data.Gender;
            item.Resource.name = data.name;
            item.Resource.Age = data.Age;
            item.Resource.DOB = data.DOB;
            item.Resource.phone = data.phone;
            item.Resource.Email = data.Email;


            await documentContainer.UpsertItemAsync(item.Resource);
            string updatemessage = "Updated employee data successfully";
            dynamic upmydata = new ExpandoObject();
            upmydata.message = updatemessage;
            upmydata.Data = item.Resource;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(upmydata);
            return new OkObjectResult(json);
        }

        public static async Task<IActionResult> DeleteItem(HttpRequest req, string id, Microsoft.Azure.Cosmos.Container documentContainer)
        {

            await documentContainer.DeleteItemAsync<Model>(id, new Microsoft.Azure.Cosmos.PartitionKey(id));
            string responseMessage = "Deleted employee record sucessfully";
            return new OkObjectResult(responseMessage);
        }
    }

}
