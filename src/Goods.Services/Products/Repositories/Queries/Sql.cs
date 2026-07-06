namespace Goods.Services.Products.Repositories.Queries;

internal static class Sql
{
    internal static String Products_Save =>
        """
            INSERT INTO products (
                id,
                category,
                name,
        	    description,
                price,
                createddatetimeutc,
                isremoved
            )
            VALUES (
                @id,
                @category,
                @name,
                @description,
                @price,
                @createdDateTimeUtc,
                @isRemoved
            )
        	ON CONFLICT (id) DO UPDATE SET
        	    category = @category,
        	    name = @name,
        	    description = @description,
        	    price = @price,
        	    modifieddatetimeutc = @modifiedDateTimeUtc
        """;

    internal static String Products_GetById =>
        """
            SELECT * 
            FROM products 
            WHERE id = @id;
        """;

    internal static String Products_GetByName =>
        """
            SELECT * 
            FROM products 
            WHERE name = @name
            AND NOT isremoved;
        """;

    internal static String Products_GetPage =>
        """
            SELECT 
                COUNT(*) OVER() as count, 
                *
            FROM products 
            WHERE NOT isremoved 
            ORDER BY createddatetimeutc DESC 
            OFFSET @offset 
            LIMIT @limit
        """;

    internal static String Products_Remove =>
        """
        	UPDATE products
        	SET 
                isremoved = TRUE,
        		modifieddatetimeutc = @modifiedDateTimeUtc
        	WHERE id = @id
        """;
}