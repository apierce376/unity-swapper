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
        
        //Get game path
        var appPath = getInput("Please enter the path to the 32 bit game .app you want to swap:");
        
        //Get Unity player path
        
        //Merge Directories
        
        //Done
    }

    private static string getInput(string prompt)
    {
        Console.WriteLine(prompt);
        var input = string.Empty;

        while (input == string.Empty)
        {
            input = Console.ReadLine();
        }

        return input;
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