namespace WebApplication8.Models.ExFile
{
    public class ImageUpload
    {

        public int Id { get; set; }
        public string? FileName { get; set; }
        public byte[]? FileData { get; set; }
    }
}
