using System;
using System.Windows.Forms;
using System.Drawing;

namespace MemoBrew
{
    public partial class CreateOccasion : Form
    {
        private int userID;
        private int newOccasionID;

        public CreateOccasion(int userID)
        {
            LanguageManager.ApplyLanguage();
            InitializeComponent();

            this.AutoScroll = true;

            this.userID = userID;
            this.FormClosing += new FormClosingEventHandler(Form_FormClosing);

            addFriendsButton.Enabled = false;
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
        }

        private void friendsButton_Click(object sender, EventArgs e)
        {
            Friends friends = new Friends(userID);
            CloseAndOpenNewForm(friends);
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard(userID);
            CloseAndOpenNewForm(dashboard);
        }

        private void CloseAndOpenNewForm(Form newForm)
        {
            newForm.Show();
            this.Hide();
            newForm.FormClosed += (s, args) => this.Close();
        }

        private void createOccasionButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtOccasionName.Text))
                {
                    MessageBox.Show("Please enter an occasion name.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (endDatePick.Value < DateTime.Today)
                {
                    MessageBox.Show("Please select a future date.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string connectionString = Properties.Settings.Default.MemoDataConnectionString;
                using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    connection.Open();

                    string insertSql = @"
                INSERT INTO Occasion (Name, Location, Description, Date, StartTime, EndTime, CreatorID)
                VALUES (@Name, @Location, @Description, @CurrentDate, NULL, NULL, @CreatorID);
                SELECT SCOPE_IDENTITY();";

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(insertSql, connection);

                    cmd.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 100).Value = txtOccasionName.Text;

                    if (string.IsNullOrWhiteSpace(locationTextBox.Text))
                        cmd.Parameters.Add("@Location", System.Data.SqlDbType.NVarChar, 255).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Location", System.Data.SqlDbType.NVarChar, 255).Value = locationTextBox.Text;

                    if (string.IsNullOrWhiteSpace(descriptionTextBox.Text))
                        cmd.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar).Value = descriptionTextBox.Text;

                    cmd.Parameters.Add("@CurrentDate", System.Data.SqlDbType.Date).Value = endDatePick.Value.Date;
                    cmd.Parameters.Add("@CreatorID", System.Data.SqlDbType.Int).Value = this.userID;

                    newOccasionID = Convert.ToInt32(cmd.ExecuteScalar());

                    string participantSql = @"
                INSERT INTO OccasionParticipants (OccasionID, UserID, Status)
                VALUES (@OccasionID, @UserID, 'going')";

                    cmd = new System.Data.SqlClient.SqlCommand(participantSql, connection);
                    cmd.Parameters.Add("@OccasionID", System.Data.SqlDbType.Int).Value = newOccasionID;
                    cmd.Parameters.Add("@UserID", System.Data.SqlDbType.Int).Value = this.userID;
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Occasion created successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    addFriendsButton.Enabled = true;

                    DialogResult addFriendsResult = MessageBox.Show("Do you want to add friends to this occasion?",
                        "Add Friends", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (addFriendsResult == DialogResult.Yes)
                    {
                        AddFriendsOccasion addFriendsOccasionForm = new AddFriendsOccasion(userID, newOccasionID);
                        addFriendsOccasionForm.ShowDialog();
                    }

                    Dashboard dashboard = new Dashboard(this.userID);
                    CloseAndOpenNewForm(dashboard);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating occasion: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addFriendsButton_Click(object sender, EventArgs e)
        {
            if (newOccasionID > 0)
            {
                AddFriendsOccasion addFriendsOccasionForm = new AddFriendsOccasion(userID, newOccasionID);
                addFriendsOccasionForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please create an occasion first before adding friends.",
                    "Occasion Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}