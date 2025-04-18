-- Script to fill out database with some data to give us things to work with.
-- Setting it to use the right database.
-- Script to fill out database with some data to give us things to work with.
-- Setting it to use the right database.
USE CPRG211_Employees;

-- Disable foreign key checks temporarily
SET FOREIGN_KEY_CHECKS = 0;

-- Delete from child tables first
DELETE FROM CPRG211_Wages;
DELETE FROM CPRG211_Shift_Log;
DELETE FROM CPRG211_Schedule_Detail;
DELETE FROM CPRG211_Schedule;
DELETE FROM CPRG211_Stat_Days;

-- Then from the base table
DELETE FROM CPRG211_Employee;

-- Optionally reset auto-increment counters (if used)
ALTER TABLE CPRG211_Employee AUTO_INCREMENT = 1;
ALTER TABLE CPRG211_Shift_Log AUTO_INCREMENT = 1;
ALTER TABLE CPRG211_Schedule_Detail AUTO_INCREMENT = 1;
ALTER TABLE CPRG211_Stat_Days AUTO_INCREMENT = 1;

-- Re-enable foreign key checks
SET FOREIGN_KEY_CHECKS = 1;


INSERT INTO CPRG211_Employee 
(Employee_ID, Hash_Password, First_Name, Last_Name, Employment_Status, Phone_Number, Email, Employee_Role) 
VALUES
(1001, 'e3afed0047b08059d0fada10f400c1e5f6a5486b1b2016da582b1fbf9b6f57f6', 'Alice', 'Walker', 'Active', '403.555.1234', 'alice.walker@gmail.com', 'Manager'),
(1006, 'e3afed0047b08059d0fada10f400c1e5f6a5486b1b2016da582b1fbf9b6f57f6', 'Fatima', 'Chen', 'Active', '587.555.3344', 'fatima.chen@outlook.com', 'Manager'),
(1002, 'e3afed0047b08059d0fada10f400c1e5f6a5486b1b2016da582b1fbf9b6f57f6', 'Bob', 'Smith', 'Active', '403.555.5678', 'bob.smith@gmail.com', 'Employee'),
(1003, 'e3afed0047b08059d0fada10f400c1e5f6a5486b1b2016da582b1fbf9b6f57f6', 'Charlie', 'Nguyen', 'Leave', '587.555.1122', 'charlie.nguyen@outlook.com', 'Employee'),
(1004, 'e3afed0047b08059d0fada10f400c1e5f6a5486b1b2016da582b1fbf9b6f57f6', 'Diana', 'Lopez', 'Active', '403.555.7890', 'diana.lopez@hotmail.com', 'Employee'),
(1005, 'e3afed0047b08059d0fada10f400c1e5f6a5486b1b2016da582b1fbf9b6f57f6', 'Ethan', 'Patel', 'Terminated', '403.555.2233', 'ethan.patel@gmail.com', 'Employee'),
(1007, 'e3afed0047b08059d0fada10f400c1e5f6a5486b1b2016da582b1fbf9b6f57f6', 'George', 'Brown', 'Active', '403.555.4455', 'george.brown@gmail.com', 'Employee'),
(1008, 'e3afed0047b08059d0fada10f400c1e5f6a5486b1b2016da582b1fbf9b6f57f6', 'Hannah', 'Wilson', 'Leave', '403.555.5566', 'hannah.wilson@gmail.com', 'Employee'),
(1009, 'e3afed0047b08059d0fada10f400c1e5f6a5486b1b2016da582b1fbf9b6f57f6', 'Isaac', 'Lee', 'Active', '403.555.6677', 'isaac.lee@gmail.com', 'Employee'),
(1010, 'e3afed0047b08059d0fada10f400c1e5f6a5486b1b2016da582b1fbf9b6f57f6', 'Jenna', 'Kim', 'Active', '403.555.7788', 'jenna.kim@gmail.com', 'Employee'),
(1011, 'e3afed0047b08059d0fada10f400c1e5f6a5486b1b2016da582b1fbf9b6f57f6', 'Kevin', 'Johnson', 'Retired', '403.555.8899', 'kevin.johnson@gmail.com', 'Employee'),
(1012, 'e3afed0047b08059d0fada10f400c1e5f6a5486b1b2016da582b1fbf9b6f57f6', 'Lily', 'Martinez', 'Active', '587.555.9900', 'lily.martinez@gmail.com', 'Employee');


