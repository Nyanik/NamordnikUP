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

namespace Namordnik.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChangedWindow.xaml
    /// </summary>
    public partial class ChangedWindow : Window
    {
        public ChangedWindow(int a)
        {
            InitializeComponent();
            SrCost.Text = a.ToString();
        }

        private void BTChange_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public int srcost
        {
            get
            {
                return Convert.ToInt32(SrCost.Text);
            }
        }
    }
}
