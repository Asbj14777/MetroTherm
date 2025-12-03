using MetroTherm.Models;
using MetroTherm.ViewModel;

namespace MetroThermTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void CustomerUsage_ShouldSumAllEquipmentValues()
        {
            // arrange
            CustomerViewModel customer = new CustomerViewModel(new Customer());
            customer.CustomerEquipments.Add(new EquipmentViewModel(new Equipment { Value = "6,7" }));
            customer.CustomerEquipments.Add(new EquipmentViewModel(new Equipment { Value = "69" }));
            customer.CustomerEquipments.Add(new EquipmentViewModel(new Equipment { Value = "nothing" }));

            // act
            double result = customer.CustomerUsage;

            // assert
            Assert.AreEqual(75.7, result);
        }

        [TestMethod]
        public void GetCalulations_ShouldCalculateConsumptionBill()
        {
            // arrange
            MainViewModel mvm = new MainViewModel();
            CustomerViewModel billingCustomer = new CustomerViewModel(new Customer("007", "John Doe", "Grove Street 123"));
            billingCustomer.CustomerEquipments.Add(new EquipmentViewModel(new Equipment
            {
                ParameterName = "Heat Energy E1",
                Value = "69",
                Timestamp = DateTime.Today.ToString("yyyy-MM-dd")
            }
            ));
            billingCustomer.CustomerEquipments.Add(new EquipmentViewModel(new Equipment
            {
                ParameterName = "Cooling energy E3",
                Value = "420",
                Timestamp = DateTime.Today.ToString("yyyy-MM-dd")
            }
            ));
            mvm.BillingCustomer = billingCustomer;
            mvm.PricePerKwh1 = 0.1; mvm.PricePerKwh2 = 0.2; mvm.PricePerKwh3 = 0.3;

            // act
            mvm.GetCalculations.Execute(null); // call GetCalculations command

            // assert
            double expectedSubtotal = 69 * 0.1 + 420 * 0.2; // e1 + e2
            double expectedVat = expectedSubtotal * 0.25;
            double expectedTotal = expectedSubtotal + expectedVat;

            Assert.AreEqual(expectedSubtotal, mvm.Subtotal);
            Assert.AreEqual(expectedVat, mvm.Vat);
            Assert.AreEqual(expectedTotal, mvm.Total);
        }

        [TestMethod]
        public void GenerateInvoice_ShouldReturnTrueWhenValid()
        {
            // arrange
            InvoiceRepository repo = new InvoiceRepository();
            string customerName = "John Doe";
            string customerAddress = "Grove Street 123";
            DateTime from = DateTime.Today;
            DateTime to = DateTime.Today;
            double subtotal = 420;
            double vat = 6.7;
            double total = 426.7;

            // act
            bool result = repo.GenerateInvoice(customerName, customerAddress, from, to, subtotal, vat, total);

            // assert
            // result is only true if DataHandler.SaveData returns true, which confirms successful persistence
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GenerateInvoice_ShouldNot_WhenBillingCustomerIsNull()
        {
            // arrange
            MainViewModel mvm = new MainViewModel();
            mvm.Subtotal = 420;

            // assert
            // CanExecute should be false since BillingCustomer is null/not selected
            Assert.IsFalse(mvm.GenerateInvoice.CanExecute(null));
        }
    }
}
