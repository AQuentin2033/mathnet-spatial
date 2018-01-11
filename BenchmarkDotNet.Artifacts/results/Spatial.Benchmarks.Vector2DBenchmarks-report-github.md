``` ini

BenchmarkDotNet=v0.10.11, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.192)
Processor=Intel Core i7-5820K CPU 3.30GHz (Broadwell), ProcessorCount=12
Frequency=3220786 Hz, Resolution=310.4832 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  Job-XDVICS : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT

Jit=RyuJit  Platform=X64  Runtime=Core  

```
|                           Method |         Mean |     Error |    StdDev |  Gen 0 | Allocated |
|--------------------------------- |-------------:|----------:|----------:|-------:|----------:|
|                           Length |     2.316 ns | 0.0113 ns | 0.0100 ns |      - |       0 B |
|                 OperatorEquality |    10.340 ns | 0.0564 ns | 0.0527 ns |      - |       0 B |
|                 OperatorAddition |     1.289 ns | 0.0244 ns | 0.0228 ns |      - |       0 B |
|              OperatorSubtraction |     1.631 ns | 0.0424 ns | 0.0396 ns |      - |       0 B |
|            OperatorUnaryNegation |     1.445 ns | 0.0164 ns | 0.0154 ns |      - |       0 B |
|                 OperatorMultiply |     2.376 ns | 0.0409 ns | 0.0382 ns |      - |       0 B |
|                 OperatorDivision |     2.338 ns | 0.0177 ns | 0.0166 ns |      - |       0 B |
|                        FromPolar |    28.244 ns | 0.1824 ns | 0.1617 ns | 0.0076 |      48 B |
|                            Parse | 1,327.433 ns | 9.5203 ns | 8.9053 ns | 0.1163 |     736 B |
|      IsParallelToDoubleTolerance |    10.349 ns | 0.0642 ns | 0.0601 ns |      - |       0 B |
|       IsParallelToAngleTolerance |    12.437 ns | 0.1212 ns | 0.1134 ns |      - |       0 B |
| IsPerpendicularToDoubleTolerance |    10.504 ns | 0.1512 ns | 0.1414 ns |      - |       0 B |
|  IsPerpendicularToAngleTolerance |    12.808 ns | 0.0183 ns | 0.0162 ns |      - |       0 B |
|                    SignedAngleTo |    49.723 ns | 0.1319 ns | 0.1169 ns |      - |       0 B |
|                          AngleTo |    10.478 ns | 0.0117 ns | 0.0098 ns |      - |       0 B |
|                           Rotate |    21.900 ns | 0.0692 ns | 0.0647 ns |      - |       0 B |
|                     RotateDouble |    21.052 ns | 0.0345 ns | 0.0322 ns |      - |       0 B |
|                       DotProduct |     1.814 ns | 0.0062 ns | 0.0052 ns |      - |       0 B |
|                     CrossProduct |     1.805 ns | 0.0056 ns | 0.0052 ns |      - |       0 B |
|                        ProjectOn |     7.704 ns | 0.0496 ns | 0.0464 ns |      - |       0 B |
|                        Normalize |    13.759 ns | 0.0417 ns | 0.0369 ns |      - |       0 B |
|                          ScaleBy |     1.524 ns | 0.0210 ns | 0.0186 ns |      - |       0 B |
|                           Negate |     1.338 ns | 0.0718 ns | 0.0671 ns |      - |       0 B |
|                         Subtract |     1.932 ns | 0.0668 ns | 0.0625 ns |      - |       0 B |
|                              Add |     1.742 ns | 0.0656 ns | 0.0613 ns |      - |       0 B |
|                           Equals |    10.473 ns | 0.0241 ns | 0.0201 ns |      - |       0 B |
|              EqualsWithTolerance |    10.135 ns | 0.0254 ns | 0.0238 ns |      - |       0 B |
