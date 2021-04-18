using System;
using System.Collections.Generic;
using System.Linq;

namespace ShannonFanCode
{
    public abstract class Coding
    {
        private readonly double _alphabetCount;
        public Dictionary<char, int> Words { get; private set; }
        
        public readonly Dictionary<char, string> Code;

        protected Coding(string text)
        {
            _alphabetCount = text.ToList().Count;
            Words = new Dictionary<char, int>(text.Distinct().Count()); //init new dictionary for count of alphabet members in text

            foreach (char character in text.Distinct()) //loop thru alphabet and count each character and than add it to counts
            {
                int i = text.Count(dataChar => dataChar == character);
                Words.Add(character, i);
            }
            SortAlphabet();
            
            Code = Words.Keys.ToDictionary(key => key, key => "");
        }
        public double GetAverageWord()
        {
            double average = 0;
            foreach ((char key, int value) in Words)
            {
                average += value * Code[key].Length;
            }
            return average / _alphabetCount;
        }

        public double GetEfficiency()
        {
            return GetEntropy() / GetAverageWord();
        }

        public double GetEntropy()
        {
            double entropy = 0;
            Dictionary<char, double> probability = GetProbability();
            foreach ((_, double value) in probability)
            {
                entropy += value * Math.Log2(value);
            }
            return entropy * -1;
        }

        public Dictionary<char, double> GetProbability()
        {
            Dictionary<char, double> probability = new Dictionary<char, double>();
            foreach ((char key, int value) in Words) 
                probability.Add(key, value / _alphabetCount);
            return probability;
        }
        
        private void SortAlphabet()
        {
            Words = new Dictionary<char, int>(Words.OrderByDescending(item => item.Value));
        }
    }
}