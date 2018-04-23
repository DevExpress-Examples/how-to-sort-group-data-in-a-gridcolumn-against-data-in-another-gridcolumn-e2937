using System;
using System.Windows;
using DevExpress.Data.Linq;
using System.Collections.Generic;
using DevExpress.Data;
using System.ComponentModel;

namespace DXGrid_EF4_ServerMode {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            //You can use this demo code to generate a large data set, if currently, you don't have a large database for testing.
            NorthwindEntities dataContext = new NorthwindEntities();

            LinqServerModeSource source = new LinqServerModeSource()
            {
                ElementType = typeof(Product),
                KeyExpression = "ProductID",
                QueryableSource = dataContext.Products
            };

            grid.ItemsSource = source;
            grid.Columns.Remove(grid.Columns["SupplierID"]);
            grid.Columns["ProductName"].SortIndex = 0;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            grid.AutoPopulateColumns = false;
            Dictionary<string, string> replacements = new Dictionary<string, string>();
            replacements.Add("ProductName", "SupplierID");
            grid.ItemsSource = new MyListSource(grid.ItemsSource as IListSource, replacements);

        }
    }
}