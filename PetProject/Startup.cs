//using CRUD;
//using FluentValidation.AspNetCore;
//using Microsoft.Azure.Cosmos.Fluent;
//using Microsoft.Azure.Functions.Extensions.DependencyInjection;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.PowerBI.Api.Models;
//using Microsoft.SharePoint.Client;
//using System;
//using TTACRUD.Domain;

//[assembly: FunctionsStartup(typeof(Startup))]

//namespace CRUD
//{
//    public class Startup : FunctionsStartup
//    {
//        private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
//            .SetBasePath(Environment.CurrentDirectory)
//            .AddJsonFile("appsettings.json", true)
//            .AddEnvironmentVariables()
//            .Build();
//        [Obsolete]
//        public override void Configure(IFunctionsHostBuilder builder)
//        {
//            builder.Services.AddCors(p => p.AddPolicy("policy1",build =>

//            { 
//                    build.WithOrigins("http://localhost:4200");
//                    build.AllowAnyMethod();
//                    build.AllowAnyHeader();
//            }));
//            builder.Services.AddFluentValidation(cfg =>
//            {
//                cfg.RegisterValidatorsFromAssemblyContaining<ModelValidator>();
//            });

//            builder.Services.AddSingleton(s =>
//            {
//                var connectionString = Configuration["CosmosDbConnectionString"];
//                if (string.IsNullOrEmpty(connectionString))
//                {
//                    throw new InvalidOperationException(
//                        "Please specify a valid CosmosDBConnection in the appSettings.json file or your Azure Functions Settings.");
//                }

//                return new CosmosClientBuilder(connectionString)
//                    .Build();
//            });

//        }
//    }
//}
using CRUD;
using CRUD.Logic;
using FluentValidation.AspNetCore;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TTACRUD.Domain;

[assembly: FunctionsStartup(typeof(Startup))]

namespace CRUD
{
    public class Startup : FunctionsStartup
    {
        private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json", true)
            .AddEnvironmentVariables()
            .Build();

        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Add CORS policy
            builder.Services.AddCors(p => p.AddPolicy("policy1", build =>
            {
                build.WithOrigins("http://localhost:4200");
                build.AllowAnyMethod();
                build.AllowAnyHeader();
            }));

            // Add FluentValidation
            builder.Services.AddFluentValidation(cfg =>
            {
                cfg.RegisterValidatorsFromAssemblyContaining<ModelValidator>();
            });

            // Add CosmosClient as a singleton
            builder.Services.AddSingleton(s =>
            {
                var connectionString = Configuration["CosmosDbConnectionString"];
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Please specify a valid CosmosDBConnection in the appSettings.json file or your Azure Functions Settings.");
                }

                return new CosmosClientBuilder(connectionString).Build();
            });

            // Add EmpDomain as a transient (created per function invocation)
            builder.Services.AddTransient<EmpDomain>();
        }
    }
}
