using System;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

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
        }

        private void signupButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Password cannot be empty.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MemoDataDataSetTableAdapters.UsersTableAdapter usersAdapter =
                    new MemoDataDataSetTableAdapters.UsersTableAdapter();
                MemoDataDataSetTableAdapters.QueriesTableAdapter queriesAdapter =
                    new MemoDataDataSetTableAdapters.QueriesTableAdapter();

                int existingCount = (int)queriesAdapter.CheckIfUsernameExists(txtUsername.Text);
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

                queriesAdapter.InsertUser(
                    txtUsername.Text,
                    txtFirstName.Text,
                    txtLastName.Text,
                    dob.ToString(),
                    weight,
                    height,
                    gender,
                    null,
                    hashedPassword
                );

                MessageBox.Show("User registered successfully! You can now log in.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Login loginForm = new Login();
                CloseAndOpenNewForm(loginForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error registering user: " + ex.Message,
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