using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Compi
{
    public class Sintactico
    {
        bool bloquefor = false;
        Nodo apuntador = new Nodo("", 1, 1);
        NodoVariable identificador;
        NodoVariable lista;
        NodoPolish listaPolish;
        int tipo1, tipo2;
        NodoVariable a = new NodoVariable("", 0);
        NodoVariable b = new NodoVariable("", 0);
        public NodoVariable listaCopia = null;
        public NodoPolish cabezaPolishCopia = null;
        public NodoPolish pPolishCopia = null;
        public NodoPolish listaPolishCopia = null;
        public NodoVariable pLista = null;
        public NodoVariable cabezaLista = null;
        public NodoVariable cabezaListaCopia = null;
        public NodoVariable pListaCopia = null;
        Stack pilaListaCopia = new Stack();
        public NodoPolish pListaPolish = null;
        public NodoPolish cabezaListaPolish = null;
        public Nodo auxiliarPila = null;
        Stack pilaTipos = new Stack();
        Stack pilaOperadores = new Stack();
        Stack pilaTiposPolish = new Stack();
        Stack pilaOperadoresPolish = new Stack();
        Stack pilaEtiquetasPolish = new Stack();
        Stack pilaFuncionPolish = new Stack();
        Stack pilaAPolish = new Stack();
        Stack pilaCPolish = new Stack();
        Stack pilaGPolish = new Stack();
        Stack pilaFPolish = new Stack();
        Stack pilaEtiquetaAPolish = new Stack();
        Stack pilaEtiquetaBPolish = new Stack();
        Stack pilaEtiquetaCPolish = new Stack();
        Stack pilaEtiquetaDPolish = new Stack();
        Stack pilaEtiquetaGPolish = new Stack();
        string etiqueta = "";
        int contadorA = 0;
        int contadorB = 0;
        int contadorC = 0;
        int contadorD = 0;
        int contadorG = 0;
        int contadorH = 0;
        int contadorF = 0;
        public int contadorPolish = 0;
        Nodo auxiliar = new Nodo("", 1, 1);
        public bool variableExiste = false;
        public NodoVariable cabeza = null;
        public NodoVariable p = null;
        public string error;
        public string errorSemantico;
        public int tokenVariable;
        public string lexemaVariable;
        bool bloqueDoWhile = false;
        bool bloqueLlaves = false;
        public Sintactico(Nodo pointer)
        {
            apuntador = pointer;
            auxiliar = pointer;
        }

        public void ProcSintactico()
        {
            if (apuntador.token == 200)
            {
                apuntador = apuntador.sig;
                if(apuntador.token == 124)
                {
                    apuntador = apuntador.sig;
                    if(apuntador.token == 125)
                    {
                        apuntador = apuntador.sig;
                        if(apuntador.token == 126)
                        {
                            apuntador = apuntador.sig;
                            if(apuntador.token != 127)
                            {
                                Declaraciones();
                                if (error != null || errorSemantico != null)
                                {
                                    return;
                                }
                                ListaDeProposiciones();
                                if (error != null || errorSemantico != null)
                                {
                                    return;
                                }
                                if (apuntador != null)
                                {
                                    if (apuntador.token == 127)
                                    {
                                        ComprobarVariableUtilizada();
                                        if(etiqueta != "")
                                        {
                                            NodoVariable auxiliar = new NodoVariable("", 0);
                                            AgregarAListaPolish(auxiliar);
                                        }
                                        TerminarPolish();
                                        return;
                                    }
                                    else if(error == null)
                                    {
                                        error = "Error: se esperaba un } en el renglon " + apuntador.renglon;
                                        return;
                                    }
                                }
                                else if (error == null)
                                {
                                    
                                    error = "Error: Se espera un } al final del documento";
                                    
                                    return;
                                }
                            }
                        }
                        else if (error == null)
                        {
                            
                            error = "Error: Se esperaba un { despues de )";
                            
                            return;
                        }
                    }
                    else if (error == null)
                    {
                        
                        error = "Error: Se esperaba un ) despues de ( en el renglon " + apuntador.renglon;
                        
                        return;
                    }
                }
                else if (error == null)
                {
                    error = "Error: Se esperaba un ( despues de main";
                    return;
                }
            }
            else if(error == null)
            {
                error = "Error: Se esperaba un main al principio del archivo";
                return;
            }
        }

        public void Declaraciones()
        {
            if(apuntador.token == 201 || apuntador.token == 202 || apuntador.token == 203 || apuntador.token == 213
                 || apuntador.token == 214)
            {
                tokenVariable = apuntador.token;
                apuntador = apuntador.sig;
                if(apuntador.token == 119)
                {
                    BuscarIdentificador(apuntador.lexema, tokenVariable);
                    if(error != null || errorSemantico != null)
                    {
                        return;
                    }
                    apuntador = apuntador.sig;
                    while(apuntador.token == 128)
                    {
                        apuntador = apuntador.sig;
                        if(apuntador.token == 119)
                        {
                            BuscarIdentificador(apuntador.lexema, tokenVariable);
                            if (error != null || errorSemantico != null)
                            {
                                return;
                            }
                            apuntador = apuntador.sig;
                            continue;
                        }
                        else if (error == null)
                        {
                            error = "Error: Se esperaba un identificador en el renglon " + apuntador.renglon;
                            return;
                        }
                    }
                    if(apuntador.token == 129)
                    {
                        apuntador = apuntador.sig;
                        if(apuntador.token >= 201 && apuntador.token <= 203 || apuntador.token == 213 || 
                            apuntador.token == 214)
                        {
                            Declaraciones();
                            if (error != null || errorSemantico != null)
                            {
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (error == null)
                    {
                        error = "Error: Se esperaba un ; en el renglon " + apuntador.renglon;
                        return;
                    }
                }
                else if (error == null)
                {
                    error = "Error: Se esperaba un identificador en el renglon " + apuntador.renglon;
                    return;
                }
            }
            else if (error == null)
            {
                error = "Error: Se esperaba un tipo de dato en el renglon " + apuntador.renglon;
                return;
            }
        }

        public void Identificador()
        {
            if(apuntador.token == 119)
            {
                apuntador = apuntador.sig;
                if (apuntador.token == 128)
                {
                    apuntador = apuntador.sig;
                    Identificador();
                    if (error != null || errorSemantico != null)
                    {
                        return;
                    }
                }
            }
            else if (error == null)
            {
                error = "Error: Se esperaba un identificador en el renglon " + apuntador.renglon;
                return;
            }
        }

        public void ListaDeProposiciones()
        {
            if(apuntador != null)
            {
                Proposiciones();
                if (error != null || errorSemantico != null)
                {
                    return;
                }
            }
        }

        public void Proposiciones()
        {
            if(apuntador.token == 119)
            {
                lexemaVariable = apuntador.lexema;
                BuscarSiExisteIdentificador(apuntador.lexema);
                if(error != null || errorSemantico != null)
                {
                    return;
                }
                if(apuntador.token == 119)
                {
                    
                        CompatibilidadDeTipos(BuscarIdentificador(lexemaVariable));

                        if(bloquefor == false) Polish(BuscarIdentificador(lexemaVariable));
                    
                    
                }
                else
                {
                    NodoVariable auxiliar = new NodoVariable(apuntador.lexema, apuntador.token);
                    CompatibilidadDeTipos(auxiliar);
                }
                apuntador = apuntador.sig;
                if(apuntador.token == 109 || apuntador.token == 110)
                {
                    if (error != null || errorSemantico != null)
                    {
                        return;
                    }
                    NodoVariable auxiliar = new NodoVariable(apuntador.lexema, apuntador.token);
                    CompatibilidadDeTipos(auxiliar);
                    if(bloquefor == false) Polish(auxiliar);
                    if (error != null || errorSemantico != null)
                    {
                        return;
                    }
                    apuntador = apuntador.sig;
                    if(bloquefor == true)
                    {
                        return;
                    }
                    if(apuntador.token == 129)
                    {
                        TerminarListaTipos();
                        VaciarPilaOperadoresPolish();
                        if (error != null || errorSemantico != null)
                        {
                            return;
                        }
                        
                        apuntador = apuntador.sig;
                        ListaDeProposiciones();
                        if (error != null || errorSemantico != null)
                        {
                            return;
                        }
                    }
                    else if(error == null)
                    {
                        error = "Error: Se esperaba un ; en el renglon " + apuntador.renglon;
                        return;
                    }

                }
                else
                {
                    Asignacion();
                    if (error != null || errorSemantico != null)
                    {
                        return;
                    }
                    Expresion();
                    if (error != null || errorSemantico != null)
                    {
                        return;
                    }
                    if (apuntador.token == 129)
                    {
                        if (bloquefor == true) return;
                        TerminarListaTipos();
                        
                        VaciarPilaOperadoresPolish();
                        if (error != null || errorSemantico != null)
                        {
                            return;
                        }
                        InicializarVariable(lexemaVariable);
                        //CompatibilidadDeTipos(apuntador);
                        apuntador = apuntador.sig;
                        ListaDeProposiciones();
                        if (error != null || errorSemantico != null)
                        {
                            return;
                        }
                    }
                    else if (error == null)
                    {
                        error = "Se esperaba un ; en el renglon " + apuntador.renglon;
                        return;
                    }
                }
            }else if(apuntador.token == 109 || apuntador.token == 110)
            {
                NodoVariable auxiliar = new NodoVariable(apuntador.lexema, apuntador.token);
                CompatibilidadDeTipos(auxiliar);
                Polish(auxiliar);
                
                apuntador = apuntador.sig;
                if(apuntador.token == 119)
                {
                    BuscarSiExisteIdentificador(apuntador.lexema);
                    if (error != null || errorSemantico != null)
                    {
                        return;
                    }

                    CompatibilidadDeTipos(BuscarIdentificador(apuntador.lexema));
                    Polish(BuscarIdentificador(apuntador.lexema));
                    
                    apuntador = apuntador.sig;
                    if(apuntador.token == 129)
                    {
                        TerminarListaTipos();
                        VaciarPilaOperadoresPolish();
                        if (error != null || errorSemantico != null)
                        {
                            return;
                        }
                        apuntador = apuntador.sig;
                        ListaDeProposiciones();
                        if (error != null || errorSemantico != null)
                        {
                            return;
                        }
                    }
                    else if (error == null)
                    {
                        error = "Error: Se esperaba un ; en el renglon " + apuntador.renglon;
                        return;
                    }
                }
                else if (error == null)
                {
                    error = "Error: Se esperaba un identificador en el renglon " + apuntador.renglon;
                    return;
                }
            }else if (apuntador.token == 204)
            {
                apuntador = apuntador.sig;
                bloqueDoWhile = true;
                contadorF = contadorF + 1;
                pilaFPolish.Push(contadorF);
                if (etiqueta == "")
                {
                    etiqueta = "F" + (contadorF).ToString();
                }
                else
                {
                    etiqueta = etiqueta + " F" + (contadorF).ToString();
                }
                Proposiciones();
                if (error != null || errorSemantico != null)
                {
                    return;
                }
                if (apuntador.token == 205)
                {
                    apuntador = apuntador.sig;
                    if(apuntador.token == 124)
                    {
                        apuntador = apuntador.sig;
                        Expresion();
                        TerminarListaTipos();
                        VaciarPilaOperadoresPolish();
                        if (error != null || errorSemantico != null)
                        {
                            return;
                        }
                        PolishDoBrincoF();
                        if (apuntador.token == 125)
                        {
                            apuntador = apuntador.sig;
                            if(apuntador.token == 129)
                            {
                                apuntador = apuntador.sig;
                                bloqueDoWhile = false;
                                ListaDeProposiciones();
                                if (error != null || errorSemantico != null)
                                {
                                    return;
                                }
                            }
                            else if (error == null)
                            {
                                error = "Error: Se esperaba un ; en el renglon " + apuntador.renglon;
                                return;
                            }
                        }
                        else if (error == null)
                        {
                            error = "Error: Se esperaba un ) en el renglon " + apuntador.renglon;
                            return;
                        }
                    }
                    else if (error == null)
                    {
                        error = "Se esperaba un ( despues del while";
                        return;
                    }
                }
                else if (error == null)
                {
                    error = "Error: Una instruccion do debe terminar con while en el renglon " + apuntador.renglon;
                    return;
                }
            }else if(apuntador.token == 205 && bloqueDoWhile == false)
            {
                if (etiqueta == "")
                {
                    etiqueta = "D" + (contadorC + 1).ToString();
                }
                else
                {
                    etiqueta = etiqueta + " D" + (contadorC + 1).ToString();
                }
                apuntador = apuntador.sig;
                if(apuntador.token == 124)
                {
                    apuntador = apuntador.sig;
                    Expresion();
                    TerminarListaTipos();
                    VaciarPilaOperadoresPolish();
                    if (error != null || errorSemantico != null)
                    {
                        return;
                    }
                    PolishWhileBrincoC();
                    if (apuntador.token == 125)
                    {
                        apuntador = apuntador.sig;
                        Proposiciones();
                        if (error != null || errorSemantico != null)
                        {
                            return;
                        }
                        PolishWhileBrincoD();
                        if (etiqueta == "")
                        {
                            etiqueta = "C" + pilaEtiquetaCPolish.Pop().ToString();
                        }
                        else
                        {
                            etiqueta = etiqueta + " C" + pilaEtiquetaCPolish.Pop().ToString();
                        }
                    }
                    else if (error == null)
                    {
                        error = "Error: Se esperaba un ) en el renglon " + apuntador.renglon;
                        return;
                    }
                }
                else if (error == null)
                {
                    error = "Error: Se espera un ( en el renglon " + apuntador.renglon;
                    return;
                }
            }else if(apuntador.token == 206)
            {
                bloquefor = true;
                apuntador = apuntador.sig;
                if(apuntador.token == 124)
                {
                    apuntador = apuntador.sig;
                    Proposiciones();
                    //Expresion();
                    if (error != null || errorSemantico != null)
                    {
                        return;
                    }
                    VaciarPilaOperadores();
                    NodoVariable inicializador = cabezaListaCopia;
                    TerminarListaTipos();
                    VaciarPilaOperadoresPolish();
                    if (error != null || errorSemantico != null)
                    {
                        return;
                    }
                    while (inicializador != null)
                    {
                        AgregarAListaPolish(inicializador);
                        inicializador = inicializador.sig;
                    }
                    if (apuntador.token == 129)
                    {
                        apuntador = apuntador.sig;
                        if (etiqueta == "")
                        {
                            etiqueta = "H" + (contadorG + 1);
                        }
                        else
                        {
                            etiqueta = etiqueta + " H" + (contadorG + 1);
                        }
                        //Proposiciones();
                        bloquefor = false;
                        Expresion();
                        bloquefor = true;
                        if (error != null || errorSemantico != null)
                        {
                            return;
                        }
                        TerminarListaTipos();
                        VaciarPilaOperadoresPolish();
                        if (error != null || errorSemantico != null)
                        {
                            return;
                        }
                        PolishForBrincoG();
                        if (apuntador.token == 129)
                        {
                            apuntador = apuntador.sig;
                            Proposiciones();
                            //Expresion();
                            if (error != null || errorSemantico != null)
                            {
                                return;
                            }
                            VaciarPilaOperadores();
                            NodoVariable incrementar = cabezaListaCopia;
                            TerminarListaTipos();
                            VaciarPilaOperadoresPolish();
                            bloquefor = false;
                            if (apuntador.token == 125)
                            {
                                apuntador = apuntador.sig;
                                Proposiciones();
                                while(incrementar != null)
                                {
                                    AgregarAListaPolish(incrementar);
                                    incrementar = incrementar.sig;
                                }
                                PolishForBrincoH();
                                if (etiqueta == "")
                                {
                                    etiqueta = "G" + pilaEtiquetaGPolish.Pop().ToString();
                                }
                                else
                                {
                                    etiqueta = etiqueta + " G" + pilaEtiquetaGPolish.Pop().ToString();
                                }
                            }
                            else if (error == null)
                            {
                                error = "Error: Se esperaba un ) en el renglon " + apuntador.renglon;
                                return;
                            }
                        }
                        else if (error == null)
                        {
                            error = "Error: Se esperaba un ; en el renglon " + apuntador.renglon;
                            return;
                        }
                    }
                    else if (error == null)
                    {
                        error = "Error: Se esperaba un ; en el renglon " + apuntador.renglon;
                        return;
                    }
                }
                else if (error == null)
                {
                    error = "Se esperaba un ( en el renglon " + apuntador.renglon;
                    return;
                }
            }else if(apuntador.token == 207)
            {
                apuntador = apuntador.sig;
                if(apuntador.token == 124)
                {
                    apuntador = apuntador.sig;
                    Expresion();
                    if (error != null || errorSemantico != null)
                    {
                        return;
                    }
                    if (apuntador.token  == 125)
                    {
                        TerminarListaTipos();
                        VaciarPilaOperadoresPolish();
                        if (error != null || errorSemantico != null)
                        {
                            return;
                        }
                        PolishIfBrincoA();
                        apuntador = apuntador.sig;
                        Proposiciones();
                        if (error != null || errorSemantico != null)
                        {
                            return;
                        }
                        PolishIfBrincoB();
                        
                        if (apuntador.token == 208)
                        {
                            apuntador = apuntador.sig;
                            if(etiqueta == "")
                            {
                                etiqueta = "A" + pilaEtiquetaAPolish.Pop().ToString();
                            }
                            else
                            {
                                etiqueta = etiqueta + " A" + pilaEtiquetaAPolish.Pop().ToString();
                            }
                            Proposiciones();
                            if (error != null || errorSemantico != null)
                            {
                                return;
                            }
                            if (etiqueta == "")
                            {
                                etiqueta = "B" + pilaEtiquetaBPolish.Pop().ToString();
                            }
                            else
                            {
                                etiqueta = etiqueta + " B" + pilaEtiquetaBPolish.Pop().ToString();
                            }
                        }
                        else
                        {
                            if (etiqueta == "")
                            {
                                etiqueta = "A" + pilaEtiquetaAPolish.Pop().ToString() + " B" + pilaEtiquetaBPolish.Pop().ToString();
                            }
                            else
                            {
                                etiqueta = etiqueta + " A" + pilaEtiquetaAPolish.Pop().ToString() + " B" + pilaEtiquetaBPolish.Pop().ToString();
                            }
                            ListaDeProposiciones();
                            if (error != null || errorSemantico != null)
                            {
                                return;
                            }
                        }
                        Proposiciones();
                    }
                    else if (error == null)
                    {
                        error = "Error: Se esperaba un ) en el renglon " + apuntador.renglon;
                        return;
                    }
                }
                else if (error == null)
                {
                    error = "Error: Se esperaba un ( en el renglon " + apuntador.renglon;
                    return;
                }
            }else if(apuntador.token == 209 || apuntador.token == 210)
            {
                NodoVariable auxiliar = new NodoVariable(apuntador.lexema, apuntador.token);
                CompatibilidadDeTipos(auxiliar);
                Polish(auxiliar);
                apuntador = apuntador.sig;
                if(apuntador.token == 124)
                {
                    apuntador = apuntador.sig;
                    if(apuntador.token == 119)
                    {
                        CompatibilidadDeTipos(BuscarIdentificador(apuntador.lexema));
                        Polish(BuscarIdentificador(apuntador.lexema));
                        if (error != null || errorSemantico != null)
                        {
                            return;
                        }
                        apuntador = apuntador.sig;
                        if(apuntador.token == 125)
                        {
                            apuntador = apuntador.sig;
                            if(apuntador.token == 129)
                            {
                                TerminarListaTipos();
                                VaciarPilaOperadoresPolish();
                                if (error != null || errorSemantico != null)
                                {
                                    return;
                                }
                                apuntador = apuntador.sig;
                                ListaDeProposiciones();
                                if (error != null || errorSemantico != null)
                                {
                                    return;
                                }
                            }
                            else if (error == null)
                            {
                                error = "Error: Se esperaba un ; en el renglon " + apuntador.renglon;
                            }
                        }
                        else if (error == null)
                        {
                            error = "Error: Se esperaba un ) en el renglon " + apuntador.renglon;
                            return;
                        }
                    }
                    else if (error == null)
                    {
                        error = "Se esperaba un identificador en el renglon " + apuntador.renglon;
                        return;
                    }
                }
                else if (error == null)
                {
                    error = "Se esperaba un ( en el renglon " + apuntador.renglon;
                    return;
                }
            }else if(apuntador.token == 126)
            {
                apuntador = apuntador.sig;
                ListaDeProposiciones();
                if (error != null || errorSemantico != null)
                {
                    return;
                }
                if (apuntador.token == 127)
                {
                    apuntador = apuntador.sig;
                    //ListaDeProposiciones();
                    if (error != null || errorSemantico != null)
                    {
                        return;
                    }
                }
                else if(error == null)
                {
                    error = "Error: Se esperaba un } en la linea " + apuntador.renglon;
                    return;
                }
            }else if(apuntador.token == 211)
            {
                NodoVariable auxiliar = new NodoVariable(apuntador.lexema, apuntador.token);
                CompatibilidadDeTipos(auxiliar);
                Polish(auxiliar);
                apuntador = apuntador.sig;
                if(apuntador.token == 124)
                {
                    apuntador = apuntador.sig;
                    if(apuntador.token == 119)
                    {
                        CompatibilidadDeTipos(BuscarIdentificador(apuntador.lexema));
                        Polish(BuscarIdentificador(apuntador.lexema));
                        apuntador = apuntador.sig;
                        while(apuntador.token == 128)
                        {
                            apuntador = apuntador.sig;
                            if(apuntador.token == 119)
                            {
                                BuscarSiExisteIdentificador(apuntador.lexema);
                                if (error != null || errorSemantico != null)
                                {
                                    return;
                                }
                                CompatibilidadDeTipos(BuscarIdentificador(apuntador.lexema));
                                Polish(BuscarIdentificador(apuntador.lexema));
                                apuntador = apuntador.sig;
                                
                            }
                            else if (error == null)
                            {
                                error = "Error: Se esperaba un identificador en el renglon " + apuntador.renglon;
                                return;
                            }
                        }
                        if(apuntador.token == 125)
                        {
                            apuntador = apuntador.sig;
                            if(apuntador.token == 129)
                            {
                                TerminarListaTipos();
                                VaciarPilaOperadoresPolish();
                                if (error != null || errorSemantico != null)
                                {
                                    return;
                                }
                                apuntador = apuntador.sig;
                                ListaDeProposiciones();
                                if (error != null || errorSemantico != null)
                                {
                                    return;
                                }
                            }
                            else if (error == null)
                            {
                                error = "Error: Se esperaba un ; en el renglon " + apuntador.renglon;
                            }
                        }
                        else if (error == null)
                        {
                            error = "Error: Se esperaba un ) en el renglon " + apuntador.renglon;
                            return;
                        }
                    }
                    else if (error == null)
                    {
                        error = "Error: Se esperaba un identificador en el renglon " + apuntador.renglon;
                        return;
                    }
                }
                else if (error == null)
                {
                    error = "Se esperaba un ( en el renglon " + apuntador.renglon;
                    return;
                }
            }else if(apuntador.token == 212)
            {
                NodoVariable auxiliar = new NodoVariable(apuntador.lexema, apuntador.token);
                CompatibilidadDeTipos(auxiliar);
                Polish(auxiliar);
                apuntador = apuntador.sig;
                if(apuntador.token == 124)
                {

                    apuntador = apuntador.sig;
                    Primario();
                    if (error != null || errorSemantico != null)
                    {
                        return;
                    }
                    if (apuntador.token == 125)
                    {
                        apuntador = apuntador.sig;
                        if(apuntador.token == 129)
                        {
                            TerminarListaTipos();
                            VaciarPilaOperadoresPolish();
                            if (error != null || errorSemantico != null)
                            {
                                return;
                            }
                            apuntador = apuntador.sig;
                            ListaDeProposiciones();
                            if (error != null || errorSemantico != null)
                            {
                                return;
                            }
                        }
                        else if (error == null)
                        {
                            error = "Error: Se esperaba un ; en el renglon " + (apuntador.renglon);
                            return;
                        }
                    }
                    else if (error == null)
                    {
                        error = "Se esperaba un ) en el renglon " + apuntador.renglon;
                        return;
                    }
                }
                else if (error == null)
                {
                    error = "Error: Se esperaba un ( en el renglon " + apuntador.renglon;
                    return;
                }
            }
            else if((apuntador.token >= 104  && apuntador.token <= 108) && error == null)
            {
                error = "Error: Se esperaba un identificador antes de la asignacion en el renglon " + apuntador.renglon;
                return;
            }
            else if((apuntador.token == 201 || apuntador.token == 202 || apuntador.token == 203) && error == null)
            {
                error = "Error: Se esperaba una proposicion valida en el renglon " + apuntador.renglon;
                return;
            }
        }

        public void Primario()
        {
            if (apuntador.token == 124)
            {
                NodoVariable auxiliar = new NodoVariable(apuntador.lexema, apuntador.token);
                CompatibilidadDeTipos(auxiliar);
                Polish(auxiliar);
                apuntador = apuntador.sig;
                Expresion();
                if (error != null || errorSemantico != null)
                {
                    return;
                }
                if (apuntador.token == 125)
                {
                    NodoVariable auxiliar2 = new NodoVariable(apuntador.lexema, apuntador.token);
                    CompatibilidadDeTipos(auxiliar2);
                    Polish(auxiliar2);
                    apuntador = apuntador.sig;
                    if (apuntador.token == 119)
                    {
                        BuscarSiExisteIdentificador(apuntador.lexema);
                        if (error != null || errorSemantico != null)
                        {
                            return;
                        }
                        CompatibilidadDeTipos(BuscarIdentificador(apuntador.lexema));
                        Polish(BuscarIdentificador(apuntador.lexema));
                        apuntador = apuntador.sig;
                        while (apuntador.token == 128)
                        {
                            apuntador = apuntador.sig;
                            if (apuntador.token == 119)
                            {
                                BuscarSiExisteIdentificador(apuntador.lexema);
                                if (error != null || errorSemantico != null)
                                {
                                    return;
                                }
                                CompatibilidadDeTipos(BuscarIdentificador(apuntador.lexema));
                                Polish(BuscarIdentificador(apuntador.lexema));
                                apuntador = apuntador.sig;
                            }
                            else if (error == null)
                            {
                                error = "Error: Se esperaba un identificador en el renglon " + apuntador.renglon;
                                return;
                            }
                        }
                    }
                }
                else if(error == null)
                {
                    error = "Error: Se esperaba un ) en el renglon " + apuntador.renglon;
                    return;
                }
            }
            else if(error == null)
            {
                error = "Error: se esperaba un ( en el renglon " + apuntador.renglon;
                return;
            }
        }

        public void Asignacion()
        {
            if(apuntador.token >= 104 && apuntador.token <= 108)
            {
                NodoVariable auxiliar = new NodoVariable(apuntador.lexema, apuntador.token);
                CompatibilidadDeTipos(auxiliar);
                if(bloquefor == false) Polish(auxiliar);
                apuntador = apuntador.sig;
            }
            else if (error == null)
            {
                error = "Error: Se esperaba un operador de asignación en el renglon " + apuntador.renglon;
                return;
            }
        }

        public void Expresion()
        {
            ExpresionSimple();
            if (error != null || errorSemantico != null)
            {
                return;
            }
            while (apuntador.token >= 111 && apuntador.token <= 116)               // Poner un While en vez de un if
            {
                NodoVariable auxiliar = new NodoVariable(apuntador.lexema, apuntador.token);
                CompatibilidadDeTipos(auxiliar);
                Polish(auxiliar);
                apuntador = apuntador.sig;
                ExpresionSimple();
                if (error != null || errorSemantico != null)
                {
                    return;
                }
            }
        }

        public void ExpresionSimple()
        {
            if (apuntador.token == 100 || apuntador.token == 101)
            {
                NodoVariable auxiliar = new NodoVariable(apuntador.lexema, apuntador.token);
                CompatibilidadDeTipos(auxiliar);
                Polish(auxiliar);
                apuntador = apuntador.sig;
                Operando();
                if (error != null || errorSemantico != null)
                {
                    return;
                }
            }
            else
            {
                Operando();
                if (error != null || errorSemantico != null)
                {
                    return;
                }
                while (apuntador.token == 100 || apuntador.token == 101 || apuntador.token == 117)
                {
                    NodoVariable auxiliar = new NodoVariable(apuntador.lexema, apuntador.token);
                    CompatibilidadDeTipos(auxiliar);
                    Polish(auxiliar);
                    apuntador = apuntador.sig;
                    Operando();
                    if (error != null || errorSemantico != null)
                    {
                        return;
                    }
                }
            }
        }

        public void Operando()
        {
            Factor();
            if (error != null || errorSemantico != null)
            {
                return;
            }
            if (apuntador.token == 102 || apuntador.token == 103 || apuntador.token == 118)
            {
                NodoVariable auxiliar = new NodoVariable(apuntador.lexema, apuntador.token);
                CompatibilidadDeTipos(auxiliar);
                Polish(auxiliar);
                apuntador = apuntador.sig;
                Operando();
                if (error != null || errorSemantico != null)
                {
                    return;
                }
            }
        }

        public void Factor()
        {
            if(apuntador.token == 119 || apuntador.token == 120 || apuntador.token == 122 || apuntador.token == 123)
            {
                if(apuntador.token == 119)
                {
                    BuscarSiExisteIdentificador(apuntador.lexema);
                    ComprobarVariableInicializada(apuntador.lexema);
                    if (error != null || errorSemantico != null)
                    {
                        return;
                    }
                    CompatibilidadDeTipos(BuscarIdentificador(apuntador.lexema));
                    Polish(BuscarIdentificador(apuntador.lexema));
                }
                else
                {
                    NodoVariable auxiliar = new NodoVariable(apuntador.lexema, apuntador.token);
                    CompatibilidadDeTipos(auxiliar);
                    if(bloquefor == false) Polish(auxiliar);
                }
                apuntador = apuntador.sig;
            }
            else if(apuntador.token == 124)
            {
                apuntador = apuntador.sig;
                Expresion();
                if(error != null || errorSemantico != null)
                {
                    return;
                }
                if(apuntador.token == 125)
                {
                    apuntador = apuntador.sig;
                }
                else if (error == null)
                {
                    error = "Error: Se esperaba un ) en el renglon " + apuntador.renglon;
                    return;
                }
            }
            else if (error == null)
            {
                error = "Error: Se esperaba un factor valido en el renglon " + apuntador.renglon;
                return;
            }
        }

        public void CrearNodoNuevo(string lexema, int token)
        {
            identificador = new NodoVariable(lexema, token);
            if (cabeza == null)
            {
                cabeza = identificador;
                p = identificador;
            }
            else
            {
                p.sig = identificador;
                p = identificador;
            }
        }

        public NodoVariable BuscarIdentificador(string lexema)
        {
            NodoVariable nodo = new NodoVariable("", 1);
            if (identificador != null)
            {
                p = cabeza;
                while(p != null)
                {
                    if(p.lexema == lexema)
                    {
                        return p;
                    }
                    p = p.sig;
                }
                
                p = cabeza;
                while(p.sig != null)
                {
                    p = p.sig;
                }
                return nodo;
            }
            else
            {
                return nodo;
            }
        }

        public void BuscarIdentificador(string lexema, int token)
        {
            if(identificador != null)
            {
                p = cabeza;
                while (p != null)
                {
                    if (p.lexema == lexema)
                    {
                        errorSemantico = "El identificador " + lexema + " ya ha sido declarado";
                        break;
                    }
                    p = p.sig;
                }
                p = cabeza;
                while(p.sig != null)
                {
                    p = p.sig;
                }
                if(errorSemantico == null)
                {
                    CrearNodoNuevo(lexema, token);
                    return;
                }
            }
            else
            {
                CrearNodoNuevo(lexema, token);
                return;
            }
        }

        public void BuscarSiExisteIdentificador(string lexema)
        {
            variableExiste = false;
            if (identificador != null)
            {
                p = cabeza;
                while (p != null)
                {
                    if (p.lexema == lexema)
                    {
                        variableExiste = true;
                        p.esUtilizada = true;
                        break;
                    }
                    p = p.sig;
                }
                p = cabeza;
                while (p.sig != null)
                {
                    p = p.sig;
                }

                if(variableExiste == false)
                {
                    errorSemantico = "El identificador " + lexema + " no ha sido declarado";
                    return;
                }
            }
            else
            {
                errorSemantico = "El identificador " + lexema + " no ha sido declarado";
            }
        }

        public void ComprobacionIncrementalDecremental(string lexema)
        {
            if(identificador != null)
            {
                p = cabeza;
                while(p != null)
                {
                    if(identificador.lexema == lexema && identificador.token != 201)
                    {
                        errorSemantico = "El operando " + lexema + " no puede usar los operadores de incremento o " +
                    "decremento";
                        break;
                    }
                    p = p.sig;
                }
                p = cabeza;
                while (p.sig != null)
                {
                    p = p.sig;
                }
            }
        }

        public void ComprobarVariableUtilizada()
        {
            if (identificador != null)
            {
                p = cabeza;
                while (p != null)
                {
                    if (p.esUtilizada == false)
                    {
                        Console.Out.WriteLine("WARNING: La variable " + p.lexema + " no se utiliza");
                    }
                    p = p.sig;
                }
                p = cabeza;
                while (p.sig != null)
                {
                    p = p.sig;
                }
            }
        }

        public void InicializarVariable(string variable)
        {
            if(identificador != null)
            {
                p = cabeza;
                while (p != null)
                {
                    if(p.lexema == variable)
                    {
                        p.estaInicializada = true;
                        break;
                    }
                    p = p.sig;
                }
                p = cabeza;
                while(p.sig != null)
                {
                    p = p.sig;
                }
            }
        }

        public void ComprobarVariableInicializada(string variable)
        {
            if(identificador != null)
            {
                p = cabeza;
                while(p != null)
                {
                    if (p.lexema == variable && p.estaInicializada == false)
                    {
                        Console.Out.WriteLine("WARNING: La variable " + p.lexema + " no se ha inicializado");
                    }
                    p = p.sig;
                }
                p = cabeza;
                while(p.sig != null)
                {
                    p = p.sig;
                }
            }
        }

        public void CompatibilidadDeTipos(NodoVariable q)
        {
            if((q.token >= 119 && q.token <= 123) || (q.token >= 201 && q.token <= 203) || q.token == 213 || q.token == 214 || 
                q.token == 215 || q.token == 216)
            {
                AgregarALista(q);
            }
            else if ((q.token >= 100 && q.token <= 104) || (q.token >= 111 && q.token <= 118) || q.token == 130 
                || q.token == 124 || q.token == 125 || q.token == 109 || q.token == 110)
            {
                if (q.token == 124 || q.token == 125)
                {
                    q.prioridad = 7;
                }
                else if (q.token == 109 || q.token == 110)
                {
                    q.prioridad = 6;
                }
                else if (q.token == 102 || q.token == 103)
                {
                    q.prioridad = 5;
                }
                else if (q.token == 100 || q.token == 101)
                {
                    q.prioridad = 4;
                }
                else if ((q.token >= 111 && q.token <= 116) || q.token == 104)
                {
                    q.prioridad = 3;
                }
                else if (q.token == 130)
                {
                    q.prioridad = 2;
                }
                else if (q.token == 117 || q.token == 118)
                {
                    q.prioridad = 1;
                }
                if(pilaOperadores.Count == 0)
                {
                    pilaOperadores.Push(q);
                }
                else
                {
                    if(q.token == 125)
                    {
                        //auxiliarPila = (Nodo)pilaOperadores.Peek();
                        while(((NodoVariable)pilaOperadores.Peek()).token != 124)
                        {
                            AgregarALista((NodoVariable)pilaOperadores.Pop());
                            //auxiliarPila = (Nodo)pilaOperadores.Peek();
                        }
                        pilaOperadores.Pop();
                    }
                    else
                    {
                        if(q.token == 124)
                        {
                            pilaOperadores.Push(q);
                        }
                        else
                        {
                            //auxiliarPila = (Nodo)pilaOperadores.Peek();
                            NodoVariable tokenAuxiliar = (NodoVariable)pilaOperadores.Peek();
                            int token124 = tokenAuxiliar.token;
                            if (token124 == 124)
                            {
                                AgregarALista(q);
                            }
                            else
                            {
                                while((q.prioridad <= ((NodoVariable)pilaOperadores.Peek()).prioridad) && ((NodoVariable)pilaOperadores.Peek()).token != 124)
                                {
                                    AgregarALista((NodoVariable)pilaOperadores.Pop());
                                    //auxiliarPila = (Nodo)pilaOperadores.Peek();
                                    if(pilaOperadores.Count == 0)
                                    {
                                        break;
                                    }
                                }
                                pilaOperadores.Push(q);
                                //AgregarALista(q);
                            }
                        }
                    }
                }
            }
        }

        public void AgregarALista(NodoVariable q)
        {
            lista = new NodoVariable(q.lexema, q.token);
            if (cabezaLista == null)
            {
                cabezaLista = lista;
                pLista = lista;
            }
            else
            {
                pLista.sig = lista;
                pLista = lista;
            }
        }

        
        public void TerminarListaTipos()
        {
            while(pilaOperadores.Count > 0)
            {
                AgregarALista((NodoVariable)pilaOperadores.Pop());
            }
            pLista = cabezaLista;
            while(pLista != null)
            {
                if((pLista.token >= 201 && pLista.token <= 203) || pLista.token == 213 || pLista.token == 214 || 
                    pLista.token == 215 || pLista.token == 216 || pLista.token == 122 || pLista.token == 123 || 
                    pLista.token == 120 || pLista.token == 121)
                {
                    if(pLista.token == 120)
                    {
                        pilaTipos.Push(213);
                    }
                    else if(pLista.token == 121)
                    {
                        pilaTipos.Push(203);
                    }
                    else if (pLista.token == 122)
                    {
                        pilaTipos.Push(201);
                    }
                    else if (pLista.token == 123)
                    {
                        pilaTipos.Push(202);
                    }
                    else
                    {
                        pilaTipos.Push(pLista.token);
                    }
                    pLista = pLista.sig;
                }
                else
                {
                    if(pLista.token == 100)
                    {
                        b.token = Convert.ToInt32(pilaTipos.Pop());
                        a.token = Convert.ToInt32(pilaTipos.Pop());
                        if((a.token == 202 && b.token == 202) || (a.token == 202 && b.token == 201) || (a.token == 201 &&
                            b.token == 202) || (a.token == 123 && b.token == 123) || (a.token == 122 && b.token == 123) || 
                            (a.token == 123 && b.token == 122))
                        {
                            pilaTipos.Push(202);
                        }
                        else if ((a.token == 201 && b.token == 201) || (a.token == 122 && b.token == 122))
                        {
                            pilaTipos.Push(201);
                        }
                        else if ((a.token == 213 && b.token == 213) || (a.token == 213 
                            && b.token == 203) || (a.token == 120 && b.token == 120) || 
                            (a.token == 120 && b.token == 121))
                        {
                            pilaTipos.Push(213);
                        }
                        else if ((a.token == 203 && b.token == 203) || (a.token == 121 && b.token == 121))
                        {
                            pilaTipos.Push(203);
                        }
                        else
                        {
                            errorSemantico = "Hay incompatibiliad de tipos";
                            return;
                        }
                    }
                    else if(pLista.token == 101)                 //Sustituir if con else if
                    {
                        b.token = Convert.ToInt32(pilaTipos.Pop());
                        a.token = Convert.ToInt32(pilaTipos.Pop());
                        if ((a.token == 202 && b.token == 202) || (a.token == 202 && b.token == 201) || (a.token == 201 &&
                            b.token == 202) || (a.token == 123 && b.token == 123) || (a.token == 122 && b.token == 123) ||
                            (a.token == 123 && b.token == 122))
                        {
                            pilaTipos.Push(202);
                        }
                        else if ((a.token == 201 && b.token == 201) || (a.token == 122 && b.token == 122))
                        {
                            pilaTipos.Push(201);
                        }
                        else
                        {
                            errorSemantico = "Hay incompatibiliad de tipos";
                            return;
                        }
                    }
                    else if(pLista.token == 102)
                    {
                        b.token = Convert.ToInt32(pilaTipos.Pop());
                        a.token = Convert.ToInt32(pilaTipos.Pop());
                        if ((a.token == 202 && b.token == 202) || (a.token == 202 && b.token == 201) || (a.token == 201 &&
                            b.token == 202) || (a.token == 123 && b.token == 123) || (a.token == 122 && b.token == 123) ||
                            (a.token == 123 && b.token == 122))
                        {
                            pilaTipos.Push(202);
                        }
                        else if ((a.token == 201 && b.token == 201) || (a.token == 122 && b.token == 122))
                        {
                            pilaTipos.Push(201);
                        }
                        else
                        {
                            errorSemantico = "Hay incompatibiliad de tipos";
                            return;
                        }
                    }
                    else if(pLista.token == 103)
                    {
                        b.token = Convert.ToInt32(pilaTipos.Pop());
                        a.token = Convert.ToInt32(pilaTipos.Pop());
                        if ((a.token == 202 && b.token == 202) || (a.token == 202 && b.token == 201) || (a.token == 201 &&
                            b.token == 202) || (a.token == 123 && b.token == 123) || (a.token == 122 && b.token == 123) ||
                            (a.token == 123 && b.token == 122) || (a.token == 201 && b.token == 201) || (a.token == 122 && 
                            b.token == 122))
                        {
                            pilaTipos.Push(202);
                        }
                        else
                        {
                            errorSemantico = "Hay incompatibiliad de tipos";
                            return;
                        }
                    }
                    else if(pLista.token == 104)
                    {
                        tipo2 = Convert.ToInt32(pilaTipos.Pop());
                        tipo1 = Convert.ToInt32(pilaTipos.Pop());
                        if(tipo1 == tipo2)
                        {
                            pilaTipos.Push(tipo1);
                        }
                        else
                        {
                            errorSemantico = "hay incompatibilidad de tipos";
                            return;
                        }
                    }
                    else if(pLista.token == 105)
                    {
                        b.token = Convert.ToInt32(pilaTipos.Pop());
                        a.token = Convert.ToInt32(pilaTipos.Pop());
                        if ((a.token == 202 && b.token == 202) || (a.token == 202 && b.token == 201) || (a.token == 201 &&
                            b.token == 202) || (a.token == 123 && b.token == 123) || (a.token == 122 && b.token == 123) ||
                            (a.token == 123 && b.token == 122))
                        {
                            pilaTipos.Push(202);
                        }
                        else if ((a.token == 201 && b.token == 201) || (a.token == 122 && b.token == 122))
                        {
                            pilaTipos.Push(201);
                        }
                        else
                        {
                            errorSemantico = "Hay incompatibiliad de tipos";
                            return;
                        }
                    }
                    else if(pLista.token == 106)
                    {
                        b.token = Convert.ToInt32(pilaTipos.Pop());
                        a.token = Convert.ToInt32(pilaTipos.Pop());
                        if ((a.token == 202 && b.token == 202) || (a.token == 202 && b.token == 201) || (a.token == 201 &&
                            b.token == 202) || (a.token == 123 && b.token == 123) || (a.token == 122 && b.token == 123) ||
                            (a.token == 123 && b.token == 122))
                        {
                            pilaTipos.Push(202);
                        }
                        else if ((a.token == 201 && b.token == 201) || (a.token == 122 && b.token == 122))
                        {
                            pilaTipos.Push(201);
                        }
                        else if ((a.token == 213 && b.token == 213) || (a.token == 213
                            && b.token == 203) || (a.token == 120 && b.token == 120) ||
                            (a.token == 120 && b.token == 121))
                        {
                            pilaTipos.Push(213);
                        }
                        else if ((a.token == 203 && b.token == 203) || (a.token == 121 && b.token == 121))
                        {
                            pilaTipos.Push(203);
                        }
                        {
                            errorSemantico = "Hay incompatibiliad de tipos";
                            return;
                        }
                    }
                    else if(pLista.token == 107)
                    {
                        b.token = Convert.ToInt32(pilaTipos.Pop());
                        a.token = Convert.ToInt32(pilaTipos.Pop());
                        if ((a.token == 202 && b.token == 202) || (a.token == 202 && b.token == 201) || (a.token == 201 &&
                            b.token == 202) || (a.token == 123 && b.token == 123) || (a.token == 122 && b.token == 123) ||
                            (a.token == 123 && b.token == 122))
                        {
                            pilaTipos.Push(202);
                        }
                        else if ((a.token == 201 && b.token == 201) || (a.token == 122 && b.token == 122))
                        {
                            pilaTipos.Push(201);
                        }
                        else
                        {
                            errorSemantico = "Hay incompatibiliad de tipos";
                            return;
                        }
                    }
                    else if(pLista.token == 108)
                    {
                        b.token = Convert.ToInt32(pilaTipos.Pop());
                        a.token = Convert.ToInt32(pilaTipos.Pop());
                        if ((a.token == 202 && b.token == 202) || (a.token == 202 && b.token == 201) || (a.token == 201 &&
                            b.token == 202) || (a.token == 123 && b.token == 123) || (a.token == 122 && b.token == 123) ||
                            (a.token == 123 && b.token == 122) || (a.token == 201 && b.token == 201) || (a.token == 122 &&
                            b.token == 122))
                        {
                            pilaTipos.Push(202);
                        }
                        else
                        {
                            errorSemantico = "Hay incompatibiliad de tipos";
                            return;
                        }
                    }
                    else if(pLista.token == 109)
                    {
                        a.token = Convert.ToInt32(pilaTipos.Pop());
                        if (a.token == 201)
                        {
                            pilaTipos.Push(201);
                        }
                        else
                        {
                            errorSemantico = "Hay incompatibiliad de tipos";
                            return;
                        }
                    }
                    else if(pLista.token == 110)
                    {
                        a.token = Convert.ToInt32(pilaTipos.Pop());
                        if (a.token == 201)
                        {
                            pilaTipos.Push(201);
                        }
                        else
                        {
                            errorSemantico = "Hay incompatibiliad de tipos";
                            return;
                        }
                    }
                    else if(pLista.token >= 111 && pLista.token <= 114)
                    {
                        b.token = Convert.ToInt32(pilaTipos.Pop());
                        a.token = Convert.ToInt32(pilaTipos.Pop());
                        if ((a.token == 202 && b.token == 202) || (a.token == 202 && b.token == 201) || (a.token == 201 &&
                            b.token == 202) || (a.token == 201 && b.token == 201) || (a.token == 123 && b.token == 123) || 
                            (a.token == 122 && b.token == 123) || (a.token == 123 && b.token == 122) || (a.token == 122 && 
                            b.token == 122))
                        {
                            pilaTipos.Push(214);
                        }
                        else
                        {
                            errorSemantico = "Hay incompatibiliad de tipos";
                            return;
                        }
                    }
                    else if(pLista.token == 115 || pLista.token == 116)
                    {
                        b.token = Convert.ToInt32(pilaTipos.Pop());
                        a.token = Convert.ToInt32(pilaTipos.Pop());
                        if ((a.token == 202 && b.token == 202) || (a.token == 202 && b.token == 201) || (a.token == 201 &&
                            b.token == 202) || (a.token == 201 && b.token == 201) || (a.token == 123 && b.token == 123) ||
                            (a.token == 122 && b.token == 123) || (a.token == 123 && b.token == 122) || (a.token == 122 &&
                            b.token == 122) || (a.token == 203 && b.token == 203) || 
                            (a.token == 213 && b.token == 203) || (a.token == 213 && b.token == 213) || (a.token == 120 && 
                            b.token == 120) || (a.token == 120 && b.token == 121) || 
                            (a.token == 121 && b.token == 121) || (a.token == 214 && b.token == 214) || (a.token == 214 && 
                            b.token == 215) || (a.token == 214 && b.token == 216) || (a.token == 215 && b.token == 214) || 
                            (a.token == 215 && b.token == 215) || (a.token == 215 && b.token == 216) || (a.token == 216 && 
                            b.token == 214) || (a.token == 216 && b.token == 215) || (a.token == 216 && b.token == 216))
                        {
                            pilaTipos.Push(214);
                        }
                        else
                        {
                            errorSemantico = "Hay incompatibiliad de tipos";
                            return;
                        }
                    }
                    else if (pLista.token == 117 || pLista.token == 118)
                    {
                        b.token = Convert.ToInt32(pilaTipos.Pop());
                        a.token = Convert.ToInt32(pilaTipos.Pop());
                        if ((a.token == 214 && b.token == 214) || (a.token == 214 && b.token == 215) || (a.token == 214 &&
                            b.token == 216) || (a.token == 215 && b.token == 214) || (a.token == 215 && b.token == 215) ||
                            (a.token == 215 && b.token == 216) || (a.token == 216 && b.token == 214) || (a.token == 216 &&
                            b.token == 215) || (a.token == 216 && b.token == 216))
                        {
                            pilaTipos.Push(214);
                        }
                        else
                        {
                            errorSemantico = "Hay incompatibiliad de tipos";
                            return;
                        }
                    }
                    pLista = pLista.sig;
                }
            }
            lista = null;
            pLista = null;
            cabezaLista = null;
            pilaTipos.Clear();
        }

        public void Polish(NodoVariable q)
        {
            if ((q.token >= 119 && q.token <= 123) || (q.token >= 201 && q.token <= 203) || q.token == 213 || q.token == 214 ||
                q.token == 215 || q.token == 216)
            {
                AgregarAListaPolish(q);
            }
            else if ((q.token >= 100 && q.token <= 104) || (q.token >= 111 && q.token <= 118) || q.token == 130
                || q.token == 124 || q.token == 125 || q.token == 109 || q.token == 110)
            {
                if (q.token == 124 || q.token == 125)
                {
                    q.prioridad = 7;
                }
                else if (q.token == 109 || q.token == 110)
                {
                    q.prioridad = 6;
                }
                else if (q.token == 102 || q.token == 103)
                {
                    q.prioridad = 5;
                }
                else if (q.token == 100 || q.token == 101)
                {
                    q.prioridad = 4;
                }
                else if ((q.token >= 111 && q.token <= 116) || q.token == 104)
                {
                    q.prioridad = 3;
                }
                else if (q.token == 130)
                {
                    q.prioridad = 2;
                }
                else if (q.token == 117 || q.token == 118)
                {
                    q.prioridad = 1;
                }
                if (pilaOperadoresPolish.Count == 0)
                {
                    pilaOperadoresPolish.Push(q);
                }
                else
                {
                    if (q.token == 125)
                    {
                        //auxiliarPila = (Nodo)pilaOperadores.Peek();
                        while (((NodoVariable)pilaOperadoresPolish.Peek()).token != 124)
                        {
                            AgregarAListaPolish((NodoVariable)pilaOperadoresPolish.Pop());
                            //auxiliarPila = (Nodo)pilaOperadores.Peek();
                        }
                        pilaOperadoresPolish.Pop();
                    }
                    else
                    {
                        if (q.token == 124)
                        {
                            pilaOperadoresPolish.Push(q);
                        }
                        else
                        {
                            //auxiliarPila = (Nodo)pilaOperadores.Peek();
                            NodoVariable tokenAuxiliar = (NodoVariable)pilaOperadoresPolish.Peek();
                            int token124 = tokenAuxiliar.token;
                            if (token124 == 124)
                            {
                                AgregarAListaPolish(q);
                            }
                            else
                            {
                                while ((q.prioridad <= ((NodoVariable)pilaOperadoresPolish.Peek()).prioridad) && ((NodoVariable)pilaOperadoresPolish.Peek()).token != 124)
                                {
                                    AgregarAListaPolish((NodoVariable)pilaOperadoresPolish.Pop());
                                    //auxiliarPila = (Nodo)pilaOperadores.Peek();
                                    if (pilaOperadoresPolish.Count == 0)
                                    {
                                        break;
                                    }
                                }
                                pilaOperadoresPolish.Push(q);
                                //AgregarALista(q);
                            }
                        }
                    }
                }
            }
        }

        public void AgregarAListaPolish(NodoVariable q)
        {
            listaPolish = new NodoPolish(q.lexema, q.token);
            listaPolish.etiqueta = etiqueta;
            if (cabezaListaPolish == null)
            {
                cabezaListaPolish = listaPolish;
                pListaPolish = listaPolish;
            }
            else
            {
                pListaPolish.sig = listaPolish;
                pListaPolish = listaPolish;
            }
            etiqueta = "";
        }

        public void VaciarPilaOperadoresPolish()
        {
            while (pilaOperadoresPolish.Count > 0)
            {
                AgregarAListaPolish((NodoVariable)pilaOperadoresPolish.Pop());
            }
        }

        public void PolishIfBrincoA()
        {
            contadorA = contadorA + 1;
            NodoVariable auxiliar = new NodoVariable("BRF-A" + contadorA,0);
            pilaAPolish.Push(contadorA);
            pilaEtiquetaAPolish.Push(contadorA);
            pilaEtiquetaBPolish.Push(contadorA);
            AgregarAListaPolish(auxiliar);
        }

        public void PolishIfBrincoB()
        {
            NodoVariable auxiliar = new NodoVariable("BRI-B" + pilaAPolish.Pop().ToString(), 0);
            AgregarAListaPolish(auxiliar);
        }

        public void PolishWhileBrincoC()
        {
            contadorC = contadorC + 1;
            NodoVariable auxiliar = new NodoVariable("BRF-C" + contadorC, 0);
            pilaCPolish.Push(contadorC);
            pilaEtiquetaCPolish.Push(contadorC);
            pilaEtiquetaDPolish.Push(contadorC);
            AgregarAListaPolish(auxiliar);
        }

        public void PolishWhileBrincoD()
        {
            NodoVariable auxiliar = new NodoVariable("BRI-D" + pilaCPolish.Pop().ToString(), 0);
            AgregarAListaPolish(auxiliar);
        }

        public void PolishDoBrincoF()
        {
            NodoVariable auxiliar = new NodoVariable("BRV-F" + pilaFPolish.Pop().ToString(), 0);
            AgregarAListaPolish(auxiliar);
        }

        public void PolishForBrincoG()
        {
            contadorG = contadorG + 1;
            pilaGPolish.Push(contadorG);
            pilaEtiquetaGPolish.Push(contadorG);
            NodoVariable auxiliar = new NodoVariable("BRF-G" + contadorG, 0);
            AgregarAListaPolish(auxiliar);
        }

        public void PolishForBrincoH()
        {
            NodoVariable auxiliar = new NodoVariable("BRI-H" + pilaGPolish.Pop().ToString(), 0);
            AgregarAListaPolish(auxiliar);
        }

        public void VaciarPilaOperadores()
        {
            pilaListaCopia = pilaOperadores;
            cabezaListaCopia = cabezaLista;
            pListaCopia = cabezaListaCopia;
            while(pListaCopia.sig != null)
            {
                pListaCopia = pListaCopia.sig;
            }
            while (pilaListaCopia.Count > 0)
            {
                AgregarAListaCopia((NodoVariable)pilaListaCopia.Pop());
            }
        }

        public void AgregarAListaCopia(NodoVariable q)
        {
            listaCopia = q;
            if (cabezaListaCopia == null)
            {
                cabezaListaCopia = listaCopia;
                pListaCopia = listaCopia;
            }
            else
            {
                pListaCopia.sig = listaCopia;
                pListaCopia = listaCopia;
            }
        }

        public NodoPolish GetPolish()
        {
            return cabezaListaPolish;
        }

        public void TerminarPolish()
        {
            NodoPolish pListaPolishCopia = new NodoPolish("",0);
            //NodoPolish cabezaListaPolishCopia = cabezaListaPolish;
            pListaPolish = cabezaListaPolish;
            while(pListaPolish != null)
            {
                if(pListaPolish.lexema.Contains("BR") && pListaPolish.token == 0 && pListaPolish.brinco == null)
                {
                    string indicador = pListaPolish.lexema.Substring(4);
                    pListaPolishCopia = cabezaListaPolish;
                    while(pListaPolishCopia != null)
                    {
                        if (pListaPolishCopia.etiqueta.Contains(indicador))
                        {
                            pListaPolish.brinco = pListaPolishCopia;
                            break;
                        }
                        pListaPolishCopia = pListaPolishCopia.sig;
                    }
                }
                pListaPolish = pListaPolish.sig;
            }
            pListaPolish = cabezaListaPolish;
            while (pListaPolish.sig != null)
            {
                pListaPolish = pListaPolish.sig;
            }
        }

        /*
        public int apuntador.renglon
        {
            while (auxiliar.sig != apuntador)
            {
                auxiliar = auxiliar.sig;
            }
            return auxiliar.renglon;
        } */
    }
}
