using System.Collections.ObjectModel;
using System.Windows.Controls;


namespace MoS.UserControls {
    /// <summary>
    /// Interaction logic for EmployeeManagement.xaml
    /// </summary>
    public partial class EmployeeManagement : UserControl {
        public EmployeeManagement() {
            InitializeComponent();
            LoadEmployees();
        }

        private ObservableCollection<Objects.Employee> employeeList = new ObservableCollection<Objects.Employee>();

        private void LoadEmployees() {
            ListViewEmployees.Items.Clear();
            employeeList = Database.EmployeeList;

            ListViewEmployees.ItemsSource = employeeList;
        }

        private void ListViewEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void BtnAddEmployee_Click(object sender, System.Windows.RoutedEventArgs e) {
            var ne = new PopUp.NewEmployee();
            var result = ne.ShowDialog();

            if (result == true) {
                employeeList.Add(ne.Employee);
                Database.RunSQL($"INSERT INTO employee (username, role) VALUES ('{ne.Employee.Name}','{ne.Employee.Role}');");
                int userID = Database.GetUserID(ne.Employee.Name);
                string salt = Crypto.CreateSalt(48);
                string password = Crypto.HashToSHA256("123", salt);
                Database.RunSQL($"INSERT INTO employee_login VALUES ({userID}, '{salt}', '{password}');");
            }
        }

        private void BtnRemoveEmplyoee_Click(object sender, System.Windows.RoutedEventArgs e) {

        }

        private void ComboboxPermission_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }
    }
}
