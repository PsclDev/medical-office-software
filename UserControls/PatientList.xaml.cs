using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace MoS.UserControls {
    /// <summary>
    /// Interaction logic for PatientList.xaml
    /// </summary>
    public partial class PatientList : UserControl {
        public PatientList() {
            InitializeComponent();
            LoadPatients();
        }

        private ObservableCollection<Objects.Patient> patientList = new ObservableCollection<Objects.Patient>();

        private void LoadPatients() {
            ListViewPatient.Items.Clear();
            patientList = Database.PatientList;

            ListViewPatient.ItemsSource = patientList;
        }

        private void ListViewPatient_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            DateTime lastVisit = Convert.ToDateTime(patientList[ListViewPatient.SelectedIndex].LastVisit);

            if ((DateTime.Now - lastVisit).TotalDays > 3650)
                BtnDeletePatient.Visibility = Visibility.Visible;
            else
                BtnDeletePatient.Visibility = Visibility.Hidden;
        }

        private void BtnEditPatient_Click(object sender, RoutedEventArgs e) {
            int index = ListViewPatient.SelectedIndex;
            if (index != -1) {
                var ep = new PopUp.EditPatient(patientList[index]);
                var result = ep.ShowDialog();

                if (result == true) {
                    patientList[index] = ep.patient;
                    Database.RunSQL($"UPDATE patient set firstName = '{ep.patient.Firstname}', lastName = '{ep.patient.Lastname}', street = '{ep.patient.Street}', zipCode = '{ep.patient.Town}', dateOfBirth = '{ep.patient.Birthday}', phoneNumber = '{ep.patient.Phone}', healthInsurance = '{ep.patient.HealthInsurance}', insuranceNumber = '{ep.patient.InsuranceNumber}' WHERE id = {ep.patient.Id}");
                }
            }
            else
                CstmMsgBx.Error("you need to select a patient");
        }

        private void BtnAddPatient_Click(object sender, RoutedEventArgs e) {
            var np = new PopUp.NewPatient();
            var result = np.ShowDialog();

            if (result == true) {
                patientList.Add(np.Patient);
                Database.RunSQL($"INSERT into patient (firstName, lastName, street, zipCode, dateOfBirth, phoneNumber, healthInsurance, insuranceNumber) VALUES ('{np.Patient.Firstname}','{np.Patient.Lastname}','{np.Patient.Street}','{np.Patient.Town}','{np.Patient.Birthday}','{np.Patient.Phone}','{np.Patient.HealthInsurance}','{np.Patient.InsuranceNumber}')");
            }
        }

        private void BtnDeletePatient_Click(object sender, RoutedEventArgs e) {
            int id = patientList[ListViewPatient.SelectedIndex].Id;
            string sql = $"DELETE FROM patient where id = {id}";
            Database.RunSQL(sql);
            sql = $"DELETE FROM patient_visit where idPatient = {id}";
            Database.RunSQL(sql);
        }
    }
}
