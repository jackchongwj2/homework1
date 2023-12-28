USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[DeleteSubject]    Script Date: 12/26/2023 11:47:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteSubject]
    @SubjectId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Subjects
    SET IsDeleted = 1
    WHERE SubjectId = @SubjectId;
END;
GO
