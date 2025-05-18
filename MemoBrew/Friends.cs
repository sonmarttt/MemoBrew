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
    public partial class Friends : Form
    {
        private int userID;

        public Friends(int userID)
        {
            LanguageManager.ApplyLanguage();
            InitializeComponent();
            this.userID = userID;
            this.FormClosing += new FormClosingEventHandler(Form_FormClosing);

            friendInfoPanel.AutoScroll = true;

            LoadFriendsList();
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
            CreateOccasion createOccasion = new CreateOccasion(userID);
            CloseAndOpenNewForm(createOccasion);
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

        private void LoadFriendsList()
        {
            try
            {
                friendInfoPanel.Controls.Clear();

                List<FriendInfo> friends = GetUserFriends(userID);

                if (friends.Count == 0)
                {
                    Label noFriendsLabel = new Label();
                    noFriendsLabel.Text = "You don't have any friends yet. Search for users to add friends.";
                    noFriendsLabel.AutoSize = true;
                    noFriendsLabel.Location = new Point(10, 10);
                    friendInfoPanel.Controls.Add(noFriendsLabel);
                    return;
                }

                Label headerName = new Label();
                headerName.Text = "Friend Name";
                headerName.Font = new Font(headerName.Font, FontStyle.Bold);
                headerName.Location = new Point(10, 10);
                headerName.AutoSize = true;
                friendInfoPanel.Controls.Add(headerName);

                Label headerUsername = new Label();
                headerUsername.Text = "Username";
                headerUsername.Font = new Font(headerUsername.Font, FontStyle.Bold);
                headerUsername.Location = new Point(friendInfoPanel.Width - 150, 10);
                headerUsername.AutoSize = true;
                friendInfoPanel.Controls.Add(headerUsername);

                Panel separatorLine = new Panel();
                separatorLine.BorderStyle = BorderStyle.FixedSingle;
                separatorLine.Height = 1;
                separatorLine.Width = friendInfoPanel.Width - 20;
                separatorLine.Location = new Point(10, 35);
                friendInfoPanel.Controls.Add(separatorLine);

                int yPos = 45;
                foreach (FriendInfo friend in friends)
                {
                    Panel friendPanel = new Panel();
                    friendPanel.BorderStyle = BorderStyle.None;
                    friendPanel.BackColor = Color.White;
                    friendPanel.Size = new Size(friendInfoPanel.Width - 30, 60);
                    friendPanel.Location = new Point(10, yPos);

                    friendPanel.MouseEnter += (sender, e) => { friendPanel.BackColor = Color.FromArgb(245, 245, 245); };
                    friendPanel.MouseLeave += (sender, e) => { friendPanel.BackColor = Color.White; };

                    Label nameLabel = new Label();
                    nameLabel.Text = $"{friend.FirstName} {friend.LastName}";
                    nameLabel.Location = new Point(10, 10);
                    nameLabel.AutoSize = true;
                    nameLabel.Font = new Font(nameLabel.Font.FontFamily, 10);
                    friendPanel.Controls.Add(nameLabel);

                    Label usernameLabel = new Label();
                    usernameLabel.Text = $"@{friend.Username}";
                    usernameLabel.Location = new Point(friendPanel.Width - 140, 10);
                    usernameLabel.AutoSize = true;
                    usernameLabel.ForeColor = Color.Gray;
                    friendPanel.Controls.Add(usernameLabel);

                    Label dateLabel = new Label();
                    dateLabel.Text = $"Friends since: {friend.FriendsSince.ToShortDateString()}";
                    dateLabel.Location = new Point(10, 35);
                    dateLabel.AutoSize = true;
                    dateLabel.Font = new Font(dateLabel.Font.FontFamily, 8);
                    dateLabel.ForeColor = Color.DarkGray;
                    friendPanel.Controls.Add(dateLabel);

                    Panel itemSeparator = new Panel();
                    itemSeparator.BorderStyle = BorderStyle.FixedSingle;
                    itemSeparator.Height = 1;
                    itemSeparator.Width = friendInfoPanel.Width - 40;
                    itemSeparator.Location = new Point(20, yPos + 59);
                    itemSeparator.BackColor = Color.LightGray;
                    friendInfoPanel.Controls.Add(itemSeparator);

                    friendInfoPanel.Controls.Add(friendPanel);

                    yPos += 65;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading friends list: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Database Operations

        private class FriendInfo
        {
            public int UserID { get; set; }
            public string Username { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime FriendsSince { get; set; }
        }

        private int? FindUserIDByUsername(string username)
        {
            string connectionString = Properties.Settings.Default.MemoDataConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT UserID FROM Users WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }

                    return null;
                }
            }
        }

        private bool CheckIfAlreadyFriends(int userID, int friendID)
        {
            string connectionString = Properties.Settings.Default.MemoDataConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT COUNT(*) FROM Friends 
                    WHERE (UserID = @UserID AND FriendID = @FriendID) 
                    OR (UserID = @FriendID AND FriendID = @UserID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@FriendID", friendID);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private bool CheckPendingFriendRequest(int senderID, int receiverID)
        {
            string connectionString = Properties.Settings.Default.MemoDataConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT COUNT(*) FROM FriendRequest 
                    WHERE (SenderID = @SenderID AND ReceiverID = @ReceiverID)
                    AND Status = 'pending'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SenderID", senderID);
                    command.Parameters.AddWithValue("@ReceiverID", receiverID);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private void CreateFriendRequest(int senderID, int receiverID)
        {
            string connectionString = Properties.Settings.Default.MemoDataConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    INSERT INTO FriendRequest (SenderID, ReceiverID, Status, RequestDate)
                    VALUES (@SenderID, @ReceiverID, 'pending', GETDATE())";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SenderID", senderID);
                    command.Parameters.AddWithValue("@ReceiverID", receiverID);

                    command.ExecuteNonQuery();
                }
            }
        }

        private List<FriendInfo> GetUserFriends(int userID)
        {
            List<FriendInfo> friends = new List<FriendInfo>();

            string connectionString = Properties.Settings.Default.MemoDataConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT 
                        f.FriendID, 
                        u.Username,
                        u.FirstName,
                        u.LastName,
                        f.FriendsSince
                    FROM 
                        Friends f
                    JOIN 
                        Users u ON f.FriendID = u.UserID
                    WHERE 
                        f.UserID = @UserID
                    ORDER BY 
                        u.FirstName, u.LastName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FriendInfo friend = new FriendInfo
                            {
                                UserID = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                FirstName = reader.GetString(2),
                                LastName = reader.GetString(3),
                                FriendsSince = reader.GetDateTime(4)
                            };

                            friends.Add(friend);
                        }
                    }
                }
            }

            return friends;
        }

        #endregion

        private void searchFriend_Click(object sender, EventArgs e)
        {
            string usernameToSearch = searchtextBox.Text.Trim();

            if (string.IsNullOrEmpty(usernameToSearch))
            {
                MessageBox.Show("Please enter a username to search for.",
                    "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                MemoDataDataSetTableAdapters.UsersTableAdapter usersAdapter =
                    new MemoDataDataSetTableAdapters.UsersTableAdapter();
                MemoDataDataSet.UsersDataTable userTable = usersAdapter.GetData();
                MemoDataDataSet.UsersRow currentUser = userTable.FindByUserID(userID);

                if (currentUser != null && currentUser.Username.Equals(usernameToSearch, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("You cannot add yourself as a friend.",
                        "Friend Request Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int? foundUserID = FindUserIDByUsername(usernameToSearch);

                if (!foundUserID.HasValue)
                {
                    MessageBox.Show($"No user found with username '{usernameToSearch}'.",
                        "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                bool alreadyFriends = CheckIfAlreadyFriends(userID, foundUserID.Value);
                if (alreadyFriends)
                {
                    MessageBox.Show($"You are already friends with {usernameToSearch}.",
                        "Friend Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                bool pendingRequest = CheckPendingFriendRequest(userID, foundUserID.Value);
                if (pendingRequest)
                {
                    MessageBox.Show($"You already have a pending friend request to {usernameToSearch}.",
                        "Friend Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CreateFriendRequest(userID, foundUserID.Value);

                MessageBox.Show($"Friend request sent to {usernameToSearch}.",
                    "Friend Request", MessageBoxButtons.OK, MessageBoxIcon.Information);

                searchtextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching for user: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FriendRequests friendRequests = new FriendRequests(userID);
            CloseAndOpenNewForm(friendRequests);
        }
    }
}