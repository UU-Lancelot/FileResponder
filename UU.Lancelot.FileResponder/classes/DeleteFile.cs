namespace UU.Lancelot.FileResponder
{
    public static class Delete
    {
        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void Delete_EventHandler(object? sender, string filePath)
        {
            DeleteFile(filePath);
        }
    }
}
