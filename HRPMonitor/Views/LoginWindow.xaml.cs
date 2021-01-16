using HRPMonitor.ICallers;
using HRPMSharedLibrary.Models;
using HRPMUILibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HRPMonitor.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        ILoginWindowCaller caller;
        List<Department> departments;
        List<User> users;
        public LoginWindow(ILoginWindowCaller callingWindow)
        {
            caller = callingWindow;
            InitializeComponent();
        }

        private async void Window_Initialized(object sender, EventArgs e)
        {
            var api = ApiHelper.GetApiHelper();
            departments = await api.GetDepartments();
            departmentsList.ItemsSource = departments;            
        }

        private async void departmentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var api = ApiHelper.GetApiHelper();
            users = await api.GetUsersByDepartment(((Department)departmentsList.SelectedItem).Id);
            usersList.ItemsSource = users;

        }

        private async void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            var api = ApiHelper.GetApiHelper();
            var user = (User)usersList.SelectedItem;
            user.Password = password.Password;
            var isValid = await api.GetUserPassword(user);
            if (isValid)
            {
                Close();
                caller.OnLogin(user);
            }
            else
            {
                MessageBox.Show("Wrong Password");
                return;
            }
            
        }
    }
}
