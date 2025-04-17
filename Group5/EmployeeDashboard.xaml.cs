public partial class EmployeeDashboard : ContentPage
{
    public string EmployeeID
    {
        set => WelcomeLabel.Text = $"Welcome Employee #{value}!";
    }

    public EmployeeDashboard()
    {
        InitializeComponent();
    }
}