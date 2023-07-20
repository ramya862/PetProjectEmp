using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTACRUD.Domain;

namespace TestProject1
{
    public class Testfactory
    {
        public static Dictionary<string, StringValues> CreateDictionary(string key, string value)
        {
            var qs = new Dictionary<string, StringValues>
            {
                { key,value}
            };
            return qs;
        }
        public static HttpRequest CreateHttpRequest(string queryStringKey, string queryStringValue)
        {
            var context = new DefaultHttpContext();
            var request = context.Request;
            request.Query = new QueryCollection(CreateDictionary(queryStringKey, queryStringValue));
            return request;

        }

    }
}
