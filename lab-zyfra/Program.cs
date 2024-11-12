using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class Program
{
    static async Task Main()
    {
        using HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5217/api/");


        Console.WriteLine("Текущее состояние данных:");
        var state = await GetState(client);
        foreach (var entry in state)
        {
            Console.WriteLine($"{entry.Key} = {entry.Value}");
        }

        Console.Write("Введите номер для обновления: ");
        string id = Console.ReadLine();

        if (!state.ContainsKey(id))
        {
            Console.WriteLine("Номер не найден.");
            return;
        }

        Console.Write($"Введите новое значение для номера {id}: ");
        string newValue = Console.ReadLine();

        await UpdateState(client, id, newValue);
    }

    // Метод для получения состояния с сервера
    public static async Task<Dictionary<string, string>> GetState(HttpClient client)
    {
        var response = await client.GetFromJsonAsync<Dictionary<string, string>>("State");
        return response ?? new Dictionary<string, string>();
    }

    // Метод для обновления состояния на сервере
    public static async Task UpdateState(HttpClient client, string id, string newValue)
    {
        var response = await client.PutAsJsonAsync($"State/{id}", newValue);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Изменено: {id} = {newValue}");
            Console.WriteLine($"Сообщение для внешнего мира: {{ 'id': '{id}', 'new_value': '{newValue}' }}");
        }
        else
        {
            Console.WriteLine($"Ошибка: {response.ReasonPhrase}");
        }
    }
}
