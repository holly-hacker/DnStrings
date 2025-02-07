using System.Diagnostics.CodeAnalysis;
using AsmResolver.DotNet;
using ConsoleAppFramework;
using DnStrings;
using JetBrains.Annotations;

var app = ConsoleApp.Create();
app.Add<Commands>();
app.Run(args);

internal class Commands
{
	/// <summary> Analyze a .NET binary to look for strings. </summary>
	/// <param name="file">The file to read</param>
	/// <param name="method">
	/// -m, Which method to find strings by. Multiple methods can be separated by commas.
	/// Possible values: All, UserStringsHeap, StringsHeap.
	/// </param>
	[Command("")]
	[UsedImplicitly]
	[SuppressMessage("Performance", "CA1822:Mark members as static")]
	public void DoStuff(
		[Argument] string file,
		SearchFlags method = SearchFlags.UserStringsHeap)
	{
		if (!File.Exists(file))
		{
			Console.Error.WriteLine("File does not exist");
			return;
		}

		AssemblyDefinition assembly;
		try
		{
			assembly = AssemblyDefinition.FromFile(file);
		}
		catch (Exception e)
		{
			Console.Error.WriteLine($"Cannot read the given input file as a .NET binary: {e.Message}");
			return;
		}

		var stringFinder = new StringFinder(assembly);
		var strings = new StringCollection();

		if (method.HasFlag(SearchFlags.UserStringsHeap))
			strings.Add(stringFinder.GetUserStrings());

		if (method.HasFlag(SearchFlags.StringsHeap))
			strings.Add(stringFinder.GetStrings());

		foreach (var entry in strings.Entries)
			Console.WriteLine(entry.Value);
	}
}
