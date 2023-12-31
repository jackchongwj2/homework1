USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[UpdateStudent]    Script Date: 12/28/2023 1:54:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateStudent]
    @StudentId INT,
    @Name NVARCHAR(100),
    @Email VARCHAR(50),
    @DOB DATE,
    @EnrollmentDate DATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Students
    SET
        Name = @Name,
        Email = @Email,
        DOB = @DOB,
        EnrollmentDate = @EnrollmentDate
    WHERE
        StudentId = @StudentId AND IsDeleted = 0;
END;
GO
