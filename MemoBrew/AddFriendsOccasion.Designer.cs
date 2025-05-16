namespace MemoBrew
{
    partial class AddFriendsOccasion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddFriendsOccasion));
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.addFriendsButton = new System.Windows.Forms.Button();
            this.goBackOccasionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.SeaShell;
            resources.ApplyResources(this.txtUsername, "txtUsername");
            this.txtUsername.Name = "txtUsername";
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
            // goBackOccasionButton
            // 
            this.goBackOccasionButton.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.goBackOccasionButton, "goBackOccasionButton");
            this.goBackOccasionButton.ForeColor = System.Drawing.Color.SeaShell;
            this.goBackOccasionButton.Name = "goBackOccasionButton";
            this.goBackOccasionButton.UseVisualStyleBackColor = false;
            this.goBackOccasionButton.Click += new System.EventHandler(this.goBackOccasionButton_Click);
            // 
            // AddFriendsOccasion
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SandyBrown;
            this.Controls.Add(this.goBackOccasionButton);
            this.Controls.Add(this.addFriendsButton);
            this.Controls.Add(this.txtUsername);
            this.Name = "AddFriendsOccasion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button addFriendsButton;
        private System.Windows.Forms.Button goBackOccasionButton;
    }
}