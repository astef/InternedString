# InternedString

[![Build Status](https://stefurishin.visualstudio.com/InternedString/_apis/build/status/InternedString-CI?branchName=master)](https://dev.azure.com/stefurishin/InternedString/_build/latest?definitionId=8) [![Nuget Package](https://img.shields.io/nuget/v/InternedString.svg)](https://www.nuget.org/packages/InternedString/)

Represents a string which is guaranteed to be interned.

Provides an optimized `GetHashCode()` and `Equals(...)` overrides, which rely on reference comparison rather than string contents.

## Usage:

```csharp
var myDict = new Dictionary<InternedString, object>();

// due to an implicit cast to string, you don't need to call new InternedString()
myDict.Add("typically_a_very_long_and_dynamically_generated_string_key", new object())
```

## Contribute

  - benchmark how performance gain depends on the string size
  - implement and benchmark`IEqualityComparer<string>` as a low-allocating alternative to `InternedString`