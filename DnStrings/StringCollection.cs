namespace DnStrings;

public class StringCollection
{
	private readonly List<FoundString> _strings = new();

	public IEnumerable<FoundString> Entries => _strings.AsEnumerable();

	public void Add(FoundString s) => _strings.Add(s);
	public void Add(IEnumerable<FoundString> s) => _strings.AddRange(s);
}
