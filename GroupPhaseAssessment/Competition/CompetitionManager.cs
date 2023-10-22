using GroupPhaseAssessment.Results;
using GroupPhaseAssessment.RuleSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPhaseAssessment.Competition
{
    public enum CompetitionStyle
    {
        Football,
        Chess
    }

    public class CompetitionManager
    {
        List<ICompetitionParticipant> participants;
        IRuleSet ruleSet;

        public CompetitionManager(CompetitionStyle style)
        {
            switch (style) {
                case CompetitionStyle.Football:
                default:
                    ruleSet = new FootballRuleSet();
                    break;
            }

            participants = new List<ICompetitionParticipant>();
        }

        void RunSingleRound()
        {
            //Run a single round, which means that all teams will play a single match against an opponent.
            //If there are an odd amount of participants, one will have a 'bye'.
            List<IMatchup> matchups = ruleSet.GenerateSingleRoundMatchups(participants);

            foreach (IMatchup matchup in matchups) {
                matchup.SimulateGame(ruleSet.GetSimulator());
            }

            participants = ruleSet.SortCompetitionParticipants(participants, false);
        }

        public void RunFullCompetition()
        {
            PrepareCompetition();
            int amountOfRounds = ruleSet.AmountOfRounds;

            for (var i = 0; i < amountOfRounds; i++) {
                RunSingleRound();
            }
        }

        void PrepareCompetition()
        {
            participants = ruleSet.GenerateParticipants();

        }

        public List<ICompetitionParticipant> GetCompetitionParticipants()
        {
            return participants;
        }

        public List<string[]> GetParticipantDataForView()
        {
            return ruleSet.GetParticipantDataForView(participants);
        }
    }
}
