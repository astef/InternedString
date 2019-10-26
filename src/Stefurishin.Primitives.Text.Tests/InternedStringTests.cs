using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Stefurishin.Primitives.Text.Tests
{
    [TestClass]
    public sealed class InternedStringTests
    {
        [TestMethod]
        public void Test()
        {
            var c = Concat("ab", "c");
            var d = Concat("a", "bc");

            string.IsInterned(c).ShouldBeNull();
            string.IsInterned(d).ShouldBeNull();

            var internedC = new InternedString(c);
            var internedD = new InternedString(d);

            ReferenceEquals(c, d).ShouldBeFalse();
            ReferenceEquals(internedC.String, internedD.String).ShouldBeTrue();
            internedC.Equals(internedD).ShouldBeTrue();
            (internedC == internedD).ShouldBeTrue();
            (internedC != internedD).ShouldBeFalse();
            internedC.GetHashCode().ShouldBe(internedD.GetHashCode());
        }

        private static string Concat(string first, string second) => first + second;
    }
}
