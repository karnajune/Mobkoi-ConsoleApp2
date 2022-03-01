using ConsoleApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Interfaces
{
   public interface ICsvParserService
    {
        Tuple<bool, string> ExportToCsv(List<Account> accountsList);
    }
}
