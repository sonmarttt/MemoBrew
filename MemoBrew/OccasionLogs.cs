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
    public partial class OccasionLogs: Form
    {
        private int userID;
        public OccasionLogs(int userID)
        {
            InitializeComponent();
            this.userID = userID;
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
    }
}
