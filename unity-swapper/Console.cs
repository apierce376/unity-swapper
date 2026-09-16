using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Cryptography;
using unity_swapper;

class CliApp
{
    static void Main(string[] args)
    {
        if (!OperatingSystem.IsMacOS())
        {
            Console.WriteLine("Unity swapper is only supported on MacOS");
            Environment.Exit(1);
        }

        MergeDirectories("/Users/apierce/Desktop/test2/test", "/Users/apierce/Desktop/test/test/");
    }

    public static void MergeDirectories(string from, string to)
    {
        if (from.Split("/").Last() != to.Split("/").Last())
        {
            Console.WriteLine("Can't merge directories");
        }

        foreach (string file in Directory.GetFiles(from).Where(f => Path.GetFileName(f)[0] != '.'))
        {
            Console.WriteLine("Copying {0} to {1}", file, to);
            //File.Copy(file, to + Path.GetFileName(file), true);
        }

        foreach (string dir in Directory.GetDirectories(from))
        {
            if (Path.Exists(to + dir))
            {
                
            }
            
            Console.WriteLine(to + Path.DirectorySeparatorChar + Path.GetRelativePath(from, dir));
            
            Console.WriteLine(Path.GetRelativePath(from, dir));
        }
    }

    private static string SendToBash(string input)
    {
        Process cmd = new Process();
        cmd.StartInfo.FileName = "/bin/bash";
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