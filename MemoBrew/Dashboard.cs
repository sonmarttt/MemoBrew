using System;
using System.Windows.Forms;

namespace MemoBrew
{
    public partial class Dashboard : Form
    {
        private int userId;

        public Dashboard(int userId)
        {
            LanguageManager.ApplyLanguage();
            UpdateLanguageComboBox();
            InitializeComponent();
            this.userId = userId;
            this.FormClosing += new FormClosingEventHandler(Form_FormClosing);
            LoadUserData();
        }

        private void LoadUserData()
        {
            try
            {
                MemoDataDataSetTableAdapters.UsersTableAdapter usersAdapter =
                    new MemoDataDataSetTableAdapters.UsersTableAdapter();
                MemoDataDataSet.UsersDataTable userTable = usersAdapter.GetData();
                MemoDataDataSet.UsersRow userRow = userTable.FindByUserID(userId);

                if (userRow != null)
                {
                    welcomeLabel.Text = $"Welcome, {userRow.FirstName} {userRow.LastName}!";
                }
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

        //update the language that was selected before
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
        //change the language again from the dropdown in signup
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