USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[CreateSubject]    Script Date: 12/26/2023 11:47:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateSubject]
    @Name NVARCHAR(30),
    @Code VARCHAR(8),
    @TeacherId INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Subjects (Name, Code, TeacherId, IsDeleted)
    VALUES (@Name, @Code, @TeacherId, 0);
END;
GO
