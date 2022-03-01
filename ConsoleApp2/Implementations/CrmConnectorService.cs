using ConsoleApp2.Interfaces;
using Microsoft.Build.Framework;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Configuration;

namespace ConsoleApp2.Implementations
{
    public class CrmConnectorService : ICrmConnectorService
    {
        private readonly ILogger<CrmConnectorService> _logger;

        public CrmConnectorService(ILogger<CrmConnectorService> logger)
        {
            _logger = logger;
        }
        public IOrganizationService GetCrmConnection()
        {
            IOrganizationService oServiceProxy;
            try
            {
                var userName = ConfigurationManager.AppSettings.Get("Username");
                var password = ConfigurationManager.AppSettings.Get("Password");
                var url = ConfigurationManager.AppSettings.Get("CrmEnvironmentUrl");
                
                _logger.LogInformation("Establishing Connection to CRM");
                
                CrmServiceClient crmClient = new CrmServiceClient($"AuthType=Office365;Username={userName};Password={password};URL={url};");
                oServiceProxy = crmClient.OrganizationWebProxyClient != null
                    ? crmClient.OrganizationWebProxyClient
                    : (IOrganizationService)crmClient.OrganizationServiceProxy;


                if (oServiceProxy != null)
                {
                    Guid userid = ((WhoAmIResponse)oServiceProxy.Execute(new WhoAmIRequest())).UserId;

                    if (userid != Guid.Empty)
                    {
                        _logger.LogInformation("Established Successfull Connection to CRM");
                    }
                }
                else
                {
                    _logger.LogError("Unable to establish connection to CRM");
                    return null;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Unable to establish connection to CRM {ex.Message}");//can remove this if this level of details is not needed
                return null;
            }
            
            return oServiceProxy;
        }
    }
}
