public partial class ManagementDashboard : ContentPage
{
    public string EmployeeID
    {
        set => WelcomeLabel.Text = $"Welcome Manager #{value}!";
    }

    public ManagementDashboard(string managerID)
    {
        InitializeComponent();
    }
}