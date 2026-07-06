namespace Students.Services.StudentGroups.Repositories.Queries;

internal static class StudentGroupsSql
{
    internal static String Save =>
        """
            INSERT INTO student_groups (
                id,
                name,
                abbreviation,
                startyear,
                endyear,
                studyformat,
                createddatetimeutc,
                isremoved
            )
            VALUES (
                @id,
                @name,
                @abbreviation,
                @startYear,
                @endYear,
                @studyFormat,
                @createdDateTimeUtc,
                @isRemoved
            )
        	ON CONFLICT (id) DO UPDATE SET
        	    name = @name,
        	    abbreviation = @abbreviation,
        	    startyear = @startYear,
        	    endyear = @endYear,
        	    studyformat = @studyFormat,
        	    modifieddatetimeutc = @modifiedDateTimeUtc
        """;

    internal static String GetById =>
        """
            SELECT * 
            FROM student_groups 
            WHERE id = @id;
        """;

    internal static String GetPage =>
        """
            SELECT 
                COUNT(*) OVER() as count, 
                *
            FROM student_groups 
            WHERE NOT isremoved 
            ORDER BY createddatetimeutc DESC 
            OFFSET @offset 
            LIMIT @limit
        """;

    internal static String Remove =>
        """
        	UPDATE student_groups
        	SET 
                isremoved = TRUE,
        		modifieddatetimeutc = @modifiedDateTimeUtc
        	WHERE id = @id
        """;
}