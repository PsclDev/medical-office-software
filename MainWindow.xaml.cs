using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MoS {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            Properties.AppContent.Load();
            Log.Trace("Initalize MainWindow");
            GridMenuList.Children.Add(new UserControls.Menu());
        }

        #region Variables
        public bool MenuOpen = false;
        public UserControl UC_PatientDetails = new UserControls.PatientDetails();
        public UserControl UC_PatientList = new UserControls.PatientList();
        public UserControl UC_UserManagement = new UserControls.EmployeeManagement();
        public UserControl UC_Settings = new UserControls.Settings();
        public UserControl UC_Logs = new UserControls.Logs();
        #endregion

        #region Header
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void BtnLogs_Click(object sender, RoutedEventArgs e) {
            Grid_Main_UC.Children.Clear();
            Grid_Main_UC.Children.Add(UC_Logs);
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e) {
            Grid_Main_UC.Children.Clear();
            Grid_Main_UC.Children.Add(UC_Settings);
        }

        private void BtnPLogout_Click(object sender, RoutedEventArgs e) {
            Properties.loggedOnUser.DisableAutoLogin();
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Close();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void BtnPowerOff_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
        #endregion

        #region Methods
        public void ButtonOpenMenu_Click(object sender, RoutedEventArgs e) {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
            MenuOpen = true;
        }

        public void ButtonCloseMenu_Click(object sender, RoutedEventArgs e) {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            MenuOpen = false;
        }
        #endregion
    }
}
