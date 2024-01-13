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
    
    [ParamsSource((nameof(SubstringsData)))]
    public (string, string) BothSubstrings { get; set; }
    public static (string, string)[] SubstringsData => new[] { ("WIDOCZNOSC NA DRODZE DOBRA", "NA DRODZE") };
    
    [Benchmark]
    public int LevensteinWords() => Functions.Levenstein(BothWords.Item1, BothWords.Item2);
    
    [Benchmark]
    public int LevensteinSentences() => Functions.Levenstein(BothSentences.Item1, BothSentences.Item2);
    
    [Benchmark]
    public int LCSWords() => Functions.LCS(BothWords.Item1, BothWords.Item2);
    
    [Benchmark]
    public int LCSSentences() => Functions.LCS(BothSentences.Item1, BothSentences.Item2);
    
    [Benchmark]
    public int BoyerMooreSubstrings() => Substrings.Functions.BoyerMoore(BothSubstrings.Item1, BothSubstrings.Item2, false);
    
    [Benchmark]
    public int KunthMorrisPrattSubstrings() => Substrings.Functions.KunthMorrisPratt(BothSubstrings.Item1, BothSubstrings.Item2, false);
}

public static class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<DistancesBenchmark>();
    }
}