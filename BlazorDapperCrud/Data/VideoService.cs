﻿using Dapper;
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

                await conn.ExecuteAsync("spVideo_Insert", parameters, commandType: CommandType.StoredProcedure);

                //const string query = @"INSERT INTO Video(Title,DatePublished,IsActive) VALUES (@Title,@DatePublished,@IsActive)";
                //await conn.ExecuteAsync(query, new { video.Title, video.DatePublished, video.IsActive }, commandType: CommandType.Text);
                
            }
            return true;
        }

        public async Task<IEnumerable<Video>> VideoList()
        {
            IEnumerable<Video> videos;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                videos = await conn.QueryAsync<Video>("spVideo_GetAll", commandType: CommandType.StoredProcedure);

            }
            return videos;
        }

        public async Task<Video> Video_GetOne(int id)
        {
            Video video = new Video();
            var parameters = new DynamicParameters();
            parameters.Add("Id",id, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                video = await conn.QueryFirstOrDefaultAsync<Video>("spVideo_GetOne",parameters, commandType: CommandType.StoredProcedure);

            }
            return video;
        }

        public async Task<bool> VideoUpdate(Video video)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Title", video.Title, DbType.String);
                parameters.Add("DatePublished", video.DatePublished, DbType.Date);
                parameters.Add("IsActive", video.IsActive, DbType.Boolean);
                parameters.Add("VideoId", video.VideoID, DbType.Int32);

                await conn.ExecuteAsync("spVideo_Update", parameters, commandType: CommandType.StoredProcedure);

                //const string query = @"INSERT INTO Video(Title,DatePublished,IsActive) VALUES (@Title,@DatePublished,@IsActive)";
                //await conn.ExecuteAsync(query, new { video.Title, video.DatePublished, video.IsActive }, commandType: CommandType.Text);

            }
            return true;
        }

        public async Task<bool> VideoDelete(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                
                parameters.Add("Id", id, DbType.Int32);

                await conn.ExecuteAsync("spVideo_Delete", parameters, commandType: CommandType.StoredProcedure);

                //const string query = @"INSERT INTO Video(Title,DatePublished,IsActive) VALUES (@Title,@DatePublished,@IsActive)";
                //await conn.ExecuteAsync(query, new { video.Title, video.DatePublished, video.IsActive }, commandType: CommandType.Text);

            }
            return true;
        }

    }
}
