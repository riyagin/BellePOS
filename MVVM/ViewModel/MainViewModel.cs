using BellePOS.Core;
using BellePOS.MVVM.Model;
using System.ComponentModel;
using System.Windows.Input;

namespace BellePOS.MVVM.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private string _selectedCustomerText;
        private Customer _selectedCustomer;

        public string SelectedCustomerText
        {
            get => _selectedCustomerText;
            set
            {
                _selectedCustomerText = value;
                OnPropertyChanged(nameof(SelectedCustomerText));
            }
        }

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));
                UpdateSelectedCustomerText();
            }
        }

        public ICommand OpenCustomerSearchCommand { get; }
        public event System.Action CustomerSelectionComplete;

        public MainViewModel()
        {
            OpenCustomerSearchCommand = new RelayCommand(OpenCustomerSearch);
        }

        private void OpenCustomerSearch()
        {
            var searchViewModel = new CustomerSearchViewModel();
            searchViewModel.CustomerSelected += OnCustomerSelected;

            // Raise event to let the View handle window creation and display
            CustomerSearchRequested?.Invoke(searchViewModel);
        }

        // Event to request customer search dialog from the View
        public event System.Action<CustomerSearchViewModel> CustomerSearchRequested;

        private void OnCustomerSelected(Customer customer)
        {
            // Pure ViewModel logic - only update data
            SelectedCustomer = customer;

            // Notify View that selection is complete (for window closing)
            CustomerSelectionComplete?.Invoke();
        }

        private void UpdateSelectedCustomerText()
        {
            if (SelectedCustomer != null)
            {
                SelectedCustomerText = $"{SelectedCustomer.Id}";
            }
            else
            {
                SelectedCustomerText = string.Empty;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
