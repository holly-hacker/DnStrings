using AsmResolver;

namespace DnStrings;

public abstract class FoundString(string value)
{
	public string Value { get; } = value;
}

public class StringHeapString(Utf8String value) : FoundString(value);

public class UserStringHeapString(string value) : FoundString(value);
