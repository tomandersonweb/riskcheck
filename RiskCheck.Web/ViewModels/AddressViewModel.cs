using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RiskCheck.Web.ViewModels
{
    [XmlRoot("Address")]
    public class AddressViewModel
    {
        public string Address1 { get; set; }

        public string Postcode { get; set; }
    }
}
