using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    public class NodoVariable
    {
        public string lexema;
        public int token;
        public bool esUtilizada = false;
        public bool estaInicializada = false;
        public int prioridad;
        public NodoVariable sig = null;

        public NodoVariable(string lex, int tok)
        {
            lexema = lex;
            token = tok;
            prioridad = 0;
        }
    }
}
