using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.Diagnostics.Metrics;

namespace CounterSegieta
{
    public partial class MainPage : ContentPage
    {
        private readonly CounterService _counterService;

        public MainPage()
        {
            InitializeComponent();
            _counterService = new CounterService();
            CountersCollectionView.ItemsSource = _counterService.Counters;
        }

        private async void OnAddCounterClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CounterNameEntry.Text))
            {
                await DisplayAlert("Błąd", "Proszę podać nazwę licznika", "OK");
                return;
            }

            if (!int.TryParse(InitialValueEntry.Text, out int initialValue))
            {
                await DisplayAlert("Błąd", "Proszę podać prawidłową wartość początkową", "OK");
                return;
            }

            _counterService.AddCounter(CounterNameEntry.Text, initialValue);

            // Czyszczenie pól
            CounterNameEntry.Text = string.Empty;
            InitialValueEntry.Text = string.Empty;
        }

        private void OnIncrementClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Counter counter)
            {
                counter.Value++;
                _counterService.UpdateCounter(counter);
            }
        }

        private void Clear(object sender, EventArgs e)
        {
            
        }

        private void OnDecrementClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Counter counter)
            {
                counter.Value--;
                _counterService.UpdateCounter(counter);
            }
        }
    }
}