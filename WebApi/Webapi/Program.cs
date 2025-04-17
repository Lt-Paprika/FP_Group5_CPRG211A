using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]"]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var connString = _config.GetConnectionString("OracleDb");

        using var conn = new OracleConnection(connString);
        await conn.OpenAsync();

        var cmd = new OracleCommand("SELECT PasswordHash, Role FROM Employees WHERE EmployeeID = :ID", conn);
        cmd.Parameters.Add(new OracleParameter("ID", request.EmployeeID));

        using var reader = await cmd.ExecuteReaderAsync();
        if (!reader.HasRows) return Unauthorized("Invalid ID");

        await reader.ReadAsync();
        string hash = reader.GetString(0);
        string role = reader.GetString(1);

        if (!BCrypt.Net.BCrypt.Verify(request.Password, hash))
            return Unauthorized("Invalid password");
        
        return Ok(new { Role = role});
    }
}