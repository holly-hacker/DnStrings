using System.CommandLine;
using AsmResolver.DotNet;
using DnStrings;

var fileArgument = new Argument<FileInfo>("file", "The file to read");
var flagsOption = new Option<SearchFlags>("--method", () => SearchFlags.UserStringsHeap,
	"Which method to find strings by. Multiple methods can be separated by commas.");
flagsOption.AddAlias("-m");

var rootCommand = new RootCommand("DnStrings - strings.exe for .NET");
rootCommand.AddArgument(fileArgument);
rootCommand.AddOption(flagsOption);

rootCommand.SetHandler((file, flag) =>
{
	if (!file.Exists)
	{
		Console.Error.WriteLine("File does not exist");
		return;
	}

	AssemblyDefinition assembly;
	try
	{
		assembly = AssemblyDefinition.FromFile(file.FullName);
	}
	catch (Exception e)
	{
		Console.Error.WriteLine($"Cannot read the given input file as a .NET binary: {e.Message}");
		return;
	}

	var stringFinder = new StringFinder(assembly);
	var strings = new StringCollection();

	if (flag.HasFlag(SearchFlags.UserStringsHeap))
		strings.Add(stringFinder.GetUserStrings());

	if (flag.HasFlag(SearchFlags.StringsHeap))
		strings.Add(stringFinder.GetStrings());

	foreach (var entry in strings.Entries)
		Console.WriteLine(entry.Value);
}, fileArgument, flagsOption);

return rootCommand.Invoke(args);
