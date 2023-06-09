namespace OneToMany.Helpers
{
    public static class FileExtentions
    {
        public static bool CheckFileType(this IFormFile file, string pattern)
        {
            return file.ContentType.Contains(pattern);
        }  
        
        public static bool CheckFileSize(this IFormFile file, long size)
        {
            return file.Length / 1024 > size;
        }

        public static async Task<string> SaveFileAsync(this IFormFile file, string filename ,string basePath, string folder)
        {

            string path = Path.Combine(basePath, folder, filename);

            using (FileStream stream = new(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filename;
        }
    }
}
