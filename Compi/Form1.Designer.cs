namespace Compi
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnLexico = new System.Windows.Forms.Button();
            this.btnSintactico = new System.Windows.Forms.Button();
            this.dgvNodo = new System.Windows.Forms.DataGridView();
            this.dgvLexema = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvToken = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRenglon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEnsamblador = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNodo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLexico
            // 
            this.btnLexico.Location = new System.Drawing.Point(12, 45);
            this.btnLexico.Name = "btnLexico";
            this.btnLexico.Size = new System.Drawing.Size(75, 23);
            this.btnLexico.TabIndex = 0;
            this.btnLexico.Text = "Lexico";
            this.btnLexico.UseVisualStyleBackColor = true;
            this.btnLexico.Click += new System.EventHandler(this.btnLexico_Click);
            // 
            // btnSintactico
            // 
            this.btnSintactico.Location = new System.Drawing.Point(12, 90);
            this.btnSintactico.Name = "btnSintactico";
            this.btnSintactico.Size = new System.Drawing.Size(75, 23);
            this.btnSintactico.TabIndex = 1;
            this.btnSintactico.Text = "Sintactico";
            this.btnSintactico.UseVisualStyleBackColor = true;
            this.btnSintactico.Click += new System.EventHandler(this.btnSintactico_Click);
            // 
            // dgvNodo
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvNodo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNodo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvNodo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNodo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvNodo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNodo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvLexema,
            this.dgvToken,
            this.dgvRenglon});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNodo.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvNodo.Location = new System.Drawing.Point(144, 45);
            this.dgvNodo.Name = "dgvNodo";
            this.dgvNodo.Size = new System.Drawing.Size(517, 400);
            this.dgvNodo.TabIndex = 2;
            // 
            // dgvLexema
            // 
            this.dgvLexema.HeaderText = "Lexema";
            this.dgvLexema.Name = "dgvLexema";
            this.dgvLexema.Width = 166;
            // 
            // dgvToken
            // 
            this.dgvToken.HeaderText = "Token";
            this.dgvToken.Name = "dgvToken";
            this.dgvToken.Width = 138;
            // 
            // dgvRenglon
            // 
            this.dgvRenglon.HeaderText = "Renglon";
            this.dgvRenglon.Name = "dgvRenglon";
            this.dgvRenglon.Width = 170;
            // 
            // btnEnsamblador
            // 
            this.btnEnsamblador.Location = new System.Drawing.Point(12, 140);
            this.btnEnsamblador.Name = "btnEnsamblador";
            this.btnEnsamblador.Size = new System.Drawing.Size(88, 23);
            this.btnEnsamblador.TabIndex = 3;
            this.btnEnsamblador.Text = "Ensamblador";
            this.btnEnsamblador.UseVisualStyleBackColor = true;
            this.btnEnsamblador.Click += new System.EventHandler(this.btnEnsamblador_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnEnsamblador);
            this.Controls.Add(this.dgvNodo);
            this.Controls.Add(this.btnSintactico);
            this.Controls.Add(this.btnLexico);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNodo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLexico;
        private System.Windows.Forms.Button btnSintactico;
        private System.Windows.Forms.DataGridView dgvNodo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvLexema;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvToken;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRenglon;
        private System.Windows.Forms.Button btnEnsamblador;
    }
}

