
namespace WordFinder.Application.UnitTests
{
    public class WordFinderTests
    {
        [Fact]
        public void Find_ShouldReturnCorrectWords()
        {
            // Arrange
            var matrix = new List<string> { "abcd", "efgh", "ijkl", "mnop" };
            var wordstream = new List<string> { "abc", "efg", "hij", "klm", "nop", "xyz" };
            var wFinder = new Services.WordFinder(matrix);

            // Act
            var result = wFinder.Find(wordstream);

            // Assert
            Assert.Equal(4, result.Count());
            Assert.Contains("abc", result);
            Assert.Contains("efg", result);
            Assert.Contains("hij", result);
            Assert.Contains("klm", result);
        }

        [Fact]
        public void Find_ShouldReturnEmptyList_WhenNoWordsFound()
        {
            // Arrange
            var matrix = new List<string> { "abcd", "efgh", "ijkl", "mnop" };
            var wordstream = new List<string> { "xyz", "uvw", "rst", "qpo" };
            var wordFinder = new Services.WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordstream);

            // Assert
            Assert.Empty(result);
        }
    }
}
