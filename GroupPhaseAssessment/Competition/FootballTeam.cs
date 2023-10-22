﻿using GroupPhaseAssessment.Results;
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
        int amountOfByes;
        List<IMatchup> matchupResults;
        string teamName;
        IRuleSet ruleSet;
        int goalKeeperStrength, defenseStrength, midfieldStrength, offenseStrength;

        public int AmountOfByes { get { return amountOfByes; } }
        public string ParticipantName { get { return teamName; } }
        public int AmountOfGamesPlayed { get { return matchupResults.Count; } }
        public int AmountOfGamePoints { get { return AmountOfWins * ruleSet.PointsOnWin + AmountOfDraws * ruleSet.PointsOnDraw + AmountOfLosses * ruleSet.PointsOnLoss; } }
        public int AmountOfWins { get { return matchupResults.Count(x => x.GetResultForParticipant(this) == TeamResult.Win); } }
        public int AmountOfDraws { get { return matchupResults.Count(x => x.GetResultForParticipant(this) == TeamResult.Draw); } }
        public int AmountOfLosses { get { return matchupResults.Count(x => x.GetResultForParticipant(this) == TeamResult.Loss); } }
        public int AmountOfGoalsMade { get { return matchupResults.Sum(x => (x as FootballMatchResult).GetGoalsForParticipant(this)); } }
        public int AmountOfGoalsConceded { get { return matchupResults.Sum(x => (x as FootballMatchResult).GetGoalsForOpponent(this)); } }
        public int GetGoalDifference { get { return AmountOfGoalsMade - AmountOfGoalsConceded; } }
        public int GetGoalKeeperStrength { get { return goalKeeperStrength; } }
        public int GetDefenseStrength { get { return defenseStrength; } }
        public int GetMidfieldStrength { get { return midfieldStrength; } }
        public int GetOffenseStrength { get { return offenseStrength; } }

        public FootballTeam(string teamName, IRuleSet ruleSet)
        {
            amountOfByes = 0;
            matchupResults = new List<IMatchup>();

            this.teamName = teamName;
            this.ruleSet = ruleSet;
        }

        public void SetTeamStrengths(int goalkeeper, int defense, int midfield, int offense)
        {
            goalKeeperStrength = goalkeeper;
            defenseStrength = defense;
            midfieldStrength = midfield;
            offenseStrength = offense;
        }

        public List<IMatchup> GetMatchupResults()
        {
            return matchupResults;
        }

        public TeamResult? GetResultAgainst(ICompetitionParticipant team)
        {
            if (matchupResults.Any(x => x.GetParticipant1() == team || x.GetParticipant2() == team)){
                
            }

            return null;
        }

        public bool HasPlayedAgainst(ICompetitionParticipant opponent)
        {
            return matchupResults.Any(match => match.HasBeenPlayed && (match.GetParticipant1() == opponent || match.GetParticipant2() == opponent));
        }


        public void AddBye()
        {
            amountOfByes++;
        }

        public void AddResult(IMatchup result)
        {
            matchupResults.Add(result);
        }
    }
}