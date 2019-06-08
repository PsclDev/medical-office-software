using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MoS.UserControls {
    /// <summary>
    /// Interaction logic for PatientDetails.xaml
    /// </summary>
    public partial class PatientDetails : UserControl {
        public PatientDetails() {
            InitializeComponent();
            CmBxPatients.ItemsSource = Database.PatientList;
        }

        private ObservableCollection<Objects.Visit> visitList = new ObservableCollection<Objects.Visit>();

        private void CmBxPatients_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var patient = Database.PatientList[CmBxPatients.SelectedIndex];
            LoadPersonalInformation(patient);
            LoadCourseOfDisease(patient);
        }

        private void LoadPersonalInformation(Objects.Patient patient) {
            TxtBxFirstname.Text = patient.Firstname;
            TxtBxLastname.Text = patient.Lastname;
            TxtBxStreet.Text = patient.Street;
            TxtBxTown.Text = patient.Town;
            TxtBxDateOfBirth.Text = CalculateAge(patient.Birthday);
            TxtBxPhone.Text = patient.Phone;
            TxtBxHealthInsurance.Text = patient.HealthInsurance;
            TxtBxInsuranceNumber.Text = patient.InsuranceNumber;
        }

        private void LoadCourseOfDisease(Objects.Patient patient) {
            visitList = Database.GetPatientVisits(patient.Id);
            CmBxVisits.ItemsSource = visitList;
        }

        private string CalculateAge(string DateOfBirth) {
            var today = DateTime.Today;
            var doB = DateTime.Parse(DateOfBirth);

            int age = today.Year - doB.Year;
            if (today.Month < doB.Month || (today.Month == doB.Month && today.Day < doB.Day))
                age--;

            return $"{DateOfBirth}  ({age})";
        }

        private void CmBxVisits_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var visit = visitList[CmBxVisits.SelectedIndex];
            TxtBxSickFromTo.Text = visit.WrittenSickFromTo;
            TxtBxWrittenBy.Text = visit.WrittenBy;
            TxtBxResult.Text = visit.Result;
            TxtBxMedication.Text = visit.Medication;
        }

        private void BtnAddVisit_Click(object sender, System.Windows.RoutedEventArgs e) {
            var nv = new PopUp.NewVisit();
            var result = nv.ShowDialog();

            if (result == true) {
                nv.Visit.PatientID = Database.PatientList[CmBxPatients.SelectedIndex].Id;
                nv.Visit.Date = DateTime.Now.ToString("dd.MM.yyy");
                visitList.Add(nv.Visit);
                Database.RunSQL($"INSERT INTO patient_visit (idPatient, date, writtenSickFromTo, writtenBy, result, medication) VALUES ('{nv.Visit.PatientID}','{nv.Visit.Date}','{nv.Visit.WrittenSickFromTo}','{nv.Visit.WrittenBy}','{nv.Visit.Result}','{nv.Visit.Medication}');");
            }
        }
    }
}
