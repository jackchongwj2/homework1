USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[GetStudentsIndex]    Script Date: 12/28/2023 1:54:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetStudentsIndex]
    @PageNumber INT,
    @PageSize INT,
    @SearchTerm NVARCHAR(30) = NULL,
    @TotalCount INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StartRow INT, @EndRow INT;

    SET @StartRow = (@PageNumber - 1) * @PageSize + 1;
    SET @EndRow = @PageNumber * @PageSize;

    WITH OrderedStudents AS
    (
        SELECT 
            ROW_NUMBER() OVER (ORDER BY StudentId) AS RowNum, 
            *
        FROM 
            dbo.Students WITH (NOLOCK)
        WHERE
            (@SearchTerm IS NULL OR 
                Name LIKE '%' + @SearchTerm + '%' OR
                Email LIKE '%' + @SearchTerm + '%')
            AND IsDeleted = 0
    )

    SELECT
        *
    FROM
        OrderedStudents
    WHERE
        RowNum BETWEEN @StartRow AND @EndRow;

    SET @TotalCount = (
        SELECT COUNT(*)
        FROM dbo.Students WITH (NOLOCK)
        WHERE
            (@SearchTerm IS NULL OR 
                Name LIKE '%' + @SearchTerm + '%' OR
                Email LIKE '%' + @SearchTerm + '%')
            AND IsDeleted = 0
    );
END;
GO
