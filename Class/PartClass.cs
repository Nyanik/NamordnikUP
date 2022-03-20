using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Namordnik
{
    public partial class Product
    {
        public string Header
        {
            get
            {
                return ProductType.Title + " | " + Title;
            }
        }
        public SolidColorBrush PrColor
        {
            get
            {
                SolidColorBrush bl = new SolidColorBrush(Color.FromRgb(176, 229, 253));
                SolidColorBrush lr = new SolidColorBrush(Color.FromRgb(250, 110, 110));
                if (MinCostForAgent > 2000)
                {
                    return lr;
                }
                else return bl;
        }
        }
    }
   
}
