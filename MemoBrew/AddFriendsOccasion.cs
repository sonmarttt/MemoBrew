using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoBrew
{
    public partial class AddFriendsOccasion : Form
    {
        private int userId;
        private int occasionId;
        private List<FriendInfo> allFriends = new List<FriendInfo>();
        private FriendInfo selectedFriend = null;

        public AddFriendsOccasion(int userId, int occasionId)
        {
            LanguageManager.ApplyLanguage();
            InitializeComponent();
            this.userId = userId;
            this.occasionId = occasionId;

            if (!OccasionExists(occasionId))
            {
                MessageBox.Show("The selected occasion does not exist. Please create an occasion first.",
                    "Invalid Occasion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            LoadFriendsList();

            txtUsername.TextChanged += TxtUsername_TextChanged;
        }

        private bool OccasionExists(int occasionId)
        {
            SqlConnection connection = null;
            try
            {
                connection = DatabaseManager.GetConnection();

                string query = "SELECT COUNT(*) FROM Occasion WHERE OccasionID = @OccasionID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OccasionID", occasionId);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking occasion: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (connection != null)
                {
                    DatabaseManager.CloseConnection(connection);
                }
            }
        }

        private void LoadFriendsList()
        {
            try
            {
                allFriends = GetUserFriends(userId);

                if (allFriends.Count == 0)
                {
                    MessageBox.Show("You don't have any friends to add to this occasion. " +
                        "Add some friends first!", "No Friends", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading friends list: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtUsername_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtUsername.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                selectedFriend = null;
                txtUsername.BackColor = SystemColors.Window;
                return;
            }

            selectedFriend = allFriends.FirstOrDefault(f =>
                f.Username.ToLower().Contains(searchText) ||
                f.FirstName.ToLower().Contains(searchText) ||
                f.LastName.ToLower().Contains(searchText));

            if (selectedFriend != null)
            {
                txtUsername.BackColor = Color.LightGreen;
            }
            else
            {
                txtUsername.BackColor = SystemColors.Window;
            }
        }

        private void addFriendsButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedFriend == null)
                {
                    MessageBox.Show("Please enter a valid friend's username or name.",
                        "Friend Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (IsAlreadyParticipant(selectedFriend.UserID, occasionId))
                {
                    MessageBox.Show($"{selectedFriend.FirstName} {selectedFriend.LastName} is already added to this occasion.",
                        "Already Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUsername.Clear();
                    txtUsername.BackColor = SystemColors.Window;
                    selectedFriend = null;
                    return;
                }

                AddParticipant(selectedFriend.UserID, occasionId);

                MessageBox.Show($"{selectedFriend.FirstName} {selectedFriend.LastName} has been added to the occasion!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                allFriends.Remove(selectedFriend);
                txtUsername.Clear();
                txtUsername.BackColor = SystemColors.Window;
                selectedFriend = null;

                if (allFriends.Count == 0)
                {
                    MessageBox.Show("All your friends have been added to this occasion.",
                        "All Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding friend to occasion: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void goBackOccasionButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Database Operations

        private class FriendInfo
        {
            public int UserID { get; set; }
            public string Username { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        private List<FriendInfo> GetUserFriends(int userID)
        {
            List<FriendInfo> friends = new List<FriendInfo>();

            SqlConnection connection = null;
            try
            {
                connection = DatabaseManager.GetConnection();

                string query = @"
                    SELECT 
                        f.FriendID, 
                        u.Username,
                        u.FirstName,
                        u.LastName
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
                                LastName = reader.GetString(3)
                            };

                            friends.Add(friend);
                        }
                    }
                }

                if (friends.Count > 0)
                {
                    List<int> existingParticipants = GetExistingParticipants(occasionId);
                    friends = friends.Where(f => !existingParticipants.Contains(f.UserID)).ToList();
                }

                return friends;
            }
            finally
            {
                if (connection != null)
                {
                    DatabaseManager.CloseConnection(connection);
                }
            }
        }

        private List<int> GetExistingParticipants(int occasionId)
        {
            List<int> participants = new List<int>();
            SqlConnection connection = null;
            try
            {
                connection = DatabaseManager.GetConnection();

                string query = "SELECT UserID FROM OccasionParticipants WHERE OccasionID = @OccasionID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OccasionID", occasionId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            participants.Add(reader.GetInt32(0));
                        }
                    }
                }

                return participants;
            }
            finally
            {
                if (connection != null)
                {
                    DatabaseManager.CloseConnection(connection);
                }
            }
        }

        private bool IsAlreadyParticipant(int userID, int occasionID)
        {
            SqlConnection connection = null;
            try
            {
                connection = DatabaseManager.GetConnection();

                string query = @"
                    SELECT COUNT(*) 
                    FROM OccasionParticipants 
                    WHERE OccasionID = @OccasionID AND UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OccasionID", occasionID);
                    command.Parameters.AddWithValue("@UserID", userID);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
            finally
            {
                if (connection != null)
                {
                    DatabaseManager.CloseConnection(connection);
                }
            }
        }

        private void AddParticipant(int userID, int occasionID)
        {
            SqlConnection connection = null;
            try
            {
                connection = DatabaseManager.GetConnection();

                string query = @"
                    INSERT INTO OccasionParticipants (OccasionID, UserID, Status)
                    VALUES (@OccasionID, @UserID, 'invited')";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OccasionID", occasionID);
                    command.Parameters.AddWithValue("@UserID", userID);

                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                if (connection != null)
                {
                    DatabaseManager.CloseConnection(connection);
                }
            }
        }

        #endregion
    }
}