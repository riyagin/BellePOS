using BellePOS.MVVM.Model;
using BellePOS.MVVM.ViewModel;
using System.Windows;

namespace BellePOS
{
    /// <summary>
    /// Interaction logic for CustomerSearch.xaml
    /// </summary>
    public partial class CustomerSearch : Window
    {
        public event Action<Customer> CustomerSelected;
        public CustomerSearch()
        {
            InitializeComponent();
            DataContext = new CustomerSearchViewModel();
        }

        private void Search_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
