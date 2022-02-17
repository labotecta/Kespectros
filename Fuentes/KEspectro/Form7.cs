using System.Drawing.Imaging;
using System.Text;

namespace KEspectro
{
    public partial class Form7 : Form
    {
        public Form1 principal;

        private int ancho_lienzo;
        private int alto_lienzo;
        private int m_izq;
        private int m_dch;
        private int m_sup;
        private int m_inf;
        private int m_rx;
        private int m_ry;
        private int m_ly;
        private int m_ily;
        private int medio_ancho_dato;
        private int ancho_dato;
        private Font fte;
        private Font fte_f;

        private double[] x;
        private Form1.Caja caja;
        private int nclusters;
        private int ndatos;
        private double xmin;
        private double xmax;
        private double ymin;
        private double ymax;
        private double fx;
        private double fy;
        private readonly Brush[] brocha =
        {
            new SolidBrush(Color.FromArgb(0xff,0x00,0x00,0x00)),
            new SolidBrush(Color.FromArgb(0xff,0x13,0xc0,0xe5)),
            new SolidBrush(Color.FromArgb(0xff,0x80,0x00,0x00)),
            new SolidBrush(Color.FromArgb(0xff,0x0e,0x2a,0x55)),
            new SolidBrush(Color.FromArgb(0xff,0x7e,0xc5,0x44)),
            new SolidBrush(Color.FromArgb(0xff,0xff,0x00,0x00)),
            new SolidBrush(Color.FromArgb(0xff,0x00,0xb0,0x00)),
            new SolidBrush(Color.FromArgb(0xff,0x00,0x00,0xff)),
            new SolidBrush(Color.FromArgb(0xff,0x40,0x40,0x40)),
            new SolidBrush(Color.FromArgb(0xff,0xf0,0xf0,0x00)),
            new SolidBrush(Color.FromArgb(0xff,0x00,0x60,0x70)),
            new SolidBrush(Color.FromArgb(0xff,0x00,0xb0,0xb0)),
            new SolidBrush(Color.FromArgb(0xff,0xd0,0x00,0xd0))
        };
        private readonly Pen[] lapiz =
        {
            new Pen(Color.FromArgb(0xff,0x00,0x00,0x00)),
            new Pen(Color.FromArgb(0xff,0x13,0xc0,0xe5)),
            new Pen(Color.FromArgb(0xff,0x80,0x00,0x00)),
            new Pen(Color.FromArgb(0xff,0x0e,0x2a,0x55)),
            new Pen(Color.FromArgb(0xff,0x7e,0xc5,0x44)),
            new Pen(Color.FromArgb(0xff,0xff,0x00,0x00)),
            new Pen(Color.FromArgb(0xff,0x00,0xb0,0x00)),
            new Pen(Color.FromArgb(0xff,0x00,0x00,0xff)),
            new Pen(Color.FromArgb(0xff,0x40,0x40,0x40)),
            new Pen(Color.FromArgb(0xff,0xf0,0xf0,0x00)),
            new Pen(Color.FromArgb(0xff,0x00,0x60,0x70)),
            new Pen(Color.FromArgb(0xff,0x00,0xb0,0xb0)),
            new Pen(Color.FromArgb(0xff,0xd0,0x00,0xd0))
        };
        private Pen lapiz_negro;
        public Bitmap img;
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }
        public void Escala(Form1.Caja caja)
        {
            int ancho = Screen.PrimaryScreen.WorkingArea.Width;
            int alto = Screen.PrimaryScreen.WorkingArea.Height;

            // Lienzo

            int ml_sup = 56;
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
            m_sup = (int)Math.Round(50 * reje);
            m_inf = (int)Math.Round(50 * reje);
            m_rx = (int)Math.Round(2 * reje);
            m_ry = (int)Math.Round(10 * reje);
            m_ly = (int)Math.Round(2 * reje);
            m_ily = (int)Math.Round(25 * reje);
            medio_ancho_dato = (int)Math.Round(4 * reje);
            ancho_dato = medio_ancho_dato + medio_ancho_dato;
            fte = new Font("Segoe UI", (int)Math.Round(14 * reje), FontStyle.Regular, GraphicsUnit.Point);
            fte_f = new Font("Courier New", (int)Math.Round(14 * reje), FontStyle.Bold, GraphicsUnit.Point);
            lapiz_negro = new Pen(Color.Black, (int)Math.Round(2 * reje));
            foreach (Pen p in lapiz)
            {
                p.Width = (int)Math.Round(3 * reje); ;
            }
            if (caja == null)
            {
                return;
            }
            this.caja = caja;
            nclusters = caja.clusters.Length;
            ndatos = principal.espectros[0].n;
            xmin = double.MaxValue;
            xmax = double.MinValue;
            x = new double[ndatos];
            for (int i = 0; i < ndatos; i++)
            {
                x[i] = principal.espectros[0].x[i];
                if (x[i] < xmin) xmin = x[i];
                if (x[i] > xmax) xmax = x[i];
            }
            fx = (xmax - xmin == 0) ? 1 : (ancho_lienzo - m_izq - m_dch) / (xmax - xmin);
        }
        private void DibujaEjeX(Graphics g)
        {
            float px;
            float py;
            string s;
            int at;

            // Eje X

            px = m_izq;
            py = alto_lienzo - (m_ly + fte.Height);
            g.DrawLine(lapiz_negro, px, py, px, alto_lienzo - m_inf);
            px = ancho_lienzo - m_dch;
            g.DrawLine(lapiz_negro, px, py, px, alto_lienzo - m_inf);

            s = string.Format("{0:N1}", xmin);
            at = (int)g.MeasureString(s, fte).Width;
            px = m_izq - at / 2;
            if (px < m_rx) px = m_rx;
            py = alto_lienzo - fte.Height - m_ly;
            g.DrawString(s, fte, Brushes.Black, px, py);

            s = string.Format("{0:N1}", xmax);
            at = (int)g.MeasureString(s, fte).Width;
            px = ancho_lienzo - m_dch - at / 2;
            py = alto_lienzo - fte.Height - m_ly;
            g.DrawString(s, fte, Brushes.Black, px, py);

            s = "Longitud de onda en Ångstrom";     // 1 Ångstrom = 10 elevado a -8 cm = 10 nm)
            at = (int)g.MeasureString(s, fte).Width;
            px = m_izq + (ancho_lienzo - m_izq - m_dch - at) / 2;
            py = alto_lienzo - fte.Height - m_ly;
            g.DrawString(s, fte, Brushes.Black, px, py);
        }
        private void DibujaEjeY(Graphics g)
        {
            float px;
            float py;
            string s;
            int at;

            // Eje Y

            px = m_rx + fte.Height;
            py = m_sup;
            g.DrawLine(lapiz_negro, px, py, m_izq, py);
            py = alto_lienzo - m_inf;
            g.DrawLine(lapiz_negro, px, py, m_izq, py);

            s = string.Format("{0:N1}", ymin);
            at = (int)g.MeasureString(s, fte).Width;
            px = m_rx;
            py = alto_lienzo - m_inf - at;
            g.DrawString(s, fte, Brushes.Black, px, py, new StringFormat(StringFormatFlags.DirectionVertical));
            s = string.Format("{0:N1}", ymax);
            py = m_ry;
            g.DrawString(s, fte, Brushes.Black, px, py, new StringFormat(StringFormatFlags.DirectionVertical));

        }
        public void Dibuja()
        {
            img = new Bitmap(ancho_lienzo, alto_lienzo, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(img);
            g.FillRectangle(Brushes.White, 0, 0, ancho_lienzo, alto_lienzo);
            if (caja == null)
            {
                Lienzo.Image = img;
                Lienzo.Refresh();
                return;
            }
            float px;
            float py;
            string s;
            int hasta;
            if (principal.calcula_hasta == 0 || principal.calcula_hasta > ndatos) hasta = ndatos;
            else if (principal.calcula_hasta < 0) hasta = ndatos + principal.calcula_hasta;
            else hasta = principal.calcula_hasta;
            DibujaEjeX(g);
            double tmin = Convert.ToDouble(V_tmin.Text.Trim());
            double tmax = Convert.ToDouble(V_tmax.Text.Trim());
            double[][] y = new double[nclusters][];
            for (int i = 0; i < nclusters; i++)
            {
                y[i] = new double[ndatos];
                Array.Clear(y[i], 0, ndatos);
            }
            int[] nespectros = new int[nclusters];
            Array.Clear(nespectros, 0, nclusters);
            int ind_e;
            ymin = double.MaxValue;
            ymax = double.MinValue;
            for (int i = 0; i < nclusters; i++)
            {
                if (caja.clusters[i].n == 0) continue;
                for (int j = 0; j < caja.clusters[i].n; j++)
                {
                    ind_e = caja.clusters[i].ind_f_espectros[j];
                    if (principal.espectros[ind_e].temperatura >= tmin && principal.espectros[ind_e].temperatura <= tmax)
                    {
                        nespectros[i]++;
                        for (int k = 0; k < ndatos; k++)
                        {
                            y[i][k] += principal.espectros[ind_e].yn[k];
                        }
                    }
                }
                if (nespectros[i] > 0)
                {
                    for (int k = 0; k < ndatos; k++)
                    {
                        y[i][k] /= nespectros[i];
                        ymin = Math.Min(ymin, y[i][k]);
                        ymax = Math.Max(ymax, y[i][k]);
                    }
                }
            }
            fy = (ymax - ymin == 0) ? 1 : (alto_lienzo - m_sup - m_inf) / (ymax - ymin);
            DibujaEjeY(g);

            s = string.Format("T min {0,6:N0} T max {1,6:N0}", tmin, tmax);
            int at = (int)g.MeasureString(s, fte_f).Width;
            int contador = 0;
            if (LeyendaIzq.Checked)
            {
                g.DrawString(s, fte_f, brocha[0], m_izq, m_ly + contador * m_ily);
            }
            else
            {
                g.DrawString(s, fte_f, brocha[0], ancho_lienzo - at - m_ry, m_ly + contador * m_ily);
            }
            s = string.Format("G {0,3}. {1,7:N0} {2,7:N0}", 1, 1, 1);
            at = (int)g.MeasureString(s, fte_f).Width;
            contador++;
            int ind_cluster;
            for (int k = 0; k < nclusters; k++)
            {
                ind_cluster = caja.orden[k];
                s = string.Format("G {0,3}. {1,7:N0} {2,7:N0}", k + 1, caja.clusters[ind_cluster].n, nespectros[ind_cluster]);
                if (LeyendaIzq.Checked)
                {
                    g.DrawString(s, fte_f, brocha[contador % brocha.Length], m_izq, m_ly + contador * m_ily);
                }
                else
                {
                    g.DrawString(s, fte_f, brocha[contador % brocha.Length], ancho_lienzo - at - m_ry, m_ly + contador * m_ily);
                }
                if (nespectros[ind_cluster] > 0)
                {
                    for (int i = principal.calcula_desde; i < hasta; i++)
                    {
                        px = m_izq + (float)((x[i] - xmin) * fx) - medio_ancho_dato;
                        py = m_sup + (float)(alto_lienzo - m_sup - m_inf - (y[ind_cluster][i] - ymin) * fy) - medio_ancho_dato;
                        g.DrawEllipse(lapiz[contador % lapiz.Length], px, py, ancho_dato, ancho_dato);
                    }
                }
                contador++;
            }
            Lienzo.Image = img;
            Lienzo.Refresh();
        }
        private void Actualizar_Click(object sender, EventArgs e)
        {
            Dibuja();
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
