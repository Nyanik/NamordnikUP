using Namordnik.Class;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Namordnik.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        List<Product> ProductStart = BaseClass.Base.Product.ToList();
        List<Product> PRFilter;
        public MainPage()
        {
            InitializeComponent();
            LVProdMat.ItemsSource = ProductStart;
            PRFilter = ProductStart;
            CBSort.Items.Add("Наименование");
            CBSort.Items.Add("Номер цеха");
            CBSort.Items.Add("Мин стоимость");

            List<ProductType> PT = BaseClass.Base.ProductType.ToList();
            CBFilter.Items.Add("Все записи");
            for (int i = 0; i < PT.Count; i++)
            {
                CBFilter.Items.Add(PT[i].Title);
            }
            CBFilter.SelectedIndex = 0;
        }
        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<ProductMaterial> TC = BaseClass.Base.ProductMaterial.Where(x => x.ProductID == index).ToList();
            string str = "Материалы: ";
            foreach (ProductMaterial item in TC)
            {
                str += item.Material.Title + ", ";
            }
            if (str == "Материалы: ")
            {
                tb.Text = str.Substring(0) + "-";
            }
            else
            {
                tb.Text = str.Substring(0, str.Length - 2);
            }

        }
        private void TextBlock_Loaded_1(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<ProductMaterial> PT = BaseClass.Base.ProductMaterial.Where(x => x.ProductID == index).ToList();

            int cost = 0;

            foreach (ProductMaterial item in PT)
            {
                cost += (int)item.Material.Cost;
            }
            tb.Text = cost + " руб.";
        }


        private void Filter()
        {
            PRFilter = ProductStart;
            int index = CBFilter.SelectedIndex;
            if (index != 0)
            {
                PRFilter = ProductStart.Where(x => x.ProductTypeID == index).ToList();
            }
            else
            {
                PRFilter = ProductStart;
            }
            if (!string.IsNullOrWhiteSpace(TBFilter.Text))
            {
                PRFilter = PRFilter.Where(x => x.Title.ToLower().Contains(TBFilter.Text)).ToList();
            }
            LVProdMat.ItemsSource = PRFilter;
        }

        private void TBFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }
        private void CBFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }
        private void BtnSortUp_Click(object sender, RoutedEventArgs e)
        {
            if (CBSort.SelectedIndex == 0)
            {
                PRFilter.Sort((x, y) => x.Title.CompareTo(y.Title));
                LVProdMat.Items.Refresh();
            }
            if (CBSort.SelectedIndex == 1)
            {
                PRFilter.Sort((x, y) => x.ProductionWorkshopNumber.CompareTo(y.ProductionWorkshopNumber));
                LVProdMat.Items.Refresh();
            }
            if (CBSort.SelectedIndex == 2)
            {
                PRFilter.Sort((x, y) => x.MinCostForAgent.CompareTo(y.MinCostForAgent));
                LVProdMat.Items.Refresh();
            }

        }

        private void BtnSortDown_Click(object sender, RoutedEventArgs e)
        {
            if (CBSort.SelectedIndex == 0)
            {
                PRFilter.Sort((x, y) => x.Title.CompareTo(y.Title));
                PRFilter.Reverse();
                LVProdMat.Items.Refresh();
            }
            if (CBSort.SelectedIndex == 1)
            {
                PRFilter.Sort((x, y) => x.ProductionWorkshopNumber.CompareTo(y.ProductionWorkshopNumber));
                PRFilter.Reverse();
                LVProdMat.Items.Refresh();
            }
            if (CBSort.SelectedIndex == 2)
            {
                PRFilter.Sort((x, y) => x.MinCostForAgent.CompareTo(y.MinCostForAgent));
                PRFilter.Reverse();
                LVProdMat.Items.Refresh();
            }

        }
    }
}
