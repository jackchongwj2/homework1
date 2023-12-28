USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[GetAvailableSubjectsByStudentId]    Script Date: 12/26/2023 11:47:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAvailableSubjectsByStudentId]
    @StudentID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Subjects WITH (NOLOCK)
    WHERE SubjectId NOT IN (
        SELECT SubjectID
        FROM Results WITH (NOLOCK)
        WHERE StudentId = @StudentId 
    ) AND IsDeleted = 0
END
GO
