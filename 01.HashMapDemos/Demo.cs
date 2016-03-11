namespace _01.HashMapDemos
{
    using System;
    using HashMap;

    public static class Demo
    {
        public static void Main(string[] args)
        {
            var hashMap = new HashMap<int, int>(2);
            hashMap.Add(5, 1500);
            hashMap.Add(15, 150);
            hashMap.Add(55, 150000000);
            hashMap.Add(555014, 150000000);
            foreach (var map in hashMap)
            {
                Console.WriteLine(map.Key);
            }
        }
    }
}
