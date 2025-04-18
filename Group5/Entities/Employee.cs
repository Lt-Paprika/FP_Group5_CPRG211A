namespace Group5.Entities
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string HashPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmploymentStatus { get; set; }
        public string PhoneNumber { get; set; }      // Phone number needs to be in the format 999.999.9999
        public string Email { get; set; }
        public string EmployeeRole { get; set; }     // Employee or Manager
    }
}