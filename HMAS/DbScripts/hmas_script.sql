IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'HMAS_SERVER')
BEGIN
    CREATE DATABASE HMASDB;
END
GO

USE HMASDB;
GO


-- Create Users table
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(100) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role NVARCHAR(50) NOT NULL
);

-- Create Departments table
CREATE TABLE Departments (
    DepartmentId INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(100) NOT NULL
);

-- Create Doctors table
CREATE TABLE Doctors (
    DoctorId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Specialization NVARCHAR(100),
    DepartmentId INT NOT NULL,
    FOREIGN KEY (DepartmentId) REFERENCES Departments(DepartmentId)
);

-- Create Patients table
CREATE TABLE Patients (
    PatientId INT PRIMARY KEY IDENTITY(1,1),
    PatientName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100),
    PhoneNumber NVARCHAR(20),
    DOB DATE,
    Gender NVARCHAR(10)
);

-- Create Appointments table
CREATE TABLE Appointments (
    AppointmentId INT PRIMARY KEY IDENTITY(1,1),
    PatientId INT NOT NULL,
    DoctorId INT NOT NULL,
    AppointmentDate DATE NOT NULL,
    AppointmentTime TIME NOT NULL,
    Status INT NOT NULL,
    FOREIGN KEY (PatientId) REFERENCES Patients(PatientId),
    FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId)
);

-- Create DoctorAvailabilities table
CREATE TABLE DoctorAvailabilities (
    DoctorAvailabilityId INT PRIMARY KEY IDENTITY(1,1),
    DoctorId INT NOT NULL,
    Day INT NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId)
);

-- Create Leaves table
CREATE TABLE Leaves (
    LeaveId INT PRIMARY KEY IDENTITY(1,1),
    DoctorId INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    Reason NVARCHAR(255),
    FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId)
);

-- Create Records table
CREATE TABLE Records (
    Id INT PRIMARY KEY IDENTITY(1,1),
    AppointmentId INT NOT NULL,
    PatientId INT NOT NULL,
    Diagnosis NVARCHAR(255),
    Prescription NVARCHAR(255),
    Notes NVARCHAR(MAX),
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (AppointmentId) REFERENCES Appointments(AppointmentId),
    FOREIGN KEY (PatientId) REFERENCES Patients(PatientId)
);

-- Sample Seeding
INSERT INTO Departments (DepartmentName) VALUES ('Cardiology'), ('Neurology'), ('Pediatrics');

INSERT INTO Doctors (Name, Specialization, DepartmentId) VALUES
('Dr. Smith', 'Cardiologist', 1),
('Dr. Alice', 'Neurologist', 2),
('Dr. Bob', 'Pediatrician', 3);

INSERT INTO Patients (PatientName, Email, PhoneNumber, DOB, Gender) VALUES
('John Doe', 'john@example.com', '1234567890', '1985-05-15', 'Male'),
('Jane Roe', 'jane@example.com', '0987654321', '1990-08-20', 'Female');

INSERT INTO Users (Username, PasswordHash, Role) VALUES
('admin', '123456', 'Admin'),
('doctor', '123456', 'Doctor'),
('reception', '123456', 'Receptionist');
