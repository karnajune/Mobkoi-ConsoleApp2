using ConsoleApp2.Interfaces;
using ConsoleApp2.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2.Implementations
{
    public class ReaderService : IReaderService
    {
        private decimal currentExchangeRate;
        private readonly ILogger<ReaderService> _logger;
        private readonly IHelperService _helperService;
        private readonly ICrmConnectorService _crmConnectorService;
        private readonly IFxService _fxService;
        private readonly ICsvParserService _csvParserService;
        public ReaderService(ICrmConnectorService crmConnectorService, 
            ILogger<ReaderService> logger,
            IHelperService helperService,
            ICsvParserService csvParserService,
            IFxService fxService)
        {
            _crmConnectorService = crmConnectorService;
            _logger = logger;
            _helperService = helperService;
            _csvParserService = csvParserService;
            _fxService = fxService;
        }
        public Tuple<bool, string> GetCsvReport(string entityName,string[] colNames)
        {
            var connection = _crmConnectorService.GetCrmConnection();
            try
            {
                if (connection != null)
                {
                    currentExchangeRate = _fxService.GetFxRate().RateNew;
                    var query = _helperService.BuildQuery(entityName,colNames);
                    var entityList =connection.RetrieveMultiple(query)
                        .Entities
                        //.Select(e => e.ToEntity<Account>()) // couldnt cast to object with nested Entity Reference and Money Types
                        .ToList();

                    var accountList = ExtractAccountObject(entityList);// this is because could not find a way to cast to object

                    if (accountList.Count != 0)
                    {
                        return _csvParserService.ExportToCsv(accountList);
                    }
                    else
                    {
                        _logger.LogError("No Accounts found to generate report");
                        return Tuple.Create(false, string.Empty);
                    }
                }
                else
                {
                    _logger.LogCritical("Connection is null");
                    return Tuple.Create(false, string.Empty);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Failed to Read Entity {ex.Message}");
                return Tuple.Create(false, string.Empty);
            }
        }

        private List<Account> ExtractAccountObject(List<Entity> entityList)
        {
            var accountList = new List<Account>();

            try
            {
                foreach (var entity in entityList)
                {
                    var account = new Account();
                    
                    foreach (var att in entity.Attributes)
                    {
                        switch (att.Key.ToLower())
                        {
                            case "accountid":
                                account.AccountId = att.Value.ToString();
                                break; 
                            case "transactioncurrencyid":
                                account.TransactionCurrency = entity.GetAttributeValue<EntityReference>("transactioncurrencyid").Name.ToString();
                                break;
                            case "emailaddress1":
                                account.Emailaddress1 = att.Value.ToString();
                                break;
                            case "revenue":
                                account.Revenue = Math.Round(entity.GetAttributeValue<Money>("revenue").Value * currentExchangeRate,2);
                                break;
                            case "creditlimit":
                                account.Creditlimit = Math.Round(entity.GetAttributeValue<Money>("creditlimit").Value * currentExchangeRate,2);
                                break;
                            case "revenue_base":
                                account.Revenue_base = Math.Round(entity.GetAttributeValue<Money>("revenue_base").Value * currentExchangeRate,2);
                                break;
                            case "name":
                                account.Name = att.Value.ToString();
                                break;
                            case "primarycontactid":
                                account.Primarycontactid = entity.GetAttributeValue<EntityReference>("primarycontactid").Name.ToString();
                                break;
                            case "telephone1":
                                account.Telephone1 = att.Value.ToString();
                                break;
                            case "creditlimit_base":
                                account.Creditlimit_base = Math.Round(entity.GetAttributeValue<Money>("creditlimit_base").Value * currentExchangeRate,2);
                                break;
                        }
                    }
                    accountList.Add(account);
                }
                return accountList;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Extract Account Object {ex.Message}");
                return accountList;
            }           
        }
    }
}
