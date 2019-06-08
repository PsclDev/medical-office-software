using System.Windows;

namespace MoS.PopUp {
    /// <summary>
    /// Interaction logic for NewVisit.xaml
    /// </summary>
    public partial class NewVisit : Window {
        public NewVisit() {
            InitializeComponent();
        }

        public Objects.Visit Visit;

        private void BtnDiscard_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void BtnAddVisit_Click(object sender, RoutedEventArgs e) {
            Visit = new Objects.Visit() {
                WrittenSickFromTo = TxtBxSickFromTo.Text,
                WrittenBy = TxtBxWrittenBy.Text,
                Result = TxtBxResult.Text,
                Medication = TxtBxMedication.Text
            };

            this.DialogResult = true;
            this.Close();
        }
    }
}
