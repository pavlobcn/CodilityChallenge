using System.Linq;
using System.Text;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrandChallenge
{
    [TestClass]
    public class PerformanceTests : SolutionTestsBase
    {
        private const int MaxLength = 100000;

        private static readonly string StringConsistOfTwoSingleCharStrings;
        private static readonly string StringConsistOfPairs;
        private static readonly string RandomStringOf2Characters;
        private static readonly string RandomStringOf3Characters;

        static PerformanceTests()
        {
            StringConsistOfTwoSingleCharStrings = GetStringConsistOfTwoSingleCharStrings();
            StringConsistOfPairs = GetStringConsistOfPairs();
            RandomStringOf2Characters = GetRandomStringOf2Characters();
            RandomStringOf3Characters = GetRandomStringOf3Characters();
        }

        private static string GetStringConsistOfTwoSingleCharStrings()
        {
            return new string('a', MaxLength / 2) + new string('b', MaxLength / 2);
        }

        private static string GetStringConsistOfPairs()
        {
            var sb = new StringBuilder();
            Enumerable.Range(0, MaxLength / 2).ForEach(i => sb.Append("ab"));
            return sb.ToString();
        }

        private static string GetRandomStringOf2Characters()
        {
            return new RandomWord(5, "ab").Next(MaxLength);
        }

        private static string GetRandomStringOf3Characters()
        {
            return new RandomWord(5, "abc").Next(MaxLength);
        }

        [TestMethod]
        public void StringConsistOfTwoSingleCharStringsTest()
        {
            Test(
                StringConsistOfTwoSingleCharStrings,
                MaxLength);
        }

        [TestMethod]
        public void StringConsistOfPairsTest()
        {
            Test(
                StringConsistOfPairs,
                MaxLength);
        }

        [TestMethod]
        public void RandomStringOf2CharactersTest()
        {
            Test(
                RandomStringOf2Characters,
                85780);
        }

        [TestMethod]
        public void RandomStringOf3CharactersTest()
        {
            Test(
                RandomStringOf3Characters,
                24);
        }
    }
}