﻿namespace BlazorDapperCrud.Data
{
    public interface IVideoService
    {
        Task<bool> VideoInsert(Video video);

        Task<IEnumerable<Video>> VideoList();

        Task<Video> Video_GetOne(int id);

        Task<bool> VideoUpdate(Video video);
    }
}