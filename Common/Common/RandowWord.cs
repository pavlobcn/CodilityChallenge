using System;
using System.Linq;

namespace Common
{
    public class RandomWord
    {
        private readonly Random _random;
        private readonly string _characters;

        public RandomWord(int seed, string characters)
        {
            _random = new Random(seed);
            _characters = characters;
        }

        public string Next(int length)
        {
            return new string(Enumerable.Range(0, length).Select(_ => _characters[_random.Next(_characters.Length)]).ToArray());
        }
    }
}
