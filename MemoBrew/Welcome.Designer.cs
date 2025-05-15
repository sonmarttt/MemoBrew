using System.Data;

namespace MemoBrew
{
    partial class Welcome
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Welcome));
            this.loginButton = new System.Windows.Forms.Button();
            this.signupButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.drinkCategoriesTableAdapter = new MemoBrew.MemoDataDataSetTableAdapters.DrinkCategoriesTableAdapter();
            this.tableAdapterManager = new MemoBrew.MemoDataDataSetTableAdapters.TableAdapterManager();
            this.selectLanguageBox = new System.Windows.Forms.ComboBox();
            this.languageLabel = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // loginButton
            // 
            this.loginButton.BackColor = System.Drawing.Color.Black;
            this.loginButton.FlatAppearance.BorderColor = System.Drawing.Color.OrangeRed;
            this.loginButton.FlatAppearance.BorderSize = 50;
            resources.ApplyResources(this.loginButton, "loginButton");
            this.loginButton.ForeColor = System.Drawing.Color.SeaShell;
            this.loginButton.Name = "loginButton";
            this.loginButton.UseVisualStyleBackColor = false;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // signupButton
            // 
            this.signupButton.BackColor = System.Drawing.Color.SeaShell;
            resources.ApplyResources(this.signupButton, "signupButton");
            this.signupButton.Name = "signupButton";
            this.signupButton.UseVisualStyleBackColor = false;
            this.signupButton.Click += new System.EventHandler(this.signupButton_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // drinkCategoriesTableAdapter
            // 
            this.drinkCategoriesTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.DrinkCategoriesTableAdapter = this.drinkCategoriesTableAdapter;
            this.tableAdapterManager.DrinksTableAdapter = null;
            this.tableAdapterManager.FriendRequestTableAdapter = null;
            this.tableAdapterManager.FriendsTableAdapter = null;
            this.tableAdapterManager.ImagesTableAdapter = null;
            this.tableAdapterManager.OccasionParticipantsTableAdapter = null;
            this.tableAdapterManager.OccasionTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = MemoBrew.MemoDataDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.UserDrinksTableAdapter = null;
            this.tableAdapterManager.UserLogTableAdapter = null;
            this.tableAdapterManager.UsersTableAdapter = null;
            // 
            // selectLanguageBox
            // 
            this.selectLanguageBox.FormattingEnabled = true;
            this.selectLanguageBox.Items.AddRange(new object[] {
            resources.GetString("selectLanguageBox.Items"),
            resources.GetString("selectLanguageBox.Items1"),
            resources.GetString("selectLanguageBox.Items2")});
            resources.ApplyResources(this.selectLanguageBox, "selectLanguageBox");
            this.selectLanguageBox.Name = "selectLanguageBox";
            this.selectLanguageBox.SelectedIndexChanged += new System.EventHandler(this.selectLanguageBox_SelectedIndexChanged);
            // 
            // languageLabel
            // 
            resources.ApplyResources(this.languageLabel, "languageLabel");
            this.languageLabel.Name = "languageLabel";
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::MemoBrew.Properties.Resources.beer_mug;
            resources.ApplyResources(this.pictureBox, "pictureBox");
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.TabStop = false;
            // 
            // Welcome
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.SandyBrown;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.languageLabel);
            this.Controls.Add(this.selectLanguageBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.signupButton);
            this.Controls.Add(this.loginButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Welcome";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Button signupButton;
        private MemoDataDataSetTableAdapters.DrinkCategoriesTableAdapter drinkCategoriesTableAdapter;
        private MemoDataDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox selectLanguageBox;
        private System.Windows.Forms.Label languageLabel;
    }
}

