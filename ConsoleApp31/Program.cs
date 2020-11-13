using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp31
{
    class Program
    {


        static void Main(string[] args)
        {

            char[] alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ1234567890 .*".ToCharArray();

            // Пытаемся вычислить размерность таблицы
            Console.WriteLine("Символов в алфавите: " + alphabet.Length);
            int rows = 0, columns;
            bool isValidTable;
            do
            {
                Console.Write("Количество колонок в таблице: ");
                isValidTable = int.TryParse(Console.ReadLine(), out columns) && columns > 1;
                if (!isValidTable)
                {
                    Console.WriteLine("Необходимо ввести число больше 1");
                }
                else
                {
                    rows = alphabet.Length / columns;
                    isValidTable &= rows > 1 && rows * columns == alphabet.Length;
                    if (!isValidTable)
                    {
                        Console.WriteLine("Необходимо ввести число колонок таким образом, чтобы число строк таблицы было больше 1 и таблица могла вмещать в себе все символы алфавита");
                    }
                }
            }
            while (!isValidTable);

            // Пытаемся получить ключевое слово
            char[] keyWord;
            bool isValidKeyWord;
            do
            {
                Console.Write("Введите ключевое слово: ");
                keyWord = Console.ReadLine().ToUpper().Distinct().ToArray();
                isValidKeyWord = keyWord.Length > 0 && keyWord.Length < 10;
                if (!isValidKeyWord)
                {
                    Console.WriteLine("Ключевое слово не может быть пустой строкой или содержать более 10 символов ");
                }
                else
                {
                    isValidKeyWord &= !keyWord.Except(alphabet).Any();
                    if (!isValidKeyWord)
                    {
                        Console.WriteLine("Ключевое слово не может содержать символы, которых нет в алфавите");
                    }
                }
            }
            while (!isValidKeyWord);
            for (int i = 0; i < keyWord.Length; i++)
            {
                if (keyWord[i] == ' ')
                    keyWord[i] = '*';
                Console.Write(keyWord[i]);
            }
            Console.WriteLine();
            // Создаем таблицу
            var table = new char[rows, columns];

            // Вписываем в нее ключевое слово
            for (var i = 0; i < keyWord.Length; i++)
            {
                table[i / columns, i % columns] = keyWord[i];
            }

            // Исключаем уникальные символы ключевого слова из алфавита
            alphabet = alphabet.Except(keyWord).ToArray();

            // Вписываем алфавит
            for (var i = 0; i < alphabet.Length; i++)
            {
                int position = i + keyWord.Length;
                table[position / columns, position % columns] = alphabet[i];
            }

            // Получаем сообщение, которое необходимо зашифровать
            string message;
            bool isValidMessage;
            do
            {
                Console.Write("Введите сообщение: ");
                message = Console.ReadLine().ToUpper();
                isValidMessage = !string.IsNullOrEmpty(message);
                if (!isValidMessage)
                {
                    Console.WriteLine("Сообщение не может быть пустой строкой");
                }
            }
            while (!isValidMessage);

            // Создаем место для будущего зашифрованного сообщения
            var result = new char[message.Length];

            // Шифруем сообщение
            for (var k = 0; k < message.Length; k++)
            {
                char symbol = message[k];
                // Пытаемся найти символ в таблице
                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < columns; j++)
                    {
                        if (symbol == table[i, j])
                        {
                            symbol = table[(i + 1) % rows, j]; // Смещаемся циклически на следующую строку таблицы и запоминаем новый символ
                            i = rows; // Завершаем цикл по строкам
                            break; // Завершаем цикл по колонкам
                        }
                    }
                }
                // Записываем найденный символ
                result[k] = symbol;
            }

            // Выводим зашифрованное сообщение
            Console.WriteLine("Зашифрованное сообщение: " + new string(result));

            //var result = new char[message.Length];

            // Шифруем сообщение
            for (var k = 0; k < result.Length; k++)
            {
                char symbol = result[k];
                // Пытаемся найти символ в таблице
                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < columns; j++)
                    {
                        if (symbol == table[i, j])
                        {
                            if(i - 1 > -1)
                            symbol = table[i - 1, j]; // Смещаемся циклически на следующую строку таблицы и запоминаем новый символ
                            else
                            symbol = table[rows - 1, j]; // Смещаемся циклически на следующую строку таблицы и запоминаем новый символ
                            i = rows; // Завершаем цикл по строкам
                            break; // Завершаем цикл по колонкам
                        }
                    }
                }
                // Записываем найденный символ
                result[k] = symbol;
            }

            // Выводим зашифрованное сообщение
            Console.WriteLine("Расшифрованное сообщение: " + new string(result));
            Console.ReadKey();
        }

    }
}