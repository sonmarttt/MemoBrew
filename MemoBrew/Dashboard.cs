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
            LanguageManager.ApplyLanguage();
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
            int itemHeight = 60;
            int yPos = 40;


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

            Panel separatorLine = new Panel();
            separatorLine.BorderStyle = BorderStyle.FixedSingle;
            separatorLine.Height = 1;
            separatorLine.Width = panel1.Width - 20;
            separatorLine.Location = new Point(10, 35);
            panel1.Controls.Add(separatorLine);

            foreach (var occasion in occasions)
            {
                Panel itemPanel = new Panel();
                itemPanel.Size = new Size(panel1.Width - 20, itemHeight);
                itemPanel.Location = new Point(10, yPos);
                itemPanel.BackColor = Color.White;
                itemPanel.Tag = occasion.OccasionID;
                itemPanel.Cursor = Cursors.Hand;
                itemPanel.Click += OccasionPanel_Click;

                itemPanel.MouseEnter += (sender, e) => { itemPanel.BackColor = Color.FromArgb(245, 245, 245); };
                itemPanel.MouseLeave += (sender, e) => { itemPanel.BackColor = Color.White; };

                Label nameLabel = new Label();
                nameLabel.Text = occasion.Name;
                nameLabel.Location = new Point(5, 5);
                nameLabel.AutoSize = true;
                nameLabel.Tag = occasion.OccasionID;
                nameLabel.Click += OccasionPanel_Click;
                itemPanel.Controls.Add(nameLabel);

                Label dateLabel = new Label();
                dateLabel.Text = occasion.Date.ToShortDateString();
                dateLabel.Location = new Point(itemPanel.Width - 90, 5);
                dateLabel.AutoSize = true;
                dateLabel.Tag = occasion.OccasionID;
                dateLabel.Click += OccasionPanel_Click;
                itemPanel.Controls.Add(dateLabel);

                Button logButton = new Button();
                logButton.Text = "Add Log";
                logButton.Size = new Size(80, 25);
                logButton.Location = new Point(itemPanel.Width - 90, 30);
                logButton.Tag = occasion.OccasionID;
                logButton.Click += OccasionPanel_Click;
                logButton.Cursor = Cursors.Hand;
                itemPanel.Controls.Add(logButton);

                panel1.Controls.Add(itemPanel);

                yPos += itemHeight + 5;
            }
        }

        private void OccasionPanel_Click(object sender, EventArgs e)
        {
            int occasionId;

            if (sender is Control control)
            {
                occasionId = (int)control.Tag;

                OccasionLogs occasionLogs = new OccasionLogs(userId, occasionId);
                CloseAndOpenNewForm(occasionLogs);
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

        private void selectLanguageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (selectLanguageBox.SelectedIndex)
            {
                case 0:
                    LanguageManager.ChangeLanguage("en-US", this);
                    break;
                case 1:
                    LanguageManager.ChangeLanguage("fr-FR", this);
                    break;
                case 2:
                    LanguageManager.ChangeLanguage("es-ES", this);
                    break;
            }
        }
    }
}