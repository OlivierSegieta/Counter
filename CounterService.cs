using System.Collections.ObjectModel;
using System.IO;

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
                var lines = File.ReadAllLines(path);
                Counters.Clear();
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2 &&
                        int.TryParse(parts[1], out int initialValue))
                    {
                        var counter = new Counter(parts[0], initialValue);
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
            var lines = new List<string>();

            foreach (var counter in Counters)
            {
                lines.Add($"{counter.Name},{counter.Value}");
            }

            File.WriteAllLines(path, lines);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving counters: {ex.Message}");
        }
    }
}
