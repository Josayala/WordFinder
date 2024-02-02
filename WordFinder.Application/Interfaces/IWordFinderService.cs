namespace WordFinder.Application.Interfaces
{
    public interface IWordFinderService
    {
        IEnumerable<string> SearchWords(IEnumerable<string> matrix, IEnumerable<string> wordstream);
    }
}
