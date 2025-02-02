namespace DnStrings;

[Flags]
public enum SearchFlags
{
	UserStringsHeap = 1,
	StringsHeap = 2,

	All = 0x7FFF_FFFF,
}
