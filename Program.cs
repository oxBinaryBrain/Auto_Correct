using System;
using System.Collections.Generic;

public class Autocorrect
{
    private static readonly Dictionary<string, string> dictionary = new Dictionary<string, string>();

    static Autocorrect()
    {
        // Populate dictionary with some sample correct words
        dictionary["hello"] = "hello";
        dictionary["world"] = "world";
        dictionary["csharp"] = "csharp";
        dictionary["programming"] = "programming";
        dictionary["language"] = "language";
        // Add more words as needed
    }

    public static string AutocorrectWord(string input)
    {
        if (dictionary.ContainsKey(input.ToLower()))
        {
            // If input is found in dictionary, return it as is
            return input;
        }
        else
        {
            // Otherwise, find the closest match in the dictionary
            string closestMatch = "";
            int minDistance = int.MaxValue;

            // Iterate over dictionary to find closest match
            foreach (string word in dictionary.Keys)
            {
                int distance = CalculateLevenshteinDistance(input.ToLower(), word);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestMatch = word;
                }
            }
            return closestMatch;
        }
    }

    // Function to calculate Levenshtein distance between two strings
    private static int CalculateLevenshteinDistance(string s1, string s2)
    {
        int[,] dp = new int[s1.Length + 1, s2.Length + 1];

        for (int i = 0; i <= s1.Length; i++)
        {
            dp[i, 0] = i;
        }

        for (int j = 0; j <= s2.Length; j++)
        {
            dp[0, j] = j;
        }

        for (int i = 1; i <= s1.Length; i++)
        {
            for (int j = 1; j <= s2.Length; j++)
            {
                int cost = (s1[i - 1] == s2[j - 1]) ? 0 : 1;
                dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1), dp[i - 1, j - 1] + cost);
            }
        }

        return dp[s1.Length, s2.Length];
    }

    public static void Main(string[] args)
    {
        string input = "helllo"; // Misspelled word
        string corrected = AutocorrectWord(input);
        Console.WriteLine("Input: " + input);
        Console.WriteLine("Corrected: " + corrected);
    }
}
P
