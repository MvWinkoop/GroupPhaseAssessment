using GroupPhaseAssessment.Competition;
using GroupPhaseAssessment.Results;
using GroupPhaseAssessment.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPhaseAssessment.RuleSets
{
    public enum CompetitionStyle
    {
        SingleRoundRobin,
        DoubleRoundRobin
    }
    public interface IRuleSet
    {
        CompetitionStyle CompetitionStyle { get; }
        int AmountOfParticipants { get; }
        int AmountOfRounds { get; }
        int PointsOnLoss { get; }
        int PointsOnDraw { get; }
        int PointsOnWin { get; }
        List<ICompetitionParticipant> SortCompetitionParticipants(List<ICompetitionParticipant> participants, bool headToHeadWasSorted);
        List<IMatchup> GenerateSingleRoundMatchups(List<ICompetitionParticipant> participants);
        IResultSimulator GetSimulator();
        List<ICompetitionParticipant> GenerateParticipants();
    }
}
