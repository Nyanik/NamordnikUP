using Microsoft.Win32;
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
    /// Логика взаимодействия для CreateOrUpdatePage.xaml
    /// </summary>
    public partial class CreateOrUpdatePage : Page
    {
        List<ProductType> PT = BaseClass.Base.ProductType.ToList();
        List<Material> M = BaseClass.Base.Material.ToList();
        List<ProductSale> PrSale = BaseClass.Base.ProductSale.ToList();
        Product PROD = new Product();
        string path;
        bool flag;
        bool artnum;
        bool del;
        public CreateOrUpdatePage()
        {                
            InitializeComponent();
            flag = true;
            CBTypePr.Items.Add("Выберите тип");            
            for (int i = 0; i < PT.Count; i++)
            {
                CBTypePr.Items.Add(PT[i].Title);
            }            
            CBTypePr.SelectedIndex = 0;            
            for (int i = 0; i < M.Count; i++)
            {
                CBMaterial.Items.Add(M[i].Title);
            }
           
        }
        public CreateOrUpdatePage(Product PrUpdate) 
        {
            InitializeComponent();
            if (flag == false)
            {
                BtnDel.Visibility = Visibility.Visible;
            }
            CBTypePr.Items.Add("Выберите тип");
            for (int i = 0; i < PT.Count; i++)
            {
                CBTypePr.Items.Add(PT[i].Title);
            }

            PROD = PrUpdate;
            CBTypePr.SelectedIndex = (int)PrUpdate.ProductTypeID;
            TBNamePr.Text = PrUpdate.Title;
            TBArct.Text = PrUpdate.ArticleNumber;
            TBKolvo.Text = Convert.ToString(PrUpdate.ProductionPersonCount);
            TBNumder.Text = Convert.ToString(PrUpdate.ProductionWorkshopNumber);
            TBMinCost.Text = Convert.ToString(PrUpdate.MinCostForAgent);
            path = PrUpdate.Image;
            if (path!=null)
            {
                BitmapImage BI = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
                ImagePr.Source = BI;
            }

        }

            private void BtnSetPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();  
            OFD.ShowDialog();  
            path = OFD.FileName;  
            int n = path.IndexOf("products");  
            path ="\\" + path.Substring(n);
            if (path != null)
            {
                BitmapImage BI = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
                ImagePr.Source = BI;
            }
        }

        private void BtnAddRed_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int IdType = 0;
                if (CBTypePr.SelectedIndex != 0)
                {
                    IdType = CBTypePr.SelectedIndex;
                }
                PROD.ProductTypeID = IdType;
                PROD.Title = TBNamePr.Text;
                PROD.ArticleNumber = TBArct.Text;
                PROD.ProductionPersonCount = Convert.ToInt32(TBKolvo.Text);
                PROD.ProductionWorkshopNumber = Convert.ToInt32(TBNumder.Text);
                PROD.MinCostForAgent = Convert.ToInt32(TBMinCost.Text);
                PROD.Image = path;

                //for (int i = 0; i <= PROD.ID; i++)
                //{
                //    if (TBArct.Text == PROD.ArticleNumber)
                //    {
                //        artnum = true;
                //    }
                //}
                if (artnum == true)
                {
                    MessageBox.Show("Данные не записаны:\nАртикул не может повторяться");
                }
                else if (Convert.ToInt32(TBMinCost.Text) < 0)
                {
                    MessageBox.Show("Данные не записаны:\nЦена не может быть отрицательной");
                }                
                else
                {
                    if (flag == true)
                    {
                        BaseClass.Base.Product.Add(PROD);
                    }
                    BaseClass.Base.SaveChanges();
                    MessageBox.Show("Данные записаны");
                    FrameClass.FrameMain.Navigate(new MainPage());
                }
                
            }
            catch
            {
                MessageBox.Show("Данные не записаны");
            }
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            
            foreach(ProductSale item in PrSale)
            {
                if(PROD.ID == item.ProductID)
                {
                    del = true;
                }
            }
            if(del == true)
            {
                MessageBox.Show("Данные не записаны:\nТовар продан агентом");
            }
            else
            {
                int ind = PROD.ID;
                Product PrDel = BaseClass.Base.Product.FirstOrDefault(y => y.ID == ind);
                BaseClass.Base.Product.Remove(PrDel);
                BaseClass.Base.SaveChanges();
                MessageBox.Show("Запись удалена");
                FrameClass.FrameMain.Navigate(new MainPage());
            }
            
        }
    }
}
