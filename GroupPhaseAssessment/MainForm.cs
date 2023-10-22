using GroupPhaseAssessment.Competition;

namespace GroupPhaseAssessment
{
    public partial class MainForm : Form
    {
        CompetitionManager competitionManager;
        public MainForm()
        {
            InitializeComponent();
            competitionManager = new CompetitionManager(CompetitionStyle.Football);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            competitionManager.RunFullCompetition();

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.Columns.Add("TeamName", "Team Name");
            dataGridView1.Columns.Add("GamesPlayed", "Played");
            dataGridView1.Columns.Add("GamesWon", "Win");
            dataGridView1.Columns.Add("GamesDrawn", "Draw");
            dataGridView1.Columns.Add("GamesLost", "Loss");
            dataGridView1.Columns.Add("GoalsMade", "For");
            dataGridView1.Columns.Add("GoalsConceded", "Against");
            dataGridView1.Columns.Add("GoalDifference", "-/+");
            dataGridView1.Columns.Add("GamePoints", "Points");

            competitionManager.GetParticipantDataForView().ForEach(x =>
                dataGridView1.Rows.Add(x)
            );

            dataGridView1.AutoResizeColumns();
        }
    }
}