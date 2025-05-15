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
            this.components = new System.ComponentModel.Container();
            this.loginButton = new System.Windows.Forms.Button();
            this.signupButton = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataSet = new MemoBrew.MemoDataDataSet();
            this.drinkCategoriesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.drinkCategoriesTableAdapter = new MemoBrew.MemoDataDataSetTableAdapters.DrinkCategoriesTableAdapter();
            this.tableAdapterManager = new MemoBrew.MemoDataDataSetTableAdapters.TableAdapterManager();
            this.usersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.usersTableAdapter = new MemoBrew.MemoDataDataSetTableAdapters.UsersTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drinkCategoriesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // loginButton
            // 
            this.loginButton.BackColor = System.Drawing.Color.Black;
            this.loginButton.FlatAppearance.BorderColor = System.Drawing.Color.OrangeRed;
            this.loginButton.FlatAppearance.BorderSize = 50;
            this.loginButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginButton.ForeColor = System.Drawing.Color.SeaShell;
            this.loginButton.Location = new System.Drawing.Point(84, 530);
            this.loginButton.Margin = new System.Windows.Forms.Padding(0);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(283, 42);
            this.loginButton.TabIndex = 2;
            this.loginButton.Text = "Log In";
            this.loginButton.UseVisualStyleBackColor = false;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // signupButton
            // 
            this.signupButton.BackColor = System.Drawing.Color.SeaShell;
            this.signupButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signupButton.Location = new System.Drawing.Point(84, 595);
            this.signupButton.Name = "signupButton";
            this.signupButton.Size = new System.Drawing.Size(283, 42);
            this.signupButton.TabIndex = 3;
            this.signupButton.Text = "Sign Up";
            this.signupButton.UseVisualStyleBackColor = false;
            this.signupButton.Click += new System.EventHandler(this.signupButton_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::MemoBrew.Properties.Resources.beer_mug;
            this.pictureBox.Location = new System.Drawing.Point(123, 114);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(205, 197);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 4;
            this.pictureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Showcard Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(146, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(457, 89);
            this.label1.TabIndex = 5;
            this.label1.Text = "MemoBrew";
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
            this.tableAdapterManager.UpdateOrder = MemoBrew.MemoDataDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.UserDrinksTableAdapter = null;
            this.tableAdapterManager.UserLogTableAdapter = null;
            this.tableAdapterManager.UsersTableAdapter = null;
            // 
            // usersBindingSource
            // 
            this.usersBindingSource.DataMember = "Users";
            this.usersBindingSource.DataSource = this.dataSet;
            // 
            // usersTableAdapter
            // 
            this.usersTableAdapter.ClearBeforeFill = true;
            // 
            // Welcome
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.SandyBrown;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(451, 675);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.signupButton);
            this.Controls.Add(this.loginButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Welcome";
            this.Text = "Welcome";
            //this.Load += new System.EventHandler(this.Welcome_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drinkCategoriesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Button signupButton;
        private MemoDataDataSet dataSet;
        private System.Windows.Forms.BindingSource drinkCategoriesBindingSource;
        private MemoDataDataSetTableAdapters.DrinkCategoriesTableAdapter drinkCategoriesTableAdapter;
        private MemoDataDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource usersBindingSource;
        private MemoDataDataSetTableAdapters.UsersTableAdapter usersTableAdapter;
    }
}

