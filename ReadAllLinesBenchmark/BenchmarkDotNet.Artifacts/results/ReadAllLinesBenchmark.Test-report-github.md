``` ini

BenchmarkDotNet=v0.12.0, OS=ubuntu 18.04
Intel Core i5-8265U CPU 1.60GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]   : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  ShortRun : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|             Method |     Mean |     Error |  StdDev | Ratio | Completed Work Items | Lock Contentions |       Gen 0 |      Gen 1 |     Gen 2 |  Allocated |
|------------------- |---------:|----------:|--------:|------:|---------------------:|-----------------:|------------:|-----------:|----------:|-----------:|
|       ReadAllLines | 663.6 ms |   0.65 ms | 0.04 ms |  1.00 |               3.0000 |                - | 139000.0000 | 40000.0000 | 7000.0000 |  793.28 MB |
|  ReadAllLinesAsync | 923.6 ms |  26.72 ms | 1.46 ms |  1.39 |           46325.0000 |                - | 144000.0000 | 41000.0000 | 7000.0000 |  806.65 MB |
| ReadAllLinesAsync2 | 937.7 ms | 156.56 ms | 8.58 ms |  1.41 |           46253.0000 |                - |  73000.0000 | 27000.0000 | 4000.0000 | 1098.44 MB |
