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

namespace MetroTherm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window


    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainViewModel();

        }

        //private void AddEquipment_Click(object sender, RoutedEventArgs e)
        //{
        //    //// Hent ViewModel
        //    //if (DataContext is MainViewModel vm)
        //    //{
        //    //    // Lav nyt Equipment baseret på UI-felterne
        //    //    var eq = new Equipment
        //    //    {
        //    //        DeviceId = TxtDeviceId.Text,
        //    //        ProductName = TxtProductName.Text,
        //    //        Model = TxtModel.Text,
        //    //        ConnectionState = ChkConnected.IsChecked == true
        //        };

        //        // Tilføj til listen – DataGrid opdaterer sig automatisk
        //        vm.Equipments.Add(eq);

        //        // (valgfrit) ryd felter
        //        //TxtDeviceId.Clear();
        //        //TxtProductName.Clear();
        //        //TxtModel.Clear();
        //        //ChkConnected.IsChecked = false;
        //    }
        //}
        //private void AddCustomer_Click(object sender, RoutedEventArgs e)
        //{
        //    if (DataContext is MainViewModel vm)
        //    {
        //        var cust = new Customer
        //        {
        //            CustomerId = TxtCustomerId.Text,
        //            Name = TxtCustomerName.Text,
        //            CustomerAddress = TxtCustomerAddress.Text,
        //            //PhoneNumber = TxtCustomerPhone.Text,
        //            //Email = TxtCustomerEmail.Text
        //        };

        //        vm.Customers.Add(cust);

        //        // (valgfrit) ryd felter
        //        TxtCustomerId.Clear();
        //        TxtCustomerName.Clear();
        //        TxtCustomerAddress.Clear();
        //        TxtCustomerPhone.Clear();
        //        TxtCustomerEmail.Clear();
        //    }
        //}




    }
}

//}
