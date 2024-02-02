using Microsoft.Extensions.Logging;
using Moq;
using WordFinder.Application.CommandService.CommandHandlers;
using WordFinder.Application.CommandService.Commands;
using WordFinder.Application.Dtos;
using WordFinder.Application.Interfaces;

namespace WordFinder.Application.UnitTests
{
    public class WordFinderCommandHandlerTests
    {
        private readonly Mock<IWordFinderService> _mockWordFinderService;

        private readonly Mock<ILogger<WordFinderCommandHandler>> _logger;
        private readonly WordFinderCommandHandler _handler;
        public WordFinderCommandHandlerTests()
        {
            _mockWordFinderService = new Mock<IWordFinderService>();
            _logger = new Mock<ILogger<WordFinderCommandHandler>>();
            _handler = new WordFinderCommandHandler(_mockWordFinderService.Object, _logger.Object);
        }

        [Fact]
        public async Task SearchWords_WithValidData_ReturnsWordsFounded()
        {
            // Arrange
            var matrix = new List<string> { "abc", "def", "ghi" };
            var wordstream = new List<string> { "abc", "def" };
            var words = new List<string> { "abc", "def" };
            var wordFinderDto = new WordFinderDto { WordsFounded = words, Errors = "" };

            _mockWordFinderService.Setup(x => x.SearchWords(matrix, wordstream)).Returns(words);
            var request = new WordFinderCommand()
            {
                Matrix = matrix,
                Wordstream = wordstream
            };

            // Act
            var result = await _handler.Handle(request, new CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(wordFinderDto.WordsFounded,result.WordsFounded);
            _mockWordFinderService.Verify(x => x.SearchWords(It.IsAny<IEnumerable<string>>(), It.IsAny<IEnumerable<string>>()), Times.Once);
        }

        [Fact]
        public async Task SearchWords_WithInvalidData_ReturnsErrors()
        {
            // Arrange
            var matrix = new List<string> { "abcuj", "def", "ghi" };
            var wordstream = new List<string> { "abc", "def" };
            var wordFinderDto = new WordFinderDto { WordsFounded = Array.Empty<string>(), Errors = "The size of the matrix is not the same in all rows" };
           
            var request = new WordFinderCommand()
            {
                Matrix = matrix,
                Wordstream = wordstream
            };

            // Act
            var result = await _handler.Handle(request, new CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.Empty(wordFinderDto.WordsFounded);
            Assert.NotEmpty(wordFinderDto.Errors);
            Assert.Equal(wordFinderDto.Errors, result.Errors); 
            _mockWordFinderService.Verify(x => x.SearchWords(It.IsAny<IEnumerable<string>>(), It.IsAny<IEnumerable<string>>()), Times.Never);
        }

    }
}
