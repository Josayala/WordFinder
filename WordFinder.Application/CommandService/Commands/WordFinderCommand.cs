
using WordFinder.Application.Dtos;
using WordFinder.Application.Interfaces;

namespace WordFinder.Application.CommandService.Commands
{
    public class WordFinderCommand : ICommand<WordFinderDto>
    {   
        /// <summary>
        /// List of string to be displayed as a matrix
        /// </summary>
  
        public IEnumerable<string> Matrix { get; set; } = Array.Empty<string>();

        /// <summary>
        /// List of string to be searched in the matrix
        /// </summary>
        public IEnumerable<string> Wordstream { get; set; } = Array.Empty<string>();


    }
}
