using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compi
{
    public partial class Form1 : Form
    {
        public Lexico lexi = new Lexico();
        public Sintactico sint;
        public Ensamblador2 ens;
        NodoVariable variables;
        NodoPolish polish;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLexico_Click(object sender, EventArgs e)
        {
            

            lexi.LeerArchivo();

            if(lexi.error != null)
            {
                MessageBox.Show(lexi.error);
            }
            else
            {
                lexi.p = lexi.cabeza;
                while (lexi.p != null)
                {
                    dgvNodo.Rows.Add(lexi.p.lexema, lexi.p.token, lexi.p.renglon);
                    lexi.p = lexi.p.sig;
                }
                lexi.p = lexi.cabeza;
            }
         
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Lexico lexi = new Lexico();
        }

        public void btnSintactico_Click(object sender, EventArgs e)
        {
            sint = new Sintactico(lexi.p);
            sint.ProcSintactico();
            if(sint.error != null)
            {
                MessageBox.Show(sint.error);
            }
            else
            {
                MessageBox.Show("No se encontraron errores de sintaxis");
            }
            if(sint.errorSemantico != null)
            {
                MessageBox.Show(sint.errorSemantico);
            }
            else
            {
                MessageBox.Show("No se encontraron errores de semantica");
            }
            if(sint.error == null && sint.errorSemantico == null)
            {
                string cadenaPolish = "";
                NodoPolish listaPolish = sint.GetPolish();
                while(listaPolish != null)
                {
                    cadenaPolish = cadenaPolish + listaPolish.lexema + " ";
                    listaPolish = listaPolish.sig;
                }
                
                listaPolish = sint.GetPolish();
                variables = sint.cabeza;
                polish = sint.GetPolish();
                frmPolish ventanaPolish = new frmPolish(listaPolish);
                ventanaPolish.Show();
            }
        }

        private void btnEnsamblador_Click(object sender, EventArgs e)
        {
            ens = new Ensamblador2(variables, polish);
            ens.Ensamblation();
        }
    }
}
