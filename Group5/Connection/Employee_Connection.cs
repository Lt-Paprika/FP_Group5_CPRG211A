using Group5.Entities;

namespace Group5.Connection
{
    public class EmployeeService
    {
        readonly string dbPath = Path.Combine(Environment.CurrentDirectory, "Connections", "CPRG211_Employees.db");

        private readonly SQLiteAsyncConnection _database;

        public EmployeeService(string dbPath)
        {
            // Initialize the SQLite connection
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Employee>().Wait(); // Create the Employee table if not exists
        }

        // Insert a new employee
        public Task<int> AddEmployeeAsync(Employee employee)
        {
            return _database.InsertAsync(employee);
        }

        // Get all employees
        public Task<List<Employee>> GetAllEmployeesAsync()
        {
            return _database.Table<Employee>().ToListAsync();
        }

        // Get an employee by ID
        public Task<Employee> GetEmployeeAsync(int employeeID)
        {
            return _database.Table<Employee>().Where(e => e.EmployeeID == employeeID).FirstOrDefaultAsync();
        }

        // Update an employee's details
        public Task<int> UpdateEmployeeAsync(Employee employee)
        {
            return _database.UpdateAsync(employee);
        }

        // Delete an employee
        public Task<int> DeleteEmployeeAsync(Employee employee)
        {
            return _database.DeleteAsync(employee);
        }
    }
}
