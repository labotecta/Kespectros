namespace KEspectro
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.Lienzo = new System.Windows.Forms.PictureBox();
            this.Sel_silh = new System.Windows.Forms.RadioButton();
            this.Sel_db = new System.Windows.Forms.RadioButton();
            this.Sel_destandar = new System.Windows.Forms.RadioButton();
            this.Sel_media = new System.Windows.Forms.RadioButton();
            this.Sel_clusters = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.Salvar = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
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
            this.Lienzo.TabIndex = 1;
            this.Lienzo.TabStop = false;
            // 
            // Sel_silh
            // 
            this.Sel_silh.AutoSize = true;
            this.Sel_silh.Location = new System.Drawing.Point(237, 15);
            this.Sel_silh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Sel_silh.Name = "Sel_silh";
            this.Sel_silh.Size = new System.Drawing.Size(60, 24);
            this.Sel_silh.TabIndex = 112;
            this.Sel_silh.Text = "SILH";
            this.Sel_silh.UseVisualStyleBackColor = true;
            this.Sel_silh.CheckedChanged += new System.EventHandler(this.Sel_silh_CheckedChanged);
            // 
            // Sel_db
            // 
            this.Sel_db.AutoSize = true;
            this.Sel_db.Location = new System.Drawing.Point(167, 15);
            this.Sel_db.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Sel_db.Name = "Sel_db";
            this.Sel_db.Size = new System.Drawing.Size(50, 24);
            this.Sel_db.TabIndex = 111;
            this.Sel_db.Text = "DB";
            this.Sel_db.UseVisualStyleBackColor = true;
            this.Sel_db.CheckedChanged += new System.EventHandler(this.Sel_db_CheckedChanged);
            // 
            // Sel_destandar
            // 
            this.Sel_destandar.AutoSize = true;
            this.Sel_destandar.Location = new System.Drawing.Point(91, 15);
            this.Sel_destandar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Sel_destandar.Name = "Sel_destandar";
            this.Sel_destandar.Size = new System.Drawing.Size(49, 24);
            this.Sel_destandar.TabIndex = 110;
            this.Sel_destandar.Text = "DE";
            this.Sel_destandar.UseVisualStyleBackColor = true;
            this.Sel_destandar.CheckedChanged += new System.EventHandler(this.Sel_destandar_CheckedChanged);
            // 
            // Sel_media
            // 
            this.Sel_media.AutoSize = true;
            this.Sel_media.Checked = true;
            this.Sel_media.Location = new System.Drawing.Point(15, 15);
            this.Sel_media.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Sel_media.Name = "Sel_media";
            this.Sel_media.Size = new System.Drawing.Size(60, 24);
            this.Sel_media.TabIndex = 109;
            this.Sel_media.TabStop = true;
            this.Sel_media.Text = "Med";
            this.Sel_media.UseVisualStyleBackColor = true;
            this.Sel_media.CheckedChanged += new System.EventHandler(this.Sel_media_CheckedChanged);
            // 
            // Sel_clusters
            // 
            this.Sel_clusters.FormattingEnabled = true;
            this.Sel_clusters.Location = new System.Drawing.Point(408, 15);
            this.Sel_clusters.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Sel_clusters.Name = "Sel_clusters";
            this.Sel_clusters.Size = new System.Drawing.Size(106, 28);
            this.Sel_clusters.TabIndex = 113;
            this.Sel_clusters.SelectedIndexChanged += new System.EventHandler(this.Sel_clusters_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(561, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 38);
            this.label1.TabIndex = 114;
            this.label1.Text = "1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(609, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 38);
            this.label2.TabIndex = 115;
            this.label2.Text = "2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(657, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 38);
            this.label3.TabIndex = 116;
            this.label3.Text = "3";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(705, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 38);
            this.label4.TabIndex = 117;
            this.label4.Text = "4";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(753, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 38);
            this.label5.TabIndex = 118;
            this.label5.Text = "5";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(801, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 38);
            this.label6.TabIndex = 119;
            this.label6.Text = "6";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(849, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 38);
            this.label7.TabIndex = 120;
            this.label7.Text = "7";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(897, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 38);
            this.label8.TabIndex = 121;
            this.label8.Text = "8";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(945, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 38);
            this.label9.TabIndex = 122;
            this.label9.Text = "9";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(993, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 38);
            this.label10.TabIndex = 123;
            this.label10.Text = "10";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Salvar
            // 
            this.Salvar.Location = new System.Drawing.Point(1127, 15);
            this.Salvar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Salvar.Name = "Salvar";
            this.Salvar.Size = new System.Drawing.Size(81, 32);
            this.Salvar.TabIndex = 135;
            this.Salvar.Text = "Salvar";
            this.Salvar.UseVisualStyleBackColor = true;
            this.Salvar.Click += new System.EventHandler(this.Salvar_Click);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label11.Location = new System.Drawing.Point(1041, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 38);
            this.label11.TabIndex = 136;
            this.label11.Text = "11";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1339, 562);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.Salvar);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Sel_clusters);
            this.Controls.Add(this.Sel_silh);
            this.Controls.Add(this.Sel_db);
            this.Controls.Add(this.Sel_destandar);
            this.Controls.Add(this.Sel_media);
            this.Controls.Add(this.Lienzo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form3";
            this.Text = "Mapa";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Lienzo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Lienzo;
        private System.Windows.Forms.RadioButton Sel_silh;
        private System.Windows.Forms.RadioButton Sel_db;
        private System.Windows.Forms.RadioButton Sel_destandar;
        private System.Windows.Forms.RadioButton Sel_media;
        private System.Windows.Forms.ComboBox Sel_clusters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private Button Salvar;
        private Label label11;
    }
}