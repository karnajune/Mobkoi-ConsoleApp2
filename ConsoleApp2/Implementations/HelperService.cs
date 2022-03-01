using ConsoleApp2.Interfaces;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace ConsoleApp2.Implementations
{
    public class HelperService : IHelperService
    {     
        public QueryExpression BuildQuery(string entityName, string[] columnNames)
        {
            var cols = new ColumnSet(columnNames);// getting all columns

            QueryExpression query = new QueryExpression
            {
                EntityName = entityName,
                ColumnSet = cols,
                Criteria = new FilterExpression
                {
                    FilterOperator = LogicalOperator.Or,
                    Filters =
                    {
                    new FilterExpression
                        {
                          FilterOperator = LogicalOperator.And,
                          Conditions =
                          {
                            new ConditionExpression("creditlimit", ConditionOperator.NotNull), // this column names and conditions to be taken from users input
                            new ConditionExpression("revenue", ConditionOperator.NotNull) // this column names and conditions to be taken from users input
                          }
                        }
                    }
                }
            };

            return query;
        }
    }
}
