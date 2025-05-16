using System;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;

namespace MemoBrew
{
    public partial class Signup : Form
    {
        private MemoDataDataSet memoDataDataSet;

        public Signup()
        {
            InitializeComponent();
            this.memoDataDataSet = new MemoDataDataSet();
            this.FormClosing += new FormClosingEventHandler(Form_FormClosing);
            LanguageManager.ApplyLanguage();
            UpdateLanguageComboBox();
        }

        private void signupButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Username cannot be empty.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Password cannot be empty.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
                {
                    MessageBox.Show("First name and last name are required.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MemoDataDataSetTableAdapters.UsersTableAdapter usersAdapter =
                    new MemoDataDataSetTableAdapters.UsersTableAdapter();
                MemoDataDataSetTableAdapters.QueriesTableAdapter queriesAdapter =
                    new MemoDataDataSetTableAdapters.QueriesTableAdapter();

                int existingCount = (int)queriesAdapter.CheckIfUsernameExists(txtUsername.Text);
                Debug.WriteLine($"Existing username count: {existingCount}");

                if (existingCount > 0)
                {
                    MessageBox.Show("Username already exists. Please choose a different username.",
                        "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!DateTime.TryParse(txtDateOfBirth.Text, out DateTime dob))
                {
                    MessageBox.Show("Invalid date format. Please use MM/DD/YYYY format.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal? weight = null;
                if (!string.IsNullOrEmpty(txtWeight.Text))
                {
                    if (!decimal.TryParse(txtWeight.Text, out decimal parsedWeight))
                    {
                        MessageBox.Show("Weight must be a valid number.",
                            "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    weight = parsedWeight;
                }

                decimal? height = null;
                if (!string.IsNullOrEmpty(txtHeight.Text))
                {
                    if (!decimal.TryParse(txtHeight.Text, out decimal parsedHeight))
                    {
                        MessageBox.Show("Height must be a valid number.",
                            "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    height = parsedHeight;
                }

                string gender = null;
                if (rbMale.Checked)
                    gender = "Male";
                else if (rbFemale.Checked)
                    gender = "Female";

                string hashedPassword = HashPassword(txtPassword.Text);

                int newUserId = InsertUser(
                    txtUsername.Text,
                    txtFirstName.Text,
                    txtLastName.Text,
                    dob,
                    weight,
                    height,
                    gender,
                    hashedPassword
                );

                Debug.WriteLine($"New user created with ID: {newUserId}");

                MessageBox.Show("User registered successfully! You can now log in.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Login loginForm = new Login();
                CloseAndOpenNewForm(loginForm);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error registering user: {ex.Message}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");

                MessageBox.Show("Error registering user: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int InsertUser(string username, string firstName, string lastName,
     DateTime dob, decimal? weight, decimal? height, string gender, string hashedPassword)
        {
            string connectionString = Properties.Settings.Default.MemoDataConnectionString;
            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();

                System.Data.SqlClient.SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string insertSql = @"
                INSERT INTO Users (Username, FirstName, LastName, DateOfBirth, Weight, Height, Gender, ProfilePicture, Password)
                VALUES (@Username, @FirstName, @LastName, @DateOfBirth, @Weight, @Height, @Gender, @ProfilePicture, @Password);
                SELECT SCOPE_IDENTITY();";

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(insertSql, connection, transaction);

                    cmd.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar, 50).Value = username;
                    cmd.Parameters.Add("@FirstName", System.Data.SqlDbType.NVarChar, 50).Value = firstName;
                    cmd.Parameters.Add("@LastName", System.Data.SqlDbType.NVarChar, 50).Value = lastName;
                    cmd.Parameters.Add("@DateOfBirth", System.Data.SqlDbType.Date).Value = dob;

                    if (weight.HasValue)
                        cmd.Parameters.Add("@Weight", System.Data.SqlDbType.Decimal).Value = weight.Value;
                    else
                        cmd.Parameters.Add("@Weight", System.Data.SqlDbType.Decimal).Value = DBNull.Value;

                    if (height.HasValue)
                        cmd.Parameters.Add("@Height", System.Data.SqlDbType.Decimal).Value = height.Value;
                    else
                        cmd.Parameters.Add("@Height", System.Data.SqlDbType.Decimal).Value = DBNull.Value;

                    if (string.IsNullOrEmpty(gender))
                        cmd.Parameters.Add("@Gender", System.Data.SqlDbType.NVarChar, 20).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Gender", System.Data.SqlDbType.NVarChar, 20).Value = gender;

                    cmd.Parameters.Add("@ProfilePicture", System.Data.SqlDbType.NVarChar, 255).Value = DBNull.Value;
                    cmd.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 255).Value = hashedPassword;

                    object result = cmd.ExecuteScalar();
                    int newUserId = Convert.ToInt32(result);

                    transaction.Commit();

                    return newUserId;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error during user registration: " + ex.Message, ex);
                }
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

        private void UpdateLanguageComboBox()
        {
            switch (LanguageManager.CurrentLanguage)
            {
                case "en-US":
                    selectLanguageBox.SelectedIndex = 0;
                    break;
                case "fr-FR":
                    selectLanguageBox.SelectedIndex = 1;
                    break;
                case "es-ES":
                    selectLanguageBox.SelectedIndex = 2;
                    break;
            }
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

        private void gobackButton_Click(object sender, EventArgs e)
        {
            Welcome welcome = new Welcome();
            CloseAndOpenNewForm(welcome);
        }
    }
}