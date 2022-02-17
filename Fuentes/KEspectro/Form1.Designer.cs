namespace KEspectro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Calcula = new System.Windows.Forms.Button();
            this.Leer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Grado = new System.Windows.Forms.TextBox();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.Coeficientes = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.F = new System.Windows.Forms.Label();
            this.Corte = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SelPicos = new System.Windows.Forms.Button();
            this.SalvaImagen = new System.Windows.Forms.Button();
            this.Recalcula = new System.Windows.Forms.Button();
            this.SalvaPicos = new System.Windows.Forms.Button();
            this.Vertical = new System.Windows.Forms.CheckBox();
            this.R_fichero = new System.Windows.Forms.ComboBox();
            this.FilaFichero = new System.Windows.Forms.NumericUpDown();
            this.DE = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Todo = new System.Windows.Forms.Button();
            this.Fcorte = new System.Windows.Forms.TextBox();
            this.R_corte = new System.Windows.Forms.Label();
            this.Proporcional = new System.Windows.Forms.CheckBox();
            this.Movil = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Esc = new System.Windows.Forms.Button();
            this.Absorcion = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Emision = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.Puntos = new System.Windows.Forms.Label();
            this.V_reducirCada = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Reducir = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.CreaClusters = new System.Windows.Forms.Button();
            this.V_nclusters = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.V_max_Iteraciones = new System.Windows.Forms.TextBox();
            this.TablaClusters = new System.Windows.Forms.DataGridView();
            this.FilaCluster = new System.Windows.Forms.NumericUpDown();
            this.R_Fichero_Clusters = new System.Windows.Forms.Label();
            this.LeerEspectrosAnalizados = new System.Windows.Forms.Button();
            this.R_espectros = new System.Windows.Forms.Label();
            this.R_Distancia = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.IndiceEspectro = new System.Windows.Forms.Label();
            this.V_semilla = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.SalvaClusters = new System.Windows.Forms.Button();
            this.ListaEvolucion = new System.Windows.Forms.ListBox();
            this.V_tipoDistancia = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.V_max_empeora_md = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.V_min_mejora_md = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.V_min_mejora_de = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.V_max_empeora_de = new System.Windows.Forms.TextBox();
            this.R_iteracionActual = new System.Windows.Forms.Label();
            this.V_podar = new System.Windows.Forms.CheckBox();
            this.R_filas = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.V_tipoDatosY = new System.Windows.Forms.TextBox();
            this.SalvaEspectrosAnalizados = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.V_modoIniciar = new System.Windows.Forms.TextBox();
            this.MLKMeans = new System.Windows.Forms.Button();
            this.V_forzar = new System.Windows.Forms.CheckBox();
            this.V_dibujaTodo = new System.Windows.Forms.CheckBox();
            this.LeeCentroides = new System.Windows.Forms.Button();
            this.SalvaCentroides = new System.Windows.Forms.Button();
            this.R_confianza = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.V_desde = new System.Windows.Forms.TextBox();
            this.V_hasta = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.V_pases = new System.Windows.Forms.TextBox();
            this.ListaIntentos = new System.Windows.Forms.ListBox();
            this.R_clusterCercano = new System.Windows.Forms.Label();
            this.V_silhouette = new System.Windows.Forms.CheckBox();
            this.label26 = new System.Windows.Forms.Label();
            this.V_hilos = new System.Windows.Forms.TextBox();
            this.R_mejorMed = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Sel_detGenetico = new System.Windows.Forms.RadioButton();
            this.Sel_dbGenetico = new System.Windows.Forms.RadioButton();
            this.Sel_destandarGenetico = new System.Windows.Forms.RadioButton();
            this.Sel_mediaGenetico = new System.Windows.Forms.RadioButton();
            this.Sel_det = new System.Windows.Forms.RadioButton();
            this.Sel_db = new System.Windows.Forms.RadioButton();
            this.Sel_destandar = new System.Windows.Forms.RadioButton();
            this.Sel_media = new System.Windows.Forms.RadioButton();
            this.R_hilosPendientes = new System.Windows.Forms.Label();
            this.R_tiempo = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.V_ppPoda = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.V_veces = new System.Windows.Forms.TextBox();
            this.Mapa = new System.Windows.Forms.Button();
            this.V_normalizaCentroides = new System.Windows.Forms.CheckBox();
            this.R_mejorDE = new System.Windows.Forms.Label();
            this.R_mejorDB = new System.Windows.Forms.Label();
            this.R_mejorDET = new System.Windows.Forms.Label();
            this.SalvaClasificacion = new System.Windows.Forms.Button();
            this.DistanciasCentroides = new System.Windows.Forms.Button();
            this.SalvaCaso = new System.Windows.Forms.Button();
            this.R_Fichero_Centroides = new System.Windows.Forms.Label();
            this.Asigna = new System.Windows.Forms.Button();
            this.V_genetico = new System.Windows.Forms.CheckBox();
            this.V_generaciones = new System.Windows.Forms.TextBox();
            this.V_minimizaGenetico = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.V_torneo = new System.Windows.Forms.CheckBox();
            this.V_ppTorneo = new System.Windows.Forms.TextBox();
            this.V_mutar = new System.Windows.Forms.CheckBox();
            this.V_ppMutar = new System.Windows.Forms.TextBox();
            this.V_mutarCuanto = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.V_modoCentroide = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.Restar = new System.Windows.Forms.Button();
            this.VerCentroides = new System.Windows.Forms.Button();
            this.VerTemperatura = new System.Windows.Forms.Button();
            this.R2 = new System.Windows.Forms.TextBox();
            this.LeerCromosomas = new System.Windows.Forms.Button();
            this.SalvaCromosomas = new System.Windows.Forms.Button();
            this.EjecutaGenetico = new System.Windows.Forms.Button();
            this.LeerClasificacion = new System.Windows.Forms.Button();
            this.ExcluirSinMaximo = new System.Windows.Forms.CheckBox();
            this.Recalcular = new System.Windows.Forms.CheckBox();
            this.Partir = new System.Windows.Forms.CheckBox();
            this.SeparaT = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Coeficientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilaFichero)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablaClusters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilaCluster)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Calcula
            // 
            this.Calcula.Location = new System.Drawing.Point(687, 101);
            this.Calcula.Name = "Calcula";
            this.Calcula.Size = new System.Drawing.Size(101, 25);
            this.Calcula.TabIndex = 0;
            this.Calcula.Text = "Calcula";
            this.Calcula.UseVisualStyleBackColor = true;
            this.Calcula.Click += new System.EventHandler(this.Calcula_Click);
            // 
            // Leer
            // 
            this.Leer.Location = new System.Drawing.Point(687, 39);
            this.Leer.Name = "Leer";
            this.Leer.Size = new System.Drawing.Size(101, 25);
            this.Leer.TabIndex = 1;
            this.Leer.Text = "Leer";
            this.Leer.UseVisualStyleBackColor = true;
            this.Leer.Click += new System.EventHandler(this.Leer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(549, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Grado";
            // 
            // Grado
            // 
            this.Grado.Location = new System.Drawing.Point(620, 101);
            this.Grado.Name = "Grado";
            this.Grado.Size = new System.Drawing.Size(39, 27);
            this.Grado.TabIndex = 4;
            this.Grado.Text = "5";
            this.Grado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tabla
            // 
            this.tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tabla.Location = new System.Drawing.Point(15, 101);
            this.tabla.Name = "tabla";
            this.tabla.RowHeadersWidth = 51;
            this.tabla.RowTemplate.Height = 24;
            this.tabla.Size = new System.Drawing.Size(255, 618);
            this.tabla.TabIndex = 5;
            // 
            // Coeficientes
            // 
            this.Coeficientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Coeficientes.Location = new System.Drawing.Point(282, 132);
            this.Coeficientes.Name = "Coeficientes";
            this.Coeficientes.RowHeadersWidth = 51;
            this.Coeficientes.RowTemplate.Height = 24;
            this.Coeficientes.Size = new System.Drawing.Size(506, 463);
            this.Coeficientes.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(280, 606);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "R2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(280, 638);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "F";
            // 
            // F
            // 
            this.F.BackColor = System.Drawing.Color.White;
            this.F.Location = new System.Drawing.Point(336, 638);
            this.F.Name = "F";
            this.F.Size = new System.Drawing.Size(155, 17);
            this.F.TabIndex = 10;
            this.F.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Corte
            // 
            this.Corte.ForeColor = System.Drawing.Color.Blue;
            this.Corte.Location = new System.Drawing.Point(336, 665);
            this.Corte.Name = "Corte";
            this.Corte.Size = new System.Drawing.Size(61, 27);
            this.Corte.TabIndex = 12;
            this.Corte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(280, 668);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Corte";
            // 
            // SelPicos
            // 
            this.SelPicos.ForeColor = System.Drawing.Color.Blue;
            this.SelPicos.Location = new System.Drawing.Point(727, 665);
            this.SelPicos.Name = "SelPicos";
            this.SelPicos.Size = new System.Drawing.Size(61, 25);
            this.SelPicos.TabIndex = 13;
            this.SelPicos.Text = "Picos";
            this.SelPicos.UseVisualStyleBackColor = true;
            this.SelPicos.Click += new System.EventHandler(this.SelPicos_Click);
            // 
            // SalvaImagen
            // 
            this.SalvaImagen.Location = new System.Drawing.Point(639, 729);
            this.SalvaImagen.Name = "SalvaImagen";
            this.SalvaImagen.Size = new System.Drawing.Size(192, 25);
            this.SalvaImagen.TabIndex = 14;
            this.SalvaImagen.Text = "Salva imagen";
            this.SalvaImagen.UseVisualStyleBackColor = true;
            this.SalvaImagen.Click += new System.EventHandler(this.SalvaImagen_Click);
            // 
            // Recalcula
            // 
            this.Recalcula.Location = new System.Drawing.Point(693, 601);
            this.Recalcula.Name = "Recalcula";
            this.Recalcula.Size = new System.Drawing.Size(95, 25);
            this.Recalcula.TabIndex = 15;
            this.Recalcula.Text = "Recalcula";
            this.Recalcula.UseVisualStyleBackColor = true;
            this.Recalcula.Click += new System.EventHandler(this.Recalcula_Click);
            // 
            // SalvaPicos
            // 
            this.SalvaPicos.Location = new System.Drawing.Point(282, 729);
            this.SalvaPicos.Name = "SalvaPicos";
            this.SalvaPicos.Size = new System.Drawing.Size(154, 25);
            this.SalvaPicos.TabIndex = 16;
            this.SalvaPicos.Text = "Salva picos";
            this.SalvaPicos.UseVisualStyleBackColor = true;
            this.SalvaPicos.Click += new System.EventHandler(this.SalvaPicos_Click);
            // 
            // Vertical
            // 
            this.Vertical.AutoSize = true;
            this.Vertical.Location = new System.Drawing.Point(601, 39);
            this.Vertical.Name = "Vertical";
            this.Vertical.Size = new System.Drawing.Size(80, 24);
            this.Vertical.TabIndex = 17;
            this.Vertical.Text = "Vertical";
            this.Vertical.UseVisualStyleBackColor = true;
            // 
            // R_fichero
            // 
            this.R_fichero.FormattingEnabled = true;
            this.R_fichero.Location = new System.Drawing.Point(15, 37);
            this.R_fichero.Name = "R_fichero";
            this.R_fichero.Size = new System.Drawing.Size(493, 28);
            this.R_fichero.TabIndex = 18;
            // 
            // FilaFichero
            // 
            this.FilaFichero.Location = new System.Drawing.Point(15, 70);
            this.FilaFichero.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FilaFichero.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FilaFichero.Name = "FilaFichero";
            this.FilaFichero.Size = new System.Drawing.Size(89, 27);
            this.FilaFichero.TabIndex = 19;
            this.FilaFichero.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FilaFichero.ValueChanged += new System.EventHandler(this.FilaFichero_ValueChanged);
            // 
            // DE
            // 
            this.DE.BackColor = System.Drawing.Color.White;
            this.DE.Location = new System.Drawing.Point(595, 638);
            this.DE.Name = "DE";
            this.DE.Size = new System.Drawing.Size(125, 17);
            this.DE.TabIndex = 21;
            this.DE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(518, 638);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 20);
            this.label6.TabIndex = 20;
            this.label6.Text = "DE error";
            // 
            // Todo
            // 
            this.Todo.ForeColor = System.Drawing.Color.Red;
            this.Todo.Location = new System.Drawing.Point(620, 70);
            this.Todo.Name = "Todo";
            this.Todo.Size = new System.Drawing.Size(168, 25);
            this.Todo.TabIndex = 22;
            this.Todo.Text = "Analiza Todos";
            this.Todo.UseVisualStyleBackColor = true;
            this.Todo.Click += new System.EventHandler(this.Todo_Click);
            // 
            // Fcorte
            // 
            this.Fcorte.Location = new System.Drawing.Point(736, 633);
            this.Fcorte.Name = "Fcorte";
            this.Fcorte.Size = new System.Drawing.Size(52, 27);
            this.Fcorte.TabIndex = 23;
            this.Fcorte.Text = "1,4";
            this.Fcorte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // R_corte
            // 
            this.R_corte.AutoSize = true;
            this.R_corte.ForeColor = System.Drawing.Color.Blue;
            this.R_corte.Location = new System.Drawing.Point(400, 668);
            this.R_corte.Name = "R_corte";
            this.R_corte.Size = new System.Drawing.Size(32, 20);
            this.R_corte.TabIndex = 24;
            this.R_corte.Text = "p.u.";
            // 
            // Proporcional
            // 
            this.Proporcional.AutoSize = true;
            this.Proporcional.Checked = true;
            this.Proporcional.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Proporcional.ForeColor = System.Drawing.Color.Blue;
            this.Proporcional.Location = new System.Drawing.Point(441, 665);
            this.Proporcional.Name = "Proporcional";
            this.Proporcional.Size = new System.Drawing.Size(116, 24);
            this.Proporcional.TabIndex = 25;
            this.Proporcional.Text = "Proporcional";
            this.Proporcional.UseVisualStyleBackColor = true;
            this.Proporcional.CheckedChanged += new System.EventHandler(this.Proporcional_CheckedChanged);
            // 
            // Movil
            // 
            this.Movil.ForeColor = System.Drawing.Color.Blue;
            this.Movil.Location = new System.Drawing.Point(678, 665);
            this.Movil.Name = "Movil";
            this.Movil.Size = new System.Drawing.Size(46, 27);
            this.Movil.TabIndex = 26;
            this.Movil.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(562, 668);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 20);
            this.label5.TabIndex = 27;
            this.label5.Text = "Distancia movil";
            // 
            // Esc
            // 
            this.Esc.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Esc.ForeColor = System.Drawing.Color.Red;
            this.Esc.Location = new System.Drawing.Point(620, 3);
            this.Esc.Name = "Esc";
            this.Esc.Size = new System.Drawing.Size(168, 31);
            this.Esc.TabIndex = 28;
            this.Esc.Text = "Esc";
            this.Esc.UseVisualStyleBackColor = true;
            this.Esc.Click += new System.EventHandler(this.Esc_Click);
            // 
            // Absorcion
            // 
            this.Absorcion.BackColor = System.Drawing.Color.White;
            this.Absorcion.ForeColor = System.Drawing.Color.Blue;
            this.Absorcion.Location = new System.Drawing.Point(478, 699);
            this.Absorcion.Name = "Absorcion";
            this.Absorcion.Size = new System.Drawing.Size(73, 19);
            this.Absorcion.TabIndex = 30;
            this.Absorcion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(394, 699);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 20);
            this.label8.TabIndex = 29;
            this.label8.Text = "Absorción";
            // 
            // Emision
            // 
            this.Emision.BackColor = System.Drawing.Color.White;
            this.Emision.ForeColor = System.Drawing.Color.Blue;
            this.Emision.Location = new System.Drawing.Point(649, 699);
            this.Emision.Name = "Emision";
            this.Emision.Size = new System.Drawing.Size(73, 19);
            this.Emision.TabIndex = 32;
            this.Emision.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(564, 699);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 20);
            this.label10.TabIndex = 31;
            this.label10.Text = "Emisión";
            // 
            // Puntos
            // 
            this.Puntos.BackColor = System.Drawing.Color.White;
            this.Puntos.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Puntos.Location = new System.Drawing.Point(165, 74);
            this.Puntos.Name = "Puntos";
            this.Puntos.Size = new System.Drawing.Size(105, 17);
            this.Puntos.TabIndex = 34;
            this.Puntos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // V_reducirCada
            // 
            this.V_reducirCada.ForeColor = System.Drawing.SystemColors.ControlText;
            this.V_reducirCada.Location = new System.Drawing.Point(66, 727);
            this.V_reducirCada.Name = "V_reducirCada";
            this.V_reducirCada.Size = new System.Drawing.Size(44, 27);
            this.V_reducirCada.TabIndex = 36;
            this.V_reducirCada.Text = "0";
            this.V_reducirCada.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(15, 730);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 20);
            this.label7.TabIndex = 35;
            this.label7.Text = "Cada";
            // 
            // Reducir
            // 
            this.Reducir.Location = new System.Drawing.Point(168, 729);
            this.Reducir.Name = "Reducir";
            this.Reducir.Size = new System.Drawing.Size(102, 25);
            this.Reducir.TabIndex = 37;
            this.Reducir.Text = "Reducir";
            this.Reducir.UseVisualStyleBackColor = true;
            this.Reducir.Click += new System.EventHandler(this.Reducir_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(117, 730);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 20);
            this.label9.TabIndex = 38;
            this.label9.Text = "datos";
            // 
            // CreaClusters
            // 
            this.CreaClusters.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CreaClusters.Location = new System.Drawing.Point(1825, 183);
            this.CreaClusters.Name = "CreaClusters";
            this.CreaClusters.Size = new System.Drawing.Size(200, 31);
            this.CreaClusters.TabIndex = 39;
            this.CreaClusters.Text = "Clasificar";
            this.CreaClusters.UseVisualStyleBackColor = true;
            this.CreaClusters.Click += new System.EventHandler(this.CreaClusters_Click);
            // 
            // V_nclusters
            // 
            this.V_nclusters.Location = new System.Drawing.Point(1173, 61);
            this.V_nclusters.Name = "V_nclusters";
            this.V_nclusters.Size = new System.Drawing.Size(36, 27);
            this.V_nclusters.TabIndex = 40;
            this.V_nclusters.Text = "8";
            this.V_nclusters.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1045, 64);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 20);
            this.label11.TabIndex = 41;
            this.label11.Text = "Número clusters";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(797, 64);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(139, 20);
            this.label12.TabIndex = 43;
            this.label12.Text = "Máximo iteraciones";
            // 
            // V_max_Iteraciones
            // 
            this.V_max_Iteraciones.Location = new System.Drawing.Point(952, 61);
            this.V_max_Iteraciones.Name = "V_max_Iteraciones";
            this.V_max_Iteraciones.Size = new System.Drawing.Size(63, 27);
            this.V_max_Iteraciones.TabIndex = 42;
            this.V_max_Iteraciones.Text = "150";
            this.V_max_Iteraciones.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TablaClusters
            // 
            this.TablaClusters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaClusters.Location = new System.Drawing.Point(797, 234);
            this.TablaClusters.Name = "TablaClusters";
            this.TablaClusters.RowHeadersWidth = 51;
            this.TablaClusters.RowTemplate.Height = 24;
            this.TablaClusters.Size = new System.Drawing.Size(861, 451);
            this.TablaClusters.TabIndex = 44;
            this.TablaClusters.SelectionChanged += new System.EventHandler(this.TablaClusters_SelectionChanged);
            // 
            // FilaCluster
            // 
            this.FilaCluster.Location = new System.Drawing.Point(797, 205);
            this.FilaCluster.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FilaCluster.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FilaCluster.Name = "FilaCluster";
            this.FilaCluster.Size = new System.Drawing.Size(89, 27);
            this.FilaCluster.TabIndex = 45;
            this.FilaCluster.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FilaCluster.ValueChanged += new System.EventHandler(this.FilaCluster_ValueChanged);
            // 
            // R_Fichero_Clusters
            // 
            this.R_Fichero_Clusters.BackColor = System.Drawing.Color.White;
            this.R_Fichero_Clusters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.R_Fichero_Clusters.Location = new System.Drawing.Point(797, 6);
            this.R_Fichero_Clusters.Name = "R_Fichero_Clusters";
            this.R_Fichero_Clusters.Size = new System.Drawing.Size(763, 22);
            this.R_Fichero_Clusters.TabIndex = 46;
            this.R_Fichero_Clusters.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LeerEspectrosAnalizados
            // 
            this.LeerEspectrosAnalizados.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LeerEspectrosAnalizados.Location = new System.Drawing.Point(1660, 2);
            this.LeerEspectrosAnalizados.Name = "LeerEspectrosAnalizados";
            this.LeerEspectrosAnalizados.Size = new System.Drawing.Size(273, 27);
            this.LeerEspectrosAnalizados.TabIndex = 47;
            this.LeerEspectrosAnalizados.Text = "Leer espectros analizados";
            this.LeerEspectrosAnalizados.UseVisualStyleBackColor = true;
            this.LeerEspectrosAnalizados.Click += new System.EventHandler(this.LeerEspectrosAnalizados_Click);
            // 
            // R_espectros
            // 
            this.R_espectros.BackColor = System.Drawing.Color.White;
            this.R_espectros.ForeColor = System.Drawing.SystemColors.ControlText;
            this.R_espectros.Location = new System.Drawing.Point(1566, 5);
            this.R_espectros.Name = "R_espectros";
            this.R_espectros.Size = new System.Drawing.Size(92, 22);
            this.R_espectros.TabIndex = 48;
            this.R_espectros.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // R_Distancia
            // 
            this.R_Distancia.BackColor = System.Drawing.Color.White;
            this.R_Distancia.ForeColor = System.Drawing.SystemColors.ControlText;
            this.R_Distancia.Location = new System.Drawing.Point(1054, 211);
            this.R_Distancia.Name = "R_Distancia";
            this.R_Distancia.Size = new System.Drawing.Size(103, 17);
            this.R_Distancia.TabIndex = 50;
            this.R_Distancia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(977, 210);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 20);
            this.label14.TabIndex = 51;
            this.label14.Text = "Distancia";
            // 
            // IndiceEspectro
            // 
            this.IndiceEspectro.BackColor = System.Drawing.Color.White;
            this.IndiceEspectro.ForeColor = System.Drawing.SystemColors.ControlText;
            this.IndiceEspectro.Location = new System.Drawing.Point(896, 211);
            this.IndiceEspectro.Name = "IndiceEspectro";
            this.IndiceEspectro.Size = new System.Drawing.Size(73, 17);
            this.IndiceEspectro.TabIndex = 52;
            this.IndiceEspectro.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // V_semilla
            // 
            this.V_semilla.Location = new System.Drawing.Point(1320, 59);
            this.V_semilla.Name = "V_semilla";
            this.V_semilla.Size = new System.Drawing.Size(56, 27);
            this.V_semilla.TabIndex = 53;
            this.V_semilla.Text = "1234";
            this.V_semilla.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1241, 64);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 20);
            this.label13.TabIndex = 54;
            this.label13.Text = "Semilla";
            // 
            // SalvaClusters
            // 
            this.SalvaClusters.Location = new System.Drawing.Point(974, 727);
            this.SalvaClusters.Name = "SalvaClusters";
            this.SalvaClusters.Size = new System.Drawing.Size(123, 27);
            this.SalvaClusters.TabIndex = 59;
            this.SalvaClusters.Text = "Salva resumen clusters";
            this.SalvaClusters.UseVisualStyleBackColor = true;
            this.SalvaClusters.Click += new System.EventHandler(this.SalvaClusters_Click);
            // 
            // ListaEvolucion
            // 
            this.ListaEvolucion.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ListaEvolucion.FormattingEnabled = true;
            this.ListaEvolucion.ItemHeight = 16;
            this.ListaEvolucion.Location = new System.Drawing.Point(1664, 217);
            this.ListaEvolucion.Name = "ListaEvolucion";
            this.ListaEvolucion.Size = new System.Drawing.Size(361, 180);
            this.ListaEvolucion.TabIndex = 60;
            // 
            // V_tipoDistancia
            // 
            this.V_tipoDistancia.Location = new System.Drawing.Point(1995, 65);
            this.V_tipoDistancia.Name = "V_tipoDistancia";
            this.V_tipoDistancia.Size = new System.Drawing.Size(30, 27);
            this.V_tipoDistancia.TabIndex = 61;
            this.V_tipoDistancia.Text = "0";
            this.V_tipoDistancia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(1846, 68);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(148, 20);
            this.label16.TabIndex = 63;
            this.label16.Text = "Tipo distancia (0 a 4)";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(797, 93);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(193, 20);
            this.label18.TabIndex = 65;
            this.label18.Text = "Max Emperora Media (p.u. )";
            // 
            // V_max_empeora_md
            // 
            this.V_max_empeora_md.Location = new System.Drawing.Point(1041, 90);
            this.V_max_empeora_md.Name = "V_max_empeora_md";
            this.V_max_empeora_md.Size = new System.Drawing.Size(56, 27);
            this.V_max_empeora_md.TabIndex = 64;
            this.V_max_empeora_md.Text = "2";
            this.V_max_empeora_md.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(1113, 92);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(172, 20);
            this.label19.TabIndex = 67;
            this.label19.Text = "Min Mejora Media (p.u. )";
            // 
            // V_min_mejora_md
            // 
            this.V_min_mejora_md.Location = new System.Drawing.Point(1320, 89);
            this.V_min_mejora_md.Name = "V_min_mejora_md";
            this.V_min_mejora_md.Size = new System.Drawing.Size(56, 27);
            this.V_min_mejora_md.TabIndex = 66;
            this.V_min_mejora_md.Text = "0,0";
            this.V_min_mejora_md.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(1113, 121);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(149, 20);
            this.label20.TabIndex = 71;
            this.label20.Text = "Min Mejora DE (p.u. )";
            // 
            // V_min_mejora_de
            // 
            this.V_min_mejora_de.Location = new System.Drawing.Point(1320, 118);
            this.V_min_mejora_de.Name = "V_min_mejora_de";
            this.V_min_mejora_de.Size = new System.Drawing.Size(56, 27);
            this.V_min_mejora_de.TabIndex = 70;
            this.V_min_mejora_de.Text = "0,0";
            this.V_min_mejora_de.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(797, 122);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(170, 20);
            this.label21.TabIndex = 69;
            this.label21.Text = "Max Emperora DE (p.u. )";
            // 
            // V_max_empeora_de
            // 
            this.V_max_empeora_de.Location = new System.Drawing.Point(1041, 119);
            this.V_max_empeora_de.Name = "V_max_empeora_de";
            this.V_max_empeora_de.Size = new System.Drawing.Size(56, 27);
            this.V_max_empeora_de.TabIndex = 68;
            this.V_max_empeora_de.Text = "2";
            this.V_max_empeora_de.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // R_iteracionActual
            // 
            this.R_iteracionActual.BackColor = System.Drawing.Color.White;
            this.R_iteracionActual.Location = new System.Drawing.Point(1475, 210);
            this.R_iteracionActual.Name = "R_iteracionActual";
            this.R_iteracionActual.Size = new System.Drawing.Size(72, 18);
            this.R_iteracionActual.TabIndex = 72;
            this.R_iteracionActual.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // V_podar
            // 
            this.V_podar.AutoSize = true;
            this.V_podar.ForeColor = System.Drawing.Color.Red;
            this.V_podar.Location = new System.Drawing.Point(1664, 123);
            this.V_podar.Name = "V_podar";
            this.V_podar.Size = new System.Drawing.Size(69, 24);
            this.V_podar.TabIndex = 75;
            this.V_podar.Text = "Podar";
            this.V_podar.UseVisualStyleBackColor = true;
            // 
            // R_filas
            // 
            this.R_filas.BackColor = System.Drawing.Color.White;
            this.R_filas.ForeColor = System.Drawing.SystemColors.ControlText;
            this.R_filas.Location = new System.Drawing.Point(521, 39);
            this.R_filas.Name = "R_filas";
            this.R_filas.Size = new System.Drawing.Size(72, 22);
            this.R_filas.TabIndex = 76;
            this.R_filas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(1664, 96);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(118, 20);
            this.label22.TabIndex = 78;
            this.label22.Text = "Tipo de Y (0 a 7)";
            // 
            // V_tipoDatosY
            // 
            this.V_tipoDatosY.Location = new System.Drawing.Point(1811, 93);
            this.V_tipoDatosY.Name = "V_tipoDatosY";
            this.V_tipoDatosY.Size = new System.Drawing.Size(30, 27);
            this.V_tipoDatosY.TabIndex = 77;
            this.V_tipoDatosY.Text = "6";
            this.V_tipoDatosY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.V_tipoDatosY.TextChanged += new System.EventHandler(this.TipoDatosY_TextChanged);
            // 
            // SalvaEspectrosAnalizados
            // 
            this.SalvaEspectrosAnalizados.Location = new System.Drawing.Point(1530, 727);
            this.SalvaEspectrosAnalizados.Name = "SalvaEspectrosAnalizados";
            this.SalvaEspectrosAnalizados.Size = new System.Drawing.Size(128, 27);
            this.SalvaEspectrosAnalizados.TabIndex = 79;
            this.SalvaEspectrosAnalizados.Text = "Salva espectros procesados";
            this.SalvaEspectrosAnalizados.UseVisualStyleBackColor = true;
            this.SalvaEspectrosAnalizados.Click += new System.EventHandler(this.SalvaEspectrosAnalizados_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(1664, 68);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(95, 20);
            this.label23.TabIndex = 81;
            this.label23.Text = "Iniciar (0 a 5)";
            // 
            // V_modoIniciar
            // 
            this.V_modoIniciar.Location = new System.Drawing.Point(1811, 64);
            this.V_modoIniciar.Name = "V_modoIniciar";
            this.V_modoIniciar.Size = new System.Drawing.Size(30, 27);
            this.V_modoIniciar.TabIndex = 80;
            this.V_modoIniciar.Text = "5";
            this.V_modoIniciar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // MLKMeans
            // 
            this.MLKMeans.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.MLKMeans.Location = new System.Drawing.Point(797, 179);
            this.MLKMeans.Name = "MLKMeans";
            this.MLKMeans.Size = new System.Drawing.Size(157, 27);
            this.MLKMeans.TabIndex = 82;
            this.MLKMeans.Text = "Clasificar ML KMeans";
            this.MLKMeans.UseVisualStyleBackColor = true;
            this.MLKMeans.Click += new System.EventHandler(this.MLKMeans_Click);
            // 
            // V_forzar
            // 
            this.V_forzar.AutoSize = true;
            this.V_forzar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.V_forzar.Location = new System.Drawing.Point(1664, 152);
            this.V_forzar.Name = "V_forzar";
            this.V_forzar.Size = new System.Drawing.Size(72, 24);
            this.V_forzar.TabIndex = 83;
            this.V_forzar.Text = "Forzar";
            this.V_forzar.UseVisualStyleBackColor = true;
            // 
            // V_dibujaTodo
            // 
            this.V_dibujaTodo.AutoSize = true;
            this.V_dibujaTodo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.V_dibujaTodo.Location = new System.Drawing.Point(1919, 152);
            this.V_dibujaTodo.Name = "V_dibujaTodo";
            this.V_dibujaTodo.Size = new System.Drawing.Size(111, 24);
            this.V_dibujaTodo.TabIndex = 84;
            this.V_dibujaTodo.Text = "Dibuja todo";
            this.V_dibujaTodo.UseVisualStyleBackColor = true;
            // 
            // LeeCentroides
            // 
            this.LeeCentroides.Location = new System.Drawing.Point(1566, 29);
            this.LeeCentroides.Name = "LeeCentroides";
            this.LeeCentroides.Size = new System.Drawing.Size(134, 27);
            this.LeeCentroides.TabIndex = 85;
            this.LeeCentroides.Text = "Leer centroides";
            this.LeeCentroides.UseVisualStyleBackColor = true;
            this.LeeCentroides.Click += new System.EventHandler(this.LeeCentroides_Click);
            // 
            // SalvaCentroides
            // 
            this.SalvaCentroides.Location = new System.Drawing.Point(1396, 727);
            this.SalvaCentroides.Name = "SalvaCentroides";
            this.SalvaCentroides.Size = new System.Drawing.Size(130, 27);
            this.SalvaCentroides.TabIndex = 86;
            this.SalvaCentroides.Text = "Salva centroides";
            this.SalvaCentroides.UseVisualStyleBackColor = true;
            this.SalvaCentroides.Click += new System.EventHandler(this.SalvaCentroides_Click);
            // 
            // R_confianza
            // 
            this.R_confianza.BackColor = System.Drawing.Color.White;
            this.R_confianza.ForeColor = System.Drawing.SystemColors.ControlText;
            this.R_confianza.Location = new System.Drawing.Point(1244, 211);
            this.R_confianza.Name = "R_confianza";
            this.R_confianza.Size = new System.Drawing.Size(60, 17);
            this.R_confianza.TabIndex = 87;
            this.R_confianza.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(1167, 210);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(75, 20);
            this.label15.TabIndex = 88;
            this.label15.Text = "Confianza";
            // 
            // V_desde
            // 
            this.V_desde.Location = new System.Drawing.Point(1586, 88);
            this.V_desde.Name = "V_desde";
            this.V_desde.Size = new System.Drawing.Size(72, 27);
            this.V_desde.TabIndex = 89;
            this.V_desde.Text = "0";
            this.V_desde.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // V_hasta
            // 
            this.V_hasta.Location = new System.Drawing.Point(1586, 117);
            this.V_hasta.Name = "V_hasta";
            this.V_hasta.Size = new System.Drawing.Size(72, 27);
            this.V_hasta.TabIndex = 90;
            this.V_hasta.Text = "0";
            this.V_hasta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(1420, 92);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(146, 20);
            this.label17.TabIndex = 91;
            this.label17.Text = "Dato inicial espectro";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(1420, 121);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(136, 20);
            this.label24.TabIndex = 92;
            this.label24.Text = "Dato final espectro";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(1420, 64);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(125, 20);
            this.label25.TabIndex = 94;
            this.label25.Text = "Número de pases";
            // 
            // V_pases
            // 
            this.V_pases.Location = new System.Drawing.Point(1586, 58);
            this.V_pases.Name = "V_pases";
            this.V_pases.Size = new System.Drawing.Size(72, 27);
            this.V_pases.TabIndex = 93;
            this.V_pases.Text = "1000";
            this.V_pases.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ListaIntentos
            // 
            this.ListaIntentos.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ListaIntentos.FormattingEnabled = true;
            this.ListaIntentos.ItemHeight = 16;
            this.ListaIntentos.Location = new System.Drawing.Point(1664, 508);
            this.ListaIntentos.Name = "ListaIntentos";
            this.ListaIntentos.Size = new System.Drawing.Size(361, 244);
            this.ListaIntentos.TabIndex = 95;
            // 
            // R_clusterCercano
            // 
            this.R_clusterCercano.BackColor = System.Drawing.Color.White;
            this.R_clusterCercano.ForeColor = System.Drawing.SystemColors.ControlText;
            this.R_clusterCercano.Location = new System.Drawing.Point(1315, 211);
            this.R_clusterCercano.Name = "R_clusterCercano";
            this.R_clusterCercano.Size = new System.Drawing.Size(50, 17);
            this.R_clusterCercano.TabIndex = 96;
            this.R_clusterCercano.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // V_silhouette
            // 
            this.V_silhouette.AutoSize = true;
            this.V_silhouette.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.V_silhouette.ForeColor = System.Drawing.Color.Maroon;
            this.V_silhouette.Location = new System.Drawing.Point(1845, 152);
            this.V_silhouette.Name = "V_silhouette";
            this.V_silhouette.Size = new System.Drawing.Size(62, 20);
            this.V_silhouette.TabIndex = 97;
            this.V_silhouette.Text = "SILH";
            this.V_silhouette.UseVisualStyleBackColor = true;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(1664, 192);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(43, 20);
            this.label26.TabIndex = 99;
            this.label26.Text = "Hilos";
            // 
            // V_hilos
            // 
            this.V_hilos.Location = new System.Drawing.Point(1719, 187);
            this.V_hilos.Name = "V_hilos";
            this.V_hilos.Size = new System.Drawing.Size(39, 27);
            this.V_hilos.TabIndex = 98;
            this.V_hilos.Text = "1";
            this.V_hilos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // R_mejorMed
            // 
            this.R_mejorMed.BackColor = System.Drawing.Color.White;
            this.R_mejorMed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.R_mejorMed.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.R_mejorMed.ForeColor = System.Drawing.SystemColors.ControlText;
            this.R_mejorMed.Location = new System.Drawing.Point(1664, 404);
            this.R_mejorMed.Name = "R_mejorMed";
            this.R_mejorMed.Size = new System.Drawing.Size(361, 24);
            this.R_mejorMed.TabIndex = 100;
            this.R_mejorMed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Sel_detGenetico);
            this.panel1.Controls.Add(this.Sel_dbGenetico);
            this.panel1.Controls.Add(this.Sel_destandarGenetico);
            this.panel1.Controls.Add(this.Sel_mediaGenetico);
            this.panel1.Controls.Add(this.Sel_det);
            this.panel1.Controls.Add(this.Sel_db);
            this.panel1.Controls.Add(this.Sel_destandar);
            this.panel1.Controls.Add(this.Sel_media);
            this.panel1.Location = new System.Drawing.Point(797, 691);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(861, 30);
            this.panel1.TabIndex = 105;
            // 
            // Sel_detGenetico
            // 
            this.Sel_detGenetico.AutoSize = true;
            this.Sel_detGenetico.Location = new System.Drawing.Point(777, 4);
            this.Sel_detGenetico.Name = "Sel_detGenetico";
            this.Sel_detGenetico.Size = new System.Drawing.Size(76, 24);
            this.Sel_detGenetico.TabIndex = 112;
            this.Sel_detGenetico.Text = "DE.T-G";
            this.Sel_detGenetico.UseVisualStyleBackColor = true;
            this.Sel_detGenetico.CheckedChanged += new System.EventHandler(this.Sel_silhGenetico_CheckedChanged);
            // 
            // Sel_dbGenetico
            // 
            this.Sel_dbGenetico.AutoSize = true;
            this.Sel_dbGenetico.Location = new System.Drawing.Point(695, 4);
            this.Sel_dbGenetico.Name = "Sel_dbGenetico";
            this.Sel_dbGenetico.Size = new System.Drawing.Size(66, 24);
            this.Sel_dbGenetico.TabIndex = 111;
            this.Sel_dbGenetico.Text = "DB-G";
            this.Sel_dbGenetico.UseVisualStyleBackColor = true;
            this.Sel_dbGenetico.CheckedChanged += new System.EventHandler(this.Sel_dbGenetico_CheckedChanged);
            // 
            // Sel_destandarGenetico
            // 
            this.Sel_destandarGenetico.AutoSize = true;
            this.Sel_destandarGenetico.Location = new System.Drawing.Point(613, 4);
            this.Sel_destandarGenetico.Name = "Sel_destandarGenetico";
            this.Sel_destandarGenetico.Size = new System.Drawing.Size(65, 24);
            this.Sel_destandarGenetico.TabIndex = 110;
            this.Sel_destandarGenetico.Text = "DE-G";
            this.Sel_destandarGenetico.UseVisualStyleBackColor = true;
            this.Sel_destandarGenetico.CheckedChanged += new System.EventHandler(this.Sel_destandarGenetico_CheckedChanged);
            // 
            // Sel_mediaGenetico
            // 
            this.Sel_mediaGenetico.AutoSize = true;
            this.Sel_mediaGenetico.Location = new System.Drawing.Point(523, 4);
            this.Sel_mediaGenetico.Name = "Sel_mediaGenetico";
            this.Sel_mediaGenetico.Size = new System.Drawing.Size(76, 24);
            this.Sel_mediaGenetico.TabIndex = 109;
            this.Sel_mediaGenetico.Text = "Med-G";
            this.Sel_mediaGenetico.UseVisualStyleBackColor = true;
            this.Sel_mediaGenetico.CheckedChanged += new System.EventHandler(this.Sel_mediaGenetico_CheckedChanged);
            // 
            // Sel_det
            // 
            this.Sel_det.AutoSize = true;
            this.Sel_det.Location = new System.Drawing.Point(227, 4);
            this.Sel_det.Name = "Sel_det";
            this.Sel_det.Size = new System.Drawing.Size(60, 24);
            this.Sel_det.TabIndex = 108;
            this.Sel_det.Text = "DE.T";
            this.Sel_det.UseVisualStyleBackColor = true;
            this.Sel_det.CheckedChanged += new System.EventHandler(this.Sel_silh_CheckedChanged);
            // 
            // Sel_db
            // 
            this.Sel_db.AutoSize = true;
            this.Sel_db.Location = new System.Drawing.Point(156, 4);
            this.Sel_db.Name = "Sel_db";
            this.Sel_db.Size = new System.Drawing.Size(50, 24);
            this.Sel_db.TabIndex = 107;
            this.Sel_db.Text = "DB";
            this.Sel_db.UseVisualStyleBackColor = true;
            this.Sel_db.CheckedChanged += new System.EventHandler(this.Sel_db_CheckedChanged);
            // 
            // Sel_destandar
            // 
            this.Sel_destandar.AutoSize = true;
            this.Sel_destandar.Location = new System.Drawing.Point(85, 4);
            this.Sel_destandar.Name = "Sel_destandar";
            this.Sel_destandar.Size = new System.Drawing.Size(49, 24);
            this.Sel_destandar.TabIndex = 106;
            this.Sel_destandar.Text = "DE";
            this.Sel_destandar.UseVisualStyleBackColor = true;
            this.Sel_destandar.CheckedChanged += new System.EventHandler(this.Sel_destandar_CheckedChanged);
            // 
            // Sel_media
            // 
            this.Sel_media.AutoSize = true;
            this.Sel_media.Checked = true;
            this.Sel_media.Location = new System.Drawing.Point(6, 4);
            this.Sel_media.Name = "Sel_media";
            this.Sel_media.Size = new System.Drawing.Size(60, 24);
            this.Sel_media.TabIndex = 105;
            this.Sel_media.TabStop = true;
            this.Sel_media.Text = "Med";
            this.Sel_media.UseVisualStyleBackColor = true;
            this.Sel_media.CheckedChanged += new System.EventHandler(this.Sel_media_CheckedChanged);
            // 
            // R_hilosPendientes
            // 
            this.R_hilosPendientes.BackColor = System.Drawing.Color.White;
            this.R_hilosPendientes.Location = new System.Drawing.Point(1766, 189);
            this.R_hilosPendientes.Name = "R_hilosPendientes";
            this.R_hilosPendientes.Size = new System.Drawing.Size(39, 20);
            this.R_hilosPendientes.TabIndex = 106;
            this.R_hilosPendientes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // R_tiempo
            // 
            this.R_tiempo.BackColor = System.Drawing.Color.White;
            this.R_tiempo.Location = new System.Drawing.Point(1560, 211);
            this.R_tiempo.Name = "R_tiempo";
            this.R_tiempo.Size = new System.Drawing.Size(72, 18);
            this.R_tiempo.TabIndex = 107;
            this.R_tiempo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(1638, 211);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(15, 20);
            this.label27.TabIndex = 108;
            this.label27.Text = "s";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.ForeColor = System.Drawing.Color.Red;
            this.label28.Location = new System.Drawing.Point(1740, 123);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(58, 20);
            this.label28.TabIndex = 110;
            this.label28.Text = "% Poda";
            // 
            // V_ppPoda
            // 
            this.V_ppPoda.ForeColor = System.Drawing.Color.Red;
            this.V_ppPoda.Location = new System.Drawing.Point(1811, 122);
            this.V_ppPoda.Name = "V_ppPoda";
            this.V_ppPoda.Size = new System.Drawing.Size(30, 27);
            this.V_ppPoda.TabIndex = 109;
            this.V_ppPoda.Text = "0";
            this.V_ppPoda.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.ForeColor = System.Drawing.Color.Red;
            this.label29.Location = new System.Drawing.Point(1846, 123);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(77, 20);
            this.label29.TabIndex = 112;
            this.label29.Text = "Veces más";
            // 
            // V_veces
            // 
            this.V_veces.ForeColor = System.Drawing.Color.Red;
            this.V_veces.Location = new System.Drawing.Point(1995, 120);
            this.V_veces.Name = "V_veces";
            this.V_veces.Size = new System.Drawing.Size(30, 27);
            this.V_veces.TabIndex = 111;
            this.V_veces.Text = "5";
            this.V_veces.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Mapa
            // 
            this.Mapa.Location = new System.Drawing.Point(1566, 178);
            this.Mapa.Name = "Mapa";
            this.Mapa.Size = new System.Drawing.Size(92, 27);
            this.Mapa.TabIndex = 113;
            this.Mapa.Text = "Mapa";
            this.Mapa.UseVisualStyleBackColor = true;
            this.Mapa.Click += new System.EventHandler(this.Mapa_Click);
            // 
            // V_normalizaCentroides
            // 
            this.V_normalizaCentroides.AutoSize = true;
            this.V_normalizaCentroides.ForeColor = System.Drawing.SystemColors.ControlText;
            this.V_normalizaCentroides.Location = new System.Drawing.Point(1846, 93);
            this.V_normalizaCentroides.Name = "V_normalizaCentroides";
            this.V_normalizaCentroides.Size = new System.Drawing.Size(169, 24);
            this.V_normalizaCentroides.TabIndex = 114;
            this.V_normalizaCentroides.Text = "Normaliza Centroide";
            this.V_normalizaCentroides.UseVisualStyleBackColor = true;
            // 
            // R_mejorDE
            // 
            this.R_mejorDE.BackColor = System.Drawing.Color.White;
            this.R_mejorDE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.R_mejorDE.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.R_mejorDE.ForeColor = System.Drawing.SystemColors.ControlText;
            this.R_mejorDE.Location = new System.Drawing.Point(1664, 429);
            this.R_mejorDE.Name = "R_mejorDE";
            this.R_mejorDE.Size = new System.Drawing.Size(361, 24);
            this.R_mejorDE.TabIndex = 115;
            this.R_mejorDE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // R_mejorDB
            // 
            this.R_mejorDB.BackColor = System.Drawing.Color.White;
            this.R_mejorDB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.R_mejorDB.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.R_mejorDB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.R_mejorDB.Location = new System.Drawing.Point(1664, 454);
            this.R_mejorDB.Name = "R_mejorDB";
            this.R_mejorDB.Size = new System.Drawing.Size(361, 24);
            this.R_mejorDB.TabIndex = 116;
            this.R_mejorDB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // R_mejorDET
            // 
            this.R_mejorDET.BackColor = System.Drawing.Color.White;
            this.R_mejorDET.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.R_mejorDET.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.R_mejorDET.ForeColor = System.Drawing.SystemColors.ControlText;
            this.R_mejorDET.Location = new System.Drawing.Point(1664, 479);
            this.R_mejorDET.Name = "R_mejorDET";
            this.R_mejorDET.Size = new System.Drawing.Size(361, 24);
            this.R_mejorDET.TabIndex = 117;
            this.R_mejorDET.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SalvaClasificacion
            // 
            this.SalvaClasificacion.Location = new System.Drawing.Point(1101, 727);
            this.SalvaClasificacion.Name = "SalvaClasificacion";
            this.SalvaClasificacion.Size = new System.Drawing.Size(142, 27);
            this.SalvaClasificacion.TabIndex = 118;
            this.SalvaClasificacion.Text = "Salva clasificación";
            this.SalvaClasificacion.UseVisualStyleBackColor = true;
            this.SalvaClasificacion.Click += new System.EventHandler(this.SalvaClasificacion_Click);
            // 
            // DistanciasCentroides
            // 
            this.DistanciasCentroides.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DistanciasCentroides.Location = new System.Drawing.Point(1451, 179);
            this.DistanciasCentroides.Name = "DistanciasCentroides";
            this.DistanciasCentroides.Size = new System.Drawing.Size(107, 27);
            this.DistanciasCentroides.TabIndex = 119;
            this.DistanciasCentroides.Text = "Ver distancias";
            this.DistanciasCentroides.UseVisualStyleBackColor = true;
            this.DistanciasCentroides.Click += new System.EventHandler(this.DistanciasCentroides_Click);
            // 
            // SalvaCaso
            // 
            this.SalvaCaso.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SalvaCaso.Location = new System.Drawing.Point(835, 727);
            this.SalvaCaso.Name = "SalvaCaso";
            this.SalvaCaso.Size = new System.Drawing.Size(134, 27);
            this.SalvaCaso.TabIndex = 120;
            this.SalvaCaso.Text = "Salva caso";
            this.SalvaCaso.UseVisualStyleBackColor = true;
            this.SalvaCaso.Click += new System.EventHandler(this.SalvaCaso_Click);
            // 
            // R_Fichero_Centroides
            // 
            this.R_Fichero_Centroides.BackColor = System.Drawing.Color.White;
            this.R_Fichero_Centroides.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.R_Fichero_Centroides.Location = new System.Drawing.Point(797, 34);
            this.R_Fichero_Centroides.Name = "R_Fichero_Centroides";
            this.R_Fichero_Centroides.Size = new System.Drawing.Size(763, 22);
            this.R_Fichero_Centroides.TabIndex = 121;
            this.R_Fichero_Centroides.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Asigna
            // 
            this.Asigna.Location = new System.Drawing.Point(1702, 29);
            this.Asigna.Name = "Asigna";
            this.Asigna.Size = new System.Drawing.Size(147, 27);
            this.Asigna.TabIndex = 122;
            this.Asigna.Text = "Asignar espectros";
            this.Asigna.UseVisualStyleBackColor = true;
            this.Asigna.Click += new System.EventHandler(this.Asigna_Click);
            // 
            // V_genetico
            // 
            this.V_genetico.AutoSize = true;
            this.V_genetico.Checked = true;
            this.V_genetico.CheckState = System.Windows.Forms.CheckState.Checked;
            this.V_genetico.ForeColor = System.Drawing.SystemColors.ControlText;
            this.V_genetico.Location = new System.Drawing.Point(907, 150);
            this.V_genetico.Name = "V_genetico";
            this.V_genetico.Size = new System.Drawing.Size(90, 24);
            this.V_genetico.TabIndex = 123;
            this.V_genetico.Text = "Genético";
            this.V_genetico.UseVisualStyleBackColor = true;
            // 
            // V_generaciones
            // 
            this.V_generaciones.Location = new System.Drawing.Point(999, 148);
            this.V_generaciones.Name = "V_generaciones";
            this.V_generaciones.Size = new System.Drawing.Size(51, 27);
            this.V_generaciones.TabIndex = 124;
            this.V_generaciones.Text = "2000";
            this.V_generaciones.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // V_minimizaGenetico
            // 
            this.V_minimizaGenetico.Location = new System.Drawing.Point(1175, 148);
            this.V_minimizaGenetico.Name = "V_minimizaGenetico";
            this.V_minimizaGenetico.Size = new System.Drawing.Size(30, 27);
            this.V_minimizaGenetico.TabIndex = 125;
            this.V_minimizaGenetico.Text = "0";
            this.V_minimizaGenetico.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(1055, 151);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(116, 20);
            this.label30.TabIndex = 126;
            this.label30.Text = "Minimiza (0 a 3)";
            // 
            // V_torneo
            // 
            this.V_torneo.AutoSize = true;
            this.V_torneo.Checked = true;
            this.V_torneo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.V_torneo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.V_torneo.Location = new System.Drawing.Point(1213, 150);
            this.V_torneo.Name = "V_torneo";
            this.V_torneo.Size = new System.Drawing.Size(77, 24);
            this.V_torneo.TabIndex = 127;
            this.V_torneo.Text = "Torneo";
            this.V_torneo.UseVisualStyleBackColor = true;
            // 
            // V_ppTorneo
            // 
            this.V_ppTorneo.Location = new System.Drawing.Point(1292, 148);
            this.V_ppTorneo.Name = "V_ppTorneo";
            this.V_ppTorneo.Size = new System.Drawing.Size(35, 27);
            this.V_ppTorneo.TabIndex = 128;
            this.V_ppTorneo.Text = "10";
            this.V_ppTorneo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // V_mutar
            // 
            this.V_mutar.AutoSize = true;
            this.V_mutar.Checked = true;
            this.V_mutar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.V_mutar.ForeColor = System.Drawing.Color.Blue;
            this.V_mutar.Location = new System.Drawing.Point(1358, 150);
            this.V_mutar.Name = "V_mutar";
            this.V_mutar.Size = new System.Drawing.Size(70, 24);
            this.V_mutar.TabIndex = 129;
            this.V_mutar.Text = "Mutar";
            this.V_mutar.UseVisualStyleBackColor = true;
            // 
            // V_ppMutar
            // 
            this.V_ppMutar.ForeColor = System.Drawing.Color.Blue;
            this.V_ppMutar.Location = new System.Drawing.Point(1508, 148);
            this.V_ppMutar.Name = "V_ppMutar";
            this.V_ppMutar.Size = new System.Drawing.Size(35, 27);
            this.V_ppMutar.TabIndex = 130;
            this.V_ppMutar.Text = "10";
            this.V_ppMutar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // V_mutarCuanto
            // 
            this.V_mutarCuanto.ForeColor = System.Drawing.Color.Blue;
            this.V_mutarCuanto.Location = new System.Drawing.Point(1623, 147);
            this.V_mutarCuanto.Name = "V_mutarCuanto";
            this.V_mutarCuanto.Size = new System.Drawing.Size(35, 27);
            this.V_mutarCuanto.TabIndex = 131;
            this.V_mutarCuanto.Text = "5";
            this.V_mutarCuanto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(1330, 151);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(21, 20);
            this.label33.TabIndex = 134;
            this.label33.Text = "%";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.ForeColor = System.Drawing.Color.Blue;
            this.label34.Location = new System.Drawing.Point(1548, 151);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(71, 20);
            this.label34.TabIndex = 135;
            this.label34.Text = "%Cuantía";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.ForeColor = System.Drawing.Color.Blue;
            this.label35.Location = new System.Drawing.Point(1435, 151);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(69, 20);
            this.label35.TabIndex = 136;
            this.label35.Text = "%Valores";
            // 
            // V_modoCentroide
            // 
            this.V_modoCentroide.Location = new System.Drawing.Point(1811, 152);
            this.V_modoCentroide.Name = "V_modoCentroide";
            this.V_modoCentroide.Size = new System.Drawing.Size(30, 27);
            this.V_modoCentroide.TabIndex = 137;
            this.V_modoCentroide.Text = "0";
            this.V_modoCentroide.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(1737, 152);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(74, 20);
            this.label36.TabIndex = 138;
            this.label36.Text = "Centroide";
            // 
            // Restar
            // 
            this.Restar.Location = new System.Drawing.Point(1938, 2);
            this.Restar.Name = "Restar";
            this.Restar.Size = new System.Drawing.Size(87, 27);
            this.Restar.TabIndex = 139;
            this.Restar.Text = "Restar";
            this.Restar.UseVisualStyleBackColor = true;
            this.Restar.Click += new System.EventHandler(this.Restar_Click);
            // 
            // VerCentroides
            // 
            this.VerCentroides.ForeColor = System.Drawing.SystemColors.ControlText;
            this.VerCentroides.Location = new System.Drawing.Point(1208, 179);
            this.VerCentroides.Name = "VerCentroides";
            this.VerCentroides.Size = new System.Drawing.Size(133, 27);
            this.VerCentroides.TabIndex = 140;
            this.VerCentroides.Text = "Ver Centroides";
            this.VerCentroides.UseVisualStyleBackColor = true;
            this.VerCentroides.Click += new System.EventHandler(this.VerCentroides_Click);
            // 
            // VerTemperatura
            // 
            this.VerTemperatura.ForeColor = System.Drawing.SystemColors.ControlText;
            this.VerTemperatura.Location = new System.Drawing.Point(1068, 179);
            this.VerTemperatura.Name = "VerTemperatura";
            this.VerTemperatura.Size = new System.Drawing.Size(133, 27);
            this.VerTemperatura.TabIndex = 141;
            this.VerTemperatura.Text = "Ver Temperatura";
            this.VerTemperatura.UseVisualStyleBackColor = true;
            this.VerTemperatura.Click += new System.EventHandler(this.VerTemperatura_Click);
            // 
            // R2
            // 
            this.R2.ForeColor = System.Drawing.Color.Blue;
            this.R2.Location = new System.Drawing.Point(336, 603);
            this.R2.Name = "R2";
            this.R2.Size = new System.Drawing.Size(155, 27);
            this.R2.TabIndex = 142;
            this.R2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LeerCromosomas
            // 
            this.LeerCromosomas.Location = new System.Drawing.Point(797, 148);
            this.LeerCromosomas.Name = "LeerCromosomas";
            this.LeerCromosomas.Size = new System.Drawing.Size(106, 27);
            this.LeerCromosomas.TabIndex = 143;
            this.LeerCromosomas.Text = "Cromosomas";
            this.LeerCromosomas.UseVisualStyleBackColor = true;
            this.LeerCromosomas.Click += new System.EventHandler(this.LeerCromosomas_Click);
            // 
            // SalvaCromosomas
            // 
            this.SalvaCromosomas.Location = new System.Drawing.Point(1247, 727);
            this.SalvaCromosomas.Name = "SalvaCromosomas";
            this.SalvaCromosomas.Size = new System.Drawing.Size(145, 27);
            this.SalvaCromosomas.TabIndex = 144;
            this.SalvaCromosomas.Text = "Salva cromosomas";
            this.SalvaCromosomas.UseVisualStyleBackColor = true;
            this.SalvaCromosomas.Click += new System.EventHandler(this.SalvaCromosomas_Click);
            // 
            // EjecutaGenetico
            // 
            this.EjecutaGenetico.Location = new System.Drawing.Point(970, 179);
            this.EjecutaGenetico.Name = "EjecutaGenetico";
            this.EjecutaGenetico.Size = new System.Drawing.Size(92, 27);
            this.EjecutaGenetico.TabIndex = 145;
            this.EjecutaGenetico.Text = "Genético";
            this.EjecutaGenetico.UseVisualStyleBackColor = true;
            this.EjecutaGenetico.Click += new System.EventHandler(this.EjecutaGenetico_Click);
            // 
            // LeerClasificacion
            // 
            this.LeerClasificacion.Location = new System.Drawing.Point(1885, 30);
            this.LeerClasificacion.Name = "LeerClasificacion";
            this.LeerClasificacion.Size = new System.Drawing.Size(140, 27);
            this.LeerClasificacion.TabIndex = 146;
            this.LeerClasificacion.Text = "Leer clasificación";
            this.LeerClasificacion.UseVisualStyleBackColor = true;
            this.LeerClasificacion.Click += new System.EventHandler(this.LeerClasificacion_Click);
            // 
            // ExcluirSinMaximo
            // 
            this.ExcluirSinMaximo.AutoSize = true;
            this.ExcluirSinMaximo.Checked = true;
            this.ExcluirSinMaximo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ExcluirSinMaximo.Location = new System.Drawing.Point(459, 70);
            this.ExcluirSinMaximo.Name = "ExcluirSinMaximo";
            this.ExcluirSinMaximo.Size = new System.Drawing.Size(154, 24);
            this.ExcluirSinMaximo.TabIndex = 147;
            this.ExcluirSinMaximo.Text = "Excluir sin máximo";
            this.ExcluirSinMaximo.UseVisualStyleBackColor = true;
            // 
            // Recalcular
            // 
            this.Recalcular.AutoSize = true;
            this.Recalcular.Checked = true;
            this.Recalcular.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Recalcular.Location = new System.Drawing.Point(339, 70);
            this.Recalcular.Name = "Recalcular";
            this.Recalcular.Size = new System.Drawing.Size(99, 24);
            this.Recalcular.TabIndex = 148;
            this.Recalcular.Text = "Recalcular";
            this.Recalcular.UseVisualStyleBackColor = true;
            // 
            // Partir
            // 
            this.Partir.AutoSize = true;
            this.Partir.Checked = true;
            this.Partir.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Partir.Location = new System.Drawing.Point(428, 100);
            this.Partir.Name = "Partir";
            this.Partir.Size = new System.Drawing.Size(65, 24);
            this.Partir.TabIndex = 149;
            this.Partir.Text = "Partir";
            this.Partir.UseVisualStyleBackColor = true;
            // 
            // SeparaT
            // 
            this.SeparaT.Location = new System.Drawing.Point(1347, 178);
            this.SeparaT.Name = "SeparaT";
            this.SeparaT.Size = new System.Drawing.Size(92, 27);
            this.SeparaT.TabIndex = 150;
            this.SeparaT.Text = "Separa T";
            this.SeparaT.UseVisualStyleBackColor = true;
            this.SeparaT.Click += new System.EventHandler(this.SeparaT_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2032, 757);
            this.Controls.Add(this.SeparaT);
            this.Controls.Add(this.Partir);
            this.Controls.Add(this.Recalcular);
            this.Controls.Add(this.ExcluirSinMaximo);
            this.Controls.Add(this.LeerClasificacion);
            this.Controls.Add(this.EjecutaGenetico);
            this.Controls.Add(this.SalvaCromosomas);
            this.Controls.Add(this.LeerCromosomas);
            this.Controls.Add(this.R2);
            this.Controls.Add(this.VerTemperatura);
            this.Controls.Add(this.VerCentroides);
            this.Controls.Add(this.Restar);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.V_modoCentroide);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.V_mutarCuanto);
            this.Controls.Add(this.V_ppMutar);
            this.Controls.Add(this.V_mutar);
            this.Controls.Add(this.V_ppTorneo);
            this.Controls.Add(this.V_torneo);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.V_minimizaGenetico);
            this.Controls.Add(this.V_generaciones);
            this.Controls.Add(this.V_genetico);
            this.Controls.Add(this.Asigna);
            this.Controls.Add(this.R_Fichero_Centroides);
            this.Controls.Add(this.SalvaCaso);
            this.Controls.Add(this.DistanciasCentroides);
            this.Controls.Add(this.SalvaClasificacion);
            this.Controls.Add(this.R_mejorDET);
            this.Controls.Add(this.R_mejorDB);
            this.Controls.Add(this.R_mejorDE);
            this.Controls.Add(this.V_normalizaCentroides);
            this.Controls.Add(this.Mapa);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.V_veces);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.V_ppPoda);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.R_tiempo);
            this.Controls.Add(this.R_hilosPendientes);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.R_mejorMed);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.V_hilos);
            this.Controls.Add(this.V_silhouette);
            this.Controls.Add(this.R_clusterCercano);
            this.Controls.Add(this.ListaIntentos);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.V_pases);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.V_hasta);
            this.Controls.Add(this.V_desde);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.R_confianza);
            this.Controls.Add(this.SalvaCentroides);
            this.Controls.Add(this.LeeCentroides);
            this.Controls.Add(this.V_dibujaTodo);
            this.Controls.Add(this.V_forzar);
            this.Controls.Add(this.MLKMeans);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.V_modoIniciar);
            this.Controls.Add(this.SalvaEspectrosAnalizados);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.V_tipoDatosY);
            this.Controls.Add(this.R_filas);
            this.Controls.Add(this.V_podar);
            this.Controls.Add(this.R_iteracionActual);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.V_min_mejora_de);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.V_max_empeora_de);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.V_min_mejora_md);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.V_max_empeora_md);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.V_tipoDistancia);
            this.Controls.Add(this.ListaEvolucion);
            this.Controls.Add(this.SalvaClusters);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.V_semilla);
            this.Controls.Add(this.IndiceEspectro);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.R_Distancia);
            this.Controls.Add(this.R_espectros);
            this.Controls.Add(this.LeerEspectrosAnalizados);
            this.Controls.Add(this.R_Fichero_Clusters);
            this.Controls.Add(this.FilaCluster);
            this.Controls.Add(this.TablaClusters);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.V_max_Iteraciones);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.V_nclusters);
            this.Controls.Add(this.CreaClusters);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.Reducir);
            this.Controls.Add(this.V_reducirCada);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Puntos);
            this.Controls.Add(this.Emision);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.Absorcion);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Esc);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Movil);
            this.Controls.Add(this.Proporcional);
            this.Controls.Add(this.R_corte);
            this.Controls.Add(this.Fcorte);
            this.Controls.Add(this.Todo);
            this.Controls.Add(this.DE);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.FilaFichero);
            this.Controls.Add(this.R_fichero);
            this.Controls.Add(this.Vertical);
            this.Controls.Add(this.SalvaPicos);
            this.Controls.Add(this.Recalcula);
            this.Controls.Add(this.SalvaImagen);
            this.Controls.Add(this.SelPicos);
            this.Controls.Add(this.Corte);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.F);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Coeficientes);
            this.Controls.Add(this.tabla);
            this.Controls.Add(this.Grado);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Leer);
            this.Controls.Add(this.Calcula);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Análisis y clasificación de espectros";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Coeficientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilaFichero)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablaClusters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilaCluster)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Calcula;
        private System.Windows.Forms.Button Leer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Grado;
        private System.Windows.Forms.DataGridView tabla;
        private System.Windows.Forms.DataGridView Coeficientes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label F;
        private System.Windows.Forms.TextBox Corte;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button SelPicos;
        private System.Windows.Forms.Button SalvaImagen;
        private System.Windows.Forms.Button Recalcula;
        private System.Windows.Forms.Button SalvaPicos;
        private System.Windows.Forms.CheckBox Vertical;
        private System.Windows.Forms.ComboBox R_fichero;
        private System.Windows.Forms.NumericUpDown FilaFichero;
        private System.Windows.Forms.Label DE;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Todo;
        private System.Windows.Forms.TextBox Fcorte;
        private System.Windows.Forms.Label R_corte;
        private System.Windows.Forms.CheckBox Proporcional;
        private System.Windows.Forms.TextBox Movil;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Esc;
        private System.Windows.Forms.Label Absorcion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label Emision;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label Puntos;
        private System.Windows.Forms.TextBox V_reducirCada;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button Reducir;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button CreaClusters;
        private System.Windows.Forms.TextBox V_nclusters;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox V_max_Iteraciones;
        private System.Windows.Forms.DataGridView TablaClusters;
        public System.Windows.Forms.NumericUpDown FilaCluster;
        private System.Windows.Forms.Label R_Fichero_Clusters;
        private System.Windows.Forms.Button LeerEspectrosAnalizados;
        private System.Windows.Forms.Label R_espectros;
        private System.Windows.Forms.Label R_Distancia;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label IndiceEspectro;
        private System.Windows.Forms.TextBox V_semilla;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button SalvaClusters;
        private System.Windows.Forms.ListBox ListaEvolucion;
        private System.Windows.Forms.TextBox V_tipoDistancia;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox V_max_empeora_md;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox V_min_mejora_md;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox V_min_mejora_de;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox V_max_empeora_de;
        private System.Windows.Forms.Label R_iteracionActual;
        private System.Windows.Forms.CheckBox V_podar;
        private System.Windows.Forms.Label R_filas;
        private System.Windows.Forms.Label label22;
        public System.Windows.Forms.TextBox V_tipoDatosY;
        private System.Windows.Forms.Button SalvaEspectrosAnalizados;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox V_modoIniciar;
        private System.Windows.Forms.Button MLKMeans;
        private System.Windows.Forms.CheckBox V_forzar;
        private System.Windows.Forms.CheckBox V_dibujaTodo;
        private System.Windows.Forms.Button LeeCentroides;
        private System.Windows.Forms.Button SalvaCentroides;
        private System.Windows.Forms.Label R_confianza;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox V_desde;
        private System.Windows.Forms.TextBox V_hasta;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox V_pases;
        private System.Windows.Forms.ListBox ListaIntentos;
        private System.Windows.Forms.Label R_clusterCercano;
        private System.Windows.Forms.CheckBox V_silhouette;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox V_hilos;
        private System.Windows.Forms.Label R_mejorMed;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton Sel_det;
        private System.Windows.Forms.RadioButton Sel_db;
        private System.Windows.Forms.RadioButton Sel_destandar;
        private System.Windows.Forms.RadioButton Sel_media;
        private System.Windows.Forms.Label R_hilosPendientes;
        private System.Windows.Forms.Label R_tiempo;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox V_ppPoda;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox V_veces;
        private System.Windows.Forms.Button Mapa;
        public System.Windows.Forms.CheckBox V_normalizaCentroides;
        private System.Windows.Forms.Label R_mejorDE;
        private System.Windows.Forms.Label R_mejorDB;
        private System.Windows.Forms.Label R_mejorDET;
        private System.Windows.Forms.Button SalvaClasificacion;
        private System.Windows.Forms.Button DistanciasCentroides;
        private System.Windows.Forms.Button SalvaCaso;
        private System.Windows.Forms.Label R_Fichero_Centroides;
        private System.Windows.Forms.Button Asigna;
        private System.Windows.Forms.CheckBox V_genetico;
        private System.Windows.Forms.TextBox V_generaciones;
        private System.Windows.Forms.RadioButton Sel_detGenetico;
        private System.Windows.Forms.RadioButton Sel_dbGenetico;
        private System.Windows.Forms.RadioButton Sel_destandarGenetico;
        private System.Windows.Forms.RadioButton Sel_mediaGenetico;
        private System.Windows.Forms.TextBox V_minimizaGenetico;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.CheckBox V_torneo;
        private System.Windows.Forms.TextBox V_ppTorneo;
        private System.Windows.Forms.CheckBox V_mutar;
        private System.Windows.Forms.TextBox V_ppMutar;
        private System.Windows.Forms.TextBox V_mutarCuanto;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox V_modoCentroide;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Button Restar;
        private Button VerCentroides;
        private Button VerTemperatura;
        private TextBox R2;
        private Button LeerCromosomas;
        private Button SalvaCromosomas;
        private Button EjecutaGenetico;
        private Button LeerClasificacion;
        private CheckBox ExcluirSinMaximo;
        private CheckBox Recalcular;
        private CheckBox Partir;
        private Button SeparaT;
    }
}