INSERT INTO CPRG211_Schedule 
(Employee_ID, Current_Payperiod, Requested_Off, Approval_Status, Manager_ID)
VALUES
(1001, '2025-04-01', False, null, 1001),
(1002, '2025-04-01', False, null, null),
(1007, '2025-04-01', True, 'Approved', null),
(1009, '2025-04-01', False, null, null),
(1010, '2025-04-01', False, null, null),
(1011, '2025-04-01', False, null, null),
(1012, '2025-04-01', False, null, null);


INSERT INTO CPRG211_Schedule_Detail 
(Schedule_ID, Employee_ID, Scheduled_Date, Start_Time, End_Time, Break_Duration, Stat_Day)
VALUES
(1, 1002, '2025-04-01', '09:00', '17:00', 30, False),
(2, 1002, '2025-04-02', '09:00', '17:00', 30, False),
(3, 1002, '2025-04-03', '09:00', '17:00', 30, False),
(4, 1002, '2025-04-04', '09:00', '17:00', 30, False),
(5, 1002, '2025-04-05', '09:00', '17:00', 30, False),
(6, 1002, '2025-04-08', '09:00', '17:00', 30, False),
(7, 1002, '2025-04-09', '09:00', '17:00', 30, False),
(8, 1002, '2025-04-10', '09:00', '17:00', 30, False),
(9, 1002, '2025-04-11', '09:00', '17:00', 30, False),
(10, 1002, '2025-04-12', '09:00', '17:00', 30, False),

(19, 1007, '2025-04-02', '10:00', '18:00', 30, False),
(20, 1007, '2025-04-03', '10:00', '18:00', 30, False),
(21, 1007, '2025-04-04', '10:00', '18:00', 30, False),
(22, 1007, '2025-04-08', '10:00', '18:00', 30, False),
(23, 1007, '2025-04-09', '10:00', '18:00', 30, False),
(24, 1007, '2025-04-10', '10:00', '18:00', 30, False),

(25, 1009, '2025-04-01', '14:00', '17:00', 0, False),
(26, 1009, '2025-04-05', '14:00', '17:00', 0, False),
(27, 1009, '2025-04-06', '14:00', '17:00', 0, False),
(28, 1009, '2025-04-07', '14:00', '17:00', 0, False),
(29, 1009, '2025-04-11', '14:00', '17:00', 0, False),
(30, 1009, '2025-04-12', '14:00', '17:00', 0, False),

(31, 1010, '2025-04-04', '14:00', '22:00', 30, False),
(32, 1010, '2025-04-05', '14:00', '22:00', 30, False),
(33, 1010, '2025-04-06', '14:00', '22:00', 30, False),
(34, 1010, '2025-04-07', '14:00', '22:00', 30, False),
(35, 1010, '2025-04-08', '14:00', '22:00', 30, False),
(36, 1010, '2025-04-09', '14:00', '22:00', 30, False),
(37, 1010, '2025-04-11', '14:00', '22:00', 30, False),
(38, 1010, '2025-04-12', '14:00', '22:00', 30, False),
(39, 1010, '2025-04-13', '14:00', '22:00', 30, False),
(40, 1010, '2025-04-14', '14:00', '22:00', 30, False),

(42, 1011, '2025-04-01', '16:00', '22:00', 0, False),
(43, 1011, '2025-04-02', '16:00', '22:00', 0, False),
(44, 1011, '2025-04-09', '16:00', '22:00', 0, False),
(45, 1011, '2025-04-10', '16:00', '22:00', 0, False),

(46, 1012, '2025-04-06', '09:00', '15:00', 0, False),
(47, 1012, '2025-04-07', '09:00', '15:00', 0, False),
(48, 1012, '2025-04-13', '09:00', '15:00', 0, False),
(49, 1012, '2025-04-14', '09:00', '15:00', 0, False);


