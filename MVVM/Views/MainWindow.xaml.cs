using BellePOS.MVVM.ViewModel;

using System.Windows;

namespace BellePOS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CustomerSearch _currentSearchWindow;
        public MainWindow()
        {
            InitializeComponent();
            if (DataContext is MainViewModel viewModel)
            {
                viewModel.CustomerSearchRequested += OnCustomerSearchRequested;
                viewModel.CustomerSelectionComplete += OnCustomerSelectionComplete;
            }
        }
        private void OnCustomerSearchRequested(CustomerSearchViewModel searchViewModel)
        {
            // View handles window creation and display
            _currentSearchWindow = new CustomerSearch();
            _currentSearchWindow.DataContext = searchViewModel;
            _currentSearchWindow.ShowDialog();
        }

        private void OnCustomerSelectionComplete()
        {
            // View handles window closing
            _currentSearchWindow?.Close();
            _currentSearchWindow = null;
        }

        protected override void OnClosed(System.EventArgs e)
        {
            // Clean up when main window closes
            if (DataContext is MainViewModel viewModel)
            {
                viewModel.CustomerSearchRequested -= OnCustomerSearchRequested;
                viewModel.CustomerSelectionComplete -= OnCustomerSelectionComplete;
            }

            base.OnClosed(e);
        }
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void RestoreDown_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var searchWindow = new CustomerSearch();
            searchWindow.Show();
        }
    }
}