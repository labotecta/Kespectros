namespace KEspectro
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            this.IndiceActual = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.IndiceMaximo = new System.Windows.Forms.Label();
            this.Adelante = new System.Windows.Forms.Button();
            this.Atras = new System.Windows.Forms.Button();
            this.Final = new System.Windows.Forms.Button();
            this.Inicio = new System.Windows.Forms.Button();
            this.Lienzo = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.NumEspectros = new System.Windows.Forms.Label();
            this.Todos = new System.Windows.Forms.CheckBox();
            this.QuitarUnicos = new System.Windows.Forms.CheckBox();
            this.Salvar = new System.Windows.Forms.Button();
            this.b_limpiar = new System.Windows.Forms.Button();
            this.lista_elegibles = new System.Windows.Forms.ComboBox();
            this.lista_elegidas = new System.Windows.Forms.ComboBox();
            this.p_raton_y = new System.Windows.Forms.Label();
            this.p_raton_x = new System.Windows.Forms.Label();
            this.raton_x = new System.Windows.Forms.Label();
            this.b_e = new System.Windows.Forms.Button();
            this.r_i = new System.Windows.Forms.Label();
            this.v_i = new System.Windows.Forms.TextBox();
            this.v_e = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LeyendaIzq = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.Lienzo)).BeginInit();
            this.SuspendLayout();
            // 
            // IndiceActual
            // 
            this.IndiceActual.BackColor = System.Drawing.Color.White;
            this.IndiceActual.ForeColor = System.Drawing.SystemColors.ControlText;
            this.IndiceActual.Location = new System.Drawing.Point(217, 13);
            this.IndiceActual.Name = "IndiceActual";
            this.IndiceActual.Size = new System.Drawing.Size(66, 21);
            this.IndiceActual.TabIndex = 108;
            this.IndiceActual.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(293, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 20);
            this.label2.TabIndex = 107;
            this.label2.Text = "de";
            // 
            // IndiceMaximo
            // 
            this.IndiceMaximo.BackColor = System.Drawing.Color.White;
            this.IndiceMaximo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.IndiceMaximo.Location = new System.Drawing.Point(325, 13);
            this.IndiceMaximo.Name = "IndiceMaximo";
            this.IndiceMaximo.Size = new System.Drawing.Size(66, 21);
            this.IndiceMaximo.TabIndex = 106;
            this.IndiceMaximo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Adelante
            // 
            this.Adelante.Location = new System.Drawing.Point(116, 8);
            this.Adelante.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Adelante.Name = "Adelante";
            this.Adelante.Size = new System.Drawing.Size(43, 32);
            this.Adelante.TabIndex = 102;
            this.Adelante.Text = ">";
            this.Adelante.UseVisualStyleBackColor = true;
            this.Adelante.Click += new System.EventHandler(this.Adelante_Click);
            // 
            // Atras
            // 
            this.Atras.Location = new System.Drawing.Point(64, 8);
            this.Atras.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Atras.Name = "Atras";
            this.Atras.Size = new System.Drawing.Size(43, 32);
            this.Atras.TabIndex = 101;
            this.Atras.Text = "<";
            this.Atras.UseVisualStyleBackColor = true;
            this.Atras.Click += new System.EventHandler(this.Atras_Click);
            // 
            // Final
            // 
            this.Final.Location = new System.Drawing.Point(168, 8);
            this.Final.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Final.Name = "Final";
            this.Final.Size = new System.Drawing.Size(43, 32);
            this.Final.TabIndex = 100;
            this.Final.Text = ">>";
            this.Final.UseVisualStyleBackColor = true;
            this.Final.Click += new System.EventHandler(this.Final_Click);
            // 
            // Inicio
            // 
            this.Inicio.Location = new System.Drawing.Point(12, 8);
            this.Inicio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Inicio.Name = "Inicio";
            this.Inicio.Size = new System.Drawing.Size(43, 32);
            this.Inicio.TabIndex = 99;
            this.Inicio.Text = "<<";
            this.Inicio.UseVisualStyleBackColor = true;
            this.Inicio.Click += new System.EventHandler(this.Inicio_Click);
            // 
            // Lienzo
            // 
            this.Lienzo.Location = new System.Drawing.Point(110, 48);
            this.Lienzo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Lienzo.Name = "Lienzo";
            this.Lienzo.Size = new System.Drawing.Size(946, 494);
            this.Lienzo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Lienzo.TabIndex = 98;
            this.Lienzo.TabStop = false;
            this.Lienzo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Lienzo_MouseDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(409, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 20);
            this.label4.TabIndex = 110;
            this.label4.Text = "espectros";
            // 
            // NumEspectros
            // 
            this.NumEspectros.BackColor = System.Drawing.Color.White;
            this.NumEspectros.ForeColor = System.Drawing.SystemColors.ControlText;
            this.NumEspectros.Location = new System.Drawing.Point(489, 13);
            this.NumEspectros.Name = "NumEspectros";
            this.NumEspectros.Size = new System.Drawing.Size(73, 21);
            this.NumEspectros.TabIndex = 109;
            this.NumEspectros.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Todos
            // 
            this.Todos.AutoSize = true;
            this.Todos.Location = new System.Drawing.Point(609, 13);
            this.Todos.Name = "Todos";
            this.Todos.Size = new System.Drawing.Size(71, 24);
            this.Todos.TabIndex = 111;
            this.Todos.Text = "Todos";
            this.Todos.UseVisualStyleBackColor = true;
            this.Todos.CheckedChanged += new System.EventHandler(this.Todos_CheckedChanged);
            // 
            // QuitarUnicos
            // 
            this.QuitarUnicos.AutoSize = true;
            this.QuitarUnicos.Location = new System.Drawing.Point(697, 13);
            this.QuitarUnicos.Name = "QuitarUnicos";
            this.QuitarUnicos.Size = new System.Drawing.Size(246, 24);
            this.QuitarUnicos.TabIndex = 112;
            this.QuitarUnicos.Text = "Quitar centroides con 1 espectro";
            this.QuitarUnicos.UseVisualStyleBackColor = true;
            this.QuitarUnicos.CheckedChanged += new System.EventHandler(this.QuitarUnicos_CheckedChanged);
            // 
            // Salvar
            // 
            this.Salvar.Location = new System.Drawing.Point(1122, 8);
            this.Salvar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Salvar.Name = "Salvar";
            this.Salvar.Size = new System.Drawing.Size(81, 32);
            this.Salvar.TabIndex = 113;
            this.Salvar.Text = "Salvar";
            this.Salvar.UseVisualStyleBackColor = true;
            this.Salvar.Click += new System.EventHandler(this.Salvar_Click);
            // 
            // b_limpiar
            // 
            this.b_limpiar.Image = ((System.Drawing.Image)(resources.GetObject("b_limpiar.Image")));
            this.b_limpiar.Location = new System.Drawing.Point(1933, 4);
            this.b_limpiar.Name = "b_limpiar";
            this.b_limpiar.Size = new System.Drawing.Size(42, 42);
            this.b_limpiar.TabIndex = 117;
            this.b_limpiar.UseVisualStyleBackColor = true;
            this.b_limpiar.Click += new System.EventHandler(this.B_limpiar_Click);
            // 
            // lista_elegibles
            // 
            this.lista_elegibles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lista_elegibles.Location = new System.Drawing.Point(1549, 11);
            this.lista_elegibles.Name = "lista_elegibles";
            this.lista_elegibles.Size = new System.Drawing.Size(180, 28);
            this.lista_elegibles.TabIndex = 115;
            this.lista_elegibles.SelectedIndexChanged += new System.EventHandler(this.Lista_elegibles_SelectedIndexChanged);
            // 
            // lista_elegidas
            // 
            this.lista_elegidas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lista_elegidas.Location = new System.Drawing.Point(1736, 11);
            this.lista_elegidas.Name = "lista_elegidas";
            this.lista_elegidas.Size = new System.Drawing.Size(180, 28);
            this.lista_elegidas.TabIndex = 114;
            this.lista_elegidas.SelectedIndexChanged += new System.EventHandler(this.Lista_elegidas_SelectedIndexChanged);
            this.lista_elegidas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Lista_elegidas_MouseDown);
            // 
            // p_raton_y
            // 
            this.p_raton_y.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.p_raton_y.ForeColor = System.Drawing.Color.Green;
            this.p_raton_y.Location = new System.Drawing.Point(12, 90);
            this.p_raton_y.Name = "p_raton_y";
            this.p_raton_y.Size = new System.Drawing.Size(92, 20);
            this.p_raton_y.TabIndex = 121;
            this.p_raton_y.Text = "0";
            this.p_raton_y.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // p_raton_x
            // 
            this.p_raton_x.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.p_raton_x.ForeColor = System.Drawing.Color.Green;
            this.p_raton_x.Location = new System.Drawing.Point(12, 60);
            this.p_raton_x.Name = "p_raton_x";
            this.p_raton_x.Size = new System.Drawing.Size(92, 20);
            this.p_raton_x.TabIndex = 120;
            this.p_raton_x.Text = "0";
            this.p_raton_x.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // raton_x
            // 
            this.raton_x.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.raton_x.ForeColor = System.Drawing.Color.Green;
            this.raton_x.Location = new System.Drawing.Point(12, 120);
            this.raton_x.Name = "raton_x";
            this.raton_x.Size = new System.Drawing.Size(92, 20);
            this.raton_x.TabIndex = 118;
            this.raton_x.Text = "0";
            this.raton_x.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // b_e
            // 
            this.b_e.Location = new System.Drawing.Point(1483, 8);
            this.b_e.Name = "b_e";
            this.b_e.Size = new System.Drawing.Size(59, 32);
            this.b_e.TabIndex = 125;
            this.b_e.Text = "Sel";
            this.b_e.UseVisualStyleBackColor = true;
            this.b_e.Click += new System.EventHandler(this.B_e_Click);
            // 
            // r_i
            // 
            this.r_i.AutoSize = true;
            this.r_i.Location = new System.Drawing.Point(1213, 13);
            this.r_i.Name = "r_i";
            this.r_i.Size = new System.Drawing.Size(78, 20);
            this.r_i.TabIndex = 124;
            this.r_i.Text = "Intensidad";
            this.r_i.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // v_i
            // 
            this.v_i.Location = new System.Drawing.Point(1303, 9);
            this.v_i.Name = "v_i";
            this.v_i.Size = new System.Drawing.Size(53, 27);
            this.v_i.TabIndex = 123;
            this.v_i.Text = "50000";
            this.v_i.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // v_e
            // 
            this.v_e.Location = new System.Drawing.Point(1443, 9);
            this.v_e.Name = "v_e";
            this.v_e.Size = new System.Drawing.Size(34, 27);
            this.v_e.TabIndex = 122;
            this.v_e.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1363, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 126;
            this.label1.Text = "Elemento";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LeyendaIzq
            // 
            this.LeyendaIzq.AutoSize = true;
            this.LeyendaIzq.Checked = true;
            this.LeyendaIzq.CheckState = System.Windows.Forms.CheckState.Checked;
            this.LeyendaIzq.Location = new System.Drawing.Point(955, 13);
            this.LeyendaIzq.Name = "LeyendaIzq";
            this.LeyendaIzq.Size = new System.Drawing.Size(152, 24);
            this.LeyendaIzq.TabIndex = 134;
            this.LeyendaIzq.Text = "Leyenda izquierda";
            this.LeyendaIzq.UseVisualStyleBackColor = true;
            this.LeyendaIzq.CheckedChanged += new System.EventHandler(this.LeyendaIzq_CheckedChanged);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1989, 554);
            this.Controls.Add(this.LeyendaIzq);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.b_e);
            this.Controls.Add(this.r_i);
            this.Controls.Add(this.v_i);
            this.Controls.Add(this.v_e);
            this.Controls.Add(this.p_raton_y);
            this.Controls.Add(this.p_raton_x);
            this.Controls.Add(this.raton_x);
            this.Controls.Add(this.b_limpiar);
            this.Controls.Add(this.lista_elegibles);
            this.Controls.Add(this.lista_elegidas);
            this.Controls.Add(this.Salvar);
            this.Controls.Add(this.QuitarUnicos);
            this.Controls.Add(this.Todos);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.NumEspectros);
            this.Controls.Add(this.IndiceActual);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IndiceMaximo);
            this.Controls.Add(this.Adelante);
            this.Controls.Add(this.Atras);
            this.Controls.Add(this.Final);
            this.Controls.Add(this.Inicio);
            this.Controls.Add(this.Lienzo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form4";
            this.Text = "Centroides";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Lienzo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Label IndiceActual;
        private Label label2;
        public Label IndiceMaximo;
        public Button Adelante;
        public Button Atras;
        public Button Final;
        public Button Inicio;
        private PictureBox Lienzo;
        private Label label4;
        public Label NumEspectros;
        private CheckBox Todos;
        private CheckBox QuitarUnicos;
        private Button Salvar;
        public Button b_limpiar;
        public ComboBox lista_elegibles;
        public ComboBox lista_elegidas;
        public Label p_raton_y;
        public Label p_raton_x;
        public Label raton_x;
        public Button b_e;
        private Label r_i;
        public TextBox v_i;
        public TextBox v_e;
        private Label label1;
        private CheckBox LeyendaIzq;
    }
}