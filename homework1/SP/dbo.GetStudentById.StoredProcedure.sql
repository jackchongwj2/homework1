USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[GetStudentById]    Script Date: 12/28/2023 1:54:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetStudentById]
    @StudentId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.Students WITH (NOLOCK) WHERE StudentId = @StudentId AND IsDeleted = 0;
END;
GO
