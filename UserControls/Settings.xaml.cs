using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MoS.UserControls {
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl {
        public Settings() {
            InitializeComponent();
            LoadSettings();
        }

        #region Variables
        private System.Windows.Forms.Timer badgeTimer = new System.Windows.Forms.Timer();
        private SolidColorBrush mintBrush = new SolidColorBrush(Color.FromRgb(113, 180, 159));
        private SolidColorBrush orangeBrush = new SolidColorBrush(Color.FromRgb(255, 193, 7));
        #endregion

        #region Methods
        private void LoadSettings() {
            TxtBxDatabasePath.Text = Properties.Settings.Default.DbPath;
        }

        private void SetSettings() {
            Properties.Settings.Default.DbPath = TxtBxDatabasePath.Text;
        }

        private void SettingsChanged() {
            BtnDiscardChanges.Visibility = Visibility.Visible;
            BtnSaveSettings.Background = orangeBrush;
            SaveBadge.Badge = "unsaved Changes";
        }

        private void SettingsNotChanged() {
            BtnDiscardChanges.Visibility = Visibility.Hidden;
            BtnSaveSettings.Background = mintBrush;
            SaveBadge.Badge = "";
        }
        #endregion

        #region Controls
        private void BtnChangePassword_Click(object sender, RoutedEventArgs e) {
            string salt = Crypto.CreateSalt(36);
            string hashedPassword = Crypto.HashToSHA256(TxtBxResetPassword.Text, salt);
            var id_user = Database.GetUserID(Properties.loggedOnUser.Username);

            string sql = $"UPDATE employee_login set salt = '{salt}', password = '{hashedPassword}' where id_user = {id_user}";
            int returnCode = Database.RunSQL(sql);

            if (returnCode == -1) {
                while (returnCode == -1) {
                    salt = Crypto.CreateSalt(36);
                    hashedPassword = Crypto.HashToSHA256(TxtBxResetPassword.Text, salt);
                    sql = $"UPDATE employee_login set salt = '{salt}', password = '{hashedPassword}' where id_user = {id_user}";
                    returnCode = Database.RunSQL(sql);
                }
            }
        }

        private void BtnDiscardChanges_Click(object sender, RoutedEventArgs e) {
            LoadSettings();
        }

        private void BtnSaveSettings_Click(object sender, RoutedEventArgs e) {
            BtnSaveSettings.Background = mintBrush;
            SaveBadge.Badge = "saved changes";
            badgeTimer.Tick += new EventHandler(OnTimeEvent);
            badgeTimer.Interval = 2250;
            badgeTimer.Start();

            BtnDiscardChanges.Visibility = Visibility.Hidden;
            SetSettings();
        }

        private void BtnDefaultSettings_Click(object sender, RoutedEventArgs e) {
            TxtBxDatabasePath.Text = "";
        }
        #endregion

        #region Events
        private void OnTimeEvent(object source, EventArgs e) {
            SaveBadge.Badge = "";
            badgeTimer.Stop();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
            switch (((TextBox)sender).Name) {
                case "TxtBxDatabasePath": {
                        if (TxtBxDatabasePath.Text != Properties.Settings.Default.DbPath) {
                            SettingsChanged();
                        }
                        break;
                    }
                case "TxtBxResetPassword":
                case "TxtBxRepeatPassword": {
                        if (TxtBxResetPassword.Text != "")
                            TxtBxRepeatPassword.Visibility = Visibility.Visible;

                        if (TxtBxResetPassword.Text == TxtBxRepeatPassword.Text) {
                            BtnChangePassword.Visibility = Visibility.Visible;
                            TxtBlckPasswordMatch.Visibility = Visibility.Hidden;
                            TxtBxRepeatPassword.Foreground = Brushes.White;
                        }

                        else if (TxtBxResetPassword.Text != TxtBxRepeatPassword.Text) {
                            BtnChangePassword.Visibility = Visibility.Hidden;
                            TxtBlckPasswordMatch.Visibility = Visibility.Visible;
                            TxtBxRepeatPassword.Foreground = Brushes.Red;
                        }
                        break;
                    }
            }
        }
        #endregion
    }
}
