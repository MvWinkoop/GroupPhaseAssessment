using GroupPhaseAssessment.Competition;
using GroupPhaseAssessment.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPhaseAssessment.Results
{
    public enum TeamResult
    {
        Win,
        Draw,
        Loss
    }

    public interface IMatchup
    {
        bool HasBeenPlayed { get; }
        ICompetitionParticipant GetParticipant1();
        ICompetitionParticipant GetParticipant2();
        void SimulateGame(IResultSimulator simulator);
        TeamResult GetResultForParticipant(ICompetitionParticipant participant);
    }
}
