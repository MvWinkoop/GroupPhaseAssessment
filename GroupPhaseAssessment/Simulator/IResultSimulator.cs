using GroupPhaseAssessment.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPhaseAssessment.Simulator
{
    public interface IResultSimulator
    {
        void SimulateGame(IMatchup matchup);
    }
}
