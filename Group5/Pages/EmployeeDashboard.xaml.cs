public partial class EmployeeDashboard : ContentPage
{
    public EmployeeDashboard()
    {
        InitializeComponent();
    }

    private async void OnClockInButtonClicked(object sender, EventArgs e)
    {
        string clockInTime = DateTime.Now.ToString("HH:mm");
        int employeeId = 1; // This should be the logged-in employee ID

        // Insert clock-in data to the database
        var shiftLog = new ShiftLog
        {
            Employee_ID = employeeId,
            Clock_In = clockInTime
        };

        using (var connection = DatabaseConnection.GetConnection())
        {
            connection.Insert(shiftLog);
        }

        await DisplayAlert("Clock-In", $"You clocked in at {clockInTime}", "OK");
    }

    private async void OnClockOutButtonClicked(object sender, EventArgs e)
    {
        string clockOutTime = DateTime.Now.ToString("HH:mm");
        int employeeId = 1; // This should be the logged-in employee ID

        // Update the clock-out data in the database
        var shiftLog = new ShiftLog
        {
            Employee_ID = employeeId,
            Clock_Out = clockOutTime
        };

        using (var connection = DatabaseConnection.GetConnection())
        {
            connection.InsertOrReplace(shiftLog);  // Insert or replace if the shift already exists
        }

        await DisplayAlert("Clock-Out", $"You clocked out at {clockOutTime}", "OK");
    }

    private async void OnBreakStartButtonClicked(object sender, EventArgs e)
    {
        string breakStartTime = DateTime.Now.ToString("HH:mm");
        int employeeId = 1; // This should be the logged-in employee ID

        // Update the break start data in the database
        var shiftLog = new ShiftLog
        {
            Employee_ID = employeeId,
            Break_Start = breakStartTime
        };

        using (var connection = DatabaseConnection.GetConnection())
        {
            connection.InsertOrReplace(shiftLog);  // Insert or replace if the shift already exists
        }

        await DisplayAlert("Break Started", $"Break started at {breakStartTime}", "OK");
    }

    private async void OnBreakEndButtonClicked(object sender, EventArgs e)
    {
        string breakEndTime = DateTime.Now.ToString("HH:mm");
        int employeeId = 1; // This should be the logged-in employee ID

        // Update the break end data in the database
        var shiftLog = new ShiftLog
        {
            Employee_ID = employeeId,
            Break_End = breakEndTime
        };

        using (var connection = DatabaseConnection.GetConnection())
        {
            connection.InsertOrReplace(shiftLog);  // Insert or replace if the shift already exists
        }

        await DisplayAlert("Break Ended", $"Break ended at {breakEndTime}", "OK");
    }
}
