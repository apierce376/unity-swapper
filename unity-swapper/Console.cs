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
        //Users/apierce/Desktop/Shadowrun Returns/Shadowrun.app
        var appPath = getInput("Please enter the path to the 32 bit game .app you want to swap:");
        
        //Get Unity player path
        //Users/apierce/Desktop/pkg/Unity.pkg/Unity/Unity.app
        var unityPath = getInput("Please enter the path to the extracted Unity.app from your Unity player:");
        
        //Search for UnityPlayer.app within Unity.app
        unityPath = FileUtilities.SearchForSubdirectory(unityPath, "UnityPlayer.app");

        //Merge Frameworks directories
        var appFrameworks = FileUtilities.SearchForSubdirectory(appPath, "Frameworks");
        var unityFrameworks = FileUtilities.SearchForSubdirectory(unityPath, "Frameworks");
        
        FileUtilities.MergeDirectories(unityFrameworks, appFrameworks);
        
        //Merge MacOS directories and rename executable
        var appMacOS = FileUtilities.SearchForSubdirectory(appPath, "MacOS");
        var unityMacOS = FileUtilities.SearchForSubdirectory(unityPath, "MacOS");
        
        FileUtilities.MergeDirectories(unityMacOS, appMacOS);

        var originalBinary = Directory.GetFiles(appMacOS).Where(f => Path.GetFileName(f) != "UnityPlayer").First();
        File.Copy(appMacOS + Path.DirectorySeparatorChar + "UnityPlayer", originalBinary, true);

        //Done
    }

    private static string getInput(string prompt)
    {
        var input = string.Empty;

        while (input == string.Empty)
        {
            Console.WriteLine(prompt);
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