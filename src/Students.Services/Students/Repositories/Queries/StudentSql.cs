namespace Students.Services.Students.Repositories.Queries;

internal static class StudentSql
{
    internal static String Students_Save =>
        """
            INSERT INTO students (
                id,
                fullname,
                gender,
                age,
                averagegrade,
                specialmarks,
                studentgroupid,
                createddatetimeutc,
                isremoved
            )
            VALUES (
                @id,
                @fullname,
                @gender,
                @age,
                @averagegrade,
                @specialmarks,
                @studentgroupid,
                @createddatetimeutc,
                @isRemoved
            )
        	ON CONFLICT (id) DO UPDATE SET
        	    fullname = @fullName,
               	gender = @gender,
               	age = @age,
               	averagegrade = @averageGrade,
               	specialmarks = @specialMarks,
               	studentgroupid = @studentGroupId,
               	modifieddatetimeutc = @modifiedDateTimeUtc
        """;

    internal static String Students_GetById =>
        """
            SELECT * 
            FROM students 
            WHERE id = @id;
        """;

    internal static String Students_GetPage =>
        """
            SELECT 
                COUNT(*) OVER() as count, 
                *
            FROM students 
            WHERE isremoved = false 
            ORDER BY createddatetimeutc DESC 
            OFFSET @offset 
            LIMIT @limit
        """;
    internal static String Students_GetByGroupId =>
        """
            SELECT 
                COUNT(*) OVER() as count, 
                *
            FROM students 
            WHERE studentgroupid = @studentGroupId
            AND NOT isremoved 
            ORDER BY createddatetimeutc DESC 
            OFFSET @offset 
            LIMIT @limit
        """;
    internal static String Students_Remove =>
        """
        	UPDATE students
        	SET 
                isremoved = true,
        		modifieddatetimeutc = @modifiedDateTimeUtc
        	WHERE id = @id AND  isremoved = false
        """;
    internal static String Students_GetAll =>
    """
        SELECT id, fullname, gender, age, averagegrade, specialmarks, studentgroupid,
               createddatetimeutc, modifieddatetimeutc, isremoved,
               COUNT(*) OVER() as count
        FROM students
        WHERE isremoved = false
        LIMIT @count OFFSET (@page - 1) * @count
    """;
}