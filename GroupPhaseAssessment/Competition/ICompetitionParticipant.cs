using GroupPhaseAssessment.Results;
using GroupPhaseAssessment.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPhaseAssessment.Competition
{
    public interface ICompetitionParticipant
    {
        int AmountOfByes { get; }
        int AmountOfGamesPlayed { get; }
        int AmountOfGamePoints { get; }
        string ParticipantName { get; }
        void AddBye();
        List<IMatchup> GetMatchupResults();
        void AddResult(IMatchup result);
        bool HasPlayedAgainst(ICompetitionParticipant opponent);
        ParticipantViewData GenerateViewData();
    }
}
