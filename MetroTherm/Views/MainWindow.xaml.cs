using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MetroTherm.ViewModel;
namespace MetroTherm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MetroTherm.ViewModel.MainViewModel viewModel = new MetroTherm.ViewModel.MainViewModel();    
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel; 

        }

        private void EquipmentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
