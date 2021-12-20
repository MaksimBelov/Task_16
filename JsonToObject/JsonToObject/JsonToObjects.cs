using System;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.IO;
using Class_Product;

namespace Task_16
{
    internal class JsonToObject
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            path = Directory.GetParent(path).FullName; // ?
            path = Directory.GetParent(path).FullName; // ?
            path = Directory.GetParent(path).FullName; // ?
            path = Directory.GetParent(path).FullName; // ?
            path = Path.Combine(path, "Product.json");

            if (File.Exists(path))
            {
                Console.WriteLine("Прочитаны данные из файла {0}", path);
                Console.WriteLine();

                StreamReader sr = new StreamReader(path);
                string jsonString = sr.ReadToEnd();
                jsonString = jsonString.Replace("}", "}|");

                string[] arrayOfProducts = jsonString.Split('|');

                double maxCost = 0;
                string maxCostProductName = null;

                for (int i = 0; i < arrayOfProducts.Length - 1; i++)
                {
                    Product productFromJson = JsonSerializer.Deserialize<Product>(arrayOfProducts[i]);
                    if (productFromJson.Cost > maxCost)
                    {
                        maxCost = productFromJson.Cost;
                        maxCostProductName = productFromJson.Name;
                    }
                    else
                    {
                        if (productFromJson.Cost == maxCost)
                        {
                            maxCost = productFromJson.Cost;
                            maxCostProductName = maxCostProductName + " и " + productFromJson.Name;
                        }
                    }
                }
                Console.WriteLine("Самый дорогой товар(ы) с ценой {0}:", maxCost);
                Console.WriteLine("{0}", maxCostProductName);
                Console.WriteLine();
                Console.Write("Нажмите любую клавишу...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Нет файла для чтения");
                Console.Write("Нажмите любую клавишу...");
                Console.ReadKey();
            }
        }
    }
}
