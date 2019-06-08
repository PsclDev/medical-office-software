using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace MoS.UserControls {
    /// <summary>
    /// Interaction logic for Logs.xaml
    /// </summary>
    public partial class Logs : UserControl {
        public Logs() {
            InitializeComponent();
        }

        private List<string> logs = new List<string>();

        #region Methods
        private void LoadLog(string filename) {
            try {
                logs.Clear();
                if (TxtBxLog.Text != "")
                    TxtBxLog.Clear();

                using (var reader = new StreamReader(filename)) {
                    while (!reader.EndOfStream) {
                        var line = reader.ReadLine();
                        logs.Add(line);
                    }
                }
            }
            catch (Exception exc) {
                CstmMsgBx.Error("A error occured while trying to load logs");
                Log.Error(Log.GetMethodName(), exc.ToString());
            }
        }
        #endregion

        #region Controls
        private void CmbBx_Spelling_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            string path;
            switch (CmbBx_Spelling.SelectedIndex) {
                case 0:
                    path = Log.GetPath("log.txt");
                    LoadLog(path);
                    foreach (string logentry in logs)
                        TxtBxLog.Text += $"{logentry} \n";
                    break;
                case 1:
                    path = Log.GetPath("log.err");
                    LoadLog(path);
                    foreach (string logentry in logs)
                        TxtBxLog.Text += $"{logentry} \n";
                    break;
            }
        }

        private void BtnReloadLog_Click(object sender, RoutedEventArgs e) => CmbBx_Spelling_SelectionChanged(CmbBx_Spelling, null);

        private void BtnClearLog_Click(object sender, RoutedEventArgs e) {
            try {
                string path = "";
                switch (CmbBx_Spelling.SelectedIndex) {
                    case 0:
                        path = Log.GetPath("log.txt");
                        break;
                    case 1:
                        path = Log.GetPath("log.err");
                        break;
                }

                System.IO.File.WriteAllText(path, string.Empty);
                TxtBxLog.Clear();
            }
            catch (Exception exc) {
                CstmMsgBx.Error("A error occured while trying to clear logs");
                Log.Error(Log.GetMethodName(), exc.ToString());
            }
        }
        #endregion
    }
}
