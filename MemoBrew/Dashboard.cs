using System;
using System.Windows.Forms;

namespace MemoBrew
{
    public partial class Dashboard : Form
    {
        private int userId;

        public Dashboard(int userId)
        {
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
    }
}