# InternedString

[![Build Status](https://stefurishin.visualstudio.com/InternedString/_apis/build/status/InternedString-CI?branchName=master)](https://dev.azure.com/stefurishin/InternedString/_build/latest?definitionId=8) [![Nuget Package](https://img.shields.io/nuget/v/InternedString.svg)](https://www.nuget.org/packages/InternedString/)

Represents a string which is guaranteed to be interned.

Provides an optimized `GetHashCode()` and `Equals(...)` overrides, which rely on reference comparison rather than string contents.

## Usage:

```csharp
// O(n) operation happens only here, so we want to re-use this object
var iString = new InternedString("typically_a_very_long_string_key");

// now any call to `GetHashCode()` and `Equals(...)` will run in a constant time
mySet.Add(iString);
```

## Benchmark

The purpose of this benchmark is to find out, how a simple `HashSet<string>.Contains(...)` method performace can be affected by a `string` length.

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  DefaultJob : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
```
|           Method | StringLength |         Mean |        Error |       StdDev |
|----------------- |------------- |-------------:|-------------:|-------------:|
| **UninternedString** |            **4** |     **19.38 ns** |     **0.115 ns** |     **0.102 ns** |
|   InternedString |            4 |     15.20 ns |     0.068 ns |     0.063 ns |
| **UninternedString** |            **8** |     **21.60 ns** |     **0.049 ns** |     **0.044 ns** |
|   InternedString |            8 |     15.22 ns |     0.083 ns |     0.078 ns |
| **UninternedString** |           **16** |     **26.69 ns** |     **0.143 ns** |     **0.134 ns** |
|   InternedString |           16 |     15.18 ns |     0.045 ns |     0.042 ns |
| **UninternedString** |           **32** |     **35.76 ns** |     **0.121 ns** |     **0.113 ns** |
|   InternedString |           32 |     15.25 ns |     0.106 ns |     0.099 ns |
| **UninternedString** |           **64** |     **57.77 ns** |     **0.121 ns** |     **0.107 ns** |
|   InternedString |           64 |     15.30 ns |     0.108 ns |     0.101 ns |
| **UninternedString** |          **128** |    **104.26 ns** |     **0.634 ns** |     **0.495 ns** |
|   InternedString |          128 |     15.22 ns |     0.070 ns |     0.065 ns |
| **UninternedString** |          **256** |    **195.68 ns** |     **0.466 ns** |     **0.364 ns** |
|   InternedString |          256 |     16.44 ns |     0.353 ns |     0.599 ns |
| **UninternedString** |          **512** |    **380.49 ns** |     **0.718 ns** |     **0.671 ns** |
|   InternedString |          512 |     15.23 ns |     0.077 ns |     0.072 ns |
| **UninternedString** |         **1024** |    **744.70 ns** |     **1.089 ns** |     **0.965 ns** |
|   InternedString |         1024 |     15.23 ns |     0.089 ns |     0.084 ns |
| **UninternedString** |         **2048** |  **1,480.07 ns** |     **8.714 ns** |     **7.725 ns** |
|   InternedString |         2048 |     15.21 ns |     0.058 ns |     0.054 ns |
| **UninternedString** |         **4096** |  **2,935.76 ns** |     **6.445 ns** |     **5.714 ns** |
|   InternedString |         4096 |     15.18 ns |     0.039 ns |     0.033 ns |
| **UninternedString** |         **8192** |  **5,862.37 ns** |     **9.224 ns** |     **8.177 ns** |
|   InternedString |         8192 |     15.22 ns |     0.092 ns |     0.086 ns |
| **UninternedString** |        **16384** | **11,731.94 ns** |    **38.340 ns** |    **35.864 ns** |
|   InternedString |        16384 |     15.19 ns |     0.052 ns |     0.048 ns |
| **UninternedString** |        **32768** | **23,415.15 ns** |    **49.521 ns** |    **46.322 ns** |
|   InternedString |        32768 |     15.19 ns |     0.068 ns |     0.063 ns |
| **UninternedString** |        **65536** | **48,998.99 ns** |   **881.609 ns** | **1,264.377 ns** |
|   InternedString |        65536 |     15.87 ns |     0.182 ns |     0.170 ns |
| **UninternedString** |       **131072** | **98,211.13 ns** | **1,952.642 ns** | **3,419.895 ns** |
|   InternedString |       131072 |     16.05 ns |     0.159 ns |     0.149 ns |
