namespace _02.CountSymbols
{
    using System;
    using System.Linq;
    using _01.HashMap;

    public static class CountSymbols
    {
        public static void Main()
        {
            var letters = new HashMap<char, int>();
            var text = Console.ReadLine();
            foreach (var ch in text)
            {
                if (!letters.ContainsKey(ch))
                {
                    letters[ch] = 0;
                }

                letters[ch]++;
            }

            var sorted = letters.OrderBy(k => k.Key);
            foreach (var ch in sorted)
            {
                Console.WriteLine($"{ch.Key}: {ch.Value} time/s");
            }
        }
    }
}
