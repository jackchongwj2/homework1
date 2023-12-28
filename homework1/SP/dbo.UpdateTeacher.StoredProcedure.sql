USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[UpdateTeacher]    Script Date: 12/26/2023 11:47:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateTeacher]
	@TeacherId INT,
    @Name NVARCHAR(30),
    @Email VARCHAR(30),
    @PhoneNumber VARCHAR(15)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Teachers
    SET
        Name = @Name,
        Email = @Email,
        PhoneNumber = @PhoneNumber
    WHERE
        TeacherId = @TeacherId AND IsDeleted = 0;
END;
GO
