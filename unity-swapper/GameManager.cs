namespace unity_swapper;

public class GameManager
{
    public string UnityVersion { get; }

    public GameManager(string path)
    {
        var level0 = FindLevel0Path("/Users/apierce/Desktop/Shadowrun Returns/Shadowrun.app");
        UnityVersion = GetUnityVersion(level0); 
    }

    public string GetEngineDownloadLink()
    {
        return UnityDownloadLinks[UnityVersion];
    }
    
    private static string FindLevel0Path(string path)
    {
        string level0Path = "";
        
        if (!Directory.Exists(path))
        {
            return "";
        }

        foreach (string file in  Directory.GetFiles(path))
        {
            if (file.Contains("level0"))
            {
                return file;
            }
        }

        foreach (string dir in Directory.GetDirectories(path))
        {
            level0Path = FindLevel0Path(dir);
            
            if (level0Path != "") break;
        }

        return level0Path;
    }

    private static string GetUnityVersion(string path)
    {
        var file = File.ReadAllText(path);
        List<char> validCharacters =
            ['a','b','c','d','e','f','g','1','2','3','4','5','6','7','8','9','0','.'];

        int startIndex = 0;
        
        while (!validCharacters.Contains(file[startIndex]))
        {
            startIndex++;
        }

        int endIndex = startIndex;
        
        while (validCharacters.Contains(file[endIndex]))
        {
            endIndex++;
        }

        return file.Substring(startIndex,  endIndex - startIndex);
    }

    private static Dictionary<string, string> UnityDownloadLinks = new Dictionary<string, string>
    {
        { "4.2.2f1", "google.com" }
    };
}