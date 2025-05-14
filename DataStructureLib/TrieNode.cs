internal class TrieNode
  {
      public Dictionary<char, TrieNode> Children { get; }
      public bool IsEndOfWord { get; set; }
      /// <summary>
      /// This is the frequency of a complete word in the Trie.
      /// </summary>
      public int WordFrequency { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public int PrefixFrequency { get; set; }

      public TrieNode()
      {
          Children = new Dictionary<char, TrieNode>();
          IsEndOfWord = false;
          WordFrequency = 0;
          PrefixFrequency = 0;
      }
  }