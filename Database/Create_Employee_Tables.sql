/* Create Tables Script for CPRG211_Employees Database
    Script will drop all the tables in case they exist already,
    Following that each table is created with constraints.
*/

/* Drop Table commands */
DROP TABLE IF EXISTS CPRG211_Stat_Tracker;
DROP TABLE IF EXISTS CPRG211_Stat_Days;
DROP TABLE IF EXISTS CPRG211_Wages;
DROP TABLE IF EXISTS CPRG211_Shift_Log;
DROP TABLE IF EXISTS CPRG211_Schedule_Detail;
DROP TABLE IF EXISTS CPRG211_Schedule;
DROP TABLE IF EXISTS CPRG211_Employee;


/* Creating the Employee Table (Base of the majority of the Database) */
CREATE TABLE CPRG211_Employee (
    Employee_ID INT PRIMARY KEY,
    Hash_Password VARCHAR(64) NOT NULL,
    First_Name VARCHAR(50) NOT NULL,
    Last_Name VARCHAR(50) NOT NULL,
    Employment_Status ENUM('Active', 'Terminated', 'Leave', 'Retired') NOT NULL,
    Phone_Number VARCHAR(12) NOT NULL,
    Email VARCHAR(100),
    Employee_Role ENUM('Employee', 'Manager') NOT NULL,
    CONSTRAINT SYS_EMP_PH_CK CHECK (Phone_Number REGEXP '^[0-9]{3}\\.[0-9]{3}\\.[0-9]{4}$'), -- 999.999.9999 format
    CONSTRAINT SYS_EMP_EML_CK CHECK (Email REGEXP '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$'), -- basic email regex
    CONSTRAINT SYS_EMP_ROLE_CK CHECK (Employee_Role IN ('Employee', 'Manager')),
    CONSTRAINT SYS_EMP_STS_CK CHECK (Employment_Status IN ('Active', 'Terminated', 'Leave', 'Retired'))
);


/* Creating the schedule table, 
this table allows a overlook at the schedule over a two week period 
allows a better seperation of data for pay or management*/
CREATE TABLE CPRG211_Shchedule (
    Employee_ID INT(9) NOT NULL,
    Current_Payperiod DATE NOT NULL,
    Requested_Off BOOLEAN,
    Approval_Status ENUM('Pending', 'Approved', 'Denied'),
    Stat_Eligible BOOLEAN,
    Manager_ID INT(9),
    PRIMARY KEY (Employee_ID, Current_Payperiod),
    FOREIGN KEY (Employee_ID) REFERENCES CPRG211_Employee(Employee_ID),
    FOREIGN KEY (Manager_ID) REFERENCES CPRG211_Employee(Employee_ID),
    CONSTRAINT SYS_SCHE_APPR_CK CHECK (Approval_Status IN ('Pending', 'Approved', 'Denied')),
    CONSTRAINT SYS_SCHE_ROFF_CK CHECK (Requested_Off = 0 OR Approval_Status IS NOT NULL)
);

/* Schedule detail table creation.
Table is for the creation of staff schedules and organizing and tacking intended shifts */
CREATE TABLE CPRG211_Schedule_Detail (
    Employee_ID INT(9) NOT NULL,
    Scheduled_Date DATE NOT NULL,
    Start_Time TIME,
    End_Time TIME,
    Break_Duration INT,
    Stat_Eligible BOOLEAN,
    PRIMARY KEY (Employee_ID, Scheduled_Date),
    FOREIGN KEY (Employee_ID) REFERENCES CPRG211_Employee(Employee_ID),
    CONSTRAINT SYS_SCHDTL_TIME_CK CHECK (Start_Time < End_Time),
    CONSTRAINT SYS_SCHD_UK UNIQUE (Employee_ID, Scheduled_Date)
);

/* Shift log table created to track the actual shifts worked by staff */
CREATE TABLE CPRG211_Shift_Log (
    Shift_ID INT PRIMARY KEY,
    Employee_ID INT NOT NULL,
    Shift_Date DATE,
    Clock_In TIME,
    Clock_Out TIME,
    Break_Start TIME,
    Break_End TIME,
    FOREIGN KEY (Employee_ID) REFERENCES CPRG211_Employee(Employee_ID),
    CONSTRAINT SYS_SHFT_CLOCK_CK CHECK (Clock_In < Clock_Out),
    CONSTRAINT SYS_SHFT_BRK_CK CHECK (
        Break_Start IS NULL OR
        (Clock_In < Break_Start AND Break_Start < Break_End AND Break_End < Clock_Out)
    ),
    CONSTRAINT SYS_SHIFT_UK UNIQUE (Employee_ID, Shift_Date)
);


/*Tracking what days are stat days either optional and paid or not using a code to identify each individual stat */
CREATE TABLE CPRG211_Stat_Days (
    Stat_Code  INT(2) PRIMARY KEY,
    Workday VARCHAR(15) NOT NULL,
    Stat_Name VARCHAR(50) NOT NULL,
    Paid_Optional_Stat ENUM ('yes', 'No')
);

/*Tracking the stats for employees, Table is created to assist in organizing data related to stat pay
E.G. If an employee has worked 5/9 last regular days of a stats weekday they are entitled to avg days pay
as well as 1.5 wages for hours worked on that day*/
CREATE TABLE CPRG211_Stat_Tracker (
    Employee_ID INT NOT NULL,
    Stat_Code INT NOT NULL,
    Eligible BOOLEAN NOT NULL,
    Stat_Avg_Hours DECIMAL(4, 2),
    PRIMARY KEY (Employee_ID, Stat_Code),
    FOREIGN KEY (Employee_ID) REFERENCES CPRG211_Employee(Employee_ID),
    FOREIGN KEY (Stat_Code) REFERENCES CPRG211_Stat_Days(Stat_Code),
    CONSTRAINT SYS_STRACK_HRS_CK CHECK (Stat_Avg_Hours >= 0)
);

/* Table creation to track employee pay data */
CREATE TABLE CPRG211_Wages (
    Employee_ID INT(9) NOT NULL,
    Payperiod_Start DATE NOT NULL,
    Payperiod_End DATE NOT NULL,
    Salary DECIMAL(10, 2),         -- Allows up to 99999999.99
    Hourly_Rate DECIMAL(6, 2),     -- Up to 9999.99
    Hours_Worked DECIMAL(5, 2),    -- Up to 999.99 hours
    Stat_Pay DECIMAL(10, 2),
    Total_Pay DECIMAL(10, 2),
    PRIMARY KEY (Employee_ID, Payperiod_Start),
    FOREIGN KEY (Employee_ID) REFERENCES CPRG211_Employee(Employee_ID),
    CONSTRAINT SYS_WAGES_PAY_CK CHECK (
        (Salary IS NOT NULL AND Hourly_Rate IS NULL) OR
        (Salary IS NULL AND Hourly_Rate IS NOT NULL)
    ),
    CONSTRAINT SYS_WAGES_TPAY_CK CHECK (Total_Pay > 0)
);
