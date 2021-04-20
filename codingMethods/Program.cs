using System;
using System.Linq;

namespace CodingMethods
{
    class Program
    {
        static void Main()
        {
            string text =
                "One morning, when Gregor Samsa woke from troubled dreams, he found himself transformed in his bed into a horrible vermin. He lay on his armour-like back, and if he lifted his head a little he could see his brown belly, slightly domed and divided by arches into stiff sections. The bedding was hardly able to cover it and seemed ready to slide off any moment. His many legs, pitifully thin compared with the size of the rest of him, waved about helplessly as he looked. What's happened to me? he thought. It wasn't a dream. His room, a proper human room although a little too small, lay peacefully between its four familiar walls. A collection of textile samples lay spread out on the table - Samsa was a travelling salesman - and above it there hung a picture that he had recently cut out of an illustrated magazine and housed in a nice, gilded frame. It showed a lady fitted out with a fur hat and fur boa who sat upright, raising a heavy fur muff that covered the whole of her lower arm towards the viewer. Gregor then turned to look out the window at the dull weather. Drops of rain could be heard hitting the pane, which made him feel quite sad. How about if I sleep a little bit longer and forget all this nonsense, he thought, but that was something he was unable to do because he was used to sleeping on his right, and in his present state couldn't get into that position. However hard he threw himself onto his right, he always rolled back to where he was. He must have tried it a hundred times, shut his eyes so that he wouldn't have to look at the floundering legs, and only stopped when he began to feel a mild, dull pain there that he had never felt before. Oh, God, he thought, what a strenuous career it is that I've chosen! Travelling day in and day out. Doing business like this";
            Console.WriteLine("Chcete demostrovat kód? (Y/N)");
            if (Console.ReadKey(false).Key == ConsoleKey.N)
            {
                while (true)
                {
                    Console.Write("Zadejte řetězec: ");
                    text = Console.ReadLine();
                    if (text?.Length > 1 && text[0] != text[1])
                        break;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Musíte zadat alespoň 2 znaky dlouhý řetězec a tyto dva znaky se musí lišit.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }


            //input text

            ShannonFano code = new ShannonFano(text);
            Console.Clear();
            Console.Write("Vstupní řetězec: ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
            PrintLine();
            PrintRow("Znak", "Počet", "Kód", "Pravděpodobnost");
            double probabilitySumCheck = 0;
            foreach ((char key, int val)in code.Words)
            {
                PrintLine();
                Console.ForegroundColor = Console.ForegroundColor == ConsoleColor.Blue
                    ? ConsoleColor.Green
                    : ConsoleColor.Blue;
                PrintRow(key.ToString(), val.ToString(), code.Code[key], $"{code.GetProbabilityAll()[key]}");
            }

            probabilitySumCheck = code.GetProbabilityAll().Values.Sum();

            Console.ForegroundColor = ConsoleColor.White;
            PrintLine();
            PrintRow("Celkem:", $"{code.WordsCount}", "Kontrola:", probabilitySumCheck > 0.99999999 ? "1" : "0");
            PrintLine();
            Console.WriteLine();

            PrintLine();
            PrintRow("Průměrná délka kódového slova", $"{code.GetAverageWordLenght()}");
            PrintLine();
            PrintRow("Efektivnost kódu", $"{(int) (code.GetEfficiency() * 100)}%");
            PrintLine();

            Console.ReadLine();
        }

        private const int TableWidth = 60;

        private static void PrintLine(char character = '-') => Console.WriteLine(new string(character, TableWidth));

        private static void PrintRow(params string[] columns)
        {
            int width = (TableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
                row += AlignCentre(column, width) + "|";

            Console.WriteLine(row, Console.ForegroundColor);
        }

        private static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text[..(width - 3)] + "..." : text;

            if (string.IsNullOrEmpty(text))
                return new string(' ', width);

            return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
        }
    }
}