using WordFinder.Application.Interfaces;

namespace WordFinder.Application.Services
{
    public class WordFinderService : IWordFinderService
    {     
        public WordFinderService()
        {            
        }

        public IEnumerable<string> SearchWords(IEnumerable<string> matrix, IEnumerable<string> wordstream)
        {
            var wordFinder = new WordFinder(matrix);
            return wordFinder.Find(wordstream);
        }
    }
}
