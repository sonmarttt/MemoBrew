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

            panel1.Controls.Clear();
            panel1.AutoScroll = true;

            LoadUserData();
            LoadUserOccasions();
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
            int itemHeight = 30; // Smaller height to fit more
            int yPos = 40; // Start below the header

            // Replace any existing headers with our own
            Label headerName = new Label();
            headerName.Text = "Occasion Name";
            headerName.Font = new Font(headerName.Font, FontStyle.Bold);
            headerName.Location = new Point(10, 10);
            headerName.AutoSize = true;
            panel1.Controls.Add(headerName);

            Label headerDate = new Label();
            headerDate.Text = "Date";
            headerDate.Font = new Font(headerDate.Font, FontStyle.Bold);
            headerDate.Location = new Point(panel1.Width - 100, 10);
            headerDate.AutoSize = true;
            panel1.Controls.Add(headerDate);

            // Draw a separator line
            Panel separatorLine = new Panel();
            separatorLine.BorderStyle = BorderStyle.FixedSingle;
            separatorLine.Height = 1;
            separatorLine.Width = panel1.Width - 20;
            separatorLine.Location = new Point(10, 35);
            panel1.Controls.Add(separatorLine);

            foreach (var occasion in occasions)
            {
                // Create a panel for this occasion
                Panel itemPanel = new Panel();
                itemPanel.Size = new Size(panel1.Width - 20, itemHeight);
                itemPanel.Location = new Point(10, yPos);
                itemPanel.BackColor = Color.White;

                // Name label
                Label nameLabel = new Label();
                nameLabel.Text = occasion.Name;
                nameLabel.Location = new Point(5, 5);
                nameLabel.AutoSize = true;
                itemPanel.Controls.Add(nameLabel);

                // Date label
                Label dateLabel = new Label();
                dateLabel.Text = occasion.Date.ToShortDateString();
                dateLabel.Location = new Point(itemPanel.Width - 90, 5);
                dateLabel.AutoSize = true;
                itemPanel.Controls.Add(dateLabel);

                // Add to panel
                panel1.Controls.Add(itemPanel);

                // Increment position for next item
                yPos += itemHeight + 5;
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