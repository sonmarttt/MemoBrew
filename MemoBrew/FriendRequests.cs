using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoBrew
{
    public partial class FriendRequests: Form
    {
        private int userID;
        public FriendRequests()
        {
            LanguageManager.ApplyLanguage();
            InitializeComponent();
            this.userID = userID;
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

        private void CloseAndOpenNewForm(Form newForm)
        {
            newForm.Show();
            this.Hide();

            newForm.FormClosed += (s, args) => this.Close();
        }
    }
}
