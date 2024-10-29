public class Program
{
    static void Main()
    {
        string filePath = "state.txt";

        Dictionary<string, string> state = ReadState(filePath);
        Console.WriteLine("Текущее состояние данных:");
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
        UpdateState(filePath, state, id, newValue);
    }

    // обновление состояния
    public static void UpdateState(string filePath, Dictionary<string, string> state, string id, string newValue)
    {
        state[id] = newValue;
        WriteState(filePath, state);

        Console.WriteLine($"Изменено: {id} = {newValue}");
        Console.WriteLine($"Сообщение для внешнего мира: {{ 'id': '{id}', 'new_value': '{newValue}' }}");
    }

    // чтение файла состояния
    public static Dictionary<string, string> ReadState(string filePath)
    {
        var state = new Dictionary<string, string>();

        if (File.Exists(filePath))
        {
            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(" = ");
                if (parts.Length == 2)
                {
                    state[parts[0]] = parts[1];
                }
            }
        }
        else
        {
            Console.WriteLine("Файл состояния не найден.");
        }

        return state;
    }

    // запись состояния в файл
    public static void WriteState(string filePath, Dictionary<string, string> state)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var entry in state)
            {
                writer.WriteLine($"{entry.Key} = {entry.Value}");
            }
        }
    }
}