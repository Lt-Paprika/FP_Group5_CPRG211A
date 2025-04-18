using System;
using Xamarin.Forms;
using SQLite;

namespace Group5.Pages
{
    public partial class EditEmployeePage : ContentPage
    {
        private string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CPRG211_Employees.db");

        public EditEmployeePage()
        {
            InitializeComponent();
        }

        // Search for employee by ID to edit
        private async void OnSearchEmployeeClicked(object sender, EventArgs e)
        {
            var employeeID = EmployeeIDEntry.Text;

            if (string.IsNullOrEmpty(employeeID))
            {
                await DisplayAlert("Error", "Please enter an employee ID.", "OK");
                return;
            }

            var employee = await GetEmployeeByID(int.Parse(employeeID));

            if (employee != null)
            {
                // Populate the fields with employee data
                FirstName.Text = employee.FirstName;
                LastName.Text = employee.LastName;
                PhoneNumber.Text = employee.PhoneNumber;
                Email.Text = employee.Email;
            }
            else
            {
                await DisplayAlert("Employee not found.");
            }
        }

        // Save the changes made to employee details
        private async void OnSaveChangesClicked(object sender, EventArgs e)
        {
            var employeeID = EmployeeID.Text;
            var firstName = FirstName.Text;
            var lastName = LastName.Text;
            var phoneNumber = PhoneNumber.Text;
            var email = Email.Text;

            if (string.IsNullOrEmpty(employeeID) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(phoneNumber))
            {
                await DisplayAlert("Ensure you enter your full name and phone number.");
                return;
            }

            // Save changes to the database
            var success = await SaveEmployeeChanges(int.Parse(employeeID), firstName, lastName);

            if (success)
            {
                await DisplayAlert("Employee information updated.");
            }
            else
            {
                await DisplayAlert("Failed to update employee information.");
            }
        }

        // Fetch employee by ID
        private async Task<Employee> GetEmployeeByID(int employeeID)
        {
            try
            {
                using var connection = new SQLiteConnection(dbPath);
                return connection.Table<Employee>().FirstOrDefault(e => e.Employee_ID == employeeID);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching employee: {ex.Message}");
                return null;
            }
        }

        // Save changes to employee details
        private async Task<bool> SaveEmployeeChanges(int employeeID, string firstName, string lastName)
        {
            try
            {
                using var connection = new SQLiteConnection(dbPath);
                var employee = connection.Table<Employee>().FirstOrDefault(e => e.Employee_ID == employeeID);

                if (employee != null)
                {
                    employee.FirstName = firstName;
                    employee.LastName = lastName;
                    connection.Update(employee);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving employee data: {ex.Message}");
                return false;
            }
        }
    }

    // Employee model for the database
    public class Employee
    {
        public int Employee_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
