using System;
using Microsoft.Maui.Controls;
using BCrypt.Net;  // Make sure you have the BCrypt.Net-Next NuGet package installed

namespace Group5.Pages
{
    public partial class EmployeeLogin : ContentPage
    {
        public EmployeeLogin()
        {
            InitializeComponent();
        }

        // Button click event handler for employee login
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string employeeID = EmployeeIDEntry.Text;
            string enteredPassword = PasswordEntry.Text;

            if (await VerifyLogin(employeeID, enteredPassword))
            {
                // Navigate to the employee dashboard if login is successful
                await Navigation.PushAsync(new EmployeeDashboard());
            }
            else
            {
                // Show error message if login fails
                await DisplayAlert("Login Failed", "Incorrect Employee ID or Password.", "OK");
            }
        }

        // Method to verify login
        private async Task<bool> VerifyLogin(string employeeID, string enteredPassword)
        {
            // Retrieve the hashed password from the database
            string storedHashedPassword = await GetEmployeeHashedPasswordFromDatabase(employeeID);

            if (string.IsNullOrEmpty(storedHashedPassword))
                return false;

            // Use BCrypt to verify the entered password
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHashedPassword);
        }

        private async Task<string> GetEmployeeHashedPasswordFromDatabase(string employeeID)
        {
            int empId = int.Parse(employeeID);

            using var connection = DatabaseConnection.GetConnection();

            var employee = connection.Find<Employee>(empId);

            if (employee != null)
            {
                return employee.Hash_Password;
            }
            else
            {
                return null; // Employee not found
            }
        }
    }
}
