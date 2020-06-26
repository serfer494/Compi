using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    public class NodoPolish
    {
        public string lexema;
        public int token;
        public bool esUtilizada = false;
        public bool estaInicializada = false;
        public int prioridad;
        public string etiqueta;
        public NodoPolish sig = null;
        public NodoPolish brinco = null;

        public NodoPolish(string lex, int tok)
        {
            lexema = lex;
            token = tok;
            prioridad = 0;
            etiqueta = "";
        }
    }
}
