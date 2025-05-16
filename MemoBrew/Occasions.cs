using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoBrew
{
    public partial class Occasions : Form
    {
        private int userID;
        private int occasionID;
        private List<string> imagePaths = new List<string>();
        private List<string> comments = new List<string>();
        private int currentImageIndex = 0;
        private int currentCommentIndex = 0;
        private Timer imageTimer = new Timer();
        private Timer commentTimer = new Timer();

        public Occasions(int userID, int occasionID)
        {
            LanguageManager.ApplyLanguage();
            InitializeComponent();
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.userID = userID;
            this.occasionID = occasionID;
            this.FormClosing += new FormClosingEventHandler(Form_FormClosing);

            imageTimer.Interval = 3000;
            imageTimer.Tick += ImageTimer_Tick;

            commentTimer.Interval = 5000;
            commentTimer.Tick += CommentTimer_Tick;

            LoadOccasionData();
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            imageTimer.Stop();
            commentTimer.Stop();

            if (Application.OpenForms.Count == 1)
            {
                Application.Exit();
            }
        }

        private void LoadOccasionData()
        {
            try
            {
                LoadImages();

                if (imagePaths.Count > 0)
                {
                    DisplayImage(0);
                    imageTimer.Start();
                }
                else
                {
                    pictureBox1.Image = null;
                }

                LoadStatistics();

                LoadComments();

                if (comments.Count > 0)
                {
                    DisplayComment(0);
                    commentTimer.Start();
                }
                else
                {
                    goingThroughComments.Text = "No comments available";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading occasion data: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadImages()
        {
            SqlConnection connection = null;
            try
            {
                connection = DatabaseManager.GetConnection();

                string query = @"
                    SELECT ImagePath 
                    FROM Images 
                    WHERE OccasionID = @OccasionID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OccasionID", occasionID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string imagePath = reader.GetString(0);

                            if (File.Exists(imagePath))
                            {
                                imagePaths.Add(imagePath);
                            }
                        }
                    }
                }
            }
            finally
            {
                if (connection != null)
                {
                    DatabaseManager.CloseConnection(connection);
                }
            }
        }

        private void DisplayImage(int index)
        {
            if (imagePaths.Count == 0) return;

            try
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }

                pictureBox1.Image = Image.FromFile(imagePaths[index]);

                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception ex)
            {
                imagePaths.RemoveAt(index);

                if (imagePaths.Count > 0)
                {
                    DisplayImage(index % imagePaths.Count);
                }
                else
                {
                    pictureBox1.Image = null;
                    imageTimer.Stop();
                }
            }
        }

        private void ImageTimer_Tick(object sender, EventArgs e)
        {
            if (imagePaths.Count == 0)
            {
                imageTimer.Stop();
                return;
            }

            currentImageIndex = (currentImageIndex + 1) % imagePaths.Count;
            DisplayImage(currentImageIndex);
        }

        private void LoadStatistics()
        {
            SqlConnection connection = null;
            try
            {
                connection = DatabaseManager.GetConnection();

                string mostDrinksQuery = @"
                    SELECT TOP 1 
                        u.Username
                    FROM 
                        UserDrinks ud
                    JOIN 
                        Users u ON ud.UserID = u.UserID
                    WHERE 
                        ud.OccasionID = @OccasionID
                    GROUP BY 
                        u.Username, ud.UserID
                    ORDER BY 
                        SUM(ud.Quantity) DESC";

                using (SqlCommand command = new SqlCommand(mostDrinksQuery, connection))
                {
                    command.Parameters.AddWithValue("@OccasionID", occasionID);

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        mostDrinksNameLabel.Text = result.ToString();
                    }
                    else
                    {
                        mostDrinksNameLabel.Text = "No data";
                    }
                }

                string mostSickQuery = @"
                    SELECT TOP 1 
                        u.Username
                    FROM 
                        UserDrinks ud
                    JOIN 
                        Users u ON ud.UserID = u.UserID
                    WHERE 
                        ud.OccasionID = @OccasionID
                    GROUP BY 
                        u.Username, ud.UserID
                    ORDER BY 
                        MAX(ud.HungoverRating) DESC";

                using (SqlCommand command = new SqlCommand(mostSickQuery, connection))
                {
                    command.Parameters.AddWithValue("@OccasionID", occasionID);

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        mostSickNameLabel.Text = result.ToString();
                    }
                    else
                    {
                        mostSickNameLabel.Text = "No data";
                    }
                }

                mostHangoverNameLabel.Text = mostSickNameLabel.Text;
            }
            finally
            {
                if (connection != null)
                {
                    DatabaseManager.CloseConnection(connection);
                }
            }
        }

        private void LoadComments()
        {
            SqlConnection connection = null;
            try
            {
                connection = DatabaseManager.GetConnection();

                string query = @"
                    SELECT DISTINCT
                        ud.Notes
                    FROM 
                        UserDrinks ud
                    WHERE 
                        ud.OccasionID = @OccasionID
                        AND ud.Notes IS NOT NULL
                        AND ud.Notes <> ''";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OccasionID", occasionID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string comment = reader.GetString(0);
                            comments.Add(comment);
                        }
                    }
                }
            }
            finally
            {
                if (connection != null)
                {
                    DatabaseManager.CloseConnection(connection);
                }
            }
        }

        private void DisplayComment(int index)
        {
            if (comments.Count == 0) return;

            goingThroughComments.Text = comments[index];
        }

        private void CommentTimer_Tick(object sender, EventArgs e)
        {
            if (comments.Count == 0)
            {
                commentTimer.Stop();
                return;
            }

            currentCommentIndex = (currentCommentIndex + 1) % comments.Count;
            DisplayComment(currentCommentIndex);
        }

        private void goBackArrowButton_Click(object sender, EventArgs e)
        {
            if (imagePaths.Count == 0) return;

            currentImageIndex = (currentImageIndex - 1 + imagePaths.Count) % imagePaths.Count;
            DisplayImage(currentImageIndex);
        }

        private void goNextArrowButton_Click(object sender, EventArgs e)
        {
            if (imagePaths.Count == 0) return;

            currentImageIndex = (currentImageIndex + 1) % imagePaths.Count;
            DisplayImage(currentImageIndex);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comments.Count == 0) return;
            currentCommentIndex = (currentCommentIndex - 1 + comments.Count) % comments.Count;
            DisplayComment(currentCommentIndex);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comments.Count == 0) return;
            currentCommentIndex = (currentCommentIndex + 1) % comments.Count;
            DisplayComment(currentCommentIndex);
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
            imageTimer.Stop();
            commentTimer.Stop();

            newForm.Show();
            this.Hide();
            newForm.FormClosed += (s, args) => this.Close();
        }
    }
}