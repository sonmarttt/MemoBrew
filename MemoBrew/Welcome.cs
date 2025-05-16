using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace MemoBrew
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();

            // Apply language settings
            LanguageManager.ApplyLanguage();

            VerifyDatabaseState();

            this.FormClosing += new FormClosingEventHandler(Form_FormClosing);
        }

        private void VerifyDatabaseState()
        {
            try
            {
                if (!DatabaseManager.IsDatabaseAccessible())
                {
                    string message = "The database is not accessible. This could be because:\n\n" +
                        "1. The database file is corrupted\n" +
                        "2. SQL Server is not running\n" +
                        "3. A previous application instance is still holding connections\n\n" +
                        "Would you like to attempt to fix this issue?";

                    DialogResult result = MessageBox.Show(message, "Database Error",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                    if (result == DialogResult.Yes)
                    {
                        AttemptDatabaseRepair();
                    }
                }
                else
                {
                    Debug.WriteLine("Database verification successful - database is accessible");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during database verification: {ex.Message}");
            }
        }

        private void AttemptDatabaseRepair()
        {
            try
            {
                Program.CloseAllDatabaseConnections();

                System.Threading.Thread.Sleep(1000);

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c sqllocaldb stop MSSQLLocalDB && sqllocaldb start MSSQLLocalDB",
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                using (Process process = Process.Start(startInfo))
                {
                    process.WaitForExit();
                }

                if (DatabaseManager.IsDatabaseAccessible())
                {
                    MessageBox.Show("Database connection repaired successfully!",
                        "Repair Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Could not repair database connection. " +
                        "Try restarting your computer and then the application.",
                        "Repair Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error attempting database repair: {ex.Message}");
                MessageBox.Show("Error attempting to repair database: " + ex.Message,
                    "Repair Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            CloseAndOpenNewForm(loginForm);
        }

        private void signupButton_Click(object sender, EventArgs e)
        {
            Signup signupForm = new Signup();
            CloseAndOpenNewForm(signupForm);
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Program.CloseAllDatabaseConnections();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error closing connections on form closing: {ex.Message}");
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

        //pick the language from the dropdown
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