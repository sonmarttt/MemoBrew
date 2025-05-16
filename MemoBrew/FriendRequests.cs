using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MemoBrew
{
    public partial class FriendRequests : Form
    {
        private int userID;

        public FriendRequests(int userID)
        {
            LanguageManager.ApplyLanguage();
            InitializeComponent();
            this.userID = userID;
            this.FormClosing += new FormClosingEventHandler(Form_FormClosing);

            LoadFriendRequests();
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Application.OpenForms.Count == 1)
            {
                Application.Exit();
            }
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard(userID);
            CloseAndOpenNewForm(dashboard);
        }

        private void eventButton_Click(object sender, EventArgs e)
        {
            CreateOccasion createOccasion = new CreateOccasion(userID);
            CloseAndOpenNewForm(createOccasion);
        }

        private void friendsButton_Click(object sender, EventArgs e)
        {
            Friends friends = new Friends(userID);
            CloseAndOpenNewForm(friends);
        }

        private void CloseAndOpenNewForm(Form newForm)
        {
            newForm.Show();
            this.Hide();
            newForm.FormClosed += (s, args) => this.Close();
        }

        private void LoadFriendRequests()
        {
            try
            {
                friendInfoPanel.Controls.Clear();

                List<FriendRequestInfo> requests = GetPendingFriendRequests(userID);

                if (requests.Count == 0)
                {
                    Label noRequestsLabel = new Label();
                    noRequestsLabel.Text = "You don't have any pending friend requests.";
                    noRequestsLabel.AutoSize = true;
                    noRequestsLabel.Location = new Point(10, 10);
                    friendInfoPanel.Controls.Add(noRequestsLabel);
                    return;
                }

                int yPos = 10;
                foreach (FriendRequestInfo request in requests)
                {
                    Panel requestPanel = new Panel();
                    requestPanel.Size = new Size(friendInfoPanel.Width - 20, 40);
                    requestPanel.Location = new Point(10, yPos);
                    requestPanel.BorderStyle = BorderStyle.FixedSingle;

                    Label nameLabel = new Label();
                    nameLabel.Text = $"@{request.SenderUsername}";
                    nameLabel.AutoSize = true;
                    nameLabel.Location = new Point(10, 10);
                    requestPanel.Controls.Add(nameLabel);

                    Button acceptButton = new Button();
                    acceptButton.Text = "Accept";
                    acceptButton.Size = new Size(80, 30);
                    acceptButton.Location = new Point(requestPanel.Width - 180, 5);
                    acceptButton.BackColor = Color.SandyBrown;
                    acceptButton.Tag = request.RequestID;
                    acceptButton.Click += new EventHandler(acceptFriendButton_Click);
                    requestPanel.Controls.Add(acceptButton);

                    Button rejectButton = new Button();
                    rejectButton.Text = "Reject";
                    rejectButton.Size = new Size(80, 30);
                    rejectButton.Location = new Point(requestPanel.Width - 90, 5);
                    rejectButton.BackColor = Color.Black;
                    rejectButton.ForeColor = Color.White;
                    rejectButton.Tag = request.RequestID;
                    rejectButton.Click += new EventHandler(rejectFriendButton_Click);
                    requestPanel.Controls.Add(rejectButton);

                    friendInfoPanel.Controls.Add(requestPanel);

                    yPos += 50;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading friend requests: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void acceptFriendButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Tag != null)
            {
                try
                {
                    int requestID = Convert.ToInt32(button.Tag);

                    UpdateFriendRequestStatus(requestID, "accepted");

                    MessageBox.Show("Friend request accepted!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadFriendRequests();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error accepting friend request: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void rejectFriendButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Tag != null)
            {
                try
                {
                    int requestID = Convert.ToInt32(button.Tag);

                    UpdateFriendRequestStatus(requestID, "rejected");

                    MessageBox.Show("Friend request rejected.",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadFriendRequests();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error rejecting friend request: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #region Database Operations

        private class FriendRequestInfo
        {
            public int RequestID { get; set; }
            public int SenderID { get; set; }
            public string SenderUsername { get; set; }
            public string SenderFirstName { get; set; }
            public string SenderLastName { get; set; }
            public DateTime RequestDate { get; set; }
        }

        private List<FriendRequestInfo> GetPendingFriendRequests(int receiverID)
        {
            List<FriendRequestInfo> requests = new List<FriendRequestInfo>();

            string connectionString = Properties.Settings.Default.MemoDataConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT 
                        fr.RequestID,
                        fr.SenderID,
                        u.Username,
                        u.FirstName,
                        u.LastName,
                        fr.RequestDate
                    FROM 
                        FriendRequest fr
                    JOIN 
                        Users u ON fr.SenderID = u.UserID
                    WHERE 
                        fr.ReceiverID = @ReceiverID
                        AND fr.Status = 'pending'
                    ORDER BY 
                        fr.RequestDate DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReceiverID", receiverID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FriendRequestInfo request = new FriendRequestInfo
                            {
                                RequestID = reader.GetInt32(0),
                                SenderID = reader.GetInt32(1),
                                SenderUsername = reader.GetString(2),
                                SenderFirstName = reader.GetString(3),
                                SenderLastName = reader.GetString(4),
                                RequestDate = reader.GetDateTime(5)
                            };

                            requests.Add(request);
                        }
                    }
                }
            }

            return requests;
        }

        private void UpdateFriendRequestStatus(int requestID, string status)
        {
            string connectionString = Properties.Settings.Default.MemoDataConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    UPDATE FriendRequest 
                    SET Status = @Status 
                    WHERE RequestID = @RequestID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestID", requestID);
                    command.Parameters.AddWithValue("@Status", status);

                    command.ExecuteNonQuery();
                }
            }
        }

        #endregion
    }
}