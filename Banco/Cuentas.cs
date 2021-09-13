using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco
{
   public class Cuentas
    {
        public string user { get; set; }
        public string account { get; set; }
        public decimal debt { get; set; }
        public string message { get; set; }
    }

    public class Pagos
    {
         public string account { get; set; }
        public decimal debt { get; set; }

    }

    public class sentPayment
    {
        public string account { get; set; }
        public decimal paid { get; set; }

    }



}
