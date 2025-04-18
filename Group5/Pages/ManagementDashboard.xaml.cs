using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using SQLite;

namespace Group5.Pages
{
    public partial class ManagerPage : ContentPage
    {
        private string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CPRG211_Employees.db");

        public ManagerPage()
        {
            InitializeComponent();
        }

        // Fetch current pay period data (example)
        private async void OnViewPayPeriodClicked(object sender, EventArgs e)
        {
            // Get the current pay period data from the database
            var payPeriodData = await GetCurrentPayPeriodData();

            if (payPeriodData != null)
            {
                // Display the pay period data on the page
                PayPeriodLabel.Text = $"Start: {payPeriodData.PayperiodStart.ToShortDateString()}\n" +
                                      $"End: {payPeriodData.PayperiodEnd.ToShortDateString()}\n" +
                                      $"Total Labor Cost: ${payPeriodData.TotalLaborCost}\n" +
                                      $"Total Hours Worked: {payPeriodData.TotalHoursWorked}";
            }
            else
            {
                await DisplayAlert("Error", "Could not retrieve pay period data.", "OK");
            }
        }

        // Sample method to fetch current pay period data from the database
        private async Task<PayPeriodData> GetCurrentPayPeriodData()
        {
            try
            {
                using (var connection = new SQLiteConnection(dbPath))
                {
                    // Get the current date and calculate the start and end of the current pay period
                    var currentDate = DateTime.Now;
                    var payPeriodStart = currentDate.AddDays(-14);  // Assuming 2-week pay periods
                    var payPeriodEnd = currentDate;

                    // Query the wages table to get total labor cost and total hours worked for the current pay period
                    var payPeriodData = connection.Table<Wages>()
                        .Where(w => w.PayperiodStart >= payPeriodStart && w.PayperiodEnd <= payPeriodEnd)
                        .ToList();

                    decimal totalLaborCost = 0;
                    decimal totalHoursWorked = 0;

                    // Calculate total labor cost and hours worked
                    foreach (var wage in payPeriodData)
                    {
                        totalLaborCost += wage.Salary.HasValue ? wage.Salary.Value : (wage.HourlyRate * wage.HoursWorked);
                        totalHoursWorked += wage.HoursWorked;
                    }

                    return new PayPeriodData
                    {
                        PayperiodStart = payPeriodStart,
                        PayperiodEnd = payPeriodEnd,
                        TotalLaborCost = totalLaborCost,
                        TotalHoursWorked = totalHoursWorked
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving pay period data: {ex.Message}");
                return null;
            }
        }

        // Click handler for editing employee information
        private async void OnEditEmployeeClicked(object sender, EventArgs e)
        {
            // Navigate to the Edit Employee page
            await Navigation.PushAsync(new EditEmployeePage());
        }

        // Click handler for generating reports (e.g., total hours worked, total labor cost, etc.)
        private async void OnGenerateReportsClicked(object sender, EventArgs e)
        {
            // Generate report data from the database (example)
            var reportData = await GenerateReport();

            if (reportData != null)
            {
                // Show a simple report summary
                await DisplayAlert("Labor Report", 
                    $"Total Labor Cost: ${reportData.TotalLaborCost}\n" +
                    $"Total Hours Worked: {reportData.TotalHoursWorked}", 
                    "OK");
            }
            else
            {
                await DisplayAlert("Error", "Could not generate the report.", "OK");
            }
        }

        // Generate labor cost and hours worked report
        private async Task<ReportData> GenerateReport()
        {
            try
            {
                using (var connection = new SQLiteConnection(dbPath))
                {
                    // Query wages data for the current pay period (example)
                    var currentDate = DateTime.Now;
                    var payPeriodStart = currentDate.AddDays(-14);  // Assuming 2-week pay periods
                    var payPeriodEnd = currentDate;

                    var reportData = connection.Table<Wages>()
                        .Where(w => w.PayperiodStart >= payPeriodStart && w.PayperiodEnd <= payPeriodEnd)
                        .ToList();

                    decimal totalLaborCost = 0;
                    decimal totalHoursWorked = 0;

                    // Calculate total labor cost and hours worked
                    foreach (var wage in reportData)
                    {
                        totalLaborCost += wage.Salary.HasValue ? wage.Salary.Value : (wage.HourlyRate * wage.HoursWorked);
                        totalHoursWorked += wage.HoursWorked;
                    }

                    return new ReportData
                    {
                        TotalLaborCost = totalLaborCost,
                        TotalHoursWorked = totalHoursWorked
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating report: {ex.Message}");
                return null;
            }
        }
    }

    // Model for Pay Period Data (used to display pay period information)
    public class PayPeriodData
    {
        public DateTime PayperiodStart { get; set; }
        public DateTime PayperiodEnd { get; set; }
        public decimal TotalLaborCost { get; set; }
        public decimal TotalHoursWorked { get; set; }
    }

    // Model for Report Data (used for reporting labor costs and hours worked)
    public class ReportData
    {
        public decimal TotalLaborCost { get; set; }
        public decimal TotalHoursWorked { get; set; }
    }

    // Wages model (database model for wages table)
    public class Wages
    {
        public int Employee_ID { get; set; }
        public DateTime Payperiod_Start { get; set; }
        public DateTime Payperiod_End { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Hourly_Rate { get; set; }
        public decimal Hours_Worked { get; set; }
    }
}
