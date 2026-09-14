using System.Diagnostics;

class CliApp
{
    static void Main(string[] args)
    {
        var level0 = FindLevel0Path("D:\\Steam\\steamapps\\common\\Shadowrun Returns");
        var version = GetUnityVersion(level0);
        
        Console.WriteLine(version);
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

    private static string SendToCmd(string input)
    {
        Process cmd = new Process();
        cmd.StartInfo.FileName = "cmd.exe";
        cmd.StartInfo.RedirectStandardInput = true;
        cmd.StartInfo.RedirectStandardOutput = true;
        cmd.StartInfo.CreateNoWindow = true;
        cmd.StartInfo.UseShellExecute = false;
        cmd.Start();

        cmd.StandardInput.WriteLine(input);
        cmd.StandardInput.Flush();
        cmd.StandardInput.Close();
        cmd.WaitForExit();
        return(cmd.StandardOutput.ReadToEnd());
    }
}