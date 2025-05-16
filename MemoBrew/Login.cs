using System;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Data.SqlClient;

namespace MemoBrew
{
    public partial class Login : Form
    {
        public Login()
        {
            LanguageManager.ApplyLanguage();
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form_FormClosing);
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!DatabaseManager.IsDatabaseAccessible())
                {
                    MessageBox.Show("The database is not accessible. Please restart the application.",
                        "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!DatabaseManager.HasUsers())
                {
                    MessageBox.Show("The user database appears to be empty. Please sign up first.",
                        "No Users Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Please enter both username and password.",
                        "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string hashedPassword = HashPassword(txtPassword.Text);
                Debug.WriteLine($"Attempting login for user: {txtUsername.Text}");

                SqlConnection connection = null;
                try
                {
                    connection = DatabaseManager.GetConnection();

                    string checkSql = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                    using (SqlCommand checkCmd = new SqlCommand(checkSql, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@Username", txtUsername.Text);

                        int userCount = (int)checkCmd.ExecuteScalar();
                        Debug.WriteLine($"User count: {userCount}");

                        if (userCount == 0)
                        {
                            MessageBox.Show("Username does not exist. Please check your username or sign up.",
                                "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    string validateSql = "SELECT UserID FROM Users WHERE Username = @Username AND Password = @Password";
                    using (SqlCommand validateCmd = new SqlCommand(validateSql, connection))
                    {
                        validateCmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                        validateCmd.Parameters.AddWithValue("@Password", hashedPassword);

                        object result = validateCmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            int userId = Convert.ToInt32(result);
                            Debug.WriteLine($"Login successful for user ID: {userId}");

                            string userSql = "SELECT FirstName, LastName FROM Users WHERE UserID = @UserID";
                            using (SqlCommand userCmd = new SqlCommand(userSql, connection))
                            {
                                userCmd.Parameters.AddWithValue("@UserID", userId);

                                using (SqlDataReader reader = userCmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        string firstName = reader["FirstName"].ToString();

                                        MessageBox.Show($"Welcome back, {firstName}!",
                                            "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        Dashboard dashboardForm = new Dashboard(userId);
                                        CloseAndOpenNewForm(dashboardForm);
                                    }
                                    else
                                    {
                                        Debug.WriteLine("User details not found after successful login!");
                                        MessageBox.Show("Error retrieving user information.",
                                            "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.WriteLine("Invalid password for user");
                            MessageBox.Show("Invalid username or password.",
                                "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                finally
                {
                    if (connection != null)
                        DatabaseManager.CloseConnection(connection);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Login error: {ex.Message}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");

                MessageBox.Show("Login error: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Program.CloseAllDatabaseConnections();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error closing connections: {ex.Message}");
            }

            if (Application.OpenForms.Count == 1)
            {
                Application.Exit();
            }
        }

        private void CloseAndOpenNewForm(Form newForm)
        {
            newForm.Show();
            this.Hide();
            newForm.FormClosed += (s, args) => this.Close();
        }

        private void gobackButton_Click(object sender, EventArgs e)
        {
            Welcome welcome = new Welcome();
            CloseAndOpenNewForm(welcome);
        }
    }
}