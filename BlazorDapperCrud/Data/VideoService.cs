using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;


namespace BlazorDapperCrud.Data
{
    public class VideoService : IVideoService
    {
        //Database connection
        private readonly SqlConnectionConfiguration _configuration;
        public VideoService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Add (create) a video table raw (SQL insert)
        public async Task<bool> VideoInsert(Video video)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Title", video.Title, DbType.String);
                parameters.Add("DatePublished", video.DatePublished, DbType.Date);
                parameters.Add("IsActive", video.IsActive, DbType.Boolean);

                const string query = @"INSERT INTO Video(Title,DatePublished,IsActive) VALUES (@Title,@DatePublished,@IsActive)";
                await conn.ExecuteAsync(query, new { video.Title, video.DatePublished, video.IsActive }, commandType: CommandType.Text);
                
            }
            return true;
        }
    }
}
