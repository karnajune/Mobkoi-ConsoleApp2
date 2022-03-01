using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Models
{
   public class Account
    {
        public string AccountId { get; set; }
        public string TransactionCurrency { get; set; }
        public string Name { get; set; }
        public string Parentaccountid { get; set; }
        public string Primarycontactid { get; set; }
        public string Telephone1 { get; set; }
        public string Emailaddress1 { get; set; }
        public decimal Revenue { get; set; }
        public decimal Revenue_base { get; set; }
        public decimal Creditlimit { get; set; }
        public decimal Creditlimit_base { get; set; }

      
    }
}
