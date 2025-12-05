using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace CryptoMonitor
{
    // Клас, що реалізує логіку роботи з API (OOP підхід)
    public class CryptoService
    {
        private readonly string _baseUrl;

        public CryptoService()
        {
            _baseUrl = "https://api.coingecko.com/api/v3";
        }

        // Метод для отримання ціни. Використовує асинхронність (Task)
        public async Task<decimal> GetPriceAsync(string coinId, string currency)
        {
            // 1. Використання бібліотеки RestSharp для створення клієнта
            var options = new RestClientOptions(_baseUrl);
            var client = new RestClient(options);

            // 2. Формування запиту
            // URL виглядатиме як: /simple/price?ids=bitcoin&vs_currencies=usd
            var request = new RestRequest("simple/price", Method.Get);
            request.AddParameter("ids", coinId);
            request.AddParameter("vs_currencies", currency);

            // 3. Виконання запиту
            RestResponse response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Помилка мережі або API. Статус: {response.StatusCode}");
            }

            // 4. Використання бібліотеки Newtonsoft.Json для парсингу
            try
            {
                // JObject.Parse дозволяє працювати з JSON як зі структурою
                JObject json = JObject.Parse(response.Content);

                // Перевірка, чи існують потрібні поля
                if (json[coinId] != null && json[coinId][currency] != null)
                {
                    // Конвертація значення у decimal
                    return (decimal)json[coinId][currency];
                }
                else
                {
                    throw new Exception("Дані не знайдено у відповіді.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Помилка обробки JSON: {ex.Message}");
            }
        }
    }

    class Program
    {
        // Main тепер теж асинхронний (async Task)
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Щоб коректно відображалась кирилиця
            Console.WriteLine("=== Лабораторна робота №3: Зовнішні бібліотеки (C#) ===");
            Console.WriteLine("Використано: RestSharp, Newtonsoft.Json\n");

            CryptoService service = new CryptoService();
            string currency = "usd";
            var coins = new List<string> { "bitcoin", "ethereum", "solana" };

            foreach (var coin in coins)
            {
                try
                {
                    Console.Write($"Отримання курсу для {coin}... ");

                    // Виклик нашого сервісу
                    decimal price = await service.GetPriceAsync(coin, currency);

                    // Зміна кольору консолі для краси
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{price} $");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Помилка: {ex.Message}");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}