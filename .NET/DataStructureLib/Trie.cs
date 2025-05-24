using System.Text.Json;

namespace DataStructureLib;
public class Trie
{
  #region Properties

  private readonly TrieNode root;

  #endregion

  #region Constructors

  public Trie()
  {
    root = new TrieNode();
  }

  #endregion

  #region Methods
  public void Insert(string word, int frequency = 1)
  {
    if (frequency < 1)
    {
      throw new ArgumentOutOfRangeException(nameof(frequency), "Invalid frequency param, must be greater than 0.");
    }

    if (string.IsNullOrEmpty(word))
    {
      throw new ArgumentNullException(nameof(word), "Word cannot be null or empty.");
    }
  
    if (word.Length > 50)
    {
      throw new ArgumentOutOfRangeException(nameof(word), "Word length exceeds the maximum limit of 100 characters.");
    }

    TrieNode currentNode = root;
    foreach (char character in word)
    {
      if (!currentNode.Children.ContainsKey(character))
      {
        currentNode.Children[character] = new TrieNode();
      }
      currentNode = currentNode.Children[character];
      currentNode.PrefixFrequency += frequency;
    }
    currentNode.IsEndOfWord = true;
    currentNode.WordFrequency += frequency;
  }

  public List<(string word, int wordFrequency)> AutoComplete(string prefix, int maxSuggestions = 5)
  {
    if (string.IsNullOrEmpty(prefix))
    {
      throw new ArgumentNullException(nameof(prefix), "Prefix cannot be null or empty.");
    }

    if (maxSuggestions < 1)
    {
      throw new ArgumentOutOfRangeException(nameof(maxSuggestions), "Max suggestions must be greater than 0.");
    }

    TrieNode currentNode = root;
    foreach (char character in prefix)
    {
      if (!currentNode.Children.ContainsKey(character))
      {
        return new List<(string word, int frequency)>();
      }
      currentNode = currentNode.Children[character];
    }

    var results = new List<(string word, int wordFrequency)>();
    Traverse(currentNode, prefix, results);

    return results
            .OrderByDescending(x => x.wordFrequency)
            .Take(maxSuggestions)
            .ToList();
  }

  public string JsonSerialize()
  {
    return JsonSerializer.Serialize(root);
  }

  // Example of Preorder Traversal
  private void Traverse(TrieNode node, string word, List<(string word, int wordFrequency)> results)
  {
    if (node.IsEndOfWord)
    {
      results.Add((word, node.WordFrequency));
    }

    foreach (var kvp in node.Children)
    {
      Traverse(kvp.Value, word + kvp.Key, results);
    }
  }
  public bool WordExists(string word)
  {
    var currentNode = root;
    foreach (var character in word)
    {
      if (!currentNode.Children.ContainsKey(character))
      {
        return false;
      }
      currentNode = currentNode.Children[character];
    }
    return currentNode.IsEndOfWord;
  }

  public int GetWordFrequency(string word)
  {
    TrieNode currentNode = root;
    foreach (char character in word)
    {
      if (!currentNode.Children.ContainsKey(character))
      {
        return 0;
      }
      currentNode = currentNode.Children[character];
    }
    return currentNode.IsEndOfWord ? currentNode.WordFrequency : 0;
  }

  public int GetPrefixFrequency(string prefix)
  {
    TrieNode currentNode = root;
    foreach (char character in prefix)
    {
      if (!currentNode.Children.ContainsKey(character))
      {
        return 0;
      }
      currentNode = currentNode.Children[character];
    }
    return currentNode.PrefixFrequency;
  }

  public bool StartsWith(string prefix)
  {
    var currentNode = root;
    foreach (var character in prefix)
    {
      if (!currentNode.Children.ContainsKey(character))
      {
        return false;
      }
      currentNode = currentNode.Children[character];
    }
    return true;
  }

  #endregion
}