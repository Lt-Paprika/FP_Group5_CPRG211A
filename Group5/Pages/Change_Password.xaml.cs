namespace Group5.Pages
{
    public partial class ChangePassword : ContentPage
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private async void OnChangePasswordButtonClicked(object sender, EventArgs e)
        {
            string employeeID = EmployeeIDEntry.Text;
            string newPassword = NewPasswordEntry.Text;
            string confirmPassword = ConfirmPasswordEntry.Text;

            // Check if the passwords match
            if (newPassword != confirmPassword)
            {
                await DisplayAlert("Error", "Incorrect password");
                return;
            }

            // Hash code the password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

            // Saveing to database
            await UpdatePasswordInDatabase(employeeID, hashedPassword);

            await DisplayAlert("Password has been updated.");
        }

        private async Task UpdatePasswordInDatabase(string employeeID, string hashedPassword)
        {
            // Update the hashed password in the database
            string query = "UPDATE Employees SET Hash_Password = @hashedPassword WHERE Employee_ID = @employeeID";

            using var connection = new SQLiteConnection(App.DatabasePath);
            var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@hashedPassword", hashedPassword);
            command.Parameters.AddWithValue("@employeeID", employeeID);
            connection.Open();
            await command.ExecuteNonQueryAsync();
        }
    }
}
