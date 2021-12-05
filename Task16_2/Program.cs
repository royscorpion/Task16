using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using Task16;

namespace Task16_2
{
    class Program
    {
        static void Main(string[] args)
        {
            ///Получение информации о товаре из json-файла, форматирование полученной информации и запись в массив строк
            string[] jsonString = File.ReadAllText("D:/Data/Products.json").Replace("\r\n", "").Replace("\n", "").Replace(" ", "").Replace("}{", "}|{").Split(Convert.ToChar("|"));

            //Десериализация данных массива строк и поиск самого дорогого товара с последующим выводом его названия и стоимости.
            double maxCost=0;
            string maxCostName="";
            foreach (var a in jsonString)
            {
                Product product = JsonSerializer.Deserialize<Product>(a);
                Console.WriteLine("Название товара: {2}; код товара: {1}; стоимость: {0}", product.ProductCost, product.ProductCode, product.ProductName);
                if (product.ProductCost > maxCost)
                {
                    maxCost = product.ProductCost;
                    maxCostName = product.ProductName;
                }
            }
            Console.WriteLine("\nСамый дорогой товар - {0}\nСтоимость: {1}", maxCostName,maxCost);
            Console.ReadKey();
        }
    }
}
