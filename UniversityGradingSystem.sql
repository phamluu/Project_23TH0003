-- Tạo cơ sở dữ liệu
CREATE DATABASE UniversityGradingSystem;
GO

-- Sử dụng cơ sở dữ liệu
USE UniversityGradingSystem;
GO

-- Tạo bảng Roles và thêm dữ liệu mặc định
CREATE TABLE Roles (
    RoleID INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(50) UNIQUE NOT NULL -- Ví dụ: 'Student', 'Instructor', 'Admin'
);

-- Thêm dữ liệu mặc định vào bảng Roles
INSERT INTO Roles (RoleName) VALUES ('Student'), ('Instructor'), ('Admin');
GO

-- Tạo bảng Users (thông tin người dùng và quyền)
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    RoleID INT NOT NULL,  -- Liên kết với bảng Roles để phân quyền
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);
GO

-- Tạo bảng Instructors (thêm UserID tham chiếu đến bảng Users)
CREATE TABLE Instructors (
    InstructorID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,  -- Liên kết với bảng Users
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Phone NVARCHAR(15),
    DepartmentID INT NULL,  -- Không bắt buộc
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO

-- Tạo bảng Departments trước bảng Instructors
CREATE TABLE Departments (
    DepartmentID INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName NVARCHAR(100) NOT NULL,
    DeanID INT NULL, -- Cho phép NULL nếu chưa có trưởng khoa
    FOREIGN KEY (DeanID) REFERENCES Instructors(InstructorID)
);
GO

-- Tạo bảng Courses
CREATE TABLE Courses (
    CourseID INT IDENTITY(1,1) PRIMARY KEY,
    CourseName NVARCHAR(100) NOT NULL,
    Credits INT NOT NULL CHECK (Credits > 0),
    DepartmentID INT NOT NULL,
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
);
GO

-- Tạo bảng Classes
CREATE TABLE Classes (
    ClassID INT IDENTITY(1,1) PRIMARY KEY,
    CourseID INT NOT NULL,
    InstructorID INT NOT NULL,
    Semester TINYINT NOT NULL CHECK (Semester IN (1, 2, 3)), -- 1: Xuân, 2: Hè, 3: Thu
    Year INT NOT NULL CHECK (Year >= 1900), -- Năm học
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID),
    FOREIGN KEY (InstructorID) REFERENCES Instructors(InstructorID)
);
GO

-- Tạo bảng Students (thêm UserID tham chiếu đến bảng Users)
CREATE TABLE Students (
    StudentID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,  -- Liên kết với bảng Users
    FullName NVARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender CHAR(1) CHECK (Gender IN ('M', 'F')) NOT NULL, -- 'M' cho Male, 'F' cho Female
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Phone NVARCHAR(15),
    Address NVARCHAR(255),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO

-- Tạo bảng Enrollments
CREATE TABLE Enrollments (
    EnrollmentID INT IDENTITY(1,1) PRIMARY KEY,
    StudentID INT NOT NULL,
    ClassID INT NOT NULL,
    Midterm DECIMAL(5, 2) NULL CHECK (Midterm BETWEEN 0 AND 10), -- Điểm giữa kỳ
    Final DECIMAL(5, 2) NULL CHECK (Final BETWEEN 0 AND 10), -- Điểm cuối kỳ
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    FOREIGN KEY (ClassID) REFERENCES Classes(ClassID)
);
GO

-- Thêm ràng buộc khóa ngoại giữa bảng `Instructors` và `Departments`
ALTER TABLE Instructors
ADD FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID);
GO

-- Thêm thông báo xác nhận
PRINT 'Database and tables created successfully!';
