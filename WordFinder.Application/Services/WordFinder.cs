namespace WordFinder.Application.Services
{
    public class WordFinder
    {
        private readonly char[,] _matrix;

        public WordFinder(IEnumerable<string> matrix)
        {
            var size = matrix.First().Length;
            _matrix = new char[size, size];

            int row = 0;
            foreach (var line in matrix)
            {
                for (int col = 0; col < size; col++)
                {
                    _matrix[row, col] = line[col];
                }
                row++;
            }
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            var foundWords = new List<string>();
            var wordList = wordstream.Distinct().ToList();

            foreach (var word in wordList)
            {
                if (WordInMatrix(word))
                {
                    foundWords.Add(word);
                }
            }

            return foundWords.OrderByDescending(word => word.Length).Take(10);
        }

        #region Private Methods
        private bool WordInMatrix(string word)
        {
            for (var i = 0; i < _matrix.GetLength(0); i++)
            {
                for (var j = 0; j < _matrix.GetLength(1); j++)
                {
                    if (SearchWord(word, i, j))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool SearchWord(string word, int row, int col)
        {
            // Check horizontally
            if (col + word.Length <= _matrix.GetLength(1))
            {
                var horizontal = new string(Enumerable.Range(0, word.Length).Select(i => _matrix[row, col + i]).ToArray());
                if (horizontal == word)
                {
                    return true;
                }
            }
            // Check vertically
            if (row + word.Length <= _matrix.GetLength(0))
            {
                var vertical = new string(Enumerable.Range(0, word.Length).Select(i => _matrix[row + i, col]).ToArray());
                if (vertical == word)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

    }
}
