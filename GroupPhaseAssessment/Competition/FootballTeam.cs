using GroupPhaseAssessment.Results;
using GroupPhaseAssessment.RuleSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPhaseAssessment.Competition
{
    public class FootballTeam : ICompetitionParticipant
    {
        List<IMatchup> matchupResults;
        string teamName;
        IRuleSet ruleSet;

        //A bye is a term from swiss systems: It simply means that you skip a round because there are an odd amount of participants.
        public int AmountOfByes { get; private set; }
        public string ParticipantName { get; private set; }
        public int AmountOfGamesPlayed { get { return matchupResults.Count; } }
        public int AmountOfGamePoints { get { return AmountOfWins * ruleSet.PointsOnWin + AmountOfDraws * ruleSet.PointsOnDraw + AmountOfLosses * ruleSet.PointsOnLoss; } }

        //These are all calculated on the fly, so that the only thing a FootballTeam has to know, is the matches it has played.
        public int AmountOfWins { get { return matchupResults.Count(x => x.GetResultForParticipant(this) == TeamResult.Win); } }
        public int AmountOfDraws { get { return matchupResults.Count(x => x.GetResultForParticipant(this) == TeamResult.Draw); } }
        public int AmountOfLosses { get { return matchupResults.Count(x => x.GetResultForParticipant(this) == TeamResult.Loss); } }
        public int AmountOfGoalsMade { get { return matchupResults.Sum(x => (x as FootballMatchResult).GetGoalsForParticipant(this)); } }
        public int AmountOfGoalsConceded { get { return matchupResults.Sum(x => (x as FootballMatchResult).GetGoalsForOpponent(this)); } }
        public int GetGoalDifference { get { return AmountOfGoalsMade - AmountOfGoalsConceded; } }
        public int GoalKeeperStrength { get; private set; }


        public int DefenseStrength { get; private set; }
        public int MidfieldStrength { get; private set; }
        public int OffenseStrength { get; private set; }

        public FootballTeam(string teamName, IRuleSet ruleSet)
        {
            matchupResults = new List<IMatchup>();
            ParticipantName = teamName;

            this.teamName = teamName;
            this.ruleSet = ruleSet;
        }

        //These strengths are all relative, and one could use the FIFA system for it.
        public void SetTeamStrengths(int goalkeeper, int defense, int midfield, int offense)
        {
            GoalKeeperStrength = goalkeeper;
            DefenseStrength = defense;
            MidfieldStrength = midfield;
            OffenseStrength = offense;
        }

        public List<IMatchup> GetMatchupResults()
        {
            return matchupResults;
        }

        public bool HasPlayedAgainst(ICompetitionParticipant opponent)
        {
            return matchupResults.Any(match => match.HasBeenPlayed && (match.GetParticipant1() == opponent || match.GetParticipant2() == opponent));
        }

        public void AddBye()
        {
            AmountOfByes++;
        }

        public void AddResult(IMatchup result)
        {
            matchupResults.Add(result);
        }
    }
}
