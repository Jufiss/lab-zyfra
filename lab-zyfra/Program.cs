using System.Net.Http.Json;
using lab_zyfra;

public class Program
{
    static void Main()
    {
        using HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5217/api/");


        Console.WriteLine("Текущее состояние данных:");
        var state = GetState(client);
        foreach (var entry in state)
        {
            Console.WriteLine($"{entry.Id} = {entry.Value}");
        }

        Console.Write("Введите номер для обновления: ");
        string id = Console.ReadLine();

        Console.Write($"Введите новое значение для номера {id}: ");
        string newValue = Console.ReadLine();

        UpdateState(client, id, newValue);
    }

    // Метод для получения состояния с сервера
    public static List<State> GetState(HttpClient client)
    {
        var response = client.GetFromJsonAsync<List<State>>("State").Result;

        return response ?? new List<State>();
    }

    // Метод для обновления состояния на сервере
    public static void UpdateState(HttpClient client, string id, string newValue)
    {
        var response = client.PutAsJsonAsync($"State/{id}", newValue).Result;

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
