using Group5.Pages;

namespace Group5;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register routes for different pages (make sure your pages are in the correct namespace)
        Routing.RegisterRoute("login", typeof(LoginPage)); // Registering LoginPage route
        Routing.RegisterRoute("employeeDashboard", typeof(EmployeeDashboard)); // Registering EmployeeDashboard route
        Routing.RegisterRoute("managementDashboard", typeof(ManagementDashboard)); // Registering ManagementDashboard route
    }
}
