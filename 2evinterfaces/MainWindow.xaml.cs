using System;
using System.Windows;
using System.Windows.Navigation;

namespace _2evinterfaces
{
    public partial class MainWindow : Window
    {
        private DatabaseHelper dbHelper;
        public MainWindow()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            closeButton.Click += CloseButton_Click;
            minimizeButton.Click += MinimizeButton_Click;
            dbHelper = new DatabaseHelper();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void loginbtn(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                bool isValidUser = dbHelper.VerifyUser(username, password);

                if (isValidUser)
                {
                    MessageBox.Show("Login successful!");
                    mainpage window1 = new mainpage();
                    window1.Show();
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("Invalid username or password. Please try again.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter both username and password.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
