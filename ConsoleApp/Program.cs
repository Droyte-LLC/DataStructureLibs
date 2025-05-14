
Trie trie = new Trie();

/* Ideally, the Trie should be populated with a large number of words and their frequencies, 
derived from a trained AI model or real-world usage data. */

trie.Insert("network", 8);
trie.Insert("networking", 6);
trie.Insert("neural", 5);
trie.Insert("neuralnet", 4);
trie.Insert("node", 7);
trie.Insert("nodejs", 6);
trie.Insert("python", 10);
trie.Insert("pytest", 5);
trie.Insert("query", 6);
trie.Insert("queue", 4);

string prefix = "py"; 
Console.WriteLine($"Searching for auto-complete suggestions for '{prefix}':");
var suggestions = trie.AutoComplete(prefix, 1);

foreach (var suggestion in suggestions)
{
    Console.WriteLine($"Word: {suggestion.word}, Frequency: {suggestion.wordFrequency}");
}

string serializedTrie = trie.JsonSerialize();
Console.WriteLine($"Serialized Trie: {serializedTrie}");
Console.WriteLine("Press any key to exit...");
Console.ReadLine();
