using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace Compi
{
   public class Ensamblador2
    {
        string ensamblador = ";/n Inicio Encabezado\nINCLUDE Macros.mac \nDOSSEG \n.MODEL SMALL \nSTACK 100h \n.DATA \n";
        string operador1, operador2 ;
        public NodoVariable variable = null, cabezaVsintaxis, cola;
        NodoPolish colaPolish, cabezaPsintaxis;
        List<string> ListaPolish = new List<string>();
        List<string> ListaApuntador = new List<string>();
        //Sintactico pila = new Sintactico();
        Lexico pila2 = new Lexico();
        Stack<string> auxPilaPolish = new Stack<string>();
        Stack<string> auxPilaPolish2 = new Stack<string>();
        bool flag = true;
        private Lexico lexi;
        private Sintactico sint;

        public Ensamblador2(NodoVariable cabezaV, NodoPolish cabezaP)
        {
            cabezaVsintaxis = cabezaV;
            cabezaPsintaxis = cabezaP;
        }

        public Ensamblador2(Lexico lexi, Sintactico sint)
        {
            this.lexi = lexi;
            this.sint = sint;
        }

        public void Ensamblation()
        {
            InsertarListaPila();
            using (System.IO.StreamWriter outputfile = new System.IO.StreamWriter("C:\\ensamblador\\compi.asm"))
            {

                ensamblador += "\tRESULTADO dw ? \n \tRESULTADOrelacional dw ? \n";
                DefinirVariables();


                while (auxPilaPolish.Count != 0)
                {
                    if (auxPilaPolish.Peek().Equals("") || auxPilaPolish.Peek().Equals("+") || auxPilaPolish.Peek().Equals("-") || auxPilaPolish.Peek().Equals("*") || auxPilaPolish.Peek().Equals("=")
                    || auxPilaPolish.Peek().Equals("<") || auxPilaPolish.Peek().Equals(">") || auxPilaPolish.Peek().Equals("==") || auxPilaPolish.Peek().Equals(">=")
                    || auxPilaPolish.Peek().Equals("<=") || auxPilaPolish.Peek().Equals("!=") || auxPilaPolish.Peek().Equals("/") || auxPilaPolish.Peek().Equals("BRF-A")
                    || auxPilaPolish.Peek().Equals("BRI-B") || auxPilaPolish.Peek().Equals("BRF-C") || auxPilaPolish.Peek().Equals("BRI-D")
                    || auxPilaPolish.Peek().Equals("BRI-F") || auxPilaPolish.Peek().Equals("BRF-G") || auxPilaPolish.Peek().Equals("BRI-H"))
                    {
                        auxPilaPolish.Pop();
                    }
                    else
                    {
                        auxPilaPolish2.Push(auxPilaPolish.Pop());
                    }
                }

                ensamblador += "\n.CODE \n.386 \nBEGIN: \n\tMOV AX, @DATA \n \tMOV DS, AX" +
                    "\n CALL COMPI\n \tMOV AX, 4C00H\n \tINT 21H\n COMPI PROC\n";

                AsignarValor();


                while (cola != null)
                {
                    ensamblador += "\n \tWRITENUM " + cola.lexema;
                    cola = cola.sig;
                }

                ensamblador += "\n \t\tret \nCOMPI ENDP \nEND BEGIN";
                outputfile.WriteLine(ensamblador);
            }
        }

        public void DefinirVariables()
        {
            cola = cabezaVsintaxis;
            while (cola != null)
            {
                ensamblador += "\t" + cola.lexema + " dw ?\n";
                cola = cola.sig;
            }
            cola = cabezaVsintaxis;
        }

        public void AsignarValor()
        {

            for (int i = 0; i < ListaPolish.Count; i++)
            {
                switch (ListaPolish[i])
                {
                    case "+":
                        operador1 = auxPilaPolish2.Pop();
                        operador2 = auxPilaPolish2.Pop();
                        ensamblador += "\t\tSUMAR " + operador2 + "," + auxPilaPolish2.Pop() + "," + operador1 + "\n";
                        flag = false;
                        i++;
                        break;

                    case "-":
                        operador1 = auxPilaPolish2.Pop();
                        operador2 = auxPilaPolish2.Pop();
                        ensamblador += "\t\tRESTA " + operador2 + "," + auxPilaPolish2.Pop() + "," + operador1 + "\n";
                        flag = false;
                        i++;
                        break;

                    case "*":
                        operador1 = auxPilaPolish2.Pop();
                        operador2 = auxPilaPolish2.Pop();
                        ensamblador += "\t\tMULTI " + operador2 + "," + auxPilaPolish2.Pop() + "," + operador1 + "\n";
                        flag = false;
                        i++;
                        break;
                    case "/":
                        operador1 = auxPilaPolish2.Pop();
                        operador2 = auxPilaPolish2.Pop();
                        ensamblador += "\t\tDIVIDE " + operador2 + "," + auxPilaPolish2.Pop() + "," + operador1 + "\n";
                        flag = false;
                        i++;
                        break;
                    case "==":
                        operador1 = auxPilaPolish2.Pop();
                        operador2 = auxPilaPolish2.Pop();
                        ensamblador += "\t\tI_IGUAL " + operador1 + "," + operador2 + "\n";
                        break;
                    case "!=":
                        operador1 = auxPilaPolish2.Pop();
                        operador2 = auxPilaPolish2.Pop();
                        ensamblador += "\t\tI_DIFERENTES " + operador1 + "," + operador2 + "\n";
                        break;
                    case "<":
                        operador1 = auxPilaPolish2.Pop();
                        operador2 = auxPilaPolish2.Pop();
                        ensamblador += "\t\tI_MENOR " + operador1 + "," + operador2 + "\n";
                        break;
                    case ">":
                        operador1 = auxPilaPolish2.Pop();
                        operador2 = auxPilaPolish2.Pop();
                        ensamblador += "\t\tI_MAYOR " + operador1 + "," + operador2 + "\n";
                        break;
                    case "<=":
                        operador1 = auxPilaPolish2.Pop();
                        operador2 = auxPilaPolish2.Pop();
                        ensamblador += "\t\tI_MENORIGUAL " + operador1 + "," + operador2 + "\n";
                        break;
                    case ">=":
                        operador1 = auxPilaPolish2.Pop();
                        operador2 = auxPilaPolish2.Pop();
                        ensamblador += "\t\tI_MAYORIGUAL " + operador1 + "," + operador2 + "\n";
                        break;


                    case "BRF-A":
                        ensamblador += "\t\tJF " + "RESULTADO," + "A1" + "\n";
                        break;
                    case "BRF-C":
                        ensamblador += "\t\tJF " + "RESULTADO," + "C1" + "\n";
                        break;
                    case "BRI-D":
                        ensamblador += "\t\tJMP D1\n";
                        break;
                }

                if (ListaPolish[i] == "=" && flag == true)
                {
                    operador1 = auxPilaPolish2.Pop();
                    operador2 = auxPilaPolish2.Pop();
                    ensamblador += "\t\tI_ASIGNAR " + operador1 + ", " + operador2 + "\n";
                }
                flag = true;
                if (ListaApuntador[i] != null)
                {


                    switch (ListaApuntador[i])
                    {
                        case "A":
                            ensamblador += "\t" + "A1" + ":\n";
                            break;
                        case "B":
                            ensamblador += "\t" + "B1" + ":\n";
                            break;
                        case "C":
                            ensamblador += "\t" + "C1" + ":\n";
                            break;
                        case "D":
                            ensamblador += "\t" + "D1" + ":\n";
                            break;

                    }

                }
            }

        }
        public void InsertarListaPila()
        {
            colaPolish = cabezaPsintaxis;
            while (colaPolish != null)
            {   
                auxPilaPolish.Push(colaPolish.lexema);
                ListaPolish.Add(colaPolish.lexema);
                ListaApuntador.Add(colaPolish.etiqueta); ///etiqueta duda
                colaPolish = colaPolish.sig;
            }
            colaPolish = cabezaPsintaxis;
        }
    }
}




