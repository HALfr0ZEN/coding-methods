using System;
using System.Collections.Generic;
using System.Linq;

namespace ShannonFanCode
{
    public class ShannonFano : Coding
    {
        public ShannonFano(string text) : base(text)
        {
            List<Dictionary<char, int>> data = new List<Dictionary<char, int>>() { Words };
            do
            {
                Coding(ref data);
            } while (data.Count < Words.Keys.Count);
        }
        
        private void Coding(ref List<Dictionary<char, int>> data)
        {
            List<Dictionary<char, int>> tmp = new List<Dictionary<char, int>>();
            List<Dictionary<char, int>> tmp2; 
            
            foreach (var item in data)
            {
                tmp2 = new List<Dictionary<char, int>>();

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
        
        /*
        *  Split dictionary evenly and return 2 indexed dictionary array
        */
        private List<Dictionary<char, int>> SplitEvenly(Dictionary<char, int> data)
        { 
            List<Dictionary<char, int>> output = new List<Dictionary<char, int>>(2);
            output.Add(new Dictionary<char, int>(data.Take(data.Count / 2)));
            output.Add(new Dictionary<char, int>(data.Skip(data.Count / 2)));
            return output;
        }
        
        /*
         *  Split dictionary unevenly and return 2 indexed dictionary array (second will be longer)
         */
        private List<Dictionary<char, int>> SplitUnEvenly (Dictionary<char, int> data)
        {
            if (data.Count == 2)
                return SplitEvenly(data);
            
            List<Dictionary<char, int>> output = new List<Dictionary<char, int>>(2);
            output.Add(new Dictionary<char, int>(data.Take((data.Count-1) / 2)));
            output.Add(new Dictionary<char, int>(data.Skip((data.Count-1) / 2)));
            return output;
        }
        
        private int[] CountSplit(List<Dictionary<char, int>> data)
        {
            int[] count = new int[2];
            count[0] = data[0].Sum(pair => pair.Value);
            count[1] = data[1].Sum(pair => pair.Value);
            return count;
        }
        private List<Dictionary<char, int>> Compare(List<Dictionary<char, int>> evenly, List<Dictionary<char, int>> unevenly)
        {
            int[] evenlyCount = CountSplit(evenly);
            int[] unevenlyCount = CountSplit(unevenly);
            return evenlyCount[0] - evenlyCount[1] < Math.Abs(unevenlyCount[0] - unevenlyCount[1]) ? evenly : unevenly;
        } 
    }
}