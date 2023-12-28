USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[GetResultById]    Script Date: 12/26/2023 11:47:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetResultById]
    @ResultId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.Results WITH (NOLOCK) WHERE ResultId = @ResultId AND IsDeleted = 0;
END;
GO
