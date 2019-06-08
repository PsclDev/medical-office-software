using System.Windows;

namespace MoS.PopUp {
    /// <summary>
    /// Interaction logic for NewPatient.xaml
    /// </summary>
    public partial class NewPatient : Window {
        public NewPatient() {
            InitializeComponent();
        }

        public Objects.Patient Patient;

        private void BtnAddPatient_Click(object sender, RoutedEventArgs e) {
            Patient = new Objects.Patient() {
                Firstname = TxtBxFirstname.Text,
                Lastname = TxtBxLastname.Text,
                Street = TxtBxStreet.Text,
                Town = TxtBxTown.Text,
                Birthday = TxtBxDateOfBirth.Text,
                Phone = TxtBxPhone.Text,
                HealthInsurance = TxtBxHealthInsurance.Text,
                InsuranceNumber = TxtBxInsuranceNumber.Text
            };

            this.DialogResult = true;
            this.Close();
        }

        private void BtnDiscard_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
