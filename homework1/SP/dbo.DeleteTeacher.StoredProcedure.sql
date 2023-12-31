USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[DeleteTeacher]    Script Date: 12/28/2023 1:54:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteTeacher]
    @TeacherId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Teachers
    SET IsDeleted = 1
    WHERE TeacherId = @TeacherId;
END;
GO
