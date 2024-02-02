using Microsoft.Extensions.Logging;
using WordFinder.Application.CommandService.Commands;
using WordFinder.Application.Dtos;
using WordFinder.Application.Interfaces;

namespace WordFinder.Application.CommandService.CommandHandlers
{
    public class WordFinderCommandHandler : ICommandHandler<WordFinderCommand, WordFinderDto>
    {

        private readonly IWordFinderService _wordFinderService;
        private readonly ILogger<WordFinderCommandHandler> _logger;        

        public WordFinderCommandHandler(IWordFinderService wordFinderService, ILogger<WordFinderCommandHandler> logger)
        {
            _ = logger ?? throw new ArgumentNullException(nameof(logger));
            _ = wordFinderService ?? throw new ArgumentNullException(nameof(wordFinderService));
            _wordFinderService = wordFinderService;
            _logger = logger;
        }

        public async Task<WordFinderDto> Handle(WordFinderCommand request, CancellationToken cancellationToken )
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var validationMessageMatrix = ValidateMatrix(request.Matrix);
            if (!string.IsNullOrEmpty(validationMessageMatrix) )
            {
                _logger.LogError(validationMessageMatrix);
                return new WordFinderDto() { WordsFounded = Array.Empty<string>(), Errors = validationMessageMatrix };
            }

            var validationMessageWordStream = ValidateWordstream(request.Wordstream);
            if (!string.IsNullOrEmpty(validationMessageWordStream))
            {
                _logger.LogError(validationMessageWordStream);
                return new WordFinderDto() { WordsFounded = Array.Empty<string>(), Errors = validationMessageWordStream };
            }

            var result = new WordFinderDto() { WordsFounded = _wordFinderService.SearchWords(request.Matrix, request.Wordstream),Errors="" };

            return result;                           
        }


        #region Validations

        // validate that the size of the matrix is the same in all rows
        // validate that the array is not empty
        // validate that the array is not null
        // validate that the matrix does not have a size greater than 64X64 
        // validate that the array does not have spaces
        // validate that the array has no special characters
        // validate that the matrix does not have numbers

        public string ValidateMatrix(IEnumerable<string> matrix)
        {
            if (matrix == null)
            {
                return "The array is null";
            }
            if (matrix.Count() == 0)
            {
                return "The array is empty";
            }
            if (matrix.Count() > 64)
            {
                return "The matrix has a size greater than 64X64";
            }
            if (matrix.Any(x => x.Length > 64))
            {
                return "The matrix has a size greater than 64X64"; 
            }
            if (matrix.Any(x => x.Length != matrix.First().Length))
            {
                return "The size of the matrix is not the same in all rows";
            }
            if (matrix.Any(x => x.Any(y => char.IsWhiteSpace(y))))
            {
                return "The array has white spaces";
            }
            if (matrix.Any(x => x.Any(y => char.IsDigit(y))))
            {
                return "The array has special characters";
            }
            if (matrix.Any(x => x.Any(y => char.IsPunctuation(y))))
            {
                return "The matrix has numbers";
            }
            return "";
        }

        // validate that the array does not have a size greater than 64
        // validate that the array does not have spaces
        // validate that the array has no special characters
        // validate that the array does not have numbers
        public string ValidateWordstream(IEnumerable<string> wordstream)
        {
            if (wordstream.Any(x => x.Length > 64))
            {
                return "The array has a size greater than 64";
            }
            if (wordstream.Any(x => x.Any(y => char.IsWhiteSpace(y))))
            {
                return "The array has white spaces";
            }
            if (wordstream.Any(x => x.Any(y => char.IsDigit(y))))
            {
                return "The array has special characters";
            }
            if (wordstream.Any(x => x.Any(y => char.IsPunctuation(y))))
            {
                return "The matrix has numbers";
            }
            return "";
        }

    

        #endregion
    }
}
