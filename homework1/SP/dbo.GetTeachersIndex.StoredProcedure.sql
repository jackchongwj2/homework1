USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[GetTeachersIndex]    Script Date: 12/28/2023 1:54:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTeachersIndex]
    @PageNumber INT,
    @PageSize INT,
    @SearchTerm NVARCHAR(30),
    @TotalCount INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StartRow INT, @EndRow INT;

    SET @StartRow = (@PageNumber - 1) * @PageSize + 1;
    SET @EndRow = @PageNumber * @PageSize;

    WITH OrderedTeachers AS
    (
        SELECT 
            ROW_NUMBER() OVER (ORDER BY Name ASC) AS RowNum, 
            *
        FROM 
            dbo.Teachers WITH (NOLOCK)
        WHERE
            (@SearchTerm IS NULL OR 
                Name LIKE '%' + @SearchTerm + '%' OR
                Email LIKE '%' + @SearchTerm + '%' OR
                PhoneNumber LIKE '%' + @SearchTerm + '%')
            AND IsDeleted = 0
    )

    SELECT
        *
    FROM
        OrderedTeachers WITH (NOLOCK)
    WHERE
        RowNum BETWEEN @StartRow AND @EndRow;

    SET @TotalCount = (
        SELECT COUNT(*)
        FROM dbo.Teachers WITH (NOLOCK)
        WHERE
            (@SearchTerm IS NULL OR 
                Name LIKE '%' + @SearchTerm + '%' OR
                Email LIKE '%' + @SearchTerm + '%' OR
                PhoneNumber LIKE '%' + @SearchTerm + '%')
            AND IsDeleted = 0
    );
END;
GO
