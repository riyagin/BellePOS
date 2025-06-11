using BellePOS.Core;
using BellePOS.MVVM.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace BellePOS.MVVM.ViewModel
{

    public class CustomerSearchViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService = new ApiService();

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        public ObservableCollection<Customer> FilteredCustomers { get; set; } = new();

        public ICommand SearchCommand { get; }
        public ICommand SelectCustomerCommand { get; }

        public CustomerSearchViewModel()
        {
            SearchCommand = new RelayCommand(Search);
            SelectCustomerCommand = new RelayCommand<Customer>(OnCustomerSelected);
        }

        public async void Search()
        {
            if (string.IsNullOrWhiteSpace(SearchText)) return;

            var result = await _apiService.SearchAsync<Customer>("api/customer/search", $"keyword={SearchText}");
            FilteredCustomers.Clear();
            if (result != null)
            {
                foreach (var customer in result)
                    FilteredCustomers.Add(customer);
            }
        }

        public event Action<Customer> CustomerSelected;

        private void OnCustomerSelected(Customer customer)
        {
            CustomerSelected?.Invoke(customer);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));




    }

}

