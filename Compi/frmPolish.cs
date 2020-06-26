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
    public partial class frmPolish : Form
    {
        NodoPolish lista;
        public frmPolish(NodoPolish polish)
        {
            InitializeComponent();
            lista = polish;
        }

        private void frmPolish_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Lexema", typeof(string));
            dt.Columns.Add("Etiqueta", typeof(string));
            while (lista != null)
            {
                dt.Rows.Add(lista.lexema, lista.etiqueta);
                lista = lista.sig;
            }
            dgvPolish.DataSource = dt;
        }
    }
}
