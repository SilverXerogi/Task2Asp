using Npgsql;

namespace Students.Tools.Utils;

public static class DatabaseUtils
{
	private const String _connectionString = "Server=localhost;Username=postgres;Password=User18;Database=goods";

    public static Task<Int32> ExecuteAsync(String sql, Action<NpgsqlParameterCollection> parameters)
    {
        return UseSqlCommandAsync(
            sql,
            parameters,
            command => command.ExecuteNonQueryAsync()
        );
    }

    public static Task<Page<T>> GetPageAsync<T>(
		String sql,
		Action<NpgsqlParameterCollection> getParameters,
		Func<NpgsqlDataReader, T> mapper
	)
	{
		return UseSqlCommandAsync(
			sql,
			getParameters,
			async command =>
			{
                await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

				List<T> values = [];
				Int32 totalRows = 0;

				while (await reader.ReadAsync())
				{
					totalRows = Convert.ToInt32(reader["count"]);
					values.Add(mapper(reader));
				}

				return new Page<T>([.. values], totalRows);
			}
		);
	}

    public static Task<T?> GetAsync<T>(
        String sql,
        Action<NpgsqlParameterCollection> parameters,
        Func<NpgsqlDataReader, T> mapper
    )
    {
        return UseSqlCommandAsync(
            sql,
            parameters,
            async command =>
            {
                await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (!await reader.ReadAsync())
                    return default;

                return mapper(reader);
            }
        );
    }

    private static async Task<T> UseSqlCommandAsync<T>(
        String sql,
        Action<NpgsqlParameterCollection> parameters,
        Func<NpgsqlCommand, Task<T>> execute
    )
    {
        await using NpgsqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        await using NpgsqlCommand command = new();
        command.Connection = connection;
        command.CommandText = sql;
        parameters(command.Parameters);

        return await execute(command);
    }
}
