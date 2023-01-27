using SaveEyes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SaveEyes.Windows
{
    /// <summary>
    /// Interaction logic for ProductCountWindow.xaml
    /// </summary>
    public partial class ProductCountWindow : Window
    {
        public ProductSale ProductSale { get; set; }
        public ProductCountWindow(ProductSale productSale)
        {
            ProductSale = productSale;
            Title = productSale.Product.Title;
            InitializeComponent();
            DataContext = ProductSale;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void tbCount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
