using WebApplication8.Models.ExFile;

namespace WebApplication8.Models.NewFolder
{
    public class FolderModel
    {
        public string? Name { get; set; }
        public List<FileModel>? Subfolders { get; set; }
        public List<FolderModel>? Files { get; set; }
    }

    public class FileModel
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
    }
}
