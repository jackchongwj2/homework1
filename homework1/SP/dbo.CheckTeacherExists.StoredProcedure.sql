USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[CheckTeacherExists]    Script Date: 12/28/2023 1:54:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CheckTeacherExists]
    @Email VARCHAR(30),
    @PhoneNumber VARCHAR(15),
    @TeacherExists BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SET @TeacherExists = 0;

    IF EXISTS (SELECT 1 FROM dbo.Teachers WITH (NOLOCK) WHERE Email = @Email OR PhoneNumber = @PhoneNumber)
    BEGIN
        SET @TeacherExists = 1;
    END;
END;
GO
