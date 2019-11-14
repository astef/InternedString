using System;
using System.Runtime.CompilerServices;

namespace Stefurishin.Primitives.Text
{
    /// <summary>
    /// Represents a string which is guaranteed to be interned.<br/>
    /// Provides an optimized <see cref="GetHashCode()"/> and <see cref="Equals(object)"/> overrides, <br/>
    /// which rely on reference comparison rather than string contents.
    /// </summary>
    public sealed class InternedString : IEquatable<InternedString>
    {
        public InternedString(string s) => String = string.Intern(s);

        /// <summary>
        /// An interned string object
        /// </summary>
        public string String { get; }

        public override bool Equals(object obj) => Equals(obj as InternedString);

        public bool Equals(InternedString other) => ReferenceEquals(String, other?.String);

        public override int GetHashCode() => RuntimeHelpers.GetHashCode(String);

        public override string ToString() => String;
    }
}
