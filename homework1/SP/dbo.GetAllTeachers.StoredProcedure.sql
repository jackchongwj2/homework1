USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[GetAllTeachers]    Script Date: 12/28/2023 1:54:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllTeachers]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.Teachers WITH (NOLOCK) WHERE IsDeleted = 0;
END;
GO
