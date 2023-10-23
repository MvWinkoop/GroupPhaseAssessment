using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPhaseAssessment.View
{
    public class ParticipantViewData
    {
        Dictionary<string, string> ParticipantData { get; set; }    

        public ParticipantViewData()
        {
            ParticipantData = new Dictionary<string, string>();
        }

        public void AddProperty(string key, string value)
        {
            ParticipantData.Add(key, value);
        }

        public string[] GetValues()
        {
            return ParticipantData.Values.ToArray();
        }

        public string[] GetKeys()
        {
            return ParticipantData.Keys.ToArray();
        }
    }
}
