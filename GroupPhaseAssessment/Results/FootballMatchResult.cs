using GroupPhaseAssessment.Competition;
using GroupPhaseAssessment.RuleSets;
using GroupPhaseAssessment.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPhaseAssessment.Results
{
    internal class FootballMatchResult : IMatchup
    {
        FootballTeam team1, team2;
        public bool HasBeenPlayed { get; private set; }


        public int HomeGoals { get; private set; }//home = team1
        public int AwayGoals { get; private set; }//away = team2

        FootballRuleSet ruleSet;

        public FootballMatchResult(FootballTeam team1, FootballTeam team2, FootballRuleSet ruleSet)
        {
            this.team1 = team1;
            this.team2 = team2;
            this.ruleSet = ruleSet;
            HasBeenPlayed = false;
        }
        public ICompetitionParticipant GetParticipant1()
        {
            return team1;
        }

        public ICompetitionParticipant GetParticipant2()
        {
            return team2;
        }

        public int GetGoalsForParticipant(ICompetitionParticipant participant)
        {
            if (team1 == participant)
                return HomeGoals;
            else
                return AwayGoals;
        }

        public int GetGoalsForOpponent(ICompetitionParticipant participant)
        {
            if (team1 == participant)
                return AwayGoals;
            else
                return HomeGoals;
        }


        public TeamResult GetResultForParticipant(ICompetitionParticipant participant)
        {
            if (participant == team1) {
                if (HomeGoals > AwayGoals)
                    return TeamResult.Win;
                else if (AwayGoals > HomeGoals)
                    return TeamResult.Loss;
                else
                    return TeamResult.Draw;
            } else if (participant == team2) {
                if (HomeGoals > AwayGoals)
                    return TeamResult.Loss;
                else if (AwayGoals > HomeGoals)
                    return TeamResult.Win;
                else
                    return TeamResult.Draw;
            } else
                return TeamResult.Draw;
        }

        public void SimulateGame(IResultSimulator resultSimulator)
        {
            resultSimulator.SimulateGame(this);

            HasBeenPlayed = true;
            AddResultToTeams();
        }

        void AddResultToTeams()
        {
            team1.AddResult(this);
            team2.AddResult(this);
        }

        public void AddHomeGoal()
        {
            HomeGoals++;
        }

        public void AddAwayGoal()
        {
            AwayGoals++;
        }
    }
}
