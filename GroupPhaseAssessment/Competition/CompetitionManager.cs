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
        //The competitionManager only holds a list of the teams (ordered by leaderboard position), as well as the IRuleSet.
        //This allows the competitionmanager to also run for other rulesets.
        List<ICompetitionParticipant> participants;
        IRuleSet ruleSet;

        public CompetitionManager(CompetitionStyle style)
        {
            switch (style) {
                case CompetitionStyle.Football:
                    ruleSet = new FootballRuleSet();
                    break;
                default:
                    throw new NotImplementedException();// No other systems have been implemented yet.
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
            //For the rendering
            return ruleSet.GetParticipantDataForView(participants);
        }
    }
}
