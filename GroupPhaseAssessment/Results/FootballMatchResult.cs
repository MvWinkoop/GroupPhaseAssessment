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
        bool hasBeenPlayed;
        public bool HasBeenPlayed { get { return hasBeenPlayed; } }

        int homeGoals; // home = team1
        int awayGoals; // away = team2

        public int HomeGoals { get { return homeGoals; } }
        public int AwayGoals { get { return awayGoals; } }

        FootballRuleSet ruleSet;

        public FootballMatchResult(FootballTeam team1, FootballTeam team2, FootballRuleSet ruleSet)
        {
            this.team1 = team1;
            this.team2 = team2;
            this.ruleSet = ruleSet;
            hasBeenPlayed = false;
        }
        public ICompetitionParticipant GetParticipant1()
        {
            return team1;
        }

        public int GetGoalsForParticipant(ICompetitionParticipant participant)
        {
            if (team1 == participant)
                return homeGoals;
            else
                return awayGoals;
        }

        public int GetGoalsForOpponent(ICompetitionParticipant participant)
        {
            if (team1 == participant)
                return awayGoals;
            else
                return homeGoals;
        }

        public ICompetitionParticipant GetParticipant2()
        {
            return team2;
        }

        public TeamResult GetResultForParticipant(ICompetitionParticipant participant)
        {
            if (participant == team1) {
                if (homeGoals > awayGoals)
                    return TeamResult.Win;
                else if (awayGoals > homeGoals)
                    return TeamResult.Loss;
                else
                    return TeamResult.Draw;
            } else if (participant == team2) {
                if (homeGoals > awayGoals)
                    return TeamResult.Loss;
                else if (awayGoals > homeGoals)
                    return TeamResult.Win;
                else
                    return TeamResult.Draw;
            } else
                return TeamResult.Draw;
        }

        public void SimulateGame(IResultSimulator resultSimulator)
        {
            resultSimulator.SimulateGame(this);

            hasBeenPlayed = true;
            AddResultToTeams();
        }

        void AddResultToTeams()
        {
            team1.AddResult(this);
            team2.AddResult(this);
        }

        public void AddHomeGoal()
        {
            homeGoals++;
        }

        public void AddAwayGoal()
        {
            awayGoals++;
        }
    }
}
