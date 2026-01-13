using Geo_radio.Models;
using Npgsql;

namespace Geo_radio.Services;

public class LeaderboardService {
    private readonly NpgsqlConnection connection;

    public LeaderboardService(NpgsqlConnection connection) {
        this.connection = connection;
    }

    public async Task<List<LeaderboardEntry>> GetTop10() {
        List<LeaderboardEntry> top10 = new List<LeaderboardEntry>();

        await connection.OpenAsync();

        String sqlQuery = @"
        SELECT ""Name"", ""Score""
        FROM ""Leaderboard""
        ORDER BY ""Score"" DESC
        LIMIT 10;
        ";

        NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, connection);
        NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync()) {
            top10.Add(new LeaderboardEntry {Name = reader.GetString(0), Score = reader.GetInt32(1)});
        }

        await connection.CloseAsync();
        
        return top10;
    }
}