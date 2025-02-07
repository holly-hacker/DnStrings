using AsmResolver.PE.DotNet;
using AsmResolver.PE.DotNet.Metadata;

namespace DnStrings;

public class StringFinder(DotNetDirectory dotNetDir)
{
	public IEnumerable<StringHeapString> GetStrings()
	{
		var metadata = dotNetDir.Metadata
		               ?? throw new Exception(".NET metadata directory not found");

		var stringsStream = metadata.GetStream<StringsStream>();
		return stringsStream.EnumerateStrings().Select(t => new StringHeapString(t.String));
	}

	public IEnumerable<UserStringHeapString> GetUserStrings()
	{
		var metadata = dotNetDir.Metadata
		               ?? throw new Exception(".NET metadata directory not found");

		var userStrings = metadata.GetStream<UserStringsStream>();
		return userStrings.EnumerateStrings().Select(t => new UserStringHeapString(t.String));
	}
}
