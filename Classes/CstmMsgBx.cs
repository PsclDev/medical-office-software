using System;
using System.Windows;
using System.Windows.Media;

namespace MoS {
    public static class CstmMsgBx {
        private static System.Windows.Forms.Timer Timer = new System.Windows.Forms.Timer();

        //Simple Anzeige in Application Grau, mit einstellbarer Nachricht und Individuellen Anzeigedauer
        public static void Show(string Text, int Interval = 2250) {
            try {
                //Suche das Fenster MainWindow
                foreach (Window window in Application.Current.Windows) {
                    if (window.GetType() == typeof(MainWindow)) {
                        Timer.Tick += new EventHandler(OnTimeEvent);
                        Timer.Interval = Interval;
                        Timer.Start();
                        //Zugriff auf das Element
                        (window as MainWindow).Snackbar_MessageBox.IsActive = true;
                        (window as MainWindow).Snackbar_MessageBox.Background = new BrushConverter().ConvertFromString("#26252E") as SolidColorBrush;
                        (window as MainWindow).Snackbar_MessageBox.Foreground = Brushes.White;
                        (window as MainWindow).SnackbarMessage.Content = Text;
                    }
                }
            }
            catch (Exception exc) {
                MessageBox.Show(exc.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Simple Anzeige in Rot, mit einstellbarer Nachricht und Individuellen Anzeigedauer
        public static void Error(string Text, int Interval = 2250) {
            try {
                foreach (Window window in Application.Current.Windows) {
                    if (window.GetType() == typeof(MainWindow)) {
                        Timer.Tick += new EventHandler(OnTimeEvent);
                        Timer.Interval = Interval;
                        Timer.Start();
                        (window as MainWindow).Snackbar_MessageBox.IsActive = true;
                        (window as MainWindow).Snackbar_MessageBox.Background = new BrushConverter().ConvertFromString("#DC143C") as SolidColorBrush;
                        (window as MainWindow).Snackbar_MessageBox.Foreground = Brushes.White;
                        (window as MainWindow).SnackbarMessage.Content = Text;
                    }
                }
            }
            catch (Exception exc) {
                MessageBox.Show(exc.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Individualisierbare Messagebox, Standardwerte entsprechen .Show
        public static void CustomMessage(string Text, string BackgroundColor = "#26252E", string ForegroundColor = "#fff", int Interval = 2250) {
            try {
                foreach (Window window in Application.Current.Windows) {
                    if (window.GetType() == typeof(MainWindow)) {
                        Timer.Tick += new EventHandler(OnTimeEvent);
                        Timer.Interval = Interval;
                        Timer.Start();
                        (window as MainWindow).Snackbar_MessageBox.IsActive = true;
                        (window as MainWindow).Snackbar_MessageBox.Background = new BrushConverter().ConvertFromString(BackgroundColor) as SolidColorBrush;
                        (window as MainWindow).Snackbar_MessageBox.Foreground = new BrushConverter().ConvertFromString(ForegroundColor) as SolidColorBrush;
                        (window as MainWindow).SnackbarMessage.Content = Text;
                    }
                }
            }
            catch (Exception exc) {
                MessageBox.Show(exc.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Timer Tick, wenn Zeit abgelaufen ist, zum ausblenden des Elemtens
        private static void OnTimeEvent(object source, EventArgs e) {
            try {
                foreach (Window window in Application.Current.Windows) {
                    if (window.GetType() == typeof(MainWindow))
                        (window as MainWindow).Snackbar_MessageBox.IsActive = false;
                }
                Timer.Stop();
            }
            catch (Exception exc) {
                MessageBox.Show(exc.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
