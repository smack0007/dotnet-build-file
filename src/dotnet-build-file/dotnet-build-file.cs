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
            var output = app.Option("-o|--output", "Specify an output path.", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                var fullPath = Path.GetFullPath(fileName.Value);

                var debugFlag = debug.HasValue() ? "/p:Debug=true" : "";
                
                var outputPathFlag = "";
                if (output.HasValue())
                {
                    var outputPath = output.Value();

                    if (!Path.IsPathRooted(outputPath))
                        outputPath = Path.Combine(Directory.GetCurrentDirectory(), outputPath);

                    outputPathFlag = $"/p:OutputPath={outputPath}";
                }

                RunProcess(
                    "dotnet",
                    new string[]
                    {
                        "msbuild",
                        Path.Combine(assemblyDirectory, "build-file.csproj"),
                        "/nologo",
                        "/restore",
                        $"/p:File={fullPath}",
                        debugFlag,
                        outputPathFlag
                    },
                    debug.HasValue());
            });

            return app.Execute(args);
        }

        private static int RunProcess(string fileName, string[] args, bool debug)
        {
            var startInfo = new ProcessStartInfo()
            {
                FileName = fileName,
                Arguments = "\"" + string.Join("\" \"", args.Where(x => !string.IsNullOrEmpty(x))) + "\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            if (debug)
                Console.WriteLine($"{startInfo.FileName} {startInfo.Arguments}");

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