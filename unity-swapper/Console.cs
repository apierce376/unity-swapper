using System.Diagnostics;
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