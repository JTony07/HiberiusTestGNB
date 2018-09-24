using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public class Transac
    {
        public int Id_Product { get; set; }
        public string Sku { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
    }
}
