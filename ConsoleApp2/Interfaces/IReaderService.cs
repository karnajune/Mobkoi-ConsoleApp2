using ConsoleApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Interfaces
{
    public interface IReaderService
    {
        Tuple<bool, string> GetCsvReport(string tableName,string[] colNames);
    }
}
