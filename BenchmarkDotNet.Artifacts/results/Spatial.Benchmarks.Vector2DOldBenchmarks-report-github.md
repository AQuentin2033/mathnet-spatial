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
|                           Length |     2.316 ns | 0.0197 ns | 0.0174 ns |      - |       0 B |
|                 OperatorEquality |     2.560 ns | 0.0110 ns | 0.0103 ns |      - |       0 B |
|                 OperatorAddition |     2.947 ns | 0.0197 ns | 0.0184 ns |      - |       0 B |
|              OperatorSubtraction |     2.900 ns | 0.0237 ns | 0.0221 ns |      - |       0 B |
|            OperatorUnaryNegation |     3.286 ns | 0.0215 ns | 0.0201 ns |      - |       0 B |
|                 OperatorMultiply |     1.837 ns | 0.0213 ns | 0.0199 ns |      - |       0 B |
|                 OperatorDivision |     2.871 ns | 0.0209 ns | 0.0196 ns |      - |       0 B |
|                        FromPolar |    22.805 ns | 0.0676 ns | 0.0633 ns |      - |       0 B |
|                            Parse | 1,327.699 ns | 9.3659 ns | 8.7608 ns | 0.1087 |     688 B |
|      IsParallelToDoubleTolerance |    24.734 ns | 0.0384 ns | 0.0340 ns |      - |       0 B |
|       IsParallelToAngleTolerance |    11.449 ns | 0.0194 ns | 0.0182 ns |      - |       0 B |
| IsPerpendicularToDoubleTolerance |    24.168 ns | 0.1152 ns | 0.1021 ns |      - |       0 B |
|  IsPerpendicularToAngleTolerance |    10.761 ns | 0.0253 ns | 0.0211 ns |      - |       0 B |
|                    SignedAngleTo |    51.513 ns | 0.5353 ns | 0.5007 ns |      - |       0 B |
|                          AngleTo |     9.637 ns | 0.0999 ns | 0.0934 ns |      - |       0 B |
|                           Rotate |    25.777 ns | 0.0929 ns | 0.0725 ns |      - |       0 B |
|                       DotProduct |     1.737 ns | 0.0098 ns | 0.0087 ns |      - |       0 B |
|                     CrossProduct |     1.993 ns | 0.0281 ns | 0.0263 ns |      - |       0 B |
|                        ProjectOn |     3.599 ns | 0.0108 ns | 0.0095 ns |      - |       0 B |
|                        Normalize |    10.312 ns | 0.1096 ns | 0.1025 ns |      - |       0 B |
|                          ScaleBy |     2.019 ns | 0.0185 ns | 0.0173 ns |      - |       0 B |
|                           Negate |     3.271 ns | 0.0261 ns | 0.0244 ns |      - |       0 B |
|                         Subtract |     2.628 ns | 0.0074 ns | 0.0058 ns |      - |       0 B |
|                              Add |     2.570 ns | 0.0243 ns | 0.0227 ns |      - |       0 B |
|                           Equals |     2.842 ns | 0.0122 ns | 0.0102 ns |      - |       0 B |
|              EqualsWithTolerance |     2.858 ns | 0.0165 ns | 0.0138 ns |      - |       0 B |