INSERT INTO CPRG211_Shift_Log 
(Shift_ID, Employee_ID, Clock_In, Clock_Out, Break_Start, Break_End)
VALUES
(1, 1002, '09:00', '17:00', '12:30', '13:00'),
(2, 1002, '09:00', '16:55', '12:30', '13:00'),
(3, 1002, '09:04', '17:01', '12:32', '13:02'),
(4, 1002, '09:00', '17:00', '12:30', '13:00'),
(5, 1002, '09:00', '17:00', '12:30', '13:00'),
(6, 1002, '09:01', '17:05', '12:31', '13:01'),
(7, 1002, '09:00', '17:00', '12:30', '13:00'),
(8, 1002, '09:15', '16:50', '12:32', '13:02'),
(9, 1002, '09:00', '17:02', '12:31', '13:01'),
(10, 1002, '08:55', '17:00', '12:30', '13:00'),

(19, 1007, '10:10', '18:00', '13:30', '14:00'),
(20, 1007, '10:15', '18:00', '13:32', '14:02'),
(21, 1007, '10:09', '18:00', '13:30', '14:00'),
(22, 1007, '10:08', '18:00', '13:30', '14:00'),
(23, 1007, '10:16', '18:00', '13:33', '14:03'),
(24, 1007, '10:10', '18:00', '13:30', '14:00'),

(25, 1009, '14:00', '17:00', NULL, NULL),
(26, 1009, '14:00', '17:00', NULL, NULL),
(27, 1009, '14:00', '17:00', NULL, NULL),
(28, 1009, '14:00', '17:00', NULL, NULL),
(29, 1009, '14:00', '17:00', NULL, NULL),
(30, 1009, '14:00', '17:00', NULL, NULL),

(31, 1010, '14:02', '22:00', '17:02', '17:32'),
(32, 1010, '14:01', '22:00', '17:01', '17:31'),
(33, 1010, '14:00', '22:00', '17:00', '17:30'),
(34, 1010, '14:00', '22:00', '17:00', '17:30'),
(35, 1010, '14:00', '22:00', '17:00', '17:30'),
(36, 1010, '14:00', '22:00', '17:00', '17:30'),
(37, 1010, '13:55', '22:05', '17:00', '17:30'),
(38, 1010, '14:00', '22:00', '17:00', '17:30'),
(39, 1010, '14:00', '22:00', '17:00', '17:30'),
(40, 1010, '14:00', '22:00', '17:00', '17:30'),

(42, 1011, '16:00', '22:00', '18:00', '18:30'),
(43, 1011, '16:00', '22:00', '18:00', '18:30'),
(44, 1011, '16:00', '22:00', '18:00', '18:30'),
(45, 1011, '16:00', '22:00', '18:00', '18:30'),

(46, 1012, '09:00', '15:00', NULL, NULL),
(47, 1012, '09:00', '15:00', NULL, NULL),
(48, 1012, '09:00', '15:00', NULL, NULL),
(49, 1012, '09:00', '15:00', NULL, NULL);


INSERT INTO CPRG211_Stat_Days 
(Stat_Code, Workday, Stat_Name, Paid_Optional_Stat)
VALUES
(01, 'Wednesday', 'New Years Day', 'Paid'),
(02, 'Monday', 'Family Day', 'Paid'),
(03, 'Friday', 'Good Friday', 'Paid'),
(04, 'Monday', 'Victoria Day', 'Paid'),
(05, 'Tuesday', 'Canada Day', 'Paid'),
(06, 'Monday', 'Heritage Day', 'No'),
(07, 'Monday', 'Labour Day', 'Paid'),
(08, 'Tuesday', 'National Reconciliation Day', 'Paid'),
(09, 'Monday', 'Thanksgiving', 'Paid'),
(10, 'Tuesday', 'Remembrance Day', 'Paid'),
(11, 'Thursday', 'Christmas Day', 'Paid'),
(12, 'Friday', 'Boxing Day', 'Paid'),
(13, 'Monday', 'Easter Monday', 'No');