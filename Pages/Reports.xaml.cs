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
using Word = Microsoft.Office.Interop.Word;

namespace Namordnik.Pages
{
    /// <summary>
    /// Логика взаимодействия для Reports.xaml
    /// </summary>
    public partial class Reports : Page
    {
        public Reports()
        {
            InitializeComponent();
        }        

        private void BtnRepProd_Click(object sender, RoutedEventArgs e)
        {
            List<Product> PR = BaseClass.Base.Product.ToList(); 
            
            var application = new Word.Application();

            Word.Document document = application.Documents.Add();

            foreach(var product  in PR)
            {
                Word.Paragraph prodParagraf = document.Paragraphs.Add();
                Word.Range prodTitleRange = prodParagraf.Range;
                prodTitleRange.Text = product.Title;

                prodTitleRange.InsertParagraphAfter();

                Word.Paragraph paragraphTable = document.Paragraphs.Add();
                Word.Range rangeTable = paragraphTable.Range;
                Word.Table paymentsTable = document.Tables.Add(rangeTable, 2,3);
                paymentsTable.Borders.InsideLineStyle = paymentsTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                paymentsTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                Word.Range cellRange;

                cellRange = paymentsTable.Cell(1, 1).Range;
                cellRange.Text = "Изображение продукции";
                cellRange = paymentsTable.Cell(1, 2).Range;
                cellRange.Text = "Тип продукции";
                cellRange = paymentsTable.Cell(1, 3).Range;
                cellRange.Text = "Артикул";              

                paymentsTable.Rows[1].Range.Bold = 1;
                paymentsTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                string path = "/Resources/picture.png";
                cellRange = paymentsTable.Cell(2, 1).Range;
                    if (product.Image != null)
                    {
                        Word.InlineShape ImageShape = cellRange.InlineShapes.AddPicture(AppDomain.CurrentDomain.BaseDirectory + "..\\.." + product.Image);
                        cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        ImageShape.Width = ImageShape.Height = 50;
                    }
                    else
                    {
                        Word.InlineShape ImageShape = cellRange.InlineShapes.AddPicture(AppDomain.CurrentDomain.BaseDirectory + "..\\.." + path);
                        cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        ImageShape.Width = ImageShape.Height = 50;
                    }
                
                cellRange = paymentsTable.Cell(2, 2).Range;
                    cellRange.Text = product.ProductType.Title;
                    cellRange = paymentsTable.Cell(2, 3).Range;
                    cellRange.Text = product.ArticleNumber;
                
                
                
            }
            application.Visible = true;

            //document.SaveAs2(@"\\main\RDP\43П\мелузовна\Desktop\Практика\Test.docx");
        }

        private void BtnRepMat_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
