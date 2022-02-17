namespace KEspectro
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.Lienzo = new System.Windows.Forms.PictureBox();
            this.Inicio = new System.Windows.Forms.Button();
            this.Final = new System.Windows.Forms.Button();
            this.Atras = new System.Windows.Forms.Button();
            this.Adelante = new System.Windows.Forms.Button();
            this.IndiceEspectro = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.R_distancia = new System.Windows.Forms.Label();
            this.IndiceActual = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.IndiceMaximo = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.R_confianza = new System.Windows.Forms.Label();
            this.R_clusterCercano = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Lienzo)).BeginInit();
            this.SuspendLayout();
            // 
            // Lienzo
            // 
            this.Lienzo.Location = new System.Drawing.Point(12, 54);
            this.Lienzo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Lienzo.Name = "Lienzo";
            this.Lienzo.Size = new System.Drawing.Size(946, 494);
            this.Lienzo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Lienzo.TabIndex = 0;
            this.Lienzo.TabStop = false;
            // 
            // Inicio
            // 
            this.Inicio.Location = new System.Drawing.Point(12, 15);
            this.Inicio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Inicio.Name = "Inicio";
            this.Inicio.Size = new System.Drawing.Size(43, 32);
            this.Inicio.TabIndex = 1;
            this.Inicio.Text = "<<";
            this.Inicio.UseVisualStyleBackColor = true;
            this.Inicio.Click += new System.EventHandler(this.Inicio_Click);
            // 
            // Final
            // 
            this.Final.Location = new System.Drawing.Point(168, 14);
            this.Final.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Final.Name = "Final";
            this.Final.Size = new System.Drawing.Size(43, 32);
            this.Final.TabIndex = 2;
            this.Final.Text = ">>";
            this.Final.UseVisualStyleBackColor = true;
            this.Final.Click += new System.EventHandler(this.Final_Click);
            // 
            // Atras
            // 
            this.Atras.Location = new System.Drawing.Point(64, 15);
            this.Atras.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Atras.Name = "Atras";
            this.Atras.Size = new System.Drawing.Size(43, 32);
            this.Atras.TabIndex = 3;
            this.Atras.Text = "<";
            this.Atras.UseVisualStyleBackColor = true;
            this.Atras.Click += new System.EventHandler(this.Atras_Click);
            // 
            // Adelante
            // 
            this.Adelante.Location = new System.Drawing.Point(116, 14);
            this.Adelante.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Adelante.Name = "Adelante";
            this.Adelante.Size = new System.Drawing.Size(43, 32);
            this.Adelante.TabIndex = 4;
            this.Adelante.Text = ">";
            this.Adelante.UseVisualStyleBackColor = true;
            this.Adelante.Click += new System.EventHandler(this.Adelante_Click);
            // 
            // IndiceEspectro
            // 
            this.IndiceEspectro.BackColor = System.Drawing.Color.White;
            this.IndiceEspectro.ForeColor = System.Drawing.SystemColors.ControlText;
            this.IndiceEspectro.Location = new System.Drawing.Point(511, 21);
            this.IndiceEspectro.Name = "IndiceEspectro";
            this.IndiceEspectro.Size = new System.Drawing.Size(73, 21);
            this.IndiceEspectro.TabIndex = 55;
            this.IndiceEspectro.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(593, 20);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 20);
            this.label14.TabIndex = 54;
            this.label14.Text = "Distancia";
            // 
            // R_distancia
            // 
            this.R_distancia.BackColor = System.Drawing.Color.White;
            this.R_distancia.ForeColor = System.Drawing.SystemColors.ControlText;
            this.R_distancia.Location = new System.Drawing.Point(670, 21);
            this.R_distancia.Name = "R_distancia";
            this.R_distancia.Size = new System.Drawing.Size(97, 21);
            this.R_distancia.TabIndex = 53;
            this.R_distancia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IndiceActual
            // 
            this.IndiceActual.BackColor = System.Drawing.Color.White;
            this.IndiceActual.ForeColor = System.Drawing.SystemColors.ControlText;
            this.IndiceActual.Location = new System.Drawing.Point(217, 21);
            this.IndiceActual.Name = "IndiceActual";
            this.IndiceActual.Size = new System.Drawing.Size(66, 21);
            this.IndiceActual.TabIndex = 58;
            this.IndiceActual.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(293, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 20);
            this.label2.TabIndex = 57;
            this.label2.Text = "de";
            // 
            // IndiceMaximo
            // 
            this.IndiceMaximo.BackColor = System.Drawing.Color.White;
            this.IndiceMaximo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.IndiceMaximo.Location = new System.Drawing.Point(325, 21);
            this.IndiceMaximo.Name = "IndiceMaximo";
            this.IndiceMaximo.Size = new System.Drawing.Size(66, 21);
            this.IndiceMaximo.TabIndex = 56;
            this.IndiceMaximo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(431, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 20);
            this.label4.TabIndex = 59;
            this.label4.Text = "espectro";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(779, 20);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(75, 20);
            this.label15.TabIndex = 90;
            this.label15.Text = "Confianza";
            // 
            // R_confianza
            // 
            this.R_confianza.BackColor = System.Drawing.Color.White;
            this.R_confianza.ForeColor = System.Drawing.SystemColors.ControlText;
            this.R_confianza.Location = new System.Drawing.Point(856, 21);
            this.R_confianza.Name = "R_confianza";
            this.R_confianza.Size = new System.Drawing.Size(60, 21);
            this.R_confianza.TabIndex = 89;
            this.R_confianza.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // R_clusterCercano
            // 
            this.R_clusterCercano.BackColor = System.Drawing.Color.White;
            this.R_clusterCercano.ForeColor = System.Drawing.SystemColors.ControlText;
            this.R_clusterCercano.Location = new System.Drawing.Point(938, 21);
            this.R_clusterCercano.Name = "R_clusterCercano";
            this.R_clusterCercano.Size = new System.Drawing.Size(50, 21);
            this.R_clusterCercano.TabIndex = 97;
            this.R_clusterCercano.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 562);
            this.Controls.Add(this.R_clusterCercano);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.R_confianza);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.IndiceActual);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IndiceMaximo);
            this.Controls.Add(this.IndiceEspectro);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.R_distancia);
            this.Controls.Add(this.Adelante);
            this.Controls.Add(this.Atras);
            this.Controls.Add(this.Final);
            this.Controls.Add(this.Inicio);
            this.Controls.Add(this.Lienzo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.ShowIcon = false;
            this.Text = "Imagen";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Lienzo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Lienzo;
        public System.Windows.Forms.Button Inicio;
        public System.Windows.Forms.Button Final;
        public System.Windows.Forms.Button Atras;
        public System.Windows.Forms.Button Adelante;
        public System.Windows.Forms.Label IndiceEspectro;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.Label R_distancia;
        public System.Windows.Forms.Label IndiceActual;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label IndiceMaximo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.Label R_confianza;
        public System.Windows.Forms.Label R_clusterCercano;
    }
}