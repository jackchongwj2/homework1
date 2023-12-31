USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[CheckResultConditions]    Script Date: 12/28/2023 1:54:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CheckResultConditions]
    @StudentId INT,
    @SubjectId INT,
    @NotPassedSubjectCount INT OUTPUT, -- Count of subjects with marks >= 50
    @ResultExists BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SET @ResultExists = 0;

    -- Count number of subjects taken that have not been passed
    SELECT @NotPassedSubjectCount = COUNT(*) - COUNT(CASE WHEN Marks >= 50 THEN 1 ELSE NULL END)
    FROM dbo.Results WITH (NOLOCK)
    WHERE StudentId = @StudentId AND IsDeleted = 0;

    -- Check if the student has already taken the same subject
    SELECT @ResultExists = CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END
    FROM dbo.Results WITH (NOLOCK)
    WHERE StudentId = @StudentId AND SubjectId = @SubjectId AND IsDeleted = 0;
END;
GO
