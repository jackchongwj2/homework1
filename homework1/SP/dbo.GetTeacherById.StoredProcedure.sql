USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[GetTeacherById]    Script Date: 12/26/2023 11:47:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTeacherById]
    @TeacherId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.Teachers WITH (NOLOCK) WHERE TeacherId = @TeacherId AND IsDeleted = 0;
END;
GO
