using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingMethods
{
    /// <summary>
    /// Base class for coding. Every coding method should inherit from this abstract class
    /// </summary>
    public abstract class Coding
    {
        public readonly double WordsCount;
        public Dictionary<char, int> Words { get; }

        public readonly Dictionary<char, string> Code;

        /// <summary>
        /// Constructor of this class. Prepare fields and properties for child classes
        /// </summary>
        /// <param name="text" type="string">Input text parameter.</param>
        protected Coding(string text)
        {
            WordsCount = text.ToList().Count;
            Words = new Dictionary<char, int>(text.Distinct()
                .Count()); //init new dictionary for count of alphabet members in text

            foreach (char character in
                text.Distinct()) //loop thru alphabet and count each character and than add it to counts
            {
                int i = text.Count(dataChar => dataChar == character);
                Words.Add(character, i);
            }

            //sort words by descending
            Words = new Dictionary<char, int>(Words.OrderByDescending(item => item.Value));

            Code = Words.Keys.ToDictionary(key => key, key => "");
        }

        /// <summary>
        /// This method will calculate the AverageWord lenght by looping thru Words that are included inside the input text and .
        /// Calculation is provided by multiplying the current word count in text with his coded version and than addition of all
        /// those calc are divided by count of words in text
        /// </summary>
        /// <returns>Average word lenght</returns>
        public double GetAverageWordLenght()
        {
            double average = 0;
            foreach ((char key, int value) in Words)
            {
                average += value * Code[key].Length;
            }

            return average / WordsCount;
        }

        /// <summary>
        /// This method will get the efficiency of coded data by dividing entropy and averageWord
        /// </summary>
        /// <seealso cref="GetEntropy"/>
        /// <seealso cref="GetAverageWordLenght"/>
        /// <returns>Division of entropy and average of words</returns>
        public double GetEfficiency()
        {
            return GetEntropy() / GetAverageWordLenght();
        }

        /// <summary>
        /// This method will calculate the Entropy of the coded words by adding looped and multiplied
        /// data by their logarithm/ln thru values returned by GetProbabilityAll()
        /// </summary>
        /// <seealso cref="GetProbabilityAll"/>
        /// <returns>Entropy of coded data as double</returns>
        public double GetEntropy()
        {
            double entropy = 0;
            Dictionary<char, double> probability = GetProbabilityAll();
            foreach ((_, double value) in probability)
            {
                entropy += value * Math.Log2(value);
            }

            return entropy * -1;
        }

        /// <summary>
        /// This method will calculate the probability for each word by dividing their value by count of them all
        /// </summary>
        /// <returns>Probability of words as dictionary</returns>
        public Dictionary<char, double> GetProbabilityAll()
        {
            Dictionary<char, double> probability = new Dictionary<char, double>();
            foreach ((char key, int value) in Words)
                probability.Add(key, value / WordsCount);
            return probability;
        }
    }
}