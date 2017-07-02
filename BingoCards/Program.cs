using System;
using System.Collections.Generic;
using System.Linq;

namespace BingoCards
{
    class Program
    {
        static void Main(string[] args)
        {
            var seedGen = new Random();

            var numOutputs = 1;
            if (args.Length > 0)
                numOutputs = int.Parse(args[0]);

            for (var x = 0; x < numOutputs; x++)
            {
                var fullCard = BingoCard(seedGen.Next());
                var cardWithHoles = PutHolesInCard(fullCard, seedGen.Next());

				Console.WriteLine("--------------------------"); 
                Console.WriteLine(string.Join(",", cardWithHoles.Where((n, i) => i % 3 == 0).Select(n => n == 0 ? "  " : n.ToString("D2"))));
                Console.WriteLine(string.Join(",", cardWithHoles.Where((n, i) => i % 3 == 1).Select(n => n == 0 ? "  " : n.ToString("D2"))));
                Console.WriteLine(string.Join(",", cardWithHoles.Where((n, i) => i % 3 == 2).Select(n => n == 0 ? "  " : n.ToString("D2"))));
				Console.WriteLine("--------------------------");
                Console.WriteLine("");
            }
        }

        static IEnumerable<int> BingoCard(int seed)
        {
            var random = new Random(seed);
            var allNumbers = new List<List<int>>();

            foreach (var tenGroup in Enumerable.Range(0, 9))
            {
                var numbers = new HashSet<int>();
                while (numbers.Count < 3)
                {
                    numbers.Add(tenGroup * 10 + random.Next(1, 10));
                }
                var list = numbers.ToList();
                list.Sort();
                allNumbers.Add(list);
            }

            return allNumbers.SelectMany(x => x);
        }

        static IEnumerable<int> PutHolesInCard(IEnumerable<int> fullCard, int seed)
        {
            var cardWithHoles = new List<int>(fullCard);
            var random = new Random(seed);

            var numbers = new HashSet<int>();
            while (numbers.Count < 12)
            {
                numbers.Add(random.Next(0, 27));
            }

            foreach (var number in numbers)
                cardWithHoles[number] = 0;

            return cardWithHoles;
        }
    }
}
