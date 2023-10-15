using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Bogus;
using Distances;

public class DistancesBenchmark
{
    [ParamsSource((nameof(Words)))] 
    public (string, string) BothWords { get; set; }
    public (string, string)[] Words { get; set; }
    
    [ParamsSource((nameof(Sentences)))]
    public (string, string) BothSentences { get; set; }
    public (string, string)[] Sentences { get; set; }
    
    private const int N = 20;
    
    public DistancesBenchmark()
    {
        Words = new (string, string)[N];
        Sentences = new (string, string)[N];

        var faker = new Faker();

        Sentences = Enumerable.Range(0, N).Select(x => (faker.Lorem.Sentence(), faker.Lorem.Sentence())).ToArray();
        Words = Enumerable.Range(0, N).Select(x => (faker.Random.Word(), faker.Random.Word())).ToArray();
    }

    [Benchmark]
    public int LevensteinWords() => Functions.Levenstein(BothWords.Item1, BothWords.Item2);
    
    [Benchmark]
    public int LevensteinSentences() => Functions.Levenstein(BothSentences.Item1, BothSentences.Item2);
    
    [Benchmark]
    public int LCSWords() => Functions.LCS(BothWords.Item1, BothWords.Item2);
    
    [Benchmark]
    public int LCSSentences() => Functions.LCS(BothSentences.Item1, BothSentences.Item2);
}

public static class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<DistancesBenchmark>();
    }
}