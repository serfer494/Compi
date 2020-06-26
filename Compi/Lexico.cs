using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compi
{
    public class Lexico
    {
        string lexema = "";
        int token;
        int renglon = 1;
        string buffer = "";
        int valormt;
        char caracter;
        int[,] matriz = new int[,] {
        {13,    17,     1,      2,      3,      4,      9,      8,      7,      12,     10,     11,     505,    128,    129,    505,    505,    505,    14,     15,     124,    125,    126,    127,    505,    505,    505,    0,      0,      0,      0,      505,    0   },
        {100,   100,    109,    100,    100,    100,    100,    100,    106,    100,    100,    100,    100,    100,    100,    100,    100,    100,    100,    100,    100,    100,    100,    100,    100,    100,    100,    100,    100,    100,    100,    100,    100 },
        {101,   101,    101,    110,    101,    101,    101,    101,    105,    101,    101,    101,    101,    101,    101,    101,    101,    101,    101,    101,    101,    101,    101,    101,    101,    101,    101,    101,    101,    101,    101,    101,    101 },
        {102,   102,    102,    102,    102,    102,    102,    102,    107,    102,    102,    102,    102,    102,    102,    102,    102,    102,    102,    102,    102,    102,    102,    102,    102,    102,    102,    102,    102,    102,    102,    102,    102 },
        {103,   103,    103,    103,    5,      103,    103,    103,    108,    103,    103,    103,    103,    103,    103,    103,    103,    103,    103,    103,    103,    103,    103,    103,    103,    103,    103,    103,    103,    103,    103,    103,    103 },
        {5,     5,      5,      5,      6,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      500,    5,      5   },
        {5,     5,      5,      5,      5,      0,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5,      5   },
        {104,   104,    104,    104,    104,    104,    104,    104,    116,    104,    104,    104,    104,    104,    104,    104,    104,    104,    104,    104,    104,    104,    104,    104,    104,    104,    104,    104,    104,    104,    104,    104,    104 },
        {111,   111,    111,    111,    111,    111,    111,    111,    113,    111,    111,    111,    111,    111,    111,    111,    111,    111,    111,    111,    111,    111,    111,    111,    111,    111,    111,    111,    111,    111,    111,    111,    111 },
        {112,   112,    112,    112,    112,    112,    112,    112,    114,    112,    112,    112,    112,    112,    112,    112,    112,    112,    112,    112,    112,    112,    112,    112,    112,    112,    112,    112,    112,    112,    112,    112,    112 },
        {501,   501,    501,    501,    501,    501,    501,    501,    501,    501,    118,    501,    501,    501,    501,    501,    501,    501,    501,    501,    501,    501,    501,    501,    501,    501,    501,    501,    501,    501,    501,    501,    501 },
        {502,   502,    502,    502,    502,    502,    502,    502,    502,    502,    502,    117,    502,    502,    502,    502,    502,    502,    502,    502,    502,    502,    502,    502,    502,    502,    502,    502,    502,    502,    502,    502,    502 },
        {130,   130,    130,    130,    130,    130,    130,    130,    115,    130,    130,    130,    130,    130,    130,    130,    130,    130,    130,    130,    130,    130,    130,    130,    130,    130,    130,    130,    130,    130,    130,    130,    130 },
        {13,    13,     119,    119,    119,    119,    119,    119,    119,    119,    119,    119,    119,    119,    119,    119,    119,    119,    119,    119,    119,    119,    119,    119,    119,    119,    119,    119,    119,    119,    119,    119,    119 },
        {14,    14,     14,     14,     14,     14,     14,     14,     14,     14,     14,     14,     14,     14,     14,     14,     14,     14,     120,    14,     14,     14,     14,     14,     14,     14,     14,     14,     14,     503,     503,    14,     14  },
        {16,    16,     16,     16,     16,     16,     16,     16,     16,     16,     16,     16,     16,     16,     16,     16,     16,     16,     16,     16,     16,     16,     16,     16,     16,     16,     16,     16,     16,     16,     506,    16,     16  },
        {507,   507,    507,    507,    507,    507,    507,    507,    507,    507,    507,    507,    507,    507,    507,    507,    507,    507,    507,    121,    507,    507,    507,    507,    507,    507,    507,    507,    507,    507,    507,    507,    507 },
        {122,   17,     122,    122,    122,    122,    122,    122,    122,    122,    122,    122,    18,     122,    122,    122,    122,    122,    122,    122,    122,    122,    122,    122,    122,    122,    122,    122,    122,    122,    122,    122,    122 },
        {504,   19,     504,    504,    504,    504,    504,    504,    504,    504,    504,    504,    504,    504,    504,    504,    504,    504,    504,    504,    504,    504,    504,    504,    504,    504,    504,    504,    504,    504,    504,    504,    504 },
        {123,   19,     123,    123,    123,    123,    123,    123,    123,    123,    123,    123,    123,    123,    123,    123,    123,    123,    123,    123,    123,    123,    123,    123,    123,    123,    123,    123,    123,    123,    123,    123,    123 }
        };
        string[] matrizErrores = new string[]{
            "Fin de comentario inesperado",
             "Se espera un &" ,
             "Se espera un |" ,
             "Se espera fin de cadena" ,
             "Se espera un digito" ,
             "Caracter no valido" 
        };
        string[,] matrizReservadas = new string[,]
        {
            { "main",       "200" },
            { "int",        "201" },
            { "float",      "202" },
            { "char",       "203" },
            { "do",         "204" },
            { "while",      "205" },
            { "for",        "206" },
            { "if",         "207" },
            { "else",       "208" },
            { "getchar",    "209" },
            { "putchar",    "210" },
            { "scanf",      "211" },
            { "printf",     "212" },
            { "string",     "213" },
            { "boolean",    "214" },
            { "true",       "215" },
            { "false",      "216" }
        };
        public int estado = 0;
        public int columna;
        public Nodo cabeza = null;
        public Nodo p = null;
        public string error;
        public int contador = 0;
        public string nodoLista;
        Boolean esPalabraReservada = false;

        public void CrearNodoNuevo(String lexema, int token, int renglon)
        {
            Nodo nuevo = new Nodo(lexema, token, renglon);
            if (cabeza == null)
            {
                cabeza = nuevo;
                p = nuevo;
            }
            else
            {
                p.sig = nuevo;
                p = nuevo;
            }
        }

        public void LeerArchivo()
        {
            string texto = File.ReadAllText(@"C:\Users\DELL\source\repos\Compi\Compi\ejemplo.cpc");
            texto = texto + (char)3;
            Procedimiento(texto);
            
        }

        public void Procedimiento(string buffer)
        {
            char[] caracterArreglo = buffer.ToCharArray();
            for (int i = 0; i <= buffer.Length - 1; i++)
            {
                if (Char.IsLetter(caracterArreglo[i])) columna = 0;
                else if (Char.IsDigit(caracterArreglo[i])) columna = 1;
                else
                {
                    switch (caracterArreglo[i])
                    {
                        case '+':
                            columna = 2;
                            break;

                        case '-':
                            columna = 3;
                            break;

                        case '*':
                            columna = 4;
                            break;

                        case '/':
                            columna = 5;
                            break;

                        case '>':
                            columna = 6;
                            break;

                        case '<':
                            columna = 7;
                            break;

                        case '=':
                            columna = 8;
                            break;

                        case '!':
                            columna = 9;
                            break;

                        case '&':
                            columna = 10;
                            break;

                        case '|':
                            columna = 11;
                            break;

                        case '.':
                            columna = 12;
                            break;

                        case ',':
                            columna = 13;
                            break;

                        case ';':
                            columna = 14;
                            break;

                        case '?':
                            columna = 15;
                            break;

                        case '#':
                            columna = 16;
                            break;

                        case '$':
                            columna = 17;
                            break;

                        case '\"':
                            columna = 18;
                            break;

                        case '\'':
                            columna = 19;
                            break;

                        case '(':
                            columna = 20;
                            break;

                        case ')':
                            columna = 21;
                            break;

                        case '{':
                            columna = 22;
                            break;

                        case '}':
                            columna = 23;
                            break;

                        case '[':
                            columna = 24;
                            break;

                        case ']':
                            columna = 25;
                            break;

                        case ':':
                            columna = 26;
                            break;

                        case ' ':
                            columna = 27;
                            break;

                        case '\t':
                            columna = 28;
                            break;

                        case '\n':
                            columna = 29;
                            renglon++;
                            break;

                        case (char)3:
                            columna = 30;
                            break;

                        case '\r':
                            columna = 32;
                            break;

                        default:
                            columna = 31;
                            break;
                    }
                }
                valormt = matriz[estado, columna];
                if (valormt < 100)
                {
                    estado = valormt;
                    if (caracterArreglo[i] != '\r' && caracterArreglo[i] != '\n' && caracterArreglo[i] != '\t' && (caracterArreglo[i] != ' ' || valormt == 14))
                    lexema = lexema + caracterArreglo[i];
                    if (lexema.Contains("*/")) lexema = string.Empty;       // Si es comentario se quita del lexema
                }
                else if (valormt >= 100 && valormt < 500)
                {
                    if (valormt == 119 && caracterArreglo[i] != ' ') i--;
                    if (valormt == 129) lexema = ";";
                    if (valormt == 128) lexema = ",";
                    if (valormt == 124) lexema = "(";
                    if (valormt == 125) lexema = ")";
                    if (valormt == 126) lexema = "{";
                    if (valormt == 127) lexema = "}";
                    if (valormt == 106) lexema = "+=";
                    if (valormt == 109) lexema = "++";
                    if (valormt == 105) lexema = "-=";
                    if (valormt == 110) lexema = "--";
                    if (valormt == 107) lexema = "*=";
                    if (valormt == 108) lexema = "/=";
                    if (valormt == 113) lexema = "<=";
                    if (valormt == 114) lexema = ">=";
                    if (valormt == 115) lexema = "!=";
                    if (valormt == 117) lexema = "||";
                    if (valormt == 118) lexema = "&&";
                    if (valormt == 120) lexema += "\"";
                    if (valormt == 121) lexema += "\'";
                    if (valormt == 112)
                    {
                        lexema = ">";
                        i--;
                    }
                    if (valormt == 111)
                    {
                        lexema = "<";
                        i--;
                    }
                    if(valormt == 100)
                    {
                        lexema = "+";
                        i--;
                    }
                    if (valormt == 101)
                    {
                        lexema = "-";
                        i--;
                    }
                    if (valormt == 102)
                    {
                        lexema = "*";
                        i--;
                    }
                    if (valormt == 103)
                    {
                        lexema = "/";
                        i--;
                    }
                    if (valormt == 104)
                    {
                        lexema = "=";
                        i--;
                    }
                    if (valormt == 116)
                    {
                        lexema = "==";
                    }
                    if (valormt == 130)
                    {
                        lexema = "!";
                        i--;
                    }
                    if (valormt == 122)
                    {
                        i--;
                    }
                    if (valormt == 123)
                    {
                        i--;
                    }
                    for (int x = 0; x <= matrizReservadas.GetLength(0) - 1; x++)
                    {
                        if (lexema == matrizReservadas[x, 0])
                        {
                            token = Convert.ToInt32(matrizReservadas[x, 1]);
                            CrearNodoNuevo(lexema, token, renglon);
                            esPalabraReservada = true;
                            break;
                        }
                        else
                        {
                            esPalabraReservada = false;
                        }
                    }
                    
                    if(esPalabraReservada == false)
                    {
                        CrearNodoNuevo(lexema, valormt, renglon);
                    }

                    if ((caracterArreglo[i] == ';' || caracterArreglo[i] == ',' || caracterArreglo[i] == '(' || caracterArreglo[i] == ')' || caracterArreglo[i] == '{' || caracterArreglo[i] == '}') && lexema != caracterArreglo[i].ToString())
                    {
                        i--;
                    }
                    
                    lexema = "";
                    estado = 0;


                }
                else
                {
                    if (valormt == 500)
                    {
                        error = "Error " + valormt + " en el renglon " + renglon + ": " + matrizErrores[0];
                        break;

                    }
                    if (valormt == 501)
                    {
                        error = "Error " + valormt + " en el renglon " + renglon + ": " + matrizErrores[1];
                        break;

                    }
                    if (valormt == 502)
                    {
                        error = "Error " + valormt + " en el renglon " + renglon + ": " + matrizErrores[2];
                        break;

                    }
                    if (valormt == 503)
                    {
                        error = "Error " + valormt + " en el renglon " + renglon + ": " + matrizErrores[3];
                        break;

                    }
                    if (valormt == 504)
                    {
                        error = "Error " + valormt + " en el renglon " + renglon + ": " + matrizErrores[4];
                        break;

                    }
                    if (valormt == 505)
                    {
                        error = "Error " + valormt + " en el renglon " + renglon + ": " + matrizErrores[5];
                        break;

                    }

                }

                if (error != null)
                {
                    return;
                }
            }

        }
    }
}
