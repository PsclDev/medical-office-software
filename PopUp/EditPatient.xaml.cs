using System.Windows;

namespace MoS.PopUp {
    /// <summary>
    /// Interaction logic for EditPatient.xaml
    /// </summary>
    public partial class EditPatient : Window {
        public EditPatient(Objects.Patient p) {
            InitializeComponent();
            LoadPatient(p);
        }

        public Objects.Patient patient { get; set; }

        private void LoadPatient(Objects.Patient p) {
            TxtBxFirstname.Text = p.Firstname;
            TxtBxLastname.Text = p.Lastname;
            TxtBxStreet.Text = p.Street;
            TxtBxTown.Text = p.Town;
            TxtBxDateOfBirth.Text = p.Birthday;
            TxtBxPhone.Text = p.Phone;
            TxtBxHealthInsurance.Text = p.HealthInsurance;
            TxtBxInsuranceNumber.Text = p.InsuranceNumber;
        }

        private void BtnSaveChanges_Click(object sender, RoutedEventArgs e) {
            patient = new Objects.Patient() {
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

        private void BtnDiscardChanges_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
