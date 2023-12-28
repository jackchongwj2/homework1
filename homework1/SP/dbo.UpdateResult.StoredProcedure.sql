USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[UpdateResult]    Script Date: 12/26/2023 11:47:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateResult]
    @ResultId INT,
    @Marks NUMERIC(4,1)
AS
BEGIN
	SET NOCOUNT ON;

    UPDATE dbo.Results
    SET
        Marks = @Marks
    WHERE
        ResultId = @ResultId AND IsDeleted = 0;
END
GO
