using System;
using Microsoft.Maui.Controls;

namespace Group5
{
    public partial class LoginPage : ContentPage
    {
        private readonly AuthService _authService;

        public LoginPage()
        {
            IntializeComponent();
            _authService = new AuthService();
        }

        private async void OnLoginButtonClicked(object sender, Eventargs e)
        {
            string employeeID = EmployeeIDEntry.Text;
            string password = PasswordEntry.Text;

            string role = await _authServiceLoginAsync(employeeID, password);

            if (role != null)
            {
                if (role.ToLower() == "manager")
                {
                    await Shell.Current.GoToAsync("//ManagementDashboard");
                }
                else
                {
                    await Shell.Current.GoToAsync("//EmployeeDashboard");
                }
            }
            else
            {
                LoginResultLabel.TextColor = Colors.Red;
                LoginResultLabel.Text = "Invalid login.";
            }
        }
    }
}