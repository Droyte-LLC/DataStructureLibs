namespace DataStructureLibTest;

public class TrieTests
{
	private readonly Trie testTrie;
	public TrieTests()
	{
		testTrie = new Trie();
		testTrie.Insert("network", 8);
		testTrie.Insert("networking", 6);
		testTrie.Insert("neural", 5);
		testTrie.Insert("neuralnet", 4);
		testTrie.Insert("node", 7);
		testTrie.Insert("nodejs", 6);
		testTrie.Insert("python", 10);
		testTrie.Insert("pytest", 5);
		testTrie.Insert("query", 6);
		testTrie.Insert("queue", 4);
	}

	[Fact]
	public void Insert_AddsWordsToTrie()
	{
		var trie = new Trie();
		trie.Insert("yabadabadoo", 4);
		trie.Insert("abracadabra", 2);
		trie.Insert("dingdong", 8);

		Assert.Equal(4, trie.GetWordFrequency("yabadabadoo"));
		Assert.Equal(2, trie.GetWordFrequency("abracadabra"));
		Assert.Equal(8, trie.GetWordFrequency("dingdong"));
	}

	[Fact]
	public void Insert_ShouldThrowException_WhenFrequencyIsLessThanOne()
	{
		var trie = new Trie();
		Assert.Throws<ArgumentOutOfRangeException>(() => trie.Insert("some", 0));
		Assert.Throws<ArgumentOutOfRangeException>(() => trie.Insert("other", -1));
	}

	[Fact]
	public void Insert_ShouldThrowException_WhenWordIsNullOrEmpty()
	{
		var trie = new Trie();
		Assert.Throws<ArgumentNullException>(() => trie.Insert("", 5));
	}

	[Fact]
	public void Insert_ShouldThrowException_WhenWordLengthIsMoreThanFifty()
	{
		var trie = new Trie();
		Assert.Throws<ArgumentOutOfRangeException>(() => trie.Insert("IamSoHappyIamSoHappyIamSoHappyIamSoHappyIamSoHappyI", 5));
	}

	[Fact]
	public void JsonSerialize_ReturnsJsonString()
	{
		string jsonString = testTrie.JsonSerialize();

		Exception ex = Record.Exception(() =>
		{
			JsonDocument.Parse(jsonString);
		});

		Assert.Null(ex);
	}

	[Fact]
	public void AutoComplete_ReturnsSuggestions()
	{
		var suggestions = testTrie.AutoComplete("ne", 5);

		Assert.Contains("network", suggestions.Select(s => s.word));
		Assert.Contains("networking", suggestions.Select(s => s.word));
		Assert.Contains("neural", suggestions.Select(s => s.word));
		Assert.Contains("neuralnet", suggestions.Select(s => s.word));

		Assert.Equal(4, suggestions.Count);
	}

	[Fact]
	public void AutoComplete_ShouldThrowException_WhenPrefixIsNullOrEmpty()
	{
		Assert.Throws<ArgumentNullException>(() => testTrie.AutoComplete("", 2));
	}

	[Fact]
	public void AutoComplete_ShouldThrowException_WhenMaxSuggestionsIsLessThanOne()
	{
		Assert.Throws<ArgumentOutOfRangeException>(() => testTrie.AutoComplete("rapid", 0));
	}

	[Fact]
	public void AutoComplete_ReturnsEmptySuggestionsWhenPrefixDoesNotExist()
	{
		var suggestions = testTrie.AutoComplete("ox", 2);
		Assert.Empty(suggestions);
	}

	[Fact]
	public void WordExists_IsTrueForExistingWords()
	{
		Assert.True(testTrie.WordExists("network"));
		Assert.True(testTrie.WordExists("nodejs"));
		Assert.True(testTrie.WordExists("python"));
		Assert.True(testTrie.WordExists("queue"));
	}

	[Fact]
	public void WordExists_IsFalseForNonExistingWords()
	{
		Assert.False(testTrie.WordExists("bambam"));
		Assert.False(testTrie.WordExists("travel"));
		Assert.False(testTrie.WordExists("hello"));
		Assert.False(testTrie.WordExists("react"));
	}

	[Theory]
	[InlineData("network", 8)]
	[InlineData("neural", 5)]
	[InlineData("pytest", 5)]
	[InlineData("query", 6)]
	public void GetWordFrequency_ReturnsCorrectWordFrequency(string word, int expectedFrequency)
	{
		Assert.Equal(expectedFrequency, testTrie.GetWordFrequency(word));
	}

	[Fact]
	public void GetWordFrequency_ReturnsZeroWhenWordDoesNotExist()
	{
		Assert.Equal(0, testTrie.GetWordFrequency("youWontFindMe"));
		Assert.Equal(0, testTrie.GetWordFrequency("notInTrie"));
		Assert.Equal(0, testTrie.GetWordFrequency("noWaY"));
	}

	[Theory]
	[InlineData("ne", 23)]
	[InlineData("no", 13)]
	[InlineData("py", 15)]
	[InlineData("que", 10)]
	public void GetPrefixFrequency_ReturnsCorrectPrefixFrequency(string prefix, int expectedFrequency)
	{
		Assert.Equal(expectedFrequency, testTrie.GetPrefixFrequency(prefix));
	}

	[Fact]
	public void GetPrefixFrequency_ReturnsZeroWhenPrefixDoesNotExist()
	{
		Assert.Equal(0, testTrie.GetPrefixFrequency("notAPrefix"));
		Assert.Equal(0, testTrie.GetPrefixFrequency("onceUponATime"));
		Assert.Equal(0, testTrie.GetPrefixFrequency("junkJunK"));
	}

	[Fact]
	public void StartsWith_ReturnsTrueForValidPrefixes()
	{
		Assert.True(testTrie.StartsWith("net"));
		Assert.True(testTrie.StartsWith("neur"));
		Assert.True(testTrie.StartsWith("nodej"));
		Assert.True(testTrie.StartsWith("pyth"));
		Assert.True(testTrie.StartsWith("queu"));
	}

	[Fact]
	public void StartsWith_ReturnsFalseForInvalidPrefixes()
	{
		Assert.False(testTrie.StartsWith("not"));
		Assert.False(testTrie.StartsWith("doe"));
		Assert.False(testTrie.StartsWith("cri"));
		Assert.False(testTrie.StartsWith("psyc"));
		Assert.False(testTrie.StartsWith("rem"));
	}
}
