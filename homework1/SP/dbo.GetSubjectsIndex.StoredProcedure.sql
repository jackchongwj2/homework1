USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[GetSubjectsIndex]    Script Date: 12/28/2023 1:54:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSubjectsIndex]
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

    WITH OrderedSubjects AS
    (
        SELECT 
            ROW_NUMBER() OVER (ORDER BY SubjectId) AS RowNum, 
            s.*,
            t.Name AS TeacherName  -- Assuming a relationship between Subjects and Teachers tables
        FROM 
            dbo.Subjects s WITH (NOLOCK)
        INNER JOIN
            dbo.Teachers t WITH (NOLOCK) ON s.TeacherId = t.TeacherId 
        WHERE
            (@SearchTerm IS NULL OR 
                s.Name LIKE '%' + @SearchTerm + '%' OR
                s.Code LIKE '%' + @SearchTerm + '%' OR
                t.Name LIKE '%' + @SearchTerm + '%')
            AND s.IsDeleted = 0
    )

    SELECT
        *
    FROM
        OrderedSubjects
    WHERE
        RowNum BETWEEN @StartRow AND @EndRow;

    SET @TotalCount = (
        SELECT COUNT(*)
        FROM dbo.Subjects s WITH (NOLOCK)
        INNER JOIN dbo.Teachers t WITH (NOLOCK) ON s.TeacherId = t.TeacherId
        WHERE
            (@SearchTerm IS NULL OR 
                s.Name LIKE '%' + @SearchTerm + '%' OR
                s.Code LIKE '%' + @SearchTerm + '%' OR
                t.Name LIKE '%' + @SearchTerm + '%')
            AND s.IsDeleted = 0
    );
END;
GO
