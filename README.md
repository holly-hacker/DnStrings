# `DnStrings`

DnStrings is a tool similar to the common `strings`/`strings.exe` tool which lists strings in a file. DnStrings is
similar, but specializes in .NET binaries.

It currently supports the following methods:
- `UserStringsHeap`: Read all strings from the `#US` heap, which contains user-defined strings (eg. for `Console.WriteLine("Hello world")` it would contain `Hello World`). This is the default.
- `StringsHeap`: Read all strings from the `#Strings` heap, which contains symbol names and such.

Keep in mind that DnStrings is a command-line tool and is intended to work together with other command-line tools (such as `grep`, `sort`, `uniq`). It will not implement features that can be trivially accomplished using an existing tool.

## Usage

Given the following program:
```cs
Console.Write("Enter the secret password: ");

var enteredPassword = Console.ReadLine();

if (enteredPassword == "SuperSecret!")
    Console.WriteLine("Success!");
else
    Console.WriteLine("Incorrect password.");
```

```
$ dnstrings bin/Release/net9.0/HelloWorld.dll
Enter the secret password: 
SuperSecret!
Success!
Incorrect password.
```
```
$ dnstrings -m StringsHeap bin/Release/net9.0/HelloWorld.dll
<Main>$
<Module>
HelloWorld
System.Console
System.Runtime
WriteLine
CompilerGeneratedAttribute
DebuggableAttribute
AssemblyTitleAttribute
TargetFrameworkAttribute
AssemblyFileVersionAttribute
AssemblyInformationalVersionAttribute
AssemblyConfigurationAttribute
RefSafetyRulesAttribute
CompilationRelaxationsAttribute
AssemblyProductAttribute
AssemblyCompanyAttribute
RuntimeCompatibilityAttribute
System.Runtime.Versioning
HelloWorld.dll
Program
System
System.Reflection
.ctor
System.Diagnostics
System.Runtime.CompilerServices
DebuggingModes
args
Object
```

### Flake

If you are using the nix package manager and have flakes enabled, you can run DnStrings with `nix run github:holly-hacker/dnstrings -- <file.dll>`
