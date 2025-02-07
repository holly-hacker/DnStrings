using AsmResolver.DotNet;
using AsmResolver.PE.DotNet.Metadata;

namespace DnStrings;

public class StringFinder(AssemblyDefinition assembly)
{
	public IEnumerable<StringHeapString> GetStrings()
	{
		var module = assembly.ManifestModule
		             ?? throw new Exception("Manifest module not found");
		var dotNetDir = module.DotNetDirectory
		                ?? throw new Exception(".net directory not found");
		var metadata = dotNetDir.Metadata
		               ?? throw new Exception(".NET metadata directory not found");

		var stringsStream = metadata.GetStream<StringsStream>();
		foreach (var (_, utf8String) in stringsStream.EnumerateStrings())
			yield return new StringHeapString(utf8String);
	}

	public IEnumerable<UserStringHeapString> GetUserStrings()
	{
		var module = assembly.ManifestModule
		             ?? throw new Exception("Manifest module not found");
		var dotNetDir = module.DotNetDirectory
		                ?? throw new Exception(".net directory not found");
		var metadata = dotNetDir.Metadata
		               ?? throw new Exception(".NET metadata directory not found");

		var userStrings = metadata.GetStream<UserStringsStream>();
		foreach (var (_, s) in userStrings.EnumerateStrings())
			yield return new UserStringHeapString(s);
	}
}
