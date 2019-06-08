namespace MoS.Properties {
    public static class AppContent {
        public static void Load() {
            Database.GetEmployeesList();
            Database.GetPatientList();
        }
    }
}
