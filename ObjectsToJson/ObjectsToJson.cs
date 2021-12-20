using System;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.IO;
using Class_Product;

namespace Task_16
{
    internal class ObjectsToJson
    {
        static void Main(string[] args)
        {
            int i = 0;
            int code = 0;
            string name = null;
            double cost = 0;
            bool slct = true;
            bool error;
            byte selection = 1;

            Console.WriteLine("Введите данные товаров:");
            Console.WriteLine(" - код (целочисленное значение);");
            Console.WriteLine(" - наименование (строка);");
            Console.WriteLine(" - цена (действительное число).");
            Console.WriteLine();

            string[,] productsArray = new string[1, 3];

            do
            {
                Console.WriteLine("Введите данные товара N{0}:", i + 1);
                Console.Write("Код: ");
                do
                {
                    try
                    {
                        int codeTry = Convert.ToInt32(Console.ReadLine());
                        error = false;
                        code = codeTry;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка! {0}", ex.Message);
                        Console.Write("Повторите ввод кода: ");
                        error = true;
                    }
                }
                while (error != false);

                Console.Write("Наименование: ");
                do
                {
                    try
                    {
                        string nameTry = Convert.ToString(Console.ReadLine());
                        error = false;
                        name = nameTry;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка! {0}", ex.Message);
                        Console.Write("Повторите ввод наименования: ");
                        error = true;
                    }
                }
                while (error != false);

                Console.Write("Цена: ");
                do
                {
                    try
                    {
                        double costTry = Convert.ToDouble(Console.ReadLine());
                        error = false;
                        cost = (double)costTry;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка! {0}", ex.Message);
                        Console.Write("Повторите ввод цены: ");
                        error = true;
                    }
                }
                while (error != false);

                Product product = new Product(code, name, cost);

                productsArray[i, 0] = Convert.ToString(product.Code);
                productsArray[i, 1] = product.Name;
                productsArray[i, 2] = Convert.ToString(product.Cost);

                Console.Write("Добавить сведения еще об одном товаре? 1 - да, 0 - нет: ");
                do
                {
                    try
                    {
                        Console.Write("Ваш выбор: ");
                        byte select = Convert.ToByte(Console.ReadLine());
                        error = false;
                        selection = select;
                        if (select > 1 || select < 0)
                        {
                            Console.WriteLine("Ошибка! Введите 1 или 2");
                            error = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка! {0}", ex.Message);
                        error = true;
                    }
                }
                while (error != false);

                if (selection == 0)
                {
                    slct = false;
                }
                else
                {
                    i++;
                    string[,] newProductsArray = new string[i + 1, 3];
                    for (int m = 0; m < productsArray.GetLength(0); m++)
                    {
                        for (int n = 0; n < productsArray.GetLength(1); n++)
                            newProductsArray[m, n] = productsArray[m, n];
                    }
                    productsArray = newProductsArray;
                }
                Console.WriteLine();
            }
            while (slct);

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            Product productReturned = new Product();

            string path = Directory.GetCurrentDirectory();
            path = Directory.GetParent(path).FullName; // наверное, следующие две строки содержат не очень правильный код, 
            path = Directory.GetParent(path).FullName; // но не могу придумать возможность для возврата общего каталога решения Task_16
            path = Directory.GetParent(path).FullName; 
            path = Path.Combine(path, "Product.json");

            StreamWriter sw = new StreamWriter(path, false);

            for (int n = 0; n < productsArray.GetLength(0); n++)
            {
                productReturned.Code = Convert.ToInt32(productsArray[n, 0]);
                productReturned.Name = productsArray[n, 1];
                productReturned.Cost = Convert.ToDouble(productsArray[n, 2]);

                string jsonString = JsonSerializer.Serialize(productReturned, options);
                sw.WriteLine(jsonString);
            }
            sw.Close();
            Console.WriteLine("Введенные данные записаны в файл {0}", path);
            Console.WriteLine();
            Console.Write("Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
