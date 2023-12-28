USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[GetAllSubjects]    Script Date: 12/26/2023 11:47:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllSubjects]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.Subjects WITH (NOLOCK) WHERE IsDeleted = 0;
END;
GO
