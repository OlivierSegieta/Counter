using System.ComponentModel;
using System.Runtime.CompilerServices;

public class Counter : INotifyPropertyChanged
{
    private int _value;
    private string _name;

    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }

    public int Value
    {
        get => _value;
        set
        {
            if (_value != value)
            {
                _value = value;
                OnPropertyChanged();
            }
        }
    }

    public int InitialValue { get; }

    public Counter(string name, int initialValue)
    {
        _name = name;
        _value = initialValue;
        InitialValue = initialValue;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
