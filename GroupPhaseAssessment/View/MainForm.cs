using GroupPhaseAssessment.Competition;
using GroupPhaseAssessment.View;

namespace GroupPhaseAssessment
{
    public partial class MainForm : Form
    {
        CompetitionManager competitionManager;
        public MainForm()
        {
            InitializeComponent();
            competitionManager = new CompetitionManager(CompetitionStyle.Football);
            RenderParticipants();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            competitionManager.PrepareCompetition();
            competitionManager.RunFullCompetition();

            RenderParticipants();
        }

        void RenderParticipants()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            List<ParticipantViewData> ViewData = competitionManager.GetParticipantDataForView();

            if (ViewData.Count == 0)
            {
                throw new ArgumentException();
            }

            foreach (string keyName in ViewData[0].GetKeys())
            {
                //It uses the keyname as the column header. Here, an i18n-implementation, to create a proper column header, would be the best solution.
                //That would be a bit overkill for this project, though.
                dataGridView1.Columns.Add(keyName, keyName);
            }

            foreach (ParticipantViewData participantViewData in ViewData)
            {
                dataGridView1.Rows.Add(participantViewData.GetValues());
            }


            dataGridView1.AutoResizeColumns();
        }
    }
}