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
                await DisplayAlert("Błąd", "Podaj nazwę licznika", "ok");
                return;
            }

            if (!int.TryParse(InitialValueEntry.Text, out int initialValue))
            {
                await DisplayAlert("Błąd", "Podaj prawidłową wartość licznika", "ok");
                return;
            }

            _counterService.AddCounter(CounterNameEntry.Text, initialValue);

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

        private void OnDecrementClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Counter counter)
            {
                counter.Value--;
                _counterService.UpdateCounter(counter);
            }
        }
        /*private void Clear(object sender, EventArgs e)
        {
            
        }
        */
    }
}
