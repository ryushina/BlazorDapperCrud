namespace BlazorDapperCrud.Data
{
    public interface IVideoService
    {
        Task<bool> VideoInsert(Video video);
    }
}