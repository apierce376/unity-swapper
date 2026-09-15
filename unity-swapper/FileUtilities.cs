namespace unity_swapper;

public static class FileUtilities
{
    public static string SearchForSubdirectory(string path, string directory)
    {
        string unityPlayerPath = "";
        
        if (!Directory.Exists(path))
            return "";

        if (path.Contains(directory))
            return path;

        foreach (string dir in Directory.GetDirectories(path))
        {
            unityPlayerPath = SearchForSubdirectory(dir, directory);

            if (unityPlayerPath != "")
                break;
        }

        return unityPlayerPath;
    }
    
    
}