using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shifrator
{
    public class Db
    {
        Database1Entities1 context = new Database1Entities1();
        public void Init()
        {
            string a = "а";
            var ch = from simbol in context.Symbols
                     where a = simbol.oldsymbol
                     select simbol;
            
            
        }
    }
}
