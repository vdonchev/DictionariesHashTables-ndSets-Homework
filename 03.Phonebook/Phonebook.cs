namespace _03.Phonebook
{
    using System;
    using _01.HashMap;

    public static class Phonebook
    {
        public static void Main()
        {
            var phonebook = new HashMap<string, string>();
            var userInput = Console.ReadLine();
            while (userInput != "search")
            {
                var tokens = userInput.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (!phonebook.ContainsKey(tokens[0]))
                {
                    phonebook[tokens[0]] = string.Empty;
                }

                phonebook[tokens[0]] = tokens[1];

                userInput = Console.ReadLine();
            }

            while (true)
            {
                var searchName = Console.ReadLine();
                var result = string.Empty;
                if (phonebook.TryGetValue(searchName, out result))
                {
                    Console.WriteLine($"{searchName} -> {phonebook[searchName]}");
                }
                else
                {
                    Console.WriteLine($"Contact {searchName} does not exist.");
                }
            }
        }
    }
}