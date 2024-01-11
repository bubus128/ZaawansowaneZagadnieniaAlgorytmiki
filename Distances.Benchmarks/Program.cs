using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Bogus;
using Distances;

public class DistancesBenchmark
{
    [ParamsSource((nameof(Words)))] 
    public (string, string) BothWords { get; set; }
    public static (string, string)[] Words => new[] { ("OUTLIER", "OUTLAW") };
    
    [ParamsSource((nameof(Sentences)))]
    public (string, string) BothSentences { get; set; }
    public static (string, string)[] Sentences => new[] { ("WIDOCZNOSC NA DRODZE DOBRA", "WIDOCZNOSC DOBRA") };
    
    [Benchmark]
    public int LevensteinWords() => Functions.Levenstein(BothWords.Item1, BothWords.Item2);
    
    [Benchmark]
    public int LevensteinSentences() => Functions.Levenstein(BothSentences.Item1, BothSentences.Item2);
    
    [Benchmark]
    public int LCSWords() => Functions.LCS(BothWords.Item1, BothWords.Item2);
    
    [Benchmark]
    public int LCSSentences() => Functions.LCS(BothSentences.Item1, BothSentences.Item2);
    
    [Benchmark]
    public int BoyerMooreWords() => Substrings.Functions.BoyerMoore(BothWords.Item1, BothWords.Item2, false);
    
    [Benchmark]
    public int BoyerMooreSentences() => Substrings.Functions.BoyerMoore(BothSentences.Item1, BothSentences.Item2, false);
    
    [Benchmark]
    public int KunthMorrisPrattWords() => Substrings.Functions.KunthMorrisPratt(BothWords.Item1, BothWords.Item2, false);
    
    [Benchmark]
    public int KunthMorrisPrattSentences() => Substrings.Functions.KunthMorrisPratt(BothSentences.Item1, BothSentences.Item2, false);
}

public static class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<DistancesBenchmark>();
    }
}