using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WordFinder.API.Controllers;
using WordFinder.Application.CommandService.Commands;
using WordFinder.Application.Dtos;
using WordFinder.Application.Interfaces;

namespace WordFinder.API.UnitTests
{
    [Trait("Category", "Controller")]
    public class WordsFinderControllerTests
    {
        private readonly Mock<IWordFinderService> _wordFinderService;
        private readonly Mock<ILogger<WordsFinderController>> _logger;
        private readonly Mock<IMediator> _mediator;            
        private readonly WordsFinderController _controller;

        public WordsFinderControllerTests()
        {
            _wordFinderService = new Mock<IWordFinderService>();
            _mediator = new Mock<IMediator>();
            _logger = new Mock<ILogger<WordsFinderController>>();
            _controller = new WordsFinderController(_mediator.Object, _logger.Object);
        }

        [Fact]
        public void SearchWords_WithValidData_ReturnsOkObjectResult()
        {
            // Arrange
            var matrix = new List<string> { "abc", "def", "ghi" };
            var wordstream = new List<string> { "abc", "def" };
            var wordFinderDto = new WordFinderDto { WordsFounded = new List<string> { "abc", "def" }, Errors = "" };
            var words = new List<string> { "abc", "def" };

             _mediator.Setup(x => x.Send(It.IsAny<WordFinderCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(wordFinderDto);
            _wordFinderService.Setup(x => x.SearchWords(matrix, wordstream)).Returns(words);

            // Act
            var result = _controller.FindWords(new WordFinderCommand { Matrix = matrix, Wordstream = wordstream });

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void SearchWords_WithInvalidData_ReturnsBadRequestObjectResult()
        {
            // Arrange
            var matrix = new List<string> { "abc", "def", "ghi" };
            var wordstream = new List<string> { "abc", "def" };
            var wordFinderDto = new WordFinderDto { WordsFounded = new List<string> { "abc", "def" }, Errors = "Error" };
            var words = new List<string> { "abc", "def" };

            _mediator.Setup(x => x.Send(It.IsAny<WordFinderCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(wordFinderDto);
            _wordFinderService.Setup(x => x.SearchWords(matrix, wordstream)).Returns(words);

            // Act
            var result = _controller.FindWords(new WordFinderCommand { Matrix = matrix, Wordstream = wordstream });

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    
    }
}
