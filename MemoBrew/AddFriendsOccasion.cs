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
    public partial class AddFriendsOccasion: Form
    {
        private int userId;
        public AddFriendsOccasion(int userId)
        {
            LanguageManager.ApplyLanguage();
            InitializeComponent();
            this.userId = userId;
          
        }

        private void goBackOccasionButton_Click(object sender, EventArgs e)
        {
            CreateOccasion createOccasion = new CreateOccasion(userId);
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
