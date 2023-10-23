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
    //The class that holds almost all the logic for the poule system
    public class FootballRuleSet : IRuleSet
    {
        public CompetitionStyle CompetitionStyle { get { return CompetitionStyle.SingleRoundRobin; } } //I didn't do an implementation for a double round robin
        public int AmountOfParticipants { get { return 4; } }
        public int PointsOnLoss { get { return 0; } }
        public int PointsOnDraw { get { return 1; } }
        public int PointsOnWin { get { return 3; } }

        FootballResultSimulator simulator;

        public FootballRuleSet()
        {
            simulator = new FootballResultSimulator();
        }

        public IResultSimulator GetSimulator()
        {
            return simulator;
        }

        public int AmountOfRounds { get { return AmountOfParticipants % 2 == 0 ? AmountOfParticipants - 1 : AmountOfParticipants; } } // 4 participants means 3 rounds, 10 participants means 9 rounds etc. An odd number of participants means the same amount of rounds as participants.

        //The trick in the sorting is that first, we take an empty list, and add the teams to it that would have the same position in the standings.
        //Then, we sort these out, add them to the empty list, and then add all the entries that weren't in the list yet, and sort that.
        //Because the original order is maintained when calling a sort where there are unresolved issues, the order that exists after sorting is the correct one.
        public List<ICompetitionParticipant> SortCompetitionParticipants(List<ICompetitionParticipant> participants, bool HeadToHeadWasSorted = false)
        {
            if (!HeadToHeadWasSorted) {
                //First, we sort the teams that would have an equal position based on the criteria game points, games played and goals.
                //This can be done by grouping these, and then sorting these first.
                var groupedTeams = participants.GroupBy(team => new
                {
                    points = team.AmountOfGamePoints,
                    games = team.AmountOfGamesPlayed,
                    goalDiff = (team as FootballTeam).GetGoalDifference,
                    goalsMade = (team as FootballTeam).AmountOfGoalsMade
                })
                .Where(grouping => grouping.Count() > 1)
                .ToList();

                List<List<ICompetitionParticipant>> sortedListsOfTeamsWithSamePosition = new List<List<ICompetitionParticipant>>();

                //If there are entries in GroupedTeams, we need to resolve these.
                foreach (var grouping in groupedTeams) {
                    sortedListsOfTeamsWithSamePosition.Add(SortUsingHeadToHead(grouping.ToList()));
                }
                participants = sortedListsOfTeamsWithSamePosition
                    .SelectMany(x => x)
                    .ToList()
                    .Union(participants)
                    .ToList();
            }
            //Then, we sort by game points, then by amount of games played (in case the group has an odd number of teams).
            //Then goal difference, then goals made. Goals conceded can be ignored, since that would be the same as well.
            participants = participants.AsQueryable()
                .OrderByDescending(team => team.AmountOfGamePoints)
                .ThenBy(team => team.AmountOfGamesPlayed)
                .ThenByDescending(team => (team as FootballTeam).GetGoalDifference)
                .ThenByDescending(team => (team as FootballTeam).AmountOfGoalsMade)
                .ToList();

            return participants;
        }

        List<ICompetitionParticipant> SortUsingHeadToHead(List<ICompetitionParticipant> teams)
        {
            //In order to sort teams by head to head, we have to take the subset of matches that have been played between these teams, and use those to sort again.
            //More than two teams could have the same position, so we have to do this properly.
            //We cheat a little for this: We grab the teams, create a new 'competition', enter the results for this competition, and use the sorter.
            List<ICompetitionParticipant> participantCopies = new List<ICompetitionParticipant>();

            foreach (ICompetitionParticipant team in teams) {
                FootballTeam teamCopy = new FootballTeam(team.ParticipantName, this);
                team.GetMatchupResults()
                    .Where(r => teams.Contains(r.GetParticipant1()) && teams.Contains(r.GetParticipant2()))
                    .ToList()
                    .ForEach(r => teamCopy.AddResult(r));

                participantCopies.Add(teamCopy);
            }

            participantCopies = SortCompetitionParticipants(participantCopies, true);

            List<ICompetitionParticipant> sortedList = new List<ICompetitionParticipant>();
            
            for (int i = 0; i < participantCopies.Count; i++) {
                sortedList.Add(teams.Single(x => x.ParticipantName == participantCopies[i].ParticipantName));
            }

            return sortedList;
        }

        public List<ICompetitionParticipant> GenerateParticipants()
        {
            //I picked this poule because I have very fond memories of this tournament. Except for Arshavin.
            List<ICompetitionParticipant> participants = new List<ICompetitionParticipant>();
            FootballTeam Nederland = new FootballTeam("Nederland", this);
            Nederland.SetTeamStrengths(75, 82, 82, 83);
            participants.Add(Nederland);

            FootballTeam Frankrijk = new FootballTeam("Frankrijk", this);
            Frankrijk.SetTeamStrengths(87, 82, 83, 84);
            participants.Add(Frankrijk);

            FootballTeam Italië = new FootballTeam("Italië", this);
            Italië.SetTeamStrengths(88, 82, 84, 83);
            participants.Add(Italië);

            FootballTeam Roemenië = new FootballTeam("Roemenië", this);
            Roemenië.SetTeamStrengths(72, 69, 74, 70);
            participants.Add(Roemenië);
            return participants;

        }

        public List<IMatchup> GenerateSingleRoundMatchups(List<ICompetitionParticipant> participants)
        {
            List<IMatchup> matches = new List<IMatchup>();

            List<FootballTeam> unmatchedTeams = participants.Cast<FootballTeam>().ToList();

            //Okay, so we have our teams. First, we check if a bye is necessary, and if so, who gets the bye. We just pick the first team that hasn't had one yet.
            if (unmatchedTeams.Count % 2 == 1) {
                //We have a bye
                int i = 0;
                while (unmatchedTeams[i].AmountOfByes != 0) {
                    i++;
                }

                unmatchedTeams[i].AddBye();
                unmatchedTeams.RemoveAt(i); // We don't need the team to determine the other matchups
            }

            while (unmatchedTeams.Count > 0) {
                int i = 1;
                //Simply find the first team that you haven't played yet.
                while (unmatchedTeams[0].HasPlayedAgainst(unmatchedTeams[i])) {
                    i++;
                }

                matches.Add(new FootballMatchResult(unmatchedTeams[0], unmatchedTeams[i], this));
                unmatchedTeams.RemoveAt(i);
                unmatchedTeams.RemoveAt(0);
            }

            return matches;
        }

        public List<string[]> GetParticipantDataForView(List<ICompetitionParticipant> participants)
        {
            List<FootballTeam> teams = participants.Cast<FootballTeam>().ToList();
            List<string[]> res = new List<string[]>();

            foreach (var team in teams) {
                res.Add(new string[] {team.ParticipantName, team.AmountOfGamesPlayed.ToString(), team.AmountOfWins.ToString(),
                                        team.AmountOfDraws.ToString(), team.AmountOfLosses.ToString(), team.AmountOfGoalsMade.ToString(),
                                        team.AmountOfGoalsConceded.ToString(), team.GetGoalDifference.ToString(), team.AmountOfGamePoints.ToString()});
            }

            return res;
        }
    }
}
