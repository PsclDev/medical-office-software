namespace MoS.Properties {
    public static class loggedOnUser {
        public static string Username { get; set; }
        public static string Permission { get; set; }

        public static void EnableAutoLogin(string Username, string Password) {
            Settings.Default.AutoLogin = true;
            Settings.Default.AutoLogin_Username = Username;
            Settings.Default.AutoLogin_Password = Password;
        }

        public static void DisableAutoLogin() {
            Settings.Default.AutoLogin = false;
            Settings.Default.AutoLogin_Username = "";
            Settings.Default.AutoLogin_Password = "";
        }
    }
}
