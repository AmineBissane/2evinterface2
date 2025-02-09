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

namespace _2evinterfaces
{
    public partial class mainpage : Window
    {
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();
        public mainpage()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            LoadProductAndCategoryList(); 
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Closing the app...");
            Application.Current.Shutdown();
        }


        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void LoadProductAndCategoryList()
        {
            var productsAndCategories = dbHelper.GetProductAndCategory();

            if (productsAndCategories != null && productsAndCategories.Count > 0)
            {
                ProductListBox.Items.Clear();
                CateListBox.Items.Clear();

                foreach (var item in productsAndCategories)
                {
                    ProductListBox.Items.Add(item.Item1); 
                    CateListBox.Items.Add(item.Item2);   
                }
            }
            else
            {
                ProductListBox.Items.Add("No products found.");
                CateListBox.Items.Add("No categories found.");
            }
        }
       

        

        private void GroupVectorButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window1 = new MainWindow();
            window1.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void Group6Button_Click(object sender, RoutedEventArgs e)
        {
            agregar agregarWindow = new agregar();
            agregarWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void Group7Button_Click(object sender, RoutedEventArgs e)
        {
            modificar agregarWindow = new modificar();
            agregarWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void Group8Button_Click(object sender, RoutedEventArgs e)
        {
            borar agregarWindow = new borar();
            agregarWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        
    

    }
}
