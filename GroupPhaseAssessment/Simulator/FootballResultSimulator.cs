using GroupPhaseAssessment.Competition;
using GroupPhaseAssessment.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPhaseAssessment.Simulator
{
    internal class FootballResultSimulator : IResultSimulator
    {
        public void SimulateGame(IMatchup matchup)
        {
            Random generator = new Random();


            FootballTeam homeTeam = (FootballTeam) matchup.GetParticipant1();
            FootballTeam awayTeam = (FootballTeam) matchup.GetParticipant2();

            //First, we start for the home team. We Simulate how many attempts on goal they get, using the average of the Eredivisie, set in FootballAssumptions

            //In order to determine how many attempts a team gets, we calculate their offensive strength:
            double homeOffensiveStrength = homeTeam.OffenseStrength + 0.5 * homeTeam.MidfieldStrength;

            //And the away team's defensive strength:
            double awayDefensiveStrength = awayTeam.DefenseStrength + 0.5 * awayTeam.MidfieldStrength;

            //if these strengths are equal, we want the amount of attempts from FootballAssumptions. However, if there's a difference, the amount of attempts of course changes.
            //A lot of interesting calculations could be made here, but I'm just gonna take it linearly, since this is not the focus of the assessment.
            //We do however add a small bonus for home advantage, just as an example of a simple extra parameter.
            int homeAttempts =
                (int) Math.Round(homeOffensiveStrength / awayDefensiveStrength
                * FootballAssumptions.AverageAttemptsPerMatch / 2 * FootballAssumptions.HomeAdvantageMultiplier);

            //Now we make those attempts by simply checking the offensive stat of a teams strikers vs the opponent's goalkeeper:
            double homeHitChance = 100 * homeTeam.OffenseStrength / awayTeam.GoalKeeperStrength * FootballAssumptions.AverageGoalsPerMatch / FootballAssumptions.AverageAttemptsPerMatch;

            // And we make these attempts!
            for (int i = 0; i < homeAttempts; i++) {
                if (generator.Next(100) < homeHitChance) {
                    //Goaaaaaaal!
                    (matchup as FootballMatchResult).AddHomeGoal();
                }  
            }

            //Now, also for the away team.
            double awayOffensiveStrength = awayTeam.OffenseStrength + 0.5 * awayTeam.MidfieldStrength;
            double homeDefensiveStrength = homeTeam.DefenseStrength + 0.5 * homeTeam.MidfieldStrength;

            int awayAttempts =
                (int)Math.Round(awayOffensiveStrength / homeDefensiveStrength
                * FootballAssumptions.AverageAttemptsPerMatch / 2 / FootballAssumptions.HomeAdvantageMultiplier);

            double awayHitChance = 100 * awayTeam.OffenseStrength / homeTeam.GoalKeeperStrength * FootballAssumptions.AverageGoalsPerMatch / FootballAssumptions.AverageAttemptsPerMatch;

            for (int i = 0; i < awayAttempts; i++) {
                if (generator.Next(100) < awayHitChance) {
                    (matchup as FootballMatchResult).AddAwayGoal();
                }
            }
        }
    }
}
