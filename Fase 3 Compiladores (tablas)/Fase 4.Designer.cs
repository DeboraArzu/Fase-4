namespace Fase_3_Compiladores__tablas_
{
    partial class Compilador
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Compilador));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cargarArchivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rchtb_Texto = new System.Windows.Forms.RichTextBox();
            this.tabresultados = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tokenstabla = new System.Windows.Forms.DataGridView();
            this.Num_Token = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precedencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Asosiatividad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.noterminalesdg = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dproductions = new System.Windows.Forms.DataGridView();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.tabresultados.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tokenstabla)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.noterminalesdg)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dproductions)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cargarArchivoToolStripMenuItem,
            this.analizarToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1253, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cargarArchivoToolStripMenuItem
            // 
            this.cargarArchivoToolStripMenuItem.Name = "cargarArchivoToolStripMenuItem";
            this.cargarArchivoToolStripMenuItem.Size = new System.Drawing.Size(111, 21);
            this.cargarArchivoToolStripMenuItem.Text = "Cargar Archivo";
            this.cargarArchivoToolStripMenuItem.Click += new System.EventHandler(this.cARGARDOCUMENTOToolStripMenuItem_Click);
            // 
            // analizarToolStripMenuItem
            // 
            this.analizarToolStripMenuItem.Name = "analizarToolStripMenuItem";
            this.analizarToolStripMenuItem.Size = new System.Drawing.Size(70, 21);
            this.analizarToolStripMenuItem.Text = "Analizar";
            this.analizarToolStripMenuItem.Click += new System.EventHandler(this.aNALIZARToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(69, 21);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.guardarToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // rchtb_Texto
            // 
            this.rchtb_Texto.BackColor = System.Drawing.SystemColors.MenuText;
            this.rchtb_Texto.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rchtb_Texto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchtb_Texto.ForeColor = System.Drawing.Color.White;
            this.rchtb_Texto.Location = new System.Drawing.Point(0, 27);
            this.rchtb_Texto.Name = "rchtb_Texto";
            this.rchtb_Texto.Size = new System.Drawing.Size(618, 405);
            this.rchtb_Texto.TabIndex = 2;
            this.rchtb_Texto.Text = "";
            // 
            // tabresultados
            // 
            this.tabresultados.Controls.Add(this.tabPage1);
            this.tabresultados.Controls.Add(this.tabPage2);
            this.tabresultados.Controls.Add(this.tabPage3);
            this.tabresultados.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabresultados.Location = new System.Drawing.Point(624, 27);
            this.tabresultados.Name = "tabresultados";
            this.tabresultados.SelectedIndex = 0;
            this.tabresultados.Size = new System.Drawing.Size(452, 405);
            this.tabresultados.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Black;
            this.tabPage1.Controls.Add(this.tokenstabla);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(444, 375);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tabla Tokens";
            // 
            // tokenstabla
            // 
            this.tokenstabla.AllowUserToAddRows = false;
            this.tokenstabla.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tokenstabla.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tokenstabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tokenstabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Num_Token,
            this.ID,
            this.Precedencia,
            this.Asosiatividad});
            this.tokenstabla.EnableHeadersVisualStyles = false;
            this.tokenstabla.GridColor = System.Drawing.Color.Navy;
            this.tokenstabla.Location = new System.Drawing.Point(5, 5);
            this.tokenstabla.Margin = new System.Windows.Forms.Padding(2);
            this.tokenstabla.Name = "tokenstabla";
            this.tokenstabla.RowHeadersVisible = false;
            this.tokenstabla.RowTemplate.Height = 24;
            this.tokenstabla.Size = new System.Drawing.Size(442, 365);
            this.tokenstabla.TabIndex = 4;
            // 
            // Num_Token
            // 
            this.Num_Token.HeaderText = "Número";
            this.Num_Token.Name = "Num_Token";
            // 
            // ID
            // 
            this.ID.HeaderText = "Simbolo";
            this.ID.Name = "ID";
            // 
            // Precedencia
            // 
            this.Precedencia.HeaderText = "Precedencia";
            this.Precedencia.Name = "Precedencia";
            // 
            // Asosiatividad
            // 
            this.Asosiatividad.HeaderText = "Asosiatividad";
            this.Asosiatividad.Name = "Asosiatividad";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Black;
            this.tabPage2.Controls.Add(this.noterminalesdg);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(444, 375);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tabla No Terminal";
            // 
            // noterminalesdg
            // 
            this.noterminalesdg.AllowUserToAddRows = false;
            this.noterminalesdg.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.noterminalesdg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.noterminalesdg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.noterminalesdg.EnableHeadersVisualStyles = false;
            this.noterminalesdg.GridColor = System.Drawing.Color.Blue;
            this.noterminalesdg.Location = new System.Drawing.Point(5, 5);
            this.noterminalesdg.Margin = new System.Windows.Forms.Padding(2);
            this.noterminalesdg.Name = "noterminalesdg";
            this.noterminalesdg.RowHeadersVisible = false;
            this.noterminalesdg.RowTemplate.Height = 24;
            this.noterminalesdg.Size = new System.Drawing.Size(442, 287);
            this.noterminalesdg.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Black;
            this.tabPage3.Controls.Add(this.dproductions);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage3.Size = new System.Drawing.Size(444, 375);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Tabla Producciones";
            // 
            // dproductions
            // 
            this.dproductions.AllowUserToAddRows = false;
            this.dproductions.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dproductions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dproductions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.dproductions.EnableHeadersVisualStyles = false;
            this.dproductions.GridColor = System.Drawing.Color.Blue;
            this.dproductions.Location = new System.Drawing.Point(4, 4);
            this.dproductions.Margin = new System.Windows.Forms.Padding(2);
            this.dproductions.Name = "dproductions";
            this.dproductions.RowHeadersVisible = false;
            this.dproductions.RowTemplate.Height = 24;
            this.dproductions.Size = new System.Drawing.Size(425, 287);
            this.dproductions.TabIndex = 6;
            // 
            // btnGenerar
            // 
            this.btnGenerar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnGenerar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGenerar.Location = new System.Drawing.Point(1102, 133);
            this.btnGenerar.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(81, 36);
            this.btnGenerar.TabIndex = 10;
            this.btnGenerar.Text = "Generar .TOK";
            this.btnGenerar.UseVisualStyleBackColor = false;
            this.btnGenerar.Visible = false;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click_1);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.Link;
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnSave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSave.Location = new System.Drawing.Point(1102, 93);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 36);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Generar .DAT";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Numero";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "No Terminal";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Produccion";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "First";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Produccion";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Longitud";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Siguientes Producciones";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Elementos";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // Compilador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1253, 458);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabresultados);
            this.Controls.Add(this.rchtb_Texto);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Compilador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compiladores";
            this.Load += new System.EventHandler(this.Compilador_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabresultados.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tokenstabla)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.noterminalesdg)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dproductions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cargarArchivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.RichTextBox rchtb_Texto;
        private System.Windows.Forms.TabControl tabresultados;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView tokenstabla;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView noterminalesdg;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dproductions;
        private System.Windows.Forms.DataGridViewTextBoxColumn Num_Token;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precedencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn Asosiatividad;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    }
}

