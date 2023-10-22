using GroupPhaseAssessment.Competition;

namespace GroupPhaseTests
{
    [TestClass]
    public class ManualTests
    {
        [TestMethod]
        public void CheckMatchups()
        {
            CompetitionManager manager = new CompetitionManager(CompetitionStyle.Football);
            manager.RunFullCompetition();
        }
    }
}