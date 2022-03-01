using ConsoleApp2.Implementations;
using ConsoleApp2.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp2
{
    public class Program
    {    
        static void Main(string[] args)
        {
            // can see how we take users to input filter columns and fitler types to make it work more dynamic for user inputs


            var host = Host.CreateDefaultBuilder()
               .ConfigureServices((context, services) =>
               {
                   services.AddTransient<IReaderService, ReaderService>();
                   services.AddTransient<ICrmConnectorService, CrmConnectorService>();
                   services.AddTransient<ICsvParserService, CsvParserService>();
                   services.AddTransient<IHelperService, HelperService>();
                   services.AddTransient<IFxService, FxService>();
               })
               .Build();

            var svc = ActivatorUtilities.CreateInstance<ReaderService>(host.Services);

            var entityName = "account";
            var columnNames = new string[] { "name", "parentaccountid", "primarycontactid", "telephone1", "emailaddress1", "revenue", "revenue_base","creditlimit", "creditlimit_base" };           

            (bool isSuccess,string filePath)=svc.GetCsvReport(entityName,columnNames);

            var message = isSuccess ? $"Please find the downloaded Csv at - {filePath}" : "Could not download Csv, please contact admin";

            Console.WriteLine(message);
           
            Console.ReadKey();
        }
    }
}
