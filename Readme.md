[![The MIT License](https://img.shields.io/badge/license-MIT-orange.svg?style=flat-square)](http://opensource.org/licenses/MIT)

# dotnet-build-file

Build a single C# file from the .NET CLI.

## Nuget Packages

| Name    | Version | Framework | Description
| --------| ---------------------------------------- | ---------- |-------
| `dotnet-build-file` | [![Nuget](https://img.shields.io/nuget/v/dotnet-build-file.svg?maxAge=10800)](https://www.nuget.org/packages/dotnet-build-file/) | `netcoreapp2.1` | .NET Core global tool

## Installing

```shell
dotnet tool install -g dotnet-build-file
```

## Usage

```
Usage: dotnet-build-file [arguments] [options]

Arguments:
  file          The file to build.

Options:
  -?|-h|--help  Show help information
  -d|--debug    Show debug info.
```

Given a simple `HelloWorld.cs`:

```c#
using System;

namespace HelloWorld
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }
        }
    }   
}
```

You can build the file with:

```shell
dotnet build-file HelloWorld.cs
```

This will produce an `obj` and `bin` in the same directory as the `HelloWorld.cs`
file. The assembly name is assumed to be the same as the `cs` file. In the `bin` 
directory is a `cmd` launcher as well.

```shell
bin\HelloWorld a b c
```

This produces the output:

```
Hello World!
a
b
c
```

If there is a `HelloWorld.csproj` in the same directory, this will be included. 
Use this if you need to make `PackageReference`(s) or include a common `cs` file.
`Directory.Build.props` and `Directory.Build.targets` files are also searched for
and included.

## Use Cases

* __Scratch Project__: I tend to have a Scratch.csproj lying around so that I always
have a project I can open an immediately try out something. This tool should
replace that need.
* __Multiple Small Programs__: I have a folder containing lots of small utility
programs that I compile on all my machines. I use this tool to compile them.

## Planned Features

* __Comiple and Run__: The ability to execute the program directly after compiling.
