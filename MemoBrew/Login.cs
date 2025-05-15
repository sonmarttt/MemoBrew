using System;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace MemoBrew
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form_FormClosing);
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Please enter both username and password.",
                        "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string hashedPassword = HashPassword(txtPassword.Text);

                MemoDataDataSetTableAdapters.QueriesTableAdapter queriesAdapter =
                    new MemoDataDataSetTableAdapters.QueriesTableAdapter();

                object result = queriesAdapter.ValidateLogin(txtUsername.Text, hashedPassword);

                if (result != null && result != DBNull.Value)
                {
                    int userId = Convert.ToInt32(result);

                    MemoDataDataSetTableAdapters.UsersTableAdapter usersAdapter =
                        new MemoDataDataSetTableAdapters.UsersTableAdapter();
                    MemoDataDataSet.UsersDataTable userTable = usersAdapter.GetData();
                    MemoDataDataSet.UsersRow userRow = userTable.FindByUserID(userId);

                    string firstName = userRow.FirstName;

                    try
                    {
                        MemoDataDataSetTableAdapters.UserLogTableAdapter logAdapter =
                            new MemoDataDataSetTableAdapters.UserLogTableAdapter();
                        logAdapter.Insert(userId, DateTime.Now);
                    }
                    catch
                    {
                    }

                    MessageBox.Show($"Welcome back, {firstName}!",
                        "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Dashboard dashboardForm = new Dashboard(userId);
                    CloseAndOpenNewForm(dashboardForm);
                }
                else
                {
                    MessageBox.Show("Invalid username or password.",
                        "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
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
    }
}