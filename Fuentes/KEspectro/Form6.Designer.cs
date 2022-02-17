namespace KEspectro
{
    partial class Form6
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
            this.Salvar = new System.Windows.Forms.Button();
            this.Lienzo = new System.Windows.Forms.PictureBox();
            this.Inicio = new System.Windows.Forms.Button();
            this.Final = new System.Windows.Forms.Button();
            this.Atras = new System.Windows.Forms.Button();
            this.Adelante = new System.Windows.Forms.Button();
            this.IndiceMaximo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.IndiceActual = new System.Windows.Forms.Label();
            this.NumEspectros = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Todos = new System.Windows.Forms.CheckBox();
            this.QuitarUnicos = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.V_n_histogramas = new System.Windows.Forms.TextBox();
            this.ActHistogramas = new System.Windows.Forms.Button();
            this.V_valores = new System.Windows.Forms.CheckBox();
            this.Superponer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.V_horquilla = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Lienzo)).BeginInit();
            this.SuspendLayout();
            // 
            // Salvar
            // 
            this.Salvar.Location = new System.Drawing.Point(1615, 7);
            this.Salvar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Salvar.Name = "Salvar";
            this.Salvar.Size = new System.Drawing.Size(81, 32);
            this.Salvar.TabIndex = 134;
            this.Salvar.Text = "Salvar";
            this.Salvar.UseVisualStyleBackColor = true;
            this.Salvar.Click += new System.EventHandler(this.Salvar_Click);
            // 
            // Lienzo
            // 
            this.Lienzo.Location = new System.Drawing.Point(9, 47);
            this.Lienzo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Lienzo.Name = "Lienzo";
            this.Lienzo.Size = new System.Drawing.Size(946, 494);
            this.Lienzo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Lienzo.TabIndex = 122;
            this.Lienzo.TabStop = false;
            // 
            // Inicio
            // 
            this.Inicio.Location = new System.Drawing.Point(9, 7);
            this.Inicio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Inicio.Name = "Inicio";
            this.Inicio.Size = new System.Drawing.Size(43, 32);
            this.Inicio.TabIndex = 123;
            this.Inicio.Text = "<<";
            this.Inicio.UseVisualStyleBackColor = true;
            this.Inicio.Click += new System.EventHandler(this.Inicio_Click);
            // 
            // Final
            // 
            this.Final.Location = new System.Drawing.Point(165, 7);
            this.Final.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Final.Name = "Final";
            this.Final.Size = new System.Drawing.Size(43, 32);
            this.Final.TabIndex = 124;
            this.Final.Text = ">>";
            this.Final.UseVisualStyleBackColor = true;
            this.Final.Click += new System.EventHandler(this.Final_Click);
            // 
            // Atras
            // 
            this.Atras.Location = new System.Drawing.Point(61, 7);
            this.Atras.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Atras.Name = "Atras";
            this.Atras.Size = new System.Drawing.Size(43, 32);
            this.Atras.TabIndex = 125;
            this.Atras.Text = "<";
            this.Atras.UseVisualStyleBackColor = true;
            this.Atras.Click += new System.EventHandler(this.Atras_Click);
            // 
            // Adelante
            // 
            this.Adelante.Location = new System.Drawing.Point(113, 7);
            this.Adelante.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Adelante.Name = "Adelante";
            this.Adelante.Size = new System.Drawing.Size(43, 32);
            this.Adelante.TabIndex = 126;
            this.Adelante.Text = ">";
            this.Adelante.UseVisualStyleBackColor = true;
            this.Adelante.Click += new System.EventHandler(this.Adelante_Click);
            // 
            // IndiceMaximo
            // 
            this.IndiceMaximo.BackColor = System.Drawing.Color.White;
            this.IndiceMaximo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.IndiceMaximo.Location = new System.Drawing.Point(322, 12);
            this.IndiceMaximo.Name = "IndiceMaximo";
            this.IndiceMaximo.Size = new System.Drawing.Size(66, 21);
            this.IndiceMaximo.TabIndex = 127;
            this.IndiceMaximo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(290, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 20);
            this.label2.TabIndex = 128;
            this.label2.Text = "de";
            // 
            // IndiceActual
            // 
            this.IndiceActual.BackColor = System.Drawing.Color.White;
            this.IndiceActual.ForeColor = System.Drawing.SystemColors.ControlText;
            this.IndiceActual.Location = new System.Drawing.Point(214, 12);
            this.IndiceActual.Name = "IndiceActual";
            this.IndiceActual.Size = new System.Drawing.Size(66, 21);
            this.IndiceActual.TabIndex = 129;
            this.IndiceActual.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NumEspectros
            // 
            this.NumEspectros.BackColor = System.Drawing.Color.White;
            this.NumEspectros.ForeColor = System.Drawing.SystemColors.ControlText;
            this.NumEspectros.Location = new System.Drawing.Point(486, 12);
            this.NumEspectros.Name = "NumEspectros";
            this.NumEspectros.Size = new System.Drawing.Size(73, 21);
            this.NumEspectros.TabIndex = 130;
            this.NumEspectros.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(406, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 20);
            this.label4.TabIndex = 131;
            this.label4.Text = "espectros";
            // 
            // Todos
            // 
            this.Todos.AutoSize = true;
            this.Todos.Location = new System.Drawing.Point(607, 12);
            this.Todos.Name = "Todos";
            this.Todos.Size = new System.Drawing.Size(71, 24);
            this.Todos.TabIndex = 132;
            this.Todos.Text = "Todos";
            this.Todos.UseVisualStyleBackColor = true;
            this.Todos.CheckedChanged += new System.EventHandler(this.Todos_CheckedChanged);
            // 
            // QuitarUnicos
            // 
            this.QuitarUnicos.AutoSize = true;
            this.QuitarUnicos.Location = new System.Drawing.Point(711, 12);
            this.QuitarUnicos.Name = "QuitarUnicos";
            this.QuitarUnicos.Size = new System.Drawing.Size(223, 24);
            this.QuitarUnicos.TabIndex = 133;
            this.QuitarUnicos.Text = "Quitar grupos con 1 espectro";
            this.QuitarUnicos.UseVisualStyleBackColor = true;
            this.QuitarUnicos.CheckedChanged += new System.EventHandler(this.QuitarUnicos_CheckedChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(1144, 13);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(169, 20);
            this.label19.TabIndex = 136;
            this.label19.Text = "Número de histogramas";
            // 
            // V_n_histogramas
            // 
            this.V_n_histogramas.Location = new System.Drawing.Point(1326, 10);
            this.V_n_histogramas.Name = "V_n_histogramas";
            this.V_n_histogramas.Size = new System.Drawing.Size(38, 27);
            this.V_n_histogramas.TabIndex = 135;
            this.V_n_histogramas.Text = "50";
            this.V_n_histogramas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ActHistogramas
            // 
            this.ActHistogramas.Location = new System.Drawing.Point(1376, 7);
            this.ActHistogramas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ActHistogramas.Name = "ActHistogramas";
            this.ActHistogramas.Size = new System.Drawing.Size(105, 32);
            this.ActHistogramas.TabIndex = 137;
            this.ActHistogramas.Text = "Actualizar";
            this.ActHistogramas.UseVisualStyleBackColor = true;
            this.ActHistogramas.Click += new System.EventHandler(this.ActHistogramas_Click);
            // 
            // V_valores
            // 
            this.V_valores.AutoSize = true;
            this.V_valores.Checked = true;
            this.V_valores.CheckState = System.Windows.Forms.CheckState.Checked;
            this.V_valores.Location = new System.Drawing.Point(1507, 15);
            this.V_valores.Name = "V_valores";
            this.V_valores.Size = new System.Drawing.Size(79, 24);
            this.V_valores.TabIndex = 138;
            this.V_valores.Text = "Valores";
            this.V_valores.UseVisualStyleBackColor = true;
            this.V_valores.CheckedChanged += new System.EventHandler(this.V_valores_CheckedChanged);
            // 
            // Superponer
            // 
            this.Superponer.Location = new System.Drawing.Point(1748, 7);
            this.Superponer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Superponer.Name = "Superponer";
            this.Superponer.Size = new System.Drawing.Size(105, 32);
            this.Superponer.TabIndex = 139;
            this.Superponer.Text = "Superponer";
            this.Superponer.UseVisualStyleBackColor = true;
            this.Superponer.Click += new System.EventHandler(this.Superponer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(954, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 140;
            this.label1.Text = "horquilla";
            // 
            // V_horquilla
            // 
            this.V_horquilla.Location = new System.Drawing.Point(1028, 10);
            this.V_horquilla.Name = "V_horquilla";
            this.V_horquilla.Size = new System.Drawing.Size(38, 27);
            this.V_horquilla.TabIndex = 141;
            this.V_horquilla.Text = "0";
            this.V_horquilla.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1075, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 142;
            this.label3.Text = "sigma";
            // 
            // Form6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1869, 560);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.V_horquilla);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Superponer);
            this.Controls.Add(this.V_valores);
            this.Controls.Add(this.ActHistogramas);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.V_n_histogramas);
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
            this.Name = "Form6";
            this.Text = "Form6";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.Lienzo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button Salvar;
        private PictureBox Lienzo;
        public Button Inicio;
        public Button Final;
        public Button Atras;
        public Button Adelante;
        public Label IndiceMaximo;
        private Label label2;
        public Label IndiceActual;
        public Label NumEspectros;
        private Label label4;
        public CheckBox Todos;
        private CheckBox QuitarUnicos;
        private Label label19;
        private TextBox V_n_histogramas;
        private Button ActHistogramas;
        public CheckBox V_valores;
        private Button Superponer;
        private Label label1;
        private TextBox V_horquilla;
        private Label label3;
    }
}