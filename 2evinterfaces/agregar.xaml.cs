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
    /// <summary>
    /// Lógica de interacción para agregar.xaml
    /// </summary>
    public partial class agregar : Window
    {
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();
        public agregar()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            LoadProductAndCategoryList();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dbHelper = new DatabaseHelper();
            var categories = dbHelper.GetDistinctCategories();

            if (categories.Count > 0)
            {
                CateComboBox.ItemsSource = categories;
            }
            else
            {
                Console.WriteLine("No categories found to populate ComboBox.");
            }
        }
        private void RefreshCategoryComboBox()
        {
            var dbHelper = new DatabaseHelper();
            var categories = dbHelper.GetDistinctCategories();

            if (categories.Count > 0)
            {
                CateComboBox.ItemsSource = categories; 
                CateComboBox.SelectedIndex = -1; 
            }
            else
            {
                Console.WriteLine("No categories found to populate ComboBox.");
            }
        }


        private void AddProductButton_Click(object sender, MouseButtonEventArgs e)
        {
            var productName = ProductNameTextBox.Text; 
            var categoryName = CateComboBox.SelectedItem as string; 

            if (!string.IsNullOrEmpty(productName) && !string.IsNullOrEmpty(categoryName))
            {
                var dbHelper = new DatabaseHelper();
                bool isAdded = dbHelper.AddProduct(productName, categoryName);

                if (isAdded)
                {
                    MessageBox.Show("Product added successfully!");
                    RefreshCategoryComboBox();
                }
                else
                {
                    MessageBox.Show("Failed to add product.");
                }
            }
            else
            {
                MessageBox.Show("Please fill in both the product name and category.");
            }
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
        private void Group5Button_Click(object sender, RoutedEventArgs e)
        {
            mainpage window1 = new mainpage();
            window1.Show();
            this.Visibility = Visibility.Hidden;
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
