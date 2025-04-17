using oracle.ManagedDataAccess.Client;

public static class OracleAuthService
{
    public static async Task<bool> ValidateCredentialAsync(string employeeID, string password)
    {
        string connection = "Connection_String_Here";

        using var conn = new OracleConnection(connection);
        await conn.OpenAsync();

        string query = "SELECT PasswordHash FROM Employees WHERE EmployeeID = :empID";

        using var cmd = new OracleCommand(query, conn);
        cmd.Parameters.Add(new OracleParameter("empID", employeeID));

        var result = await cmd.ExecuteScalarAsync();
        if (result == null) return false;

        string storedHash = result.ToString();
        return Bcrypt.Net.BCrypt.Verify(password, storedHash);
    }
}