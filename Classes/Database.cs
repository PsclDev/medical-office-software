using s = MoS.Properties.Settings;
using System.Data.SQLite;
using System;
using System.Collections.ObjectModel;

namespace MoS.Classes {
    public static class Database {
        private static bool connectionIsOpen = false;
        private static SQLiteConnection CONNECTION = new SQLiteConnection($"Data Source={s.Default.DbPath};Version=3;");

        public static ObservableCollection<Objects.Patient> PatientList = new ObservableCollection<Objects.Patient>();
        public static ObservableCollection<Objects.Employee> EmployeeList = new ObservableCollection<Objects.Employee>();

        private static void ConnectOpen() {
            CONNECTION.Open();
            connectionIsOpen = true;
        }

        private static void ConnectClose() {
            connectionIsOpen = false;
            CONNECTION.Close();
        }

        private static void CheckConnectionStatus() {
            if (connectionIsOpen == true) {
                connectionIsOpen = false;
                CONNECTION.Close();
            }
        }

        public static int RunSQL(string SQL) {
            try {
                ConnectOpen();

                using (SQLiteCommand cmd = new SQLiteCommand(SQL, CONNECTION)) {
                    cmd.ExecuteNonQuery();
                }

                ConnectClose();
                return 0;
            }
            catch (SQLiteException sqlex) {
                CheckConnectionStatus();
                if (sqlex.ErrorCode == 19)
                    return -1;
            }
            catch (Exception exc) {
                Log.Error(Log.GetMethodName(), exc.ToString());
                CheckConnectionStatus();
                return -1;
            }
            return -1;
        }

        public static int GetUserID(string Username) {
            int IdUser = -1;

            ConnectOpen();
            string sql = $"SELECT id, role FROM employee WHERE username = '{Username}'";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, CONNECTION)) {
                SQLiteDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read()) {
                    IdUser = Convert.ToInt32(dataReader["id"]);
                }
            }
            ConnectClose();

            return IdUser;
        }

        public static (string salt, string password, string permissions) AuthenticateLogin(string Username) {
            try {
                string salt = "", password = "", permission = "", sql = "";
                int IdUser = GetUserID(Username);

                ConnectOpen();
                sql = $"SELECT id, role FROM employee WHERE username = '{Username}'";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, CONNECTION)) {
                    SQLiteDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read()) {
                        permission = dataReader["role"].ToString();
                    }
                }

                sql = $"SELECT salt, password FROM employee_login WHERE id_user = '{IdUser}'";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, CONNECTION)) {
                    SQLiteDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read()) {
                        salt = dataReader["salt"].ToString();
                        password = dataReader["password"].ToString();
                    }
                }
                ConnectClose();

                return (salt, password, permission);
            }
            catch (Exception exc) {
                CstmMsgBx.Error("A error occurred while trying to authenticate username and password");
                Log.Error(Log.GetMethodName(), exc.ToString());
                CheckConnectionStatus();
                return (null, null, null);
            }
        }

        public static void GetPatientList() {
            try {
                ConnectOpen();
                string sql = "SELECT * FROM patient";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, CONNECTION)) {
                    SQLiteDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read()) {
                        Objects.Patient patient = new Objects.Patient() {
                            Id = Convert.ToInt32(dataReader["id"]),
                            HealthInsurance = dataReader["healthInsurance"].ToString(),
                            InsuranceNumber = dataReader["insuranceNumber"].ToString(),
                            Firstname = dataReader["firstName"].ToString(),
                            Lastname = dataReader["lastName"].ToString(),
                            Street = dataReader["street"].ToString(),
                            Town = dataReader["zipCode"].ToString(),
                            Birthday = dataReader["dateOfBirth"].ToString(),
                            Phone = dataReader["phoneNumber"].ToString(),
                        };

                        PatientList.Add(patient);
                    }
                }

                ConnectClose();
            }
            catch (Exception exc) {
                Log.Error(Log.GetMethodName(), exc.ToString());
            }
        }

        public static void GetEmployeesList() {
            try {
                ConnectOpen();
                string sql = "SELECT * FROM employee";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, CONNECTION)) {
                    SQLiteDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read()) {
                        Objects.Employee employee = new Objects.Employee() {
                            Name = dataReader["username"].ToString(),
                            Role = dataReader["role"].ToString()
                        };

                        EmployeeList.Add(employee);
                    }
                }

                ConnectClose();
            }
            catch (Exception exc) {
                Log.Error(Log.GetMethodName(), exc.ToString());
            }
        }

        public static ObservableCollection<Objects.Visit> GetPatientVisits(int patientId) {
            try {
                ObservableCollection<Objects.Visit> visitList = new ObservableCollection<Objects.Visit>();
                ConnectOpen();
                string sql = $"SELECT * FROM patient_visit where idPatient =  {patientId}";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, CONNECTION)) {
                    SQLiteDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read()) {
                        Objects.Visit visit = new Objects.Visit() {
                            Id = Convert.ToInt32(dataReader["id"]),
                            PatientID = Convert.ToInt32(dataReader["idPatient"]),
                            Date = dataReader["date"].ToString(),
                            WrittenSickFromTo = dataReader["writtenSickFromTo"].ToString(),
                            WrittenBy = dataReader["writtenBy"].ToString(),
                            Result = dataReader["result"].ToString(),
                            Medication = dataReader["medication"].ToString()
                        };

                        visitList.Add(visit);
                    }
                }

                ConnectClose();
                return visitList;
            }
            catch (Exception exc) {
                Log.Error(Log.GetMethodName(), exc.ToString());
                return null;
            }
        }
    }
}
