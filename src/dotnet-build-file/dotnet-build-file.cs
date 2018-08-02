using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using McMaster.Extensions.CommandLineUtils;

namespace DotnetBuildFile
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var assemblyDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            var app = new CommandLineApplication()
            {
                Name = "dotnet-build-file",
                ExtendedHelpText = ""
            };

            app.HelpOption();
            var fileName = app.Argument("file", "The file to build.").IsRequired();
            var debug = app.Option("-d|--debug", "Show debug info.", CommandOptionType.NoValue);

            app.OnExecute(() =>
            {
                var fullPath = Path.GetFullPath(fileName.Value);

                var debugFlag = debug.HasValue() ? "/p:Debug=true" : "";

                RunProcess("dotnet", "msbuild", Path.Combine(assemblyDirectory, "build-file.csproj"), "/nologo", debugFlag, $"/p:File={fullPath}", "/t:Restore");
                RunProcess("dotnet", "msbuild", Path.Combine(assemblyDirectory, "build-file.csproj"), "/nologo", debugFlag, $"/p:File={fullPath}");
            });

            return app.Execute(args);
        }

        private static int RunProcess(string fileName, params string[] args)
        {
            var startInfo = new ProcessStartInfo()
            {
                FileName = fileName,
                Arguments = "\"" + string.Join("\" \"", args.Where(x => !string.IsNullOrEmpty(x))) + "\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using (var process = Process.Start(startInfo))
            {
                process.WaitForExit();
                
                Console.WriteLine(process.StandardOutput.ReadToEnd());
                Console.Error.WriteLine(process.StandardError.ReadToEnd());

                return process.ExitCode;
            }
        }
    }
}