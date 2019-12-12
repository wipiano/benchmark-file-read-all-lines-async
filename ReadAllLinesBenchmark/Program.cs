using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using Microsoft.VisualBasic;

namespace ReadAllLinesBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "make")
            {
                // make test data
                var sb = new StringBuilder();
                for (var i = 1; i < 10000; i++)
                {
                    sb.AppendLine(string.Join(null, Enumerable.Range(0, i)));
                }

                File.WriteAllText("testfile.txt", sb.ToString());
                return;
            }

            var config = new ManualConfig();
            config.Add(Job.ShortRun);
            config.Add(MarkdownExporter.GitHub);
            config.Add(MemoryDiagnoser.Default);
            config.Add(ConsoleLogger.Default);
            config.Add(DefaultColumnProviders.Instance);
            config.Add(ThreadingDiagnoser.Default);

            BenchmarkRunner.Run<Test>(config);
        }
    }

    public class Test
    {
        [Benchmark(Baseline = true)]
        public string[] ReadAllLines()
        {
            return File.ReadAllLines("testfile.txt");
        }

        [Benchmark]
        public async Task<string[]> ReadAllLinesAsync()
        {
            return await File.ReadAllLinesAsync("testfile.txt").ConfigureAwait(false);
        }

        [Benchmark]
        public async Task<string[]> ReadAllLinesAsync2()
        {
            // TODO: this is a too simple implementation
            // we need to write more complete impl.
            return (await File.ReadAllTextAsync("testfile.txt").ConfigureAwait(false))
                .Split('\n', '\r', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}