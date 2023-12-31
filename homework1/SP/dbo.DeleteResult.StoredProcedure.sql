USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[DeleteResult]    Script Date: 12/28/2023 1:54:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteResult]
    @ResultId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Results
    SET IsDeleted = 1
    WHERE ResultId = @ResultId;
END;
GO
