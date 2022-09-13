namespace BlazorDapperCrud.Data
{
    public class Video
    {
        public int VideoID { get; set; }
        public string Title { get; set; }

        public DateTime DatePublished { get; set; }

        public bool IsActive { get; set; }
    }
}
