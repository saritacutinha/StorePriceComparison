using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorePriceComparison.Data
{
    public interface IPriceData
    {
        public void UpdatePriceValue(int product, int store, float productPrice);
    }
}
