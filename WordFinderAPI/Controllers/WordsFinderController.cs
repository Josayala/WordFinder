using MediatR;
using Microsoft.AspNetCore.Mvc;
using WordFinder.Application.CommandService.Commands;

namespace WordFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsFinderController : ControllerBase
    {

        private readonly ILogger<WordsFinderController> _logger;
        private readonly IMediator mediator;
        public WordsFinderController(IMediator mediator,ILogger<WordsFinderController> logger)
        {           
            _logger = logger;
            this.mediator = mediator;
        }



        /// <summary>
        /// Finds words in a matrix based on a given wordstream.
        /// </summary>
        /// <param name="wordFinderCommand">The command to find words in a matrix.</param>
        /// <returns>A collection of words found in the matrix.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<IEnumerable<string>> FindWords([FromBody] WordFinderCommand wordFinderCommand)
        {
            _logger.LogInformation("Finding words in matrix");           
             var result = mediator.Send(wordFinderCommand);

            if (!string.IsNullOrEmpty(result.Result.Errors))
            {
                return BadRequest(result.Result.Errors);
            }
             return Ok(result);                      
        }
    }

}
