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
    public partial class OccasionLogs : Form
    {
        private int userID;
        private int occasionID;
        private List<DrinkItem> userDrinks = new List<DrinkItem>();
        private Dictionary<int, DrinkInfo> availableDrinks = new Dictionary<int, DrinkInfo>();
        private string imagesFolderPath;

        public OccasionLogs(int userID, int occasionID)
        {
            LanguageManager.ApplyLanguage();
            InitializeComponent();

            this.userID = userID;
            this.occasionID = occasionID;
            this.FormClosing += new FormClosingEventHandler(Form_FormClosing);

            SetupImagesFolder();
            LoadAvailableDrinks();
            LoadOccasionData();
        }

        private void SetupImagesFolder()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            imagesFolderPath = Path.Combine(baseDir, "OccasionImages");

            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }

            string occasionFolderPath = Path.Combine(imagesFolderPath, $"Occasion_{occasionID}");
            if (!Directory.Exists(occasionFolderPath))
            {
                Directory.CreateDirectory(occasionFolderPath);
            }

            imagesFolderPath = occasionFolderPath;
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Application.OpenForms.Count == 1)
            {
                Application.Exit();
            }
        }

        private void LoadOccasionData()
        {
            try
            {
                string occasionName = GetOccasionName(occasionID);

                this.Text = $"Logs for {occasionName}";
                if (logOccasionNameLabel != null)
                    logOccasionNameLabel.Text = occasionName;

                LoadExistingLogs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading occasion data: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetOccasionName(int occasionID)
        {
            SqlConnection connection = null;
            try
            {
                connection = DatabaseManager.GetConnection();

                string query = "SELECT Name FROM Occasion WHERE OccasionID = @OccasionID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OccasionID", occasionID);

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString();
                    }

                    return "Unknown Occasion";
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

        private void LoadAvailableDrinks()
        {
            SqlConnection connection = null;
            try
            {
                connection = DatabaseManager.GetConnection();

                string query = @"
                    SELECT 
                        d.DrinkID, 
                        d.Name, 
                        d.AlcoholContent, 
                        d.Amount, 
                        d.Unit, 
                        c.Name AS CategoryName
                    FROM 
                        Drinks d
                    LEFT JOIN 
                        DrinkCategories c ON d.CategoryID = c.CategoryID
                    ORDER BY 
                        c.Name, d.Name";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int drinkId = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            decimal alcoholContent = reader.GetDecimal(2);
                            decimal amount = reader.GetDecimal(3);
                            string unit = reader.GetString(4);
                            string category = reader.IsDBNull(5) ? "Other" : reader.GetString(5);

                            DrinkInfo drink = new DrinkInfo
                            {
                                DrinkID = drinkId,
                                Name = name,
                                AlcoholContent = alcoholContent,
                                Amount = amount,
                                Unit = unit,
                                Category = category
                            };

                            availableDrinks.Add(drinkId, drink);
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

        private void LoadExistingLogs()
        {
            userDrinks.Clear();

            SqlConnection connection = null;
            try
            {
                connection = DatabaseManager.GetConnection();

                string query = @"
                    SELECT 
                        ud.UserDrinkID,
                        ud.DrinkID,
                        d.Name AS DrinkName,
                        ud.Quantity,
                        ud.HungoverRating,
                        ud.Notes
                    FROM 
                        UserDrinks ud
                    JOIN 
                        Drinks d ON ud.DrinkID = d.DrinkID
                    WHERE 
                        ud.UserID = @UserID AND ud.OccasionID = @OccasionID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@OccasionID", occasionID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int userDrinkId = reader.GetInt32(0);
                            int drinkId = reader.GetInt32(1);
                            string drinkName = reader.GetString(2);
                            int quantity = reader.GetInt32(3);
                            int hangoverRating = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                            string notes = reader.IsDBNull(5) ? "" : reader.GetString(5);

                            DrinkItem item = new DrinkItem
                            {
                                UserDrinkID = userDrinkId,
                                DrinkID = drinkId,
                                DrinkName = drinkName,
                                Quantity = quantity
                            };

                            userDrinks.Add(item);
                        }

                        if (reader.HasRows)
                        {
                            reader.Close();

                            string ratingQuery = @"
                                SELECT MAX(HungoverRating)
                                FROM UserDrinks
                                WHERE UserID = @UserID AND OccasionID = @OccasionID";

                            using (SqlCommand ratingCommand = new SqlCommand(ratingQuery, connection))
                            {
                                ratingCommand.Parameters.AddWithValue("@UserID", userID);
                                ratingCommand.Parameters.AddWithValue("@OccasionID", occasionID);

                                object result = ratingCommand.ExecuteScalar();
                                if (result != null && result != DBNull.Value)
                                {
                                    int rating = Convert.ToInt32(result);
                                    if (HangoverLevelNumber != null)
                                        HangoverLevelNumber.Text = rating.ToString();

                                    if (sicknessLevelNumber != null)
                                        sicknessLevelNumber.Text = rating.ToString();
                                }
                            }

                            string notesQuery = @"
                                SELECT TOP 1 Notes
                                FROM UserDrinks
                                WHERE UserID = @UserID AND OccasionID = @OccasionID
                                AND Notes IS NOT NULL
                                ORDER BY UserDrinkID DESC";

                            using (SqlCommand notesCommand = new SqlCommand(notesQuery, connection))
                            {
                                notesCommand.Parameters.AddWithValue("@UserID", userID);
                                notesCommand.Parameters.AddWithValue("@OccasionID", occasionID);

                                object result = notesCommand.ExecuteScalar();
                                if (result != null && result != DBNull.Value)
                                {
                                    if (addCommentTextbox != null)
                                        addCommentTextbox.Text = result.ToString();
                                }
                            }
                        }
                    }
                }

                UpdateDrinksDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading existing logs: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection != null)
                {
                    DatabaseManager.CloseConnection(connection);
                }
            }
        }

        private void UpdateDrinksDisplay()
        {
            if (textBox1 != null)
            {
                textBox1.Text = "";

                foreach (var drink in userDrinks)
                {
                    string drinkInfo = $"{drink.DrinkName} x{drink.Quantity}";
                    textBox1.Text += drinkInfo + Environment.NewLine;
                }
            }
        }

        private void SaveDrinkLogs(int hangoverRating, int sicknessRating, string comments)
        {
            SqlConnection connection = null;
            try
            {
                connection = DatabaseManager.GetConnection();

                if (userDrinks.Count == 0 && availableDrinks.Count > 0)
                {
                    int defaultDrinkId = availableDrinks.Keys.First();

                    DrinkItem defaultDrink = new DrinkItem
                    {
                        DrinkID = defaultDrinkId,
                        DrinkName = availableDrinks[defaultDrinkId].Name,
                        Quantity = 1
                    };

                    userDrinks.Add(defaultDrink);
                }

                string deleteQuery = @"
                    DELETE FROM UserDrinks 
                    WHERE UserID = @UserID AND OccasionID = @OccasionID";

                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@UserID", userID);
                    deleteCommand.Parameters.AddWithValue("@OccasionID", occasionID);
                    deleteCommand.ExecuteNonQuery();
                }

                foreach (var drink in userDrinks)
                {
                    string insertQuery = @"
                        INSERT INTO UserDrinks (UserID, DrinkID, OccasionID, Quantity, ConsumedAt, HungoverRating, Notes)
                        VALUES (@UserID, @DrinkID, @OccasionID, @Quantity, GETDATE(), @HungoverRating, @Notes)";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@UserID", userID);
                        insertCommand.Parameters.AddWithValue("@DrinkID", drink.DrinkID);
                        insertCommand.Parameters.AddWithValue("@OccasionID", occasionID);
                        insertCommand.Parameters.AddWithValue("@Quantity", drink.Quantity);
                        insertCommand.Parameters.AddWithValue("@HungoverRating", hangoverRating);

                        if (string.IsNullOrEmpty(comments))
                            insertCommand.Parameters.AddWithValue("@Notes", DBNull.Value);
                        else
                            insertCommand.Parameters.AddWithValue("@Notes", comments);

                        insertCommand.ExecuteNonQuery();
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

        private void uploadPicturesButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Select Images";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string sourcePath in openFileDialog.FileNames)
                    {
                        try
                        {
                            string fileName = $"img_{DateTime.Now.Ticks}_{Path.GetFileName(sourcePath)}";
                            string destinationPath = Path.Combine(imagesFolderPath, fileName);
                            File.Copy(sourcePath, destinationPath, true);
                            string relativePath = $"OccasionImages/Occasion_{occasionID}/{fileName}";
                            SaveImageReference(relativePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error copying image {Path.GetFileName(sourcePath)}: {ex.Message}",
                                "Image Copy Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    MessageBox.Show("Images uploaded successfully!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void SaveImageReference(string relativePath)
        {
            SqlConnection connection = null;
            try
            {
                connection = DatabaseManager.GetConnection();

                string query = @"
                    INSERT INTO Images (ImagePath, OccasionID, CreatorID, UploadDate)
                    VALUES (@ImagePath, @OccasionID, @CreatorID, GETDATE())";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ImagePath", relativePath);
                    command.Parameters.AddWithValue("@OccasionID", occasionID);
                    command.Parameters.AddWithValue("@CreatorID", userID);

                    command.ExecuteNonQuery();
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

        private void submitOccasionLogButton_Click(object sender, EventArgs e)
        {
            try
            {
                int hangoverRating = 0;
                int sicknessRating = 0;

                if (HangoverLevelNumber != null)
                    int.TryParse(HangoverLevelNumber.Text, out hangoverRating);

                if (sicknessLevelNumber != null)
                    int.TryParse(sicknessLevelNumber.Text, out sicknessRating);

                string comments = "";
                if (addCommentTextbox != null)
                    comments = addCommentTextbox.Text;

                SaveDrinkLogs(hangoverRating, sicknessRating, comments);

                MessageBox.Show("Your logs have been saved successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Dashboard dashboard = new Dashboard(userID);
                CloseAndOpenNewForm(dashboard);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving occasion logs: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addDrinkButton_Click(object sender, EventArgs e)
        {
            using (DrinkSelectionForm drinkForm = new DrinkSelectionForm(availableDrinks.Values.ToList()))
            {
                if (drinkForm.ShowDialog() == DialogResult.OK)
                {
                    DrinkItem newDrink = new DrinkItem
                    {
                        DrinkID = drinkForm.SelectedDrink.DrinkID,
                        DrinkName = drinkForm.SelectedDrink.Name,
                        Quantity = drinkForm.Quantity
                    };

                    userDrinks.Add(newDrink);
                    UpdateDrinksDisplay();
                }
            }
        }

        private void removeDrinkButton_Click(object sender, EventArgs e)
        {
            if (userDrinks.Count > 0)
            {
                userDrinks.RemoveAt(userDrinks.Count - 1);
                UpdateDrinksDisplay();
            }
            else
            {
                MessageBox.Show("There are no drinks to remove.",
                    "No Drinks", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    public class DrinkItem
    {
        public int UserDrinkID { get; set; }
        public int DrinkID { get; set; }
        public string DrinkName { get; set; }
        public int Quantity { get; set; }
    }

    public class DrinkInfo
    {
        public int DrinkID { get; set; }
        public string Name { get; set; }
        public decimal AlcoholContent { get; set; }
        public decimal Amount { get; set; }
        public string Unit { get; set; }
        public string Category { get; set; }
    }

    public class DrinkSelectionForm : Form
    {
        private ComboBox drinkComboBox;
        private NumericUpDown quantityNumeric;
        private Button okButton;
        private Button cancelButton;

        public DrinkInfo SelectedDrink { get; private set; }
        public int Quantity { get; private set; }

        public DrinkSelectionForm(List<DrinkInfo> drinks)
        {
            this.Text = "Select a Drink";
            this.Size = new Size(300, 200);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label drinkLabel = new Label
            {
                Text = "Drink:",
                Location = new Point(20, 20),
                AutoSize = true
            };

            drinkComboBox = new ComboBox
            {
                Location = new Point(100, 20),
                Width = 160,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            foreach (var drink in drinks)
            {
                drinkComboBox.Items.Add(drink);
            }

            drinkComboBox.DisplayMember = "Name";
            drinkComboBox.ValueMember = "DrinkID";

            if (drinkComboBox.Items.Count > 0)
                drinkComboBox.SelectedIndex = 0;

            Label quantityLabel = new Label
            {
                Text = "Quantity:",
                Location = new Point(20, 60),
                AutoSize = true
            };

            quantityNumeric = new NumericUpDown
            {
                Location = new Point(100, 60),
                Width = 80,
                Minimum = 1,
                Maximum = 20,
                Value = 1
            };

            okButton = new Button
            {
                Text = "OK",
                Location = new Point(100, 110),
                Width = 80,
                DialogResult = DialogResult.OK
            };

            cancelButton = new Button
            {
                Text = "Cancel",
                Location = new Point(190, 110),
                Width = 80,
                DialogResult = DialogResult.Cancel
            };

            okButton.Click += OkButton_Click;

            this.Controls.Add(drinkLabel);
            this.Controls.Add(drinkComboBox);
            this.Controls.Add(quantityLabel);
            this.Controls.Add(quantityNumeric);
            this.Controls.Add(okButton);
            this.Controls.Add(cancelButton);

            this.AcceptButton = okButton;
            this.CancelButton = cancelButton;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (drinkComboBox.SelectedItem != null)
            {
                SelectedDrink = (DrinkInfo)drinkComboBox.SelectedItem;
                Quantity = (int)quantityNumeric.Value;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Please select a drink.", "Selection Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
            }
        }
    }
}