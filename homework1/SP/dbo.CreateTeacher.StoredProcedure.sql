USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[CreateTeacher]    Script Date: 12/28/2023 1:54:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateTeacher]
    @Name NVARCHAR(30),
    @Email VARCHAR(30),
    @PhoneNumber VARCHAR(15)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Teachers (Name, Email, PhoneNumber, IsDeleted)
    VALUES (@Name, @Email, @PhoneNumber, 0);
END;
GO
