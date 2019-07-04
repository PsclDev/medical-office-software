using System.Windows;
using System.Windows.Input;
using Settings = MoS.Properties.Settings;

namespace MoS {
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window {
        public LoginWindow() {
            InitializeComponent();
            if (Properties.Settings.Default.AutoLogin == true)
                AuthenticateLogin(Settings.Default.AutoLogin_Username, Settings.Default.AutoLogin_Password, true);
        }

        #region Header
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void BtnShutdown_Click(object sender, RoutedEventArgs e) {
            Log.Trace(Message: "Shutdown Application");
            Application.Current.Shutdown();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }
        #endregion

        #region Window Content
        private void PwBx_KeyUp(object sender, KeyEventArgs e) {
            if (e.Key != System.Windows.Input.Key.Enter) return;

            e.Handled = true;
            AuthenticateLogin(TxtBx.Text, PwBx.Password.ToString(), false);
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e) {
            AuthenticateLogin(TxtBx.Text, PwBx.Password.ToString(), false);
        }
        #endregion

        #region Methods
        private void AuthenticateLogin(string enteredUsername, string enteredPassword, bool autoLogin) {
            string hashed_enteredPassword = "";
            var db_data = GetUserInformations(enteredUsername);
            if (db_data.password != null) {
                if (autoLogin == false)
                    hashed_enteredPassword = Crypto.HashToSHA256(enteredPassword, db_data.salt);

                if (hashed_enteredPassword.ToUpper() == db_data.password.ToUpper()) {
                    SetUserInformations(enteredUsername, db_data.permission, ChckBxAutoLogin.IsChecked, hashed_enteredPassword);
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    Log.Trace(Message: "Authenticate login successful");
                    this.Close();
                }
                else {
                    LblIncorrectInput.Visibility = Visibility.Visible;
                }
            }
            else
                LblDatabaseError.Visibility = Visibility.Visible;
        }

        private (string salt, string password, string permission) GetUserInformations(string username) => Database.AuthenticateLogin(username);

        private void SetUserInformations(string username, string permission, bool? AutoLogin, string password) {
            Properties.loggedOnUser.Username = username;
            Properties.loggedOnUser.Permission = permission;

            if (AutoLogin == true)
                Properties.loggedOnUser.EnableAutoLogin(username, password);
        }
        #endregion
    }
}
