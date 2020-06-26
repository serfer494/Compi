using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    public class Nodo
    {
        public string lexema;
        public int token;
        public int renglon;
        public int prioridad;
        public Nodo sig = null;

        public Nodo(string lex, int tok, int ren)
        {
            lexema = lex;
            token = tok;
            renglon = ren;
            prioridad = 0;
        }
    }
}
