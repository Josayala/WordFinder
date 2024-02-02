namespace WordFinder.Application.Dtos
{
    public class WordFinderDto
    {
        public IEnumerable<string> WordsFounded { get; set; } = Array.Empty<string>();
        public string Errors { get; set; } 
    }
}
