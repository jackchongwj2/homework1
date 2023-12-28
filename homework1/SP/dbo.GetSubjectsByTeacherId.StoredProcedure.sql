USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[GetSubjectsByTeacherId]    Script Date: 12/26/2023 11:47:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSubjectsByTeacherId]
    @TeacherId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.Subjects WITH (NOLOCK)
	WHERE TeacherId = @TeacherId AND IsDeleted = 0;
END;
GO
