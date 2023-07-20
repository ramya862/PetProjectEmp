using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;


namespace TestProject1
{
    public class Testfactory
    {


        private static RouteValueDictionary CreateRouteValueDictionary(string key, string value)
        {
            var routeValues = new RouteValueDictionary
    {
        { key, value }
    };
            return routeValues;
        }

        public static Mock<HttpRequest> GenerateHttpRequest(TTACRUD.Domain.Model emp)
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(emp);
            var request = new Mock<HttpRequest>();
            request.Setup(r => r.Method).Returns("POST");
            request.Setup(r => r.ContentType).Returns("application/json");
            request.Setup(r => r.Body).Returns(new MemoryStream(Encoding.UTF8.GetBytes(jsonString)));
            request.Setup(r => r.RouteValues).Returns(CreateRouteValueDictionary("id", "35"));
            var httpContext = new Mock<HttpContext>();
            return request;
        }
    }
}

