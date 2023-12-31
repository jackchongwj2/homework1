USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[CheckStudentExists]    Script Date: 12/28/2023 1:54:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CheckStudentExists]
    @Email VARCHAR(30),
    @StudentExists BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SET @StudentExists = 0;

    IF EXISTS (SELECT 1 FROM dbo.Students WITH (NOLOCK) WHERE Email = @Email)
    BEGIN
        SET @StudentExists = 1;
    END;
END;
GO
