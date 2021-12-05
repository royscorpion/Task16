using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Task16
{
    class Program
    {
        static void Main(string[] args)
        {
            ///Создание массива из 5 (quantity) товаров 
            ///с вводом значений пользователем с клавиатуры, 
            ///с контролем правильности вводимых значений
            #region Создание массива
            int quantity = 5;
            string[,] products = new string[quantity, 3];
            for (int i = 0; i < quantity; i++)
            {
                Console.WriteLine("\nЗаполните данные товара {0}", i + 1);
                Console.Write("Введите код товара: ");
                InputIntNumber(out int code);
                products[i, 0] = Convert.ToString(code);
                Console.Write("Введите название товара: ");
                products[i, 1] = Convert.ToString(Console.ReadLine());
                Console.Write("Введите стоимость товара: ");
                InputDoubleNumber(out double cost);
                products[i, 2] = Convert.ToString(cost);
            }
            #endregion

            #region Сериализация массива в json-строку и сохранение ее программно в файл D:/Data/Products.json
            string jsonString = "";
            DirectoryInfo directory = new DirectoryInfo("D:/Data/");
            if (!directory.Exists) directory.Create();
            for (int i = 0; i < quantity; i++)
            {
                Product product = new Product()
                {
                    ProductCode = Convert.ToInt32(products[i, 0]),
                    ProductName = products[i, 1],
                    ProductCost = Convert.ToDouble(products[i, 2])
                };
                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                    WriteIndented = true
                };
                jsonString += JsonSerializer.Serialize(product, options)+"\n";
            }
            File.WriteAllText("D:/Data/Products.json", jsonString);
            Console.WriteLine("\nСпасибо! Введенные данные записаны\nНажмите любую клавишу на клавиатуре... ");
            #endregion

            Console.ReadKey();
        }

        #region Проверка корректности введенных данных
        static void InputIntNumber(out int number)
        {
            number = 0;
            bool x;
            do
            {
                try
                {
                    number = Convert.ToInt32(Console.ReadLine());
                    x = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка! {0}\nПопробуйте еще раз\n", ex.Message);
                    Console.Write("Введите целое число: ");
                    x = true;
                }
            } while (x);
        }
        static void InputDoubleNumber(out double number)
        {
            number = 0;
            bool x;
            do
            {
                try
                {
                    number = Convert.ToDouble(Console.ReadLine());
                    x = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка! {0}\nПопробуйте еще раз\n", ex.Message);
                    Console.Write("Введите число: ");
                    x = true;
                }
            } while (x);
        }
        #endregion
    }

    public class Product
    {
        public int ProductCode { get; set; }
        public string ProductName { get; set; }
        public double ProductCost { get; set; }
    }
}
