using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter sequences of characters to search for, separated by commas:");
        string input = Console.ReadLine();
        var sequences = new List<string>(input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

        Console.WriteLine("Enter the path to your documents: ");
        string directoryPath = Console.ReadLine();
        var documents = ReadDocuments(directoryPath);
        var results = SearchSequences(documents, sequences);

        // Display results
        foreach (var result in results)
        {
            Console.WriteLine($"Document: {result.Key}");
            foreach (var kvp in result.Value)
            {
                Console.WriteLine($"Sequence '{kvp.Key}' Occurrences: {kvp.Value}");
            }
            Console.WriteLine();
        }
        Console.ReadKey();
    }

    static Dictionary<string, string> ReadDocuments(string directoryPath)
    {
        var documents = new Dictionary<string, string>();
        foreach (var filePath in Directory.GetFiles(directoryPath))
        {
            string content = File.ReadAllText(filePath);
            documents[Path.GetFileName(filePath)] = content;
        }
        return documents;
    }

    static Dictionary<string, Dictionary<string, int>> SearchSequences(Dictionary<string, string> documents, List<string> sequences)
    {
        var results = new Dictionary<string, Dictionary<string, int>>();
        foreach (var document in documents)
        {
            var sequenceCounts = new Dictionary<string, int>();
            foreach (var sequence in sequences)
            {
                int count = CountOccurrences(document.Value, sequence.Trim());
                sequenceCounts[sequence.Trim()] = count;
            }
            results[document.Key] = sequenceCounts;
        }
        return results;
    }

    static int CountOccurrences(string content, string sequence)
    {
        int count = 0;
        int index = 0;
        while ((index = content.IndexOf(sequence, index, StringComparison.OrdinalIgnoreCase)) != -1)
        {
            count++;
            index += sequence.Length;
        }
        return count;
    }
}
