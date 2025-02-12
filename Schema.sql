USE [Project_23TH0003]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 2/13/2025 4:00:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 2/13/2025 4:00:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 2/13/2025 4:00:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 2/13/2025 4:00:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 2/13/2025 4:00:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Classes]    Script Date: 2/13/2025 4:00:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classes](
	[ClassID] [int] IDENTITY(1,1) NOT NULL,
	[CourseID] [int] NOT NULL,
	[InstructorID] [int] NOT NULL,
	[Semester] [tinyint] NOT NULL,
	[Year] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 2/13/2025 4:00:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[CourseID] [int] IDENTITY(1,1) NOT NULL,
	[CourseName] [nvarchar](100) NOT NULL,
	[Credits] [int] NOT NULL,
	[DepartmentID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 2/13/2025 4:00:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[DepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](100) NOT NULL,
	[DeanID] [int] NULL,
	[Created_at] [datetime] NULL,
	[Updated_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Enrollments]    Script Date: 2/13/2025 4:00:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enrollments](
	[EnrollmentID] [int] IDENTITY(1,1) NOT NULL,
	[StudentID] [int] NOT NULL,
	[ClassID] [int] NOT NULL,
	[Midterm] [decimal](5, 2) NULL,
	[Final] [decimal](5, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[EnrollmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Instructors]    Script Date: 2/13/2025 4:00:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Instructors](
	[InstructorID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[FullName] [nvarchar](100) NULL,
	[Phone] [nvarchar](15) NULL,
	[DepartmentID] [int] NULL,
 CONSTRAINT [PK__Instruct__9D010B7BC836CB28] PRIMARY KEY CLUSTERED 
(
	[InstructorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 2/13/2025 4:00:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[StudentID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[FullName] [nvarchar](100) NULL,
	[DateOfBirth] [date] NULL,
	[Gender] [bit] NULL,
	[Phone] [varchar](15) NULL,
	[Address] [nvarchar](255) NULL,
 CONSTRAINT [PK__Students__32C52A79A4166E0F] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Departments] ADD  CONSTRAINT [DF_Departments_CreationDate]  DEFAULT (getdate()) FOR [Created_at]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD FOREIGN KEY([CourseID])
REFERENCES [dbo].[Courses] ([CourseID])
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK__Classes__Instruc__5DCAEF64] FOREIGN KEY([InstructorID])
REFERENCES [dbo].[Instructors] ([InstructorID])
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK__Classes__Instruc__5DCAEF64]
GO
ALTER TABLE [dbo].[Courses]  WITH CHECK ADD FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Departments] ([DepartmentID])
GO
ALTER TABLE [dbo].[Departments]  WITH CHECK ADD  CONSTRAINT [FK__Departmen__DeanI__5441852A] FOREIGN KEY([DeanID])
REFERENCES [dbo].[Instructors] ([InstructorID])
GO
ALTER TABLE [dbo].[Departments] CHECK CONSTRAINT [FK__Departmen__DeanI__5441852A]
GO
ALTER TABLE [dbo].[Enrollments]  WITH CHECK ADD FOREIGN KEY([ClassID])
REFERENCES [dbo].[Classes] ([ClassID])
GO
ALTER TABLE [dbo].[Enrollments]  WITH CHECK ADD  CONSTRAINT [FK__Enrollmen__Stude__6754599E] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Students] ([StudentID])
GO
ALTER TABLE [dbo].[Enrollments] CHECK CONSTRAINT [FK__Enrollmen__Stude__6754599E]
GO
ALTER TABLE [dbo].[Instructors]  WITH CHECK ADD  CONSTRAINT [FK__Instructo__Depar__693CA210] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Departments] ([DepartmentID])
GO
ALTER TABLE [dbo].[Instructors] CHECK CONSTRAINT [FK__Instructo__Depar__693CA210]
GO
ALTER TABLE [dbo].[Instructors]  WITH CHECK ADD  CONSTRAINT [FK_Instructors_AspNetUsers] FOREIGN KEY([UserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Instructors] CHECK CONSTRAINT [FK_Instructors_AspNetUsers]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_AspNetUsers] FOREIGN KEY([UserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_AspNetUsers]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD CHECK  (([Semester]=(3) OR [Semester]=(2) OR [Semester]=(1)))
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD CHECK  (([Year]>=(1900)))
GO
ALTER TABLE [dbo].[Courses]  WITH CHECK ADD CHECK  (([Credits]>(0)))
GO
ALTER TABLE [dbo].[Enrollments]  WITH CHECK ADD CHECK  (([Final]>=(0) AND [Final]<=(10)))
GO
ALTER TABLE [dbo].[Enrollments]  WITH CHECK ADD CHECK  (([Midterm]>=(0) AND [Midterm]<=(10)))
GO
/****** Object:  StoredProcedure [dbo].[Class_TimKiem]    Script Date: 2/13/2025 4:00:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Class_TimKiem] 
@CourseID int = NULL,
@InstructorID int = NULL,
@Semester int = NULL,
@Year int = NULL
AS 
BEGIN
	DECLARE @SqlStr NVARCHAR(4000),
			@ParamList NVARCHAR(2000)

	SET @SqlStr = N'SELECT * FROM Classes WHERE (1=1)'
	SET @ParamList = N'@CourseID int, @InstructorID INT, @Semester INT, @Year int'

if @CourseID is not null
	begin
	SET @SqlStr = @SqlStr + ' AND CourseID = @CourseID'
	end
	if @InstructorID is not null
		begin
		SET @SqlStr = @SqlStr + 'AND InstructorID = @InstructorID'
		end

	if @Semester is not null
		begin
		SET @SqlStr = @SqlStr + 'AND Semester = @Semester'
		end
	if @Year is not null
		begin
		SET @SqlStr = @SqlStr + 'AND Year = @Year'
		end
	EXEC sp_executesql @SqlStr,
                       @ParamList,
                       @CourseID, @InstructorID, @Semester, @Year
END
GO
/****** Object:  StoredProcedure [dbo].[Instructor_TimKiem]    Script Date: 2/13/2025 4:00:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Instructor_TimKiem] 
@FullName nvarchar(50) = NULL,
@Phone varchar(50) = NULL,
@DepartmentID int = NULL
AS 
BEGIN
	DECLARE @SqlStr NVARCHAR(4000)

	SET @SqlStr = N'SELECT * FROM Instructors WHERE (1=1)'

	if @FullName is not null
		BEGIN
		SET @SqlStr = @SqlStr + N' AND FullName COLLATE Latin1_General_CI_AI LIKE N''%' + @FullName + '%'''
		END
	if @Phone is not null
		BEGIN
		SET @SqlStr = @SqlStr + N' AND Phone COLLATE Latin1_General_CI_AI LIKE N''%' + @Phone + '%'''
		END
	if @DepartmentID is not null
		BEGIN
		SET @SqlStr = @SqlStr + ' AND DepartmentID = @DepartmentID'
		END
	EXEC sp_executesql @SqlStr,
							N'@FullName NVARCHAR(50), @Phone VARCHAR(50), @DepartmentID int',
							@FullName,
							@Phone,
							@DepartmentID
END
GO
/****** Object:  StoredProcedure [dbo].[Khoa_TimKiem]    Script Date: 2/13/2025 4:00:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Khoa_TimKiem] 
@DepartmentName nvarchar(100) = NULL
as 
begin
	DECLARE @SqlStr NVARCHAR(4000),
	@ParamList NVARCHAR(2000)

	SET @SqlStr = N'SELECT * FROM Departments WHERE (1=1)'
	SET @ParamList = N'@DepartmentName NVARCHAR(100)'

	if @DepartmentName is not null
		begin
		SET @SqlStr = @SqlStr + N' AND DepartmentName COLLATE Latin1_General_CI_AI LIKE N''%' + @DepartmentName + '%'''
		end
		EXEC sp_executesql @SqlStr,
							@ParamList,
							@DepartmentName
end
GO
/****** Object:  StoredProcedure [dbo].[KhoaHoc_TimKiem]    Script Date: 2/13/2025 4:00:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[KhoaHoc_TimKiem] 
@CourseName nvarchar(100) = NULL,
@DepartmentID int = NULL,
@Credits int = NULL
AS 
BEGIN
	DECLARE @SqlStr NVARCHAR(4000),
			@ParamList NVARCHAR(2000)

	SET @SqlStr = N'SELECT * FROM Courses WHERE (1=1)'
	SET @ParamList = N'@CourseName NVARCHAR(100), @DepartmentID INT, @Credits INT'

if @CourseName is not null
	begin
	SET @SqlStr = @SqlStr + N' AND CourseName COLLATE Latin1_General_CI_AI LIKE N''%' + @CourseName + '%'''
	end
	if @DepartmentID is not null
		begin
		SET @SqlStr = @SqlStr + 'AND DepartmentID = @DepartmentID'
		end

	if @Credits is not null
		begin
		SET @SqlStr = @SqlStr + 'AND Credits = @Credits'
		end

	EXEC sp_executesql @SqlStr,
                       @ParamList,
                       @CourseName, @DepartmentID, @Credits
END
GO
/****** Object:  StoredProcedure [dbo].[Student_TimKiem]    Script Date: 2/13/2025 4:00:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Student_TimKiem] 
@FullName nvarchar(50) = NULL,
@Phone varchar(15) = NULL,
@Address nvarchar(255) = NULL,
@Gender bit = NULL,
@DateOfBirthFrom int = NULL,
@DateOfBirthTo int = NULL
AS 
BEGIN
	DECLARE @SqlStr NVARCHAR(4000)

	SET @SqlStr = N'SELECT * FROM Students WHERE (1=1)'

	if @FullName is not null
		BEGIN
		SET @SqlStr = @SqlStr + N' AND FullName COLLATE Latin1_General_CI_AI LIKE N''%' + @FullName + '%'''
		END
	if @Phone is not null
		BEGIN
		SET @SqlStr = @SqlStr + N' AND Phone COLLATE Latin1_General_CI_AI LIKE N''%' + @Phone + '%'''
		END
	if @Address is NOT NULL
		BEGIN
		SET @SqlStr = @SqlStr + N' AND Address COLLATE Latin1_General_CI_AI LIKE N''%' + @Address + '%'''
		END
	if @Gender is not null
		BEGIN
		SET @SqlStr = @SqlStr + ' AND Gender = @Gender'
		END
	if @DateOfBirthFrom is not null
		BEGIN
		SET @SqlStr = @SqlStr + ' AND YEAR(DateOfBirth) >= @DateOfBirthFrom'
		END
	if @DateOfBirthTo is not null
		BEGIN
		SET @SqlStr = @SqlStr + ' AND (YEARDateOfBirth) <= @DateOfBirthTo'
		END
	EXEC sp_executesql @SqlStr,
							N'@FullName NVARCHAR(50), @Phone VARCHAR(50), @Address NVARCHAR(255), @Gender bit, @DateOfBirthFrom int, @DateOfBirthTo int',
							@FullName,
							@Phone,
							@Address,
							@Gender,
							@DateOfBirthFrom,
							@DateOfBirthTo
END
GO
/****** Object:  StoredProcedure [dbo].[User_TimKiem]    Script Date: 2/13/2025 4:00:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[User_TimKiem] 
@RoleID varchar(50) = NULL,
@Email varchar(50) = NULL,
@Username nvarchar(100) = NULL
AS 
BEGIN
	DECLARE @SqlStr NVARCHAR(4000)

	SET @SqlStr = N'SELECT * FROM Users WHERE (1=1)'

	if @Email is not null
		BEGIN
		SET @SqlStr = @SqlStr + N' AND Email COLLATE Latin1_General_CI_AI LIKE N''%' + @Email + '%'''
		END
	if @Username is not null
		BEGIN
		SET @SqlStr = @SqlStr + N' AND Username COLLATE Latin1_General_CI_AI LIKE N''%' + @Username + '%'''
		END
	if @RoleID is not null
		BEGIN
		SET @SqlStr = @SqlStr + N' AND RoleID COLLATE Latin1_General_CI_AI LIKE N''%' + @RoleID + '%'''
		END
	EXEC sp_executesql @SqlStr,
							N'@RoleID VARCHAR(50), @Email VARCHAR(50), @Username NVARCHAR(100)',
							@RoleID,
							@Email,
							@Username
END
GO
