using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }

}
