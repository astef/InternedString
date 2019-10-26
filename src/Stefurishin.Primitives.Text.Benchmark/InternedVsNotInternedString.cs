using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stefurishin.Primitives.Text.Benchmark
{
    [CsvExporter]
    [MarkdownExporterAttribute.GitHub]
    public class InternedVsNotInternedString
    {
        static void Main() => BenchmarkRunner.Run<InternedVsNotInternedString>();

        private string _string;
        private InternedString _internedString;

        private HashSet<string> _stringSet = new HashSet<string>();
        private HashSet<InternedString> _internedStringSet = new HashSet<InternedString>();

        [Params(
            2 << 1,
            2 << 2,
            2 << 3,
            2 << 4,
            2 << 5,
            2 << 6,
            2 << 7,
            2 << 8,
            2 << 9,
            2 << 10,
            2 << 11,
            2 << 12,
            2 << 13,
            2 << 14,
            2 << 15,
            2 << 16)]
        public int StringLength;

        [GlobalSetup]
        public void Setup()
        {
            // we don't care about the size of collections
            // but it should contain SOME items, to be sure it's not optimizing the lookup
            foreach (var rs in GenerateRandomStrings(42))
            {
                _stringSet.Add(rs);
                _internedStringSet.Add(new InternedString(rs));
            }

            _string = new string(Enumerable.Repeat('x', StringLength).ToArray());
            _internedString = new InternedString(_string);

            _stringSet.Add(_string);
            _internedStringSet.Add(_internedString);
        }

        [Benchmark]
        public bool String() => _stringSet.Contains(_string);

        [Benchmark]
        public bool InternedString() => _internedStringSet.Contains(_internedString);


        private static IEnumerable<string> GenerateRandomStrings(int count)
        {
            for (int i = 0; i < count; i++)
                yield return Guid.NewGuid().ToString();
        }
    }
}
