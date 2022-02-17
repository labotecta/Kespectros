namespace KEspectro
{
    partial class Form7
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
            this.r_i = new System.Windows.Forms.Label();
            this.V_tmax = new System.Windows.Forms.TextBox();
            this.Lienzo = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.V_tmin = new System.Windows.Forms.TextBox();
            this.Salvar = new System.Windows.Forms.Button();
            this.Actualizar = new System.Windows.Forms.Button();
            this.LeyendaIzq = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.Lienzo)).BeginInit();
            this.SuspendLayout();
            // 
            // r_i
            // 
            this.r_i.AutoSize = true;
            this.r_i.Location = new System.Drawing.Point(276, 11);
            this.r_i.Name = "r_i";
            this.r_i.Size = new System.Drawing.Size(150, 20);
            this.r_i.TabIndex = 127;
            this.r_i.Text = "Temperatura máxima";
            this.r_i.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // V_tmax
            // 
            this.V_tmax.Location = new System.Drawing.Point(457, 8);
            this.V_tmax.Name = "V_tmax";
            this.V_tmax.Size = new System.Drawing.Size(62, 27);
            this.V_tmax.TabIndex = 126;
            this.V_tmax.Text = "10000";
            this.V_tmax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Lienzo
            // 
            this.Lienzo.Location = new System.Drawing.Point(12, 42);
            this.Lienzo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Lienzo.Name = "Lienzo";
            this.Lienzo.Size = new System.Drawing.Size(946, 494);
            this.Lienzo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Lienzo.TabIndex = 125;
            this.Lienzo.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 20);
            this.label1.TabIndex = 129;
            this.label1.Text = "Temperatura mínima";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // V_tmin
            // 
            this.V_tmin.Location = new System.Drawing.Point(193, 8);
            this.V_tmin.Name = "V_tmin";
            this.V_tmin.Size = new System.Drawing.Size(62, 27);
            this.V_tmin.TabIndex = 128;
            this.V_tmin.Text = "0";
            this.V_tmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Salvar
            // 
            this.Salvar.Location = new System.Drawing.Point(931, 5);
            this.Salvar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Salvar.Name = "Salvar";
            this.Salvar.Size = new System.Drawing.Size(81, 32);
            this.Salvar.TabIndex = 131;
            this.Salvar.Text = "Salvar";
            this.Salvar.UseVisualStyleBackColor = true;
            this.Salvar.Click += new System.EventHandler(this.Salvar_Click);
            // 
            // Actualizar
            // 
            this.Actualizar.Location = new System.Drawing.Point(715, 5);
            this.Actualizar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Actualizar.Name = "Actualizar";
            this.Actualizar.Size = new System.Drawing.Size(103, 32);
            this.Actualizar.TabIndex = 132;
            this.Actualizar.Text = "Actualizar";
            this.Actualizar.UseVisualStyleBackColor = true;
            this.Actualizar.Click += new System.EventHandler(this.Actualizar_Click);
            // 
            // LeyendaIzq
            // 
            this.LeyendaIzq.AutoSize = true;
            this.LeyendaIzq.Checked = true;
            this.LeyendaIzq.CheckState = System.Windows.Forms.CheckState.Checked;
            this.LeyendaIzq.Location = new System.Drawing.Point(541, 9);
            this.LeyendaIzq.Name = "LeyendaIzq";
            this.LeyendaIzq.Size = new System.Drawing.Size(152, 24);
            this.LeyendaIzq.TabIndex = 133;
            this.LeyendaIzq.Text = "Leyenda izquierda";
            this.LeyendaIzq.UseVisualStyleBackColor = true;
            // 
            // Form7
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 571);
            this.Controls.Add(this.LeyendaIzq);
            this.Controls.Add(this.Actualizar);
            this.Controls.Add(this.Salvar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.V_tmin);
            this.Controls.Add(this.r_i);
            this.Controls.Add(this.V_tmax);
            this.Controls.Add(this.Lienzo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form7";
            this.Text = "Form7";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form7_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Lienzo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label r_i;
        public TextBox V_tmax;
        private PictureBox Lienzo;
        private Label label1;
        public TextBox V_tmin;
        private Button Salvar;
        private Button Actualizar;
        private CheckBox LeyendaIzq;
    }
}