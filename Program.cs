using System.Text;

namespace ZemerovaLongestWordFinder
{
    internal class Program
    {
        static void Error(string error)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"{error}");
            Console.ResetColor();
        }

        static bool IsLettersOnly(string word)
        {
            foreach (char c in word)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Программа ищет самое длинное слово, состоящее только из букв, в указанном .txt файле.\n");
            Console.WriteLine("Введите путь к файлу или напишите \"выход\" для завершения.\n");

            while (true)
            {
                Console.Write("Путь к .txt файлу: ");
                string filePath = Console.ReadLine()?.Trim().Trim('"');

                if (string.IsNullOrWhiteSpace(filePath))
                {
                    Error("Путь не должен быть пустым.\n");
                    continue;
                }

                if (filePath.Equals("выход", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Завершение программы.");
                    break;
                }

                if (!File.Exists(filePath))
                {
                    Error("Файл не найден. Вы можете попробовать снова.\n");
                    continue;
                }

                string text = File.ReadAllText(filePath, Encoding.UTF8).Trim();

                if (string.IsNullOrEmpty(text))
                {
                    Error("Файл пуст.\n");
                    continue;
                }

                string[] words = text.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                string longest = "";
                List<string> invalidWords = new List<string>();
                int validWords = 0;

                foreach (string word in words)
                {
                    if (IsLettersOnly(word))
                    {
                        validWords++;
                        if (word.Length > longest.Length)
                        {
                            longest = word;
                        }
                    }
                    else
                    {
                        invalidWords.Add(word);
                    }
                }

                if (validWords == 0)
                {
                    Error("В тексте нет слов, состоящих только из букв. Возможно, все слова содержат цифры или символы.\n");

                    if (invalidWords.Count > 0)
                    {
                        Console.WriteLine("Проигнорированные слова:");
                        foreach (var badWord in invalidWords)
                        {
                            Console.WriteLine($"{badWord}");
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine($"Самое длинное слово {longest} с длиной {longest.Length}\n");

                    if (invalidWords.Count > 0)
                    {
                        Console.WriteLine("Следующие слова были проигнорированы (содержат цифры или символы):");
                        foreach (var badWord in invalidWords)
                        {
                            Console.WriteLine($"{badWord}");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}