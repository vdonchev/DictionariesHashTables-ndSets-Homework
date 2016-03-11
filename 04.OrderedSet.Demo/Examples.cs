namespace _04.OrderedSet.Demo
{
    using System;

    public static class Examples
    {
        public static void Main()
        {
            var set = new OrderedSet<int>();
            set.Add(20);
            set.Add(10);
            set.Add(8);
            set.Add(12);
            set.Add(40);
            set.Add(30);
            set.Add(50);
            set.Add(41);
            set.Add(60);
            set.Add(22);
            set.Add(21);
            set.Add(31);
            set.Add(62);

            set.PrintBfsStyle();

            Console.WriteLine(string.Join(", ", set));

            set.Remove(40);

            Console.WriteLine(string.Join(", ", set));

            set.Remove(20);
            Console.WriteLine(string.Join(", ", set));

            set.Remove(8);
            Console.WriteLine(string.Join(", ", set));

            set.Remove(62);
            Console.WriteLine(string.Join(", ", set));

            set.Add(62);
            set.Add(62);
            set.Add(62);
            set.Add(62);
            set.Add(62);
            set.Add(62);

            set.PrintBfsStyle();

            Console.WriteLine("Set count is: " + set.Count);
            Console.WriteLine("Set contains 62? = " + set.Contains(62));
        }
    }
}
