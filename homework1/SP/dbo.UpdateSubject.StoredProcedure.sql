USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[UpdateSubject]    Script Date: 12/28/2023 1:54:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateSubject]
    @SubjectId INT,
    @Name NVARCHAR(30),
    @Code VARCHAR(8),
    @TeacherId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Subjects
    SET
        Name = @Name,
        Code = @Code,
        TeacherId = @TeacherId
    WHERE
        SubjectId = @SubjectId AND IsDeleted = 0;
END;
GO
