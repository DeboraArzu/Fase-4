using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fase_3_Compiladores__tablas_
{
    class Dtoken
    {
        public string key;
        public string contenido;
        public string check;

        public Dtoken(string k, string con, string chk)
        {
            this.key = k;
            this.contenido = con;
            this.check = chk;
        }
    }
}
