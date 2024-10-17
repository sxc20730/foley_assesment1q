using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class WordConcordance
{
    static void Main()
    {
        string text = "This is a sample sentence. This is another sentence. Sample sentence again!";
        var concordance = GenerateConcordance(text);

        Console.WriteLine("Word Concordance:");
        foreach (var entry in concordance.OrderBy(e => e.Key))
        {
            Console.WriteLine($"Word: {entry.Key}, Frequency: {entry.Value.Item1}, Sentence Numbers: {string.Join(", ", entry.Value.Item2)}");
        }
    }

    static Dictionary<string, (int, List<int>)> GenerateConcordance(string text)
    {
        var concordance = new Dictionary<string, (int, List<int>)>();

        string[] sentences = Regex.Split(text, @"(?<=[.!?])\s+");

        for (int sentenceIndex = 0; sentenceIndex < sentences.Length; sentenceIndex++)
        {
            string[] words = Regex.Split(sentences[sentenceIndex].ToLower(), @"\W+");
            foreach (var word in words.Where(w => !string.IsNullOrWhiteSpace(w)))
            {
                if (concordance.ContainsKey(word))
                {
                    var value = concordance[word];
                    value.Item1++;
                    if (!value.Item2.Contains(sentenceIndex + 1))
                    {
                        value.Item2.Add(sentenceIndex + 1);
                    }
                    concordance[word] = value;
                }
                else
                {
                    concordance[word] = (1, new List<int> { sentenceIndex + 1 });
                }
            }
        }
        return concordance;
    }
}
