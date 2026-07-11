namespace Students.Services.StudentGroups.Repositories.Queries;

internal static class StudentGroupsSql
{
    internal static String Save =>
        """
            INSERT INTO student_groups (
            id, name, abbreviation, startdatetime, enddatetime, studyformat,
            createddatetimeutc, modifieddatetimeutc, isremoved
        )
        VALUES (
            @id, @name, @abbreviation, @startDateTime, @endDateTime, @studyFormat,
            @createdDateTimeUtc, @modifiedDateTimeUtc, @isRemoved
        )
        ON CONFLICT (id) DO UPDATE SET
            name = @name,
            abbreviation = @abbreviation,
            startdatetime = @startDateTime,
            enddatetime = @endDateTime,
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
    internal static String GetAll =>
    """
        SELECT id, name, abbreviation, startdatetime, enddatetime, studyformat,
           createddatetimeutc, modifieddatetimeutc, isremoved,
           COUNT(*) OVER() as count
    FROM student_groups
    WHERE isremoved = false
    LIMIT @count OFFSET (@page - 1) * @count
    """;
}