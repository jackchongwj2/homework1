USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[GetSubjectById]    Script Date: 12/28/2023 1:54:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSubjectById]
    @SubjectId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.Subjects WITH (NOLOCK) WHERE SubjectId = @SubjectId AND IsDeleted = 0;
END;
GO
