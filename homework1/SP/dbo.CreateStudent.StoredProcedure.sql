USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[CreateStudent]    Script Date: 12/26/2023 11:47:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateStudent]
    @Name NVARCHAR(30),
    @Email VARCHAR(30),
    @DOB DATE,
    @EnrollmentDate DATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Students (Name, Email, DOB, EnrollmentDate, IsDeleted)
    VALUES (@Name, @Email, @DOB, @EnrollmentDate, 0);
END;
GO
