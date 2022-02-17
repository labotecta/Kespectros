using System.Drawing.Imaging;

namespace KEspectro
{
    public partial class Form3 : Form
    {
        public Form1 principal;

        private int ancho_lienzo;
        private int alto_lienzo;
        private int m_izq;
        private int m_dch;
        private int m_sup;
        private int m_inf;
        private int m_rx;
        private int medio_ancho_dato;
        private int ancho_dato;
        private Font fte;
        private readonly Color[] color = { Color.Black, Color.Red, Color.Green, Color.Blue, Color.Orange, Color.Purple, Color.DarkGreen, Color.Turquoise, Color.Aqua, Color.Gray, Color.Yellow };
        private readonly Brush[] brocha = { Brushes.Black, Brushes.Red, Brushes.Green, Brushes.Blue, Brushes.Orange, Brushes.Purple, Brushes.DarkGreen, Brushes.Turquoise, Brushes.Aqua, Brushes.Gray, Brushes.Yellow };
        private bool omite_cambios;
        public Bitmap img;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            int ancho = ClientSize.Width;
            int alto = ClientSize.Height;

            // Lienzo

            int ml_sup = 60;
            int ml_izq = 4;
            ancho_lienzo = ancho - ml_izq - 4;
            alto_lienzo = alto - ml_sup - 60;
            Lienzo.Location = new Point(ml_izq, ml_sup);
            Lienzo.Size = new Size(ancho_lienzo, alto_lienzo);
            double rejex = ancho_lienzo / Form1.ancho_lienzo_referencia;
            double rejey = alto_lienzo / Form1.alto_lienzo_referencia;
            double reje = Math.Max(rejex, rejey);
            m_izq = (int)Math.Round(50 * reje);
            m_dch = (int)Math.Round(50 * reje);
            m_sup = (int)Math.Round(80 * reje);
            m_inf = (int)Math.Round(90 * reje);
            m_rx = (int)Math.Round(10 * reje);
            medio_ancho_dato = (int)Math.Round(4 * reje);
            ancho_dato = medio_ancho_dato + medio_ancho_dato;
            fte = new Font("Segoe UI", (int)Math.Round(14 * reje), FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label2.ForeColor = Color.White;
            label3.ForeColor = Color.White;
            label4.ForeColor = Color.White;
            label5.ForeColor = Color.White;
            label6.ForeColor = Color.White;
            label7.ForeColor = Color.White;
            label10.ForeColor = Color.White;
            label1.BackColor = color[0];
            label2.BackColor = color[1];
            label3.BackColor = color[2];
            label4.BackColor = color[3];
            label5.BackColor = color[4];
            label6.BackColor = color[5];
            label7.BackColor = color[6];
            label8.BackColor = color[7];
            label9.BackColor = color[8];
            label10.BackColor = color[9];
            label11.BackColor = color[10];
        }
        public void Dibuja(Form1.Caja caja, List<Form1.EspectroAnalizado> espectros, int cluster_sel, int mejorcaja)
        {
            omite_cambios = true;
            switch (mejorcaja)
            {
                case 0:
                    Sel_media.Checked = true;
                    break;
                case 1:
                    Sel_destandar.Checked = true;
                    break;
                case 2:
                    Sel_db.Checked = true;
                    break;
                default:
                    Sel_silh.Checked = true;
                    break;
            }
            Sel_clusters.Items.Clear();
            Sel_clusters.Items.Add("Todos");
            for (int i = 0; i < caja.clusters.Length; i++)
            {
                Sel_clusters.Items.Add(i + 1);
            }
            Sel_clusters.SelectedIndex = cluster_sel + 1;
            omite_cambios = false;
            int ancho = Lienzo.Width;
            int alto = Lienzo.Height;
            img = new Bitmap(ancho, alto, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(img);
            g.FillRectangle(Brushes.White, 0, 0, ancho, alto);
            int n;
            double armin;
            double armax;
            double fx;
            double decmin;
            double decmax;
            double fy;
            float px;
            float py;
            string s;
            int at;

            n = espectros.Count;
            double[] AR = new double[n];
            double[] DEC = new double[n];

            for (int i = 0; i < n; i++)
            {
                AR[i] = espectros[i].AR;
                DEC[i] = espectros[i].DEC;
            }
            armin = 0;
            armax = 360;
            decmin = -90;
            decmax = 90;

            fx = (ancho - m_izq - m_dch) / (armax - armin);
            fy = (alto - m_sup - m_inf) / (decmax - decmin);
            g.DrawString(string.Format("{0:N1}", armin), fte, Brushes.Black, m_rx, alto_lienzo - m_inf);
            s = string.Format("{0:N1}", armax);
            at = (int)g.MeasureString(s, fte).Width;
            g.DrawString(s, fte, Brushes.Black, ancho_lienzo - m_dch - at - m_rx, alto_lienzo - m_inf);
            s = "Ascensión recta (grados)";
            at = (int)g.MeasureString(s, fte).Width;
            g.DrawString(s, fte, Brushes.Black, (ancho_lienzo - m_dch - at) / 2, alto_lienzo - m_inf);
            s = string.Format("{0:N1}", decmin);
            at = (int)g.MeasureString(s, fte).Width;
            g.DrawString(s, fte, Brushes.Blue, 10, alto_lienzo - m_inf - at, new StringFormat(StringFormatFlags.DirectionVertical));
            s = string.Format("{0:N1}", decmax);
            g.DrawString(s, fte, Brushes.Blue, 10, 10, new StringFormat(StringFormatFlags.DirectionVertical));
            s = "Declinación (grados)";
            at = (int)g.MeasureString(s, fte).Width;
            StringFormat orientacion = new StringFormat();
            orientacion.FormatFlags = StringFormatFlags.DirectionVertical;
            g.DrawString(s, fte, Brushes.Blue, 10, (alto_lienzo - m_inf - at) / 2, orientacion);

            int ind_espectro;
            int ind;
            if (cluster_sel == -1)
            {
                for (int ind_cluster = 0; ind_cluster < caja.clusters.Length; ind_cluster++)
                {
                    ind = caja.orden[ind_cluster];
                    for (int i = 0; i < caja.clusters[ind].n; i++)
                    {
                        ind_espectro = caja.clusters[ind].ind_f_espectros[i];
                        px = m_izq + (float)((AR[ind_espectro] - armin) * fx) - medio_ancho_dato;
                        py = m_sup + (float)(alto - m_sup - m_inf - (DEC[ind_espectro] - decmin) * fy) - medio_ancho_dato;
                        g.FillEllipse(brocha[ind_cluster % brocha.Length], px, py, ancho_dato, ancho_dato);
                    }
                }
            }
            else
            {
                ind = caja.orden[cluster_sel];
                for (int i = 0; i < caja.clusters[ind].n; i++)
                {
                    ind_espectro = caja.clusters[ind].ind_f_espectros[i];
                    px = m_izq + (float)((AR[ind_espectro] - armin) * fx) - medio_ancho_dato;
                    py = m_sup + (float)(alto - m_sup - m_inf - (DEC[ind_espectro] - decmin) * fy) - medio_ancho_dato;
                    g.FillEllipse(brocha[cluster_sel % brocha.Length], px, py, ancho_dato, ancho_dato);
                }
            }
            Lienzo.Image = img;
            Lienzo.Refresh();
        }
        private int MejorCajaSeleccionada()
        {
            if (Sel_media.Checked) return 0;
            else if (Sel_destandar.Checked) return 1;
            else if (Sel_db.Checked) return 2;
            return 3;
        }
        private void Sel_media_CheckedChanged(object sender, EventArgs e)
        {
            CambiaSelSel();
        }
        private void Sel_destandar_CheckedChanged(object sender, EventArgs e)
        {
            CambiaSelSel();
        }
        private void Sel_db_CheckedChanged(object sender, EventArgs e)
        {
            CambiaSelSel();
        }
        private void Sel_silh_CheckedChanged(object sender, EventArgs e)
        {
            CambiaSelSel();
        }
        private void Sel_clusters_SelectedIndexChanged(object sender, EventArgs e)
        {
            CambiaSelSel();
        }
        private void CambiaSelSel()
        {
            if (omite_cambios) return;
            int m = MejorCajaSeleccionada();
            int c = Sel_clusters.SelectedIndex - 1;
            principal.CambiaSelSel(m, c);
        }
        private void Salvar_Click(object sender, EventArgs e)
        {
            if (img == null) return;
            SaveFileDialog ficheroescritura = new SaveFileDialog()
            {
                Filter = "PNG |*.png|TODO |*.*",
                FilterIndex = 1
            };
            if (ficheroescritura.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    img.Save(ficheroescritura.FileName, ImageFormat.Png);
                    Console.Beep();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(this, Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
