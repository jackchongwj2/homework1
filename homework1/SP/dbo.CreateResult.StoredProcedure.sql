USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[CreateResult]    Script Date: 12/26/2023 11:47:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateResult]
    @Marks NUMERIC(4,1),
    @StudentId INT,
    @SubjectId INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Results (Marks, IsDeleted, StudentId, SubjectId)
    VALUES (@Marks, 0, @StudentId, @SubjectId)
END;
GO
