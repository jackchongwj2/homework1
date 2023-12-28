USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[GetAllStudents]    Script Date: 12/26/2023 11:47:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllStudents]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.Students WITH (NOLOCK) WHERE IsDeleted = 0;
END;
GO
