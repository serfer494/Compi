using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    public class Ensamblador
    {
        string path = @"C:\MACROS\compi.asm";
        NodoVariable listaVariables;
        NodoPolish polish;
        public Ensamblador(NodoVariable variables, NodoPolish polish)
        {
            this.listaVariables = variables;
            this.polish = polish;
        }

        public void CrearArchivo()
        {
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    NodoVariable p = listaVariables;
                    sw.WriteLine("INCLUDE macros.mac");
                    sw.WriteLine("DOSSEG");
                    sw.WriteLine(".MODEL SMALL");
                    sw.WriteLine("STACK 100h");
                    sw.WriteLine(".DATA");

                    while (p != null)
                    {
                        sw.WriteLine();
                    }
                }
            }
        }
    }
}
