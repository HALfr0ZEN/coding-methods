using System;

namespace ShannonFanCode
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Hello my name is &xasfd4165í and i think you are beautiful!";
            //input text
            
            ShannonFano code = new ShannonFano(text);
            
            foreach (var (key,val) in code.Words)
            {
                Console.WriteLine(key + " : " + val);
            } 
            
            Console.WriteLine();
            
            foreach (var (key,val) in code.Code)
            {
                Console.WriteLine(key + " : " + val);
            }

            Console.WriteLine(code.GetAverageWord());
            Console.WriteLine(code.GetEfficiency());
        }
    }
}