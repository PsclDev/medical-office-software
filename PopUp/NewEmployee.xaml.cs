using System.Windows;

namespace MoS.PopUp {
    /// <summary>
    /// Interaction logic for NewEmployee.xaml
    /// </summary>
    public partial class NewEmployee : Window {
        public NewEmployee() {
            InitializeComponent();
        }

        public Objects.Employee Employee;

        private void BtnDiscard_Click(object sender, RoutedEventArgs e) => this.Close();

        private void BtnAddEmployee_Click(object sender, RoutedEventArgs e) {
            Employee = new Objects.Employee() {
                Name = TxtBxUsername.Text,
                Role = ComboboxPermission.Text
            };

            this.DialogResult = true;
            this.Close();
        }
    }
}
