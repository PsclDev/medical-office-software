using System.Collections.ObjectModel;
using System.Windows;
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
            int index = ListViewEmployees.SelectedIndex;
            if (index != -1) {
                employeeList.RemoveAt(index);
                string sql = $"DELETE FROM employee where id = {index}";
                Database.RunSQL(sql);
                sql = $"DELETE FROM employee_login where id = {index}";
                Database.RunSQL(sql);
            }
            else
                CstmMsgBx.Error("you need to select a employee");
        }

        private void ComboboxPermission_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            int index = ListViewEmployees.SelectedIndex;
            if (index != -1) {
                var newRole = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content as string;
                string sql = $"UPDATE employee SET role = '{newRole}' WHERE id = {index}";
                Database.RunSQL(sql);
            }
        }
    }
}
