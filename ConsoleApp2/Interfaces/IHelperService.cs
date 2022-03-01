using Microsoft.Xrm.Sdk.Query;

namespace ConsoleApp2.Interfaces
{
   public interface IHelperService
    {
        QueryExpression BuildQuery(string entityName, string[] columnNames);
    }
}
