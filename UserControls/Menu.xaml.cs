using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MoS.UserControls {
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl {
        public Menu() {
            InitializeComponent();
            LoadMenu();
        }

        private List<Objects.MenuItem> menuList = new List<Objects.MenuItem>();

        private void LoadMenu() {
            menuList.Add(new Objects.MenuItem("FolderUserOutline", "Patient details"));
            menuList.Add(new Objects.MenuItem("UserSettingsVariant", "Patientlist"));

            if (Properties.loggedOnUser.Permission == "doctor")
                menuList.Add(new Objects.MenuItem("UserGroupOutline", "User Management"));

            ListViewMenu.ItemsSource = menuList;
        }

        private void MoveCursorMenu(int index) {
            GridCursor.Visibility = Visibility.Visible;
            GridCursor.Margin = new Thickness(0, (60 * index), 0, 0);
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            foreach (Window window in Application.Current.Windows) {
                if (window.GetType() == typeof(MainWindow)) {
                    try {
                        RoutedEventArgs newEventArgs = new RoutedEventArgs(Button.ClickEvent);
                        int index = ListViewMenu.SelectedIndex;
                        MoveCursorMenu(index);

                        switch (index) {
                            case 0:
                                (window as MainWindow).Grid_Main_UC.Children.Clear();
                                (window as MainWindow).Grid_Main_UC.Children.Add((window as MainWindow).UC_PatientDetails);

                                if ((window as MainWindow).MenuOpen == true) {
                                    (window as MainWindow).ButtonCloseMenu.RaiseEvent(newEventArgs);
                                    (window as MainWindow).MenuOpen = false;
                                }
                                break;
                            case 1:
                                (window as MainWindow).Grid_Main_UC.Children.Clear();
                                (window as MainWindow).Grid_Main_UC.Children.Add((window as MainWindow).UC_PatientList);

                                if ((window as MainWindow).MenuOpen == true) {
                                    (window as MainWindow).ButtonCloseMenu.RaiseEvent(newEventArgs);
                                    (window as MainWindow).MenuOpen = false;
                                }
                                break;
                            case 2:
                                (window as MainWindow).Grid_Main_UC.Children.Clear();
                                (window as MainWindow).Grid_Main_UC.Children.Add((window as MainWindow).UC_UserManagement);

                                if ((window as MainWindow).MenuOpen == true) {
                                    (window as MainWindow).ButtonCloseMenu.RaiseEvent(newEventArgs);
                                    (window as MainWindow).MenuOpen = false;
                                }
                                break;
                        }
                    }
                    catch (Exception exc) {
                        CstmMsgBx.Error("A error occured in ListView_Menu_SelectionChanged");
                        Log.Error(Log.GetMethodName(), exc.ToString());
                    }
                }
            }
        }
    }
}
