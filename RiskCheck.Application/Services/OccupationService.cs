using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Threading.Tasks;
using RiskCheck.Domain;

namespace RiskCheck.Application
{
    public class OccupationService : IOccupationService
    {
        private readonly string _xml = @"<Occupations>
                        <Occupation>
                          <Desc>Chef</Desc>
                          <Action>Refer</Action>
                        </Occupation>
                        <Occupation>
                          <Desc>Footballer</Desc>
                          <Action>Accept</Action>
                        </Occupation>
                        <Occupation>
                          <Desc>IT Contractor</Desc>
                          <Action>Decline</Action>
                        </Occupation>
                    </Occupations> ";

        private readonly List<Occupation> _occupations;

        public OccupationService()
        {
            XmlSerializer x = new XmlSerializer(typeof(List<Occupation>), new XmlRootAttribute("Occupations"));
            _occupations = (List<Occupation>)x.Deserialize(new StringReader(_xml));
        }

        public async Task<ActionEnum> GetActionForOccupation(string occupation)
        {
            // If the occupation is not present we should decline them.
            ActionEnum actionEnum = ActionEnum.Decline;

            var result = _occupations.Where(x => x.Desc == occupation).Select(x => x.Action).SingleOrDefault();

            Enum.TryParse(result, true, out actionEnum);

            return actionEnum;
        }


    }
}
