using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GSB_ClassLibrary
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public String CustomerName { get; set; }
        public int YTDOrders { get; set; }
        public int YTDSales { get; set; }

   
        private string email;
        public string Email
        {
            get { return this.email; }
            set
            {
                string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                Regex reg = new Regex(pattern);
                if (reg.IsMatch(value))
                {
                        if (this.email != value)
                            this.email = value;                 
                }
            }
        }
    }
}
