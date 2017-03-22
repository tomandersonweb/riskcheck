using RiskCheck.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Omu.ValueInjecter;
using System.ComponentModel.DataAnnotations;

namespace RiskCheck.Web.ViewModels
{
    [XmlRoot("Risk")]
    public class RiskViewModel
    {
        [XmlElement("Name")]
        [Required]
        public string Name { get; set; }

        [XmlElement("Occupation")]
        [Required]
        public string Occupation { get; set; }

        [XmlElement("Address")]
        [Required]
        public AddressViewModel Address { get; set; }

        [XmlElement("KeptPostcode")]
        public string KeptPostcode { get; set; }
    }
}
