USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[DeleteStudent]    Script Date: 12/26/2023 11:47:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteStudent]
    @StudentId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Students
    SET IsDeleted = 1
    WHERE StudentId = @StudentId;
END;
GO
