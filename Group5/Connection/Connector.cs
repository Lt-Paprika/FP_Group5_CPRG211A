namespace Group5.Connections
{
    public static class DatabaseConnection
    {
        public static SQLiteConnection GetConnection()
        {
            string dbPath = Path.Combine(Environment.CurrentDirectory, "Connections", "CPRG211_Employees.db");
            return new SQLiteConnection(dbPath);
        }
    }
}
