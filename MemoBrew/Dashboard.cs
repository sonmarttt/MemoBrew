using System;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;

namespace MemoBrew
{
    public partial class Dashboard : Form
    {
        private int userId;

        public Dashboard(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            this.FormClosing += new FormClosingEventHandler(Form_FormClosing);

            ConfigurePanelForOccasions();

            LoadUserData();
            LoadUserOccasions();
        }

        private void ConfigurePanelForOccasions()
        {
            panel1.AutoScroll = true;

            foreach (Control control in panel1.Controls)
            {
                if (control is Label)
                {
                    control.Visible = false;
                }
            }
        }

        private void LoadUserData()
        {
            try
            {
                MemoDataDataSetTableAdapters.UsersTableAdapter usersAdapter =
                    new MemoDataDataSetTableAdapters.UsersTableAdapter();
                MemoDataDataSet.UsersDataTable userTable = usersAdapter.GetData();
                MemoDataDataSet.UsersRow userRow = userTable.FindByUserID(userId);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user data: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Application.OpenForms.Count == 1)
            {
                Application.Exit();
            }
        }

        private void eventButton_Click(object sender, EventArgs e)
        {
            CreateOccasion createOccasion = new CreateOccasion(userId);
            CloseAndOpenNewForm(createOccasion);
        }

        private void friendsButton_Click(object sender, EventArgs e)
        {
            Friends friends = new Friends(userId);
            CloseAndOpenNewForm(friends);
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            LoadUserData();
            LoadUserOccasions();
        }

        private void CloseAndOpenNewForm(Form newForm)
        {
            newForm.Show();
            this.Hide();
            newForm.FormClosed += (s, args) => this.Close();
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You have logged out successfully.", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Welcome welcome = new Welcome();
            CloseAndOpenNewForm(welcome);
        }

        private void LoadUserOccasions()
        {
            try
            {
                ClearOccasionDisplay();

                MemoDataDataSetTableAdapters.QueriesTableAdapter queriesAdapter =
                    new MemoDataDataSetTableAdapters.QueriesTableAdapter();
                int occasionCount = (int)queriesAdapter.GetUserOccasionCount(userId);

                if (occasionCount > 0)
                {
                    MemoDataDataSetTableAdapters.OccasionTableAdapter occasionAdapter =
                        new MemoDataDataSetTableAdapters.OccasionTableAdapter();
                    MemoDataDataSet.OccasionDataTable allOccasions = occasionAdapter.GetData();

                    MemoDataDataSetTableAdapters.OccasionParticipantsTableAdapter participantsAdapter =
                        new MemoDataDataSetTableAdapters.OccasionParticipantsTableAdapter();
                    MemoDataDataSet.OccasionParticipantsDataTable participants = participantsAdapter.GetData();

                    var userOccasionIds = participants.AsEnumerable()
                        .Where(p => p.UserID == userId)
                        .Select(p => p.OccasionID);

                    var userOccasions = allOccasions.AsEnumerable()
                        .Where(o => userOccasionIds.Contains(o.OccasionID))
                        .OrderBy(o => o.Date);

                    if (userOccasions.Any())
                    {
                        DisplayAllOccasions(userOccasions);
                    }
                    else
                    {
                        DisplayNoOccasionsMessage();
                    }
                }
                else
                {
                    DisplayNoOccasionsMessage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading occasions: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayAllOccasions(IOrderedEnumerable<MemoDataDataSet.OccasionRow> occasions)
        {
            int itemHeight = 80;
            int panelWidth = panel1.Width - 25;
            int yPos = 10;

            Label headerNameLabel = new Label();
            headerNameLabel.Text = "Occasion Name";
            headerNameLabel.Font = new Font(headerNameLabel.Font, FontStyle.Bold);
            headerNameLabel.Location = new Point(10, yPos);
            headerNameLabel.Size = new Size(200, 20);
            panel1.Controls.Add(headerNameLabel);

            Label headerDateLabel = new Label();
            headerDateLabel.Text = "Date";
            headerDateLabel.Font = new Font(headerDateLabel.Font, FontStyle.Bold);
            headerDateLabel.Location = new Point(panelWidth - 110, yPos);
            headerDateLabel.Size = new Size(100, 20);
            headerDateLabel.TextAlign = ContentAlignment.TopRight;
            panel1.Controls.Add(headerDateLabel);

            yPos += 25;

            foreach (var occasion in occasions)
            {
                Panel occasionPanel = new Panel();
                occasionPanel.Size = new Size(panelWidth, itemHeight);
                occasionPanel.Location = new Point(0, yPos);
                occasionPanel.BorderStyle = BorderStyle.FixedSingle;
                occasionPanel.BackColor = Color.White;

                Label nameLabel = new Label();
                nameLabel.Text = occasion.Name;
                nameLabel.Font = new Font(nameLabel.Font, FontStyle.Regular);
                nameLabel.Location = new Point(10, 5);
                nameLabel.Size = new Size(panelWidth - 130, 20);
                nameLabel.AutoEllipsis = true;
                occasionPanel.Controls.Add(nameLabel);

                Label dateLabel = new Label();
                dateLabel.Text = occasion.Date.ToShortDateString();
                dateLabel.Location = new Point(panelWidth - 120, 5);
                dateLabel.Size = new Size(100, 20);
                dateLabel.TextAlign = ContentAlignment.TopRight;
                occasionPanel.Controls.Add(dateLabel);

                if (!occasion.IsLocationNull())
                {
                    string locationInfo = occasion.Location;
                    if (locationInfo.Length > 30)
                    {
                        locationInfo = locationInfo.Substring(0, 27) + "...";
                    }

                    Label locationLabel = new Label();
                    locationLabel.Text = "📍 " + locationInfo;
                    locationLabel.Location = new Point(10, 20);
                    locationLabel.Size = new Size(panelWidth - 20, 15);
                    locationLabel.Font = new Font(locationLabel.Font.FontFamily, 7.5f);
                    locationLabel.ForeColor = Color.Gray;
                    occasionPanel.Controls.Add(locationLabel);
                }

                panel1.Controls.Add(occasionPanel);

                yPos += itemHeight + 3;
            }
        }

        private void ClearOccasionDisplay()
        {
            panel1.Controls.Clear();
        }

        private void DisplayNoOccasionsMessage()
        {
            Label noOccasionsLabel = new Label();
            noOccasionsLabel.Text = "No upcoming occasions";
            noOccasionsLabel.Location = new Point(10, 10);
            noOccasionsLabel.Size = new Size(panel1.Width - 20, 30);
            noOccasionsLabel.TextAlign = ContentAlignment.MiddleCenter;
            panel1.Controls.Add(noOccasionsLabel);
        }
    }
}