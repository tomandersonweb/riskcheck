using System;
using System.Collections.Generic;
using System.Text;

namespace RiskCheck.Domain
{
    public class Risk
    {
        public Risk()
        {
            this.Address = new Address();
        }
        public string Name { get; set; }
        public string Occupation { get; set; }
        public Address Address { get; set; }
        public string KeptPostcode { get; set; }
    }
}
