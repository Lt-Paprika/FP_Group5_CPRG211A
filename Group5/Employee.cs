using SQLite;

namespace Group5

{
    public class Employee
    {
        [PrimaryKey]
        public string EmplyeeID { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}