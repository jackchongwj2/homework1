USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[CheckSubjectExists]    Script Date: 12/26/2023 11:47:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CheckSubjectExists]
    @Name NVARCHAR(30),
    @Code VARCHAR(8),
    @SubjectExists BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SET @SubjectExists = 0;

    IF EXISTS (SELECT 1 FROM dbo.Subjects WITH (NOLOCK) WHERE Name = @Name OR Code = @Code)
    BEGIN
        SET @SubjectExists = 1;
    END;
END;
GO
