using System.Collections.ObjectModel;
using System.Text.Json;

public class CounterService
{
    private const string FileName = "counters.txt";
    public ObservableCollection<Counter> Counters { get; private set; }

    public CounterService()
    {
        Counters = new ObservableCollection<Counter>();
        LoadCounters();
    }

    public void AddCounter(string name, int initialValue)
    {
        var counter = new Counter(name, initialValue);
        Counters.Add(counter);
        SaveCounters();
    }

    public void UpdateCounter(Counter counter)
    {
        SaveCounters();
    }

    private void LoadCounters()
    {
        try
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, FileName);
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var counters = JsonSerializer.Deserialize<List<Counter>>(json);
                if (counters != null)
                {
                    Counters.Clear();
                    foreach (var counter in counters)
                    {
                        Counters.Add(counter);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading counters: {ex.Message}");
        }
    }

    private void SaveCounters()
    {
        try
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, FileName);
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(Counters.ToList(), options);
            File.WriteAllText(path, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving counters: {ex.Message}");
        }
    }
}
