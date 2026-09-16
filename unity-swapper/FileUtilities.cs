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

    public static void MergeDirectories(string from, string to, bool shouldOverwrite = true)
    {
        if (from.Split("/").Last() != to.Split("/").Last())
        {
            Console.WriteLine("Can't merge directories");
        }

        foreach (string file in Directory.GetFiles(from).Where(f => Path.GetFileName(f)[0] != '.'))
        {
            Console.WriteLine("Copying {0} to {1}", file, to + Path.DirectorySeparatorChar + Path.GetFileName(file));
            File.Copy(file, to + Path.DirectorySeparatorChar + Path.GetFileName(file), shouldOverwrite);
        }

        foreach (string dir in Directory.GetDirectories(from))
        {
            if (!Directory.Exists(to + Path.DirectorySeparatorChar + Path.GetFileName(dir)))
            {
                Console.WriteLine("Creating new directory {0}", to + Path.DirectorySeparatorChar + Path.GetFileName(dir));
                Directory.CreateDirectory(to + Path.DirectorySeparatorChar + Path.GetFileName(dir));
            }

            MergeDirectories(dir, to + Path.DirectorySeparatorChar + Path.GetFileName(dir));
        }
    }
}