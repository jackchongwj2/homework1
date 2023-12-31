USE [SchoolDB]
GO
/****** Object:  StoredProcedure [dbo].[GetResultsIndex]    Script Date: 12/28/2023 1:54:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetResultsIndex]
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

    WITH OrderedResults AS
    (
        SELECT 
            ROW_NUMBER() OVER (ORDER BY ResultId) AS RowNum, 
            r.ResultId,
            r.Marks,
            r.StudentId,
            r.SubjectId,
            s.Name AS SubjectName,  -- Assuming a relationship between Results and Subjects tables
            st.Name AS StudentName, -- Assuming a relationship between Results and Students tables
            r.IsDeleted
        FROM 
            dbo.Results r WITH (NOLOCK)
        INNER JOIN
            dbo.Subjects s WITH (NOLOCK) ON r.SubjectId = s.SubjectId
        INNER JOIN
            dbo.Students st WITH (NOLOCK) ON r.StudentId = st.StudentId
        WHERE
            (@SearchTerm IS NULL OR 
                s.Name LIKE '%' + @SearchTerm + '%' OR
                st.Name LIKE '%' + @SearchTerm + '%')
            AND r.IsDeleted = 0
    )

    SELECT
        ResultId,
        Marks,
        StudentId,
        SubjectId,
        SubjectName,
        StudentName,
        IsDeleted
    FROM
        OrderedResults
    WHERE
        RowNum BETWEEN @StartRow AND @EndRow;

    SET @TotalCount = (
        SELECT COUNT(*)
        FROM dbo.Results r WITH (NOLOCK)
        INNER JOIN dbo.Subjects s ON r.SubjectId = s.SubjectId
        INNER JOIN dbo.Students st ON r.StudentId = st.StudentId
        WHERE
            (@SearchTerm IS NULL OR 
                s.Name LIKE '%' + @SearchTerm + '%' OR
                st.Name LIKE '%' + @SearchTerm + '%')
            AND r.IsDeleted = 0
    );
END;
GO
