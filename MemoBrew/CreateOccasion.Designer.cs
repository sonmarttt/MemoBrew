namespace MemoBrew
{
    partial class CreateOccasion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateOccasion));
            this.txtOccasionName = new System.Windows.Forms.TextBox();
            this.occasionNameLabel = new System.Windows.Forms.Label();
            this.endDateLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.v = new System.Windows.Forms.Label();
            this.endDatePick = new System.Windows.Forms.DateTimePicker();
            this.createOccasionButton = new System.Windows.Forms.Button();
            this.locationLabel = new System.Windows.Forms.Label();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.locationTextBox = new System.Windows.Forms.TextBox();
            this.addFriendsButton = new System.Windows.Forms.Button();
            this.friendsButton = new System.Windows.Forms.Button();
            this.eventButton = new System.Windows.Forms.Button();
            this.homeButton = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtOccasionName
            // 
            this.txtOccasionName.BackColor = System.Drawing.Color.SandyBrown;
            resources.ApplyResources(this.txtOccasionName, "txtOccasionName");
            this.txtOccasionName.Name = "txtOccasionName";
            // 
            // occasionNameLabel
            // 
            resources.ApplyResources(this.occasionNameLabel, "occasionNameLabel");
            this.occasionNameLabel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.occasionNameLabel.ForeColor = System.Drawing.Color.SeaShell;
            this.occasionNameLabel.Name = "occasionNameLabel";
            // 
            // endDateLabel
            // 
            resources.ApplyResources(this.endDateLabel, "endDateLabel");
            this.endDateLabel.BackColor = System.Drawing.Color.Transparent;
            this.endDateLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.endDateLabel.Name = "endDateLabel";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Name = "label2";
            // 
            // v
            // 
            resources.ApplyResources(this.v, "v");
            this.v.BackColor = System.Drawing.Color.Transparent;
            this.v.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.v.Name = "v";
            // 
            // endDatePick
            // 
            resources.ApplyResources(this.endDatePick, "endDatePick");
            this.endDatePick.Name = "endDatePick";
            // 
            // createOccasionButton
            // 
            this.createOccasionButton.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.createOccasionButton, "createOccasionButton");
            this.createOccasionButton.ForeColor = System.Drawing.Color.SeaShell;
            this.createOccasionButton.Name = "createOccasionButton";
            this.createOccasionButton.UseVisualStyleBackColor = false;
            this.createOccasionButton.Click += new System.EventHandler(this.createOccasionButton_Click);
            // 
            // locationLabel
            // 
            resources.ApplyResources(this.locationLabel, "locationLabel");
            this.locationLabel.BackColor = System.Drawing.Color.Transparent;
            this.locationLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.locationLabel.Name = "locationLabel";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.BackColor = System.Drawing.Color.SeaShell;
            resources.ApplyResources(this.descriptionTextBox, "descriptionTextBox");
            this.descriptionTextBox.Name = "descriptionTextBox";
            // 
            // locationTextBox
            // 
            this.locationTextBox.BackColor = System.Drawing.Color.SeaShell;
            resources.ApplyResources(this.locationTextBox, "locationTextBox");
            this.locationTextBox.Name = "locationTextBox";
            // 
            // addFriendsButton
            // 
            this.addFriendsButton.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.addFriendsButton, "addFriendsButton");
            this.addFriendsButton.ForeColor = System.Drawing.Color.SeaShell;
            this.addFriendsButton.Name = "addFriendsButton";
            this.addFriendsButton.UseVisualStyleBackColor = false;
            this.addFriendsButton.Click += new System.EventHandler(this.addFriendsButton_Click);
            // 
            // friendsButton
            // 
            this.friendsButton.BackColor = System.Drawing.Color.SandyBrown;
            this.friendsButton.BackgroundImage = global::MemoBrew.Properties.Resources.add_user;
            resources.ApplyResources(this.friendsButton, "friendsButton");
            this.friendsButton.Name = "friendsButton";
            this.friendsButton.UseVisualStyleBackColor = false;
            this.friendsButton.Click += new System.EventHandler(this.friendsButton_Click);
            // 
            // eventButton
            // 
            this.eventButton.BackColor = System.Drawing.Color.SandyBrown;
            this.eventButton.BackgroundImage = global::MemoBrew.Properties.Resources.plus;
            resources.ApplyResources(this.eventButton, "eventButton");
            this.eventButton.Name = "eventButton";
            this.eventButton.UseVisualStyleBackColor = false;
            this.eventButton.Click += new System.EventHandler(this.eventButton_Click);
            // 
            // homeButton
            // 
            this.homeButton.BackColor = System.Drawing.Color.SandyBrown;
            this.homeButton.BackgroundImage = global::MemoBrew.Properties.Resources.home__1_;
            resources.ApplyResources(this.homeButton, "homeButton");
            this.homeButton.Name = "homeButton";
            this.homeButton.UseVisualStyleBackColor = false;
            this.homeButton.Click += new System.EventHandler(this.homeButton_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::MemoBrew.Properties.Resources.orange;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::MemoBrew.Properties.Resources.black;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // CreateOccasion
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.occasionNameLabel);
            this.Controls.Add(this.addFriendsButton);
            this.Controls.Add(this.friendsButton);
            this.Controls.Add(this.eventButton);
            this.Controls.Add(this.homeButton);
            this.Controls.Add(this.locationTextBox);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.locationLabel);
            this.Controls.Add(this.createOccasionButton);
            this.Controls.Add(this.endDatePick);
            this.Controls.Add(this.v);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.endDateLabel);
            this.Controls.Add(this.txtOccasionName);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "CreateOccasion";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtOccasionName;
        private System.Windows.Forms.Label occasionNameLabel;
        private System.Windows.Forms.Label endDateLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label v;
        private System.Windows.Forms.DateTimePicker endDatePick;
        private System.Windows.Forms.Button createOccasionButton;
        private System.Windows.Forms.Label locationLabel;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.TextBox locationTextBox;
        private System.Windows.Forms.Button friendsButton;
        private System.Windows.Forms.Button eventButton;
        private System.Windows.Forms.Button homeButton;
        private System.Windows.Forms.Button addFriendsButton;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}