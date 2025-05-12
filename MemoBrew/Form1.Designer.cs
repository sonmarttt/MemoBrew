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
            this.components = new System.ComponentModel.Container();
            this.usertextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.signupButton = new System.Windows.Forms.Button();
            this.dataSet = new MemoBrew.DataSet();
            this.drinkCategoriesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.drinkCategoriesTableAdapter = new MemoBrew.DataSetTableAdapters.DrinkCategoriesTableAdapter();
            this.tableAdapterManager = new MemoBrew.DataSetTableAdapters.TableAdapterManager();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drinkCategoriesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // usertextBox
            // 
            this.usertextBox.Location = new System.Drawing.Point(108, 377);
            this.usertextBox.Name = "usertextBox";
            this.usertextBox.Size = new System.Drawing.Size(324, 22);
            this.usertextBox.TabIndex = 0;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(108, 423);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(324, 22);
            this.PasswordTextBox.TabIndex = 1;
            // 
            // loginButton
            // 
            this.loginButton.BackColor = System.Drawing.Color.Black;
            this.loginButton.FlatAppearance.BorderColor = System.Drawing.Color.OrangeRed;
            this.loginButton.FlatAppearance.BorderSize = 50;
            this.loginButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.loginButton.Location = new System.Drawing.Point(130, 469);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(283, 42);
            this.loginButton.TabIndex = 2;
            this.loginButton.Text = "log in";
            this.loginButton.UseVisualStyleBackColor = false;
            // 
            // signupButton
            // 
            this.signupButton.BackColor = System.Drawing.Color.SeaShell;
            this.signupButton.Location = new System.Drawing.Point(130, 528);
            this.signupButton.Name = "signupButton";
            this.signupButton.Size = new System.Drawing.Size(283, 42);
            this.signupButton.TabIndex = 3;
            this.signupButton.Text = "sign up";
            this.signupButton.UseVisualStyleBackColor = false;
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "DataSet";
            this.dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // drinkCategoriesBindingSource
            // 
            this.drinkCategoriesBindingSource.DataMember = "DrinkCategories";
            this.drinkCategoriesBindingSource.DataSource = this.dataSet;
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
            this.tableAdapterManager.UpdateOrder = MemoBrew.DataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.UserDrinksTableAdapter = null;
            this.tableAdapterManager.UserLogTableAdapter = null;
            this.tableAdapterManager.UsersTableAdapter = null;
            // 
            // Welcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SandyBrown;
            this.ClientSize = new System.Drawing.Size(545, 622);
            this.Controls.Add(this.signupButton);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.usertextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Welcome";
            this.Text = "Welcome";
            this.Load += new System.EventHandler(this.Welcome_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drinkCategoriesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox usertextBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Button signupButton;
        private DataSet dataSet;
        private System.Windows.Forms.BindingSource drinkCategoriesBindingSource;
        private DataSetTableAdapters.DrinkCategoriesTableAdapter drinkCategoriesTableAdapter;
        private DataSetTableAdapters.TableAdapterManager tableAdapterManager;
    }
}

