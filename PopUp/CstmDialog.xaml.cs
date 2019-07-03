using System.Windows;
using System.Windows.Media;

namespace MoS.PopUp {
    /// <summary>
    /// Interaction logic for CstmDialog.xaml
    /// </summary>
    public partial class CstmDialog : Window {
        public CstmDialog(string hexColor, string Header, string Text, string Button1Text, string Button2Text) {
            InitializeComponent();
            LoadProperties(hexColor, Header, Text, Button1Text, Button2Text);
        }

        private void LoadProperties(string hexColor, string Header, string Text, string Button1Text, string Button2Text) {
            Window.Background = new BrushConverter().ConvertFromString($"{hexColor}") as SolidColorBrush;
            TxtBlck_Header.Text = Header;
            TxtBlck_Text.Text = Text;
            Btn_Ok.Content = Button1Text;
            Btn_Cancel.Content = Button2Text;
        }

        private void Btn_Ok_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = true;
            this.Close();
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
            this.Close();
        }
    }
}
