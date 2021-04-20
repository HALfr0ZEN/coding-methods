using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingMethods
{
    /// <summary>
    /// This class defines coding by shannonFann algorithm.
    /// </summary>
    public class ShannonFano : Coding
    {
        /// <summary>
        /// Constructor. Main usage is for code the data by Coding method and to pass data into parent class
        /// </summary>
        /// <param name="text">Input text parameter.</param>
        public ShannonFano(string text) : base(text)
        {
            List<Dictionary<char, int>> data = new List<Dictionary<char, int>>() { Words };
            do
            {
                Coding(ref data);
            } while (data.Count < Words.Keys.Count);
        }
        
        /// <summary>
        /// This method will change data provided to it by shannon-fann algorithm.
        /// </summary>
        /// <param name="data">Data that will be modified</param>
        /// <seealso cref="SplitEvenly"/>
        /// <seealso cref="SplitUnEvenly"/>
        /// <seealso cref="Compare"/>
        private void Coding(ref List<Dictionary<char, int>> data)
        {
            List<Dictionary<char, int>> tmp = new List<Dictionary<char, int>>();
            List<Dictionary<char, int>> tmp2; 
            
            foreach (var item in data)
            {
                tmp2 = new List<Dictionary<char, int>>();

                // if there are less than two items add it to tmp and continue
                if (item.Count < 2)
                {
                    tmp.Add(item);
                    continue;
                }
                
                tmp2.Add(Compare(SplitEvenly(item), SplitUnEvenly(item))[0]);
                tmp2.Add(Compare(SplitEvenly(item), SplitUnEvenly(item))[1]);
                foreach (char j in tmp2[0].Keys)
                {
                    Code[j] += "0";
                }

                foreach (char j in tmp2[1].Keys)
                {
                    Code[j] += "1";
                }

                tmp.Add(tmp2[0]);
                tmp.Add(tmp2[1]);
            }
            data = tmp;
        }
        
        /// <summary>
        /// Split provided data evenly
        /// </summary>
        /// <param name="data">dictionary with chars as keys and int as values</param>
        /// <returns>Splited data</returns>
        private List<Dictionary<char, int>> SplitEvenly(Dictionary<char, int> data)
        { 
            List<Dictionary<char, int>> output = new List<Dictionary<char, int>>(2);
            output.Add(new Dictionary<char, int>(data.Take(data.Count / 2)));
            output.Add(new Dictionary<char, int>(data.Skip(data.Count / 2)));
            return output;
        }
        
        /// <summary>
        /// Split provided data unevenly
        /// </summary>
        /// <param name="data">dictionary with chars as keys and int as values</param>
        /// <returns>Splited data</returns>
        private List<Dictionary<char, int>> SplitUnEvenly (Dictionary<char, int> data)
        {
            if (data.Count == 2)
                return SplitEvenly(data);
            
            List<Dictionary<char, int>> output = new List<Dictionary<char, int>>(2);
            output.Add(new Dictionary<char, int>(data.Take((data.Count-1) / 2)));
            output.Add(new Dictionary<char, int>(data.Skip((data.Count-1) / 2)));
            return output;
        }
        
        /// <summary>
        /// Count provided data
        /// </summary>
        /// <param name="data">data as list containing two dictionaries with with key as char and values as int</param>
        /// <returns>count of data in each dictionary</returns>
        private int[] CountSplit(List<Dictionary<char, int>> data)
        {
            int[] count = new int[2];
            count[0] = data[0].Sum(pair => pair.Value);
            count[1] = data[1].Sum(pair => pair.Value);
            return count;
        }
        
        /// <summary>
        /// Compare evenly and unevenly splited data and return one of them.
        /// </summary>
        /// <param name="evenly">evenly splited data as dictionary with char as key and int as values</param>
        /// <param name="unevenly">unevenly splited data as dictionary with char as key and int as values</param>
        /// <seealso cref="CountSplit"/>
        /// <returns>one of the provided parameters</returns>
        private List<Dictionary<char, int>> Compare(List<Dictionary<char, int>> evenly, List<Dictionary<char, int>> unevenly)
        {
            int[] evenlyCount = CountSplit(evenly);
            int[] unevenlyCount = CountSplit(unevenly);
            return evenlyCount[0] - evenlyCount[1] < Math.Abs(unevenlyCount[0] - unevenlyCount[1]) ? evenly : unevenly;
        } 
    }
}