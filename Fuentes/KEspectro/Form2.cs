using System.Drawing.Imaging;

namespace KEspectro
{
    public partial class Form2 : Form
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
        private int medio_ancho_pico;
        private int ancho_dato;
        private int ancho_pico;
        private Font fte;
        private readonly Brush rojo = Brushes.Red;
        private readonly Brush marron = Brushes.DarkRed;
        private readonly Brush naranja = Brushes.Orange;
        private readonly Brush verde = Brushes.Green;
        private readonly Brush azul = Brushes.Blue;
        private readonly Brush turquesa = Brushes.DarkTurquoise;
        private Pen lapiz_dato;
        private Pen lapiz_negro;
        public Bitmap img;
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            int ancho = ClientSize.Width;
            int alto = ClientSize.Height;

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
            m_inf = (int)Math.Round(80 * reje);
            m_rx = (int)Math.Round(2 * reje);
            m_ry = (int)Math.Round(10 * reje);
            m_ly = (int)Math.Round(2 * reje);
            m_ily = (int)Math.Round(25 * reje);
            medio_ancho_dato = (int)Math.Round(4 * reje);
            medio_ancho_pico = (int)Math.Round(5 * reje);
            ancho_dato = medio_ancho_dato + medio_ancho_dato;
            ancho_pico = medio_ancho_pico + medio_ancho_pico;
            fte = new Font("Segoe UI", (int)Math.Round(14 * reje), FontStyle.Regular, GraphicsUnit.Point);
            lapiz_dato = new Pen(Color.Orchid, (int)Math.Round(2 * reje));
            lapiz_negro = new Pen(Color.Black, (int)Math.Round(2 * reje));
        }
        private void DibujaEjeX(Graphics g, double xmin, double xmax)
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
        private void DibujaEjeY(Graphics g, double ymin, double ymax)
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
        public void Dibuja(double[] centro, List<Form1.EspectroAnalizado> espectros, int ind, List<Form1.Dato> pmas, List<Form1.Dato> pmenos, double ymin, double ymax, bool todo)
        {
            if (centro == null)
            {
                Inicio.Enabled = false;
                Atras.Enabled = false;
                Adelante.Enabled = false;
                Final.Enabled = false;
                IndiceActual.Text = string.Empty;
                IndiceMaximo.Text = string.Empty;
                IndiceEspectro.Text = string.Empty;
                R_distancia.Text = string.Empty;
                R_confianza.Text = string.Empty;
                R_clusterCercano.Text = string.Empty;
            }
            else
            {
                Inicio.Enabled = true;
                Atras.Enabled = true;
                Adelante.Enabled = true;
                Final.Enabled = true;
            }
            img = new Bitmap(ancho_lienzo, alto_lienzo, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(img);
            g.FillRectangle(Brushes.White, 0, 0, ancho_lienzo, alto_lienzo);
            int n;
            double xmin = double.MaxValue;
            double xmax = double.MinValue;
            double fx;
            double fy;
            float px;
            float py;
            string s;
            int at;

            n = espectros[ind].n;
            for (int i = 0; i < n; i++)
            {
                if (espectros[ind].x[i] < xmin) xmin = espectros[ind].x[i];
                if (espectros[ind].x[i] > xmax) xmax = espectros[ind].x[i];
            }
            fx = (xmax - xmin == 0) ? 1 : (ancho_lienzo - m_izq - m_dch) / (xmax - xmin);
            fy = (ymax - ymin == 0) ? 1 : (alto_lienzo - m_sup - m_inf) / (ymax - ymin);

            DibujaEjeX(g, xmin, xmax);
            DibujaEjeY(g, ymin, ymax);


            if (todo)
            {
                for (int i = 0; i < n; i++)
                {
                    px = m_izq + (float)((espectros[ind].x[i] - xmin) * fx) - medio_ancho_dato;
                    py = m_sup + (float)(alto_lienzo - m_sup - m_inf - (espectros[ind].y[i] - ymin) * fy) - medio_ancho_dato;
                    g.FillEllipse(rojo, px, py, ancho_dato, ancho_dato);
                }
                if (espectros[ind].yc != null)
                {
                    // Calculado

                    for (int i = 0; i < n; i++)
                    {
                        px = m_izq + (float)((espectros[ind].x[i] - xmin) * fx) - medio_ancho_dato;
                        py = m_sup + (float)(alto_lienzo - m_sup - m_inf - (espectros[ind].yc[i] - ymin) * fy) - medio_ancho_dato;
                        g.FillEllipse(marron, px, py, ancho_dato, ancho_dato);
                    }

                    // Banda

                    if (espectros[ind].corte >= 0)
                    {
                        double margen = espectros[ind].corte;
                        for (int i = 0; i < n; i++)
                        {
                            if (espectros[ind].distancia_movil > 0)
                            {
                                if (espectros[ind].pro)
                                {
                                    margen = espectros[ind].fcorte * espectros[ind].SDVMovil[i] * 2 / (espectros[ind].MY + espectros[ind].MYc) * espectros[ind].yc[i];
                                }
                                else
                                {
                                    margen = espectros[ind].fcorte * espectros[ind].SDVMovil[i];
                                }
                            }
                            else
                            {
                                if (espectros[ind].pro)
                                {
                                    margen = espectros[ind].yc[i] * espectros[ind].corte;
                                }
                            }
                            px = m_izq + (float)((espectros[ind].x[i] - xmin) * fx) - medio_ancho_dato;
                            py = m_sup + (float)(alto_lienzo - m_sup - m_inf - (espectros[ind].yc[i] + margen - ymin) * fy) - medio_ancho_dato;
                            g.FillEllipse(naranja, px, py, ancho_dato, ancho_dato);
                            py = m_sup + (float)(alto_lienzo - m_sup - m_inf - (espectros[ind].yc[i] - margen - ymin) * fy) - medio_ancho_dato;
                            g.FillEllipse(naranja, px, py, ancho_dato, ancho_dato);
                        }
                    }
                }

                // Picos

                if (pmas != null && pmas.Count > 0)
                {
                    foreach (Form1.Dato d in pmas)
                    {
                        px = m_izq + (float)((d.x - xmin) * fx) - medio_ancho_pico;
                        py = m_sup + (float)(alto_lienzo - m_sup - m_inf - (d.y - ymin) * fy) - medio_ancho_pico;
                        g.FillRectangle(azul, px, py, ancho_pico, ancho_pico);
                    }
                }
                if (pmenos != null && pmenos.Count > 0)
                {
                    foreach (Form1.Dato d in pmenos)
                    {
                        px = m_izq + (float)((d.x - xmin) * fx) - medio_ancho_pico;
                        py = m_sup + (float)(alto_lienzo - m_sup - m_inf - (d.y - ymin) * fy) - medio_ancho_pico;
                        g.FillRectangle(verde, px, py, ancho_pico, ancho_pico);
                    }
                }
            }
            if (espectros[ind].yn != null)
            {
                int hasta;
                if (principal.calcula_hasta == 0 || principal.calcula_hasta > n) hasta = n;
                else if (principal.calcula_hasta < 0) hasta = n + principal.calcula_hasta;
                else hasta = principal.calcula_hasta;
                if (centro != null)
                {
                    int tipo_normalizacion = Convert.ToInt32(principal.V_tipoDatosY.Text.Trim());
                    bool normaliza_centroides = principal.V_normalizaCentroides.Checked;

                    // Centroide

                    s = "Centroide";
                    at = (int)g.MeasureString(s, fte).Width;
                    g.DrawString(s, fte, Brushes.DarkTurquoise, this.ancho_lienzo - m_izq - at, m_ly + m_ily);
                    for (int i = principal.calcula_desde; i < hasta; i++)
                    {
                        px = m_izq + (float)((espectros[ind].x[i] - xmin) * fx) - medio_ancho_dato;
                        py = m_sup + (float)(alto_lienzo - m_sup - m_inf - (centro[i] - ymin) * fy) - medio_ancho_dato;
                        if (tipo_normalizacion == 4 && normaliza_centroides)
                        {
                            // A efectos de visualización, evitar solapes

                            py -= ancho_dato;
                        }
                        g.FillEllipse(turquesa, px, py, ancho_dato, ancho_dato);
                    }
                }
                s = "Datos";
                at = (int)g.MeasureString(s, fte).Width;
                g.DrawString(s, fte, Brushes.Orchid, this.ancho_lienzo - m_izq - at, m_ly);
                for (int i = principal.calcula_desde; i < hasta; i++)
                {
                    px = m_izq + (float)((espectros[ind].x[i] - xmin) * fx) - medio_ancho_dato;
                    py = m_sup + (float)(alto_lienzo - m_sup - m_inf - (espectros[ind].yn[i] - ymin) * fy) - medio_ancho_dato;
                    g.DrawRectangle(lapiz_dato, px, py, ancho_dato, ancho_dato);
                }
            }

            // Longitud de onda con el máximo valor de flujo

            if (espectros[ind].londaMaximo > 0)
            {
                px = m_izq + (float)((espectros[ind].londaMaximo - xmin) * fx);
                g.DrawLine(lapiz_negro, px, m_sup, px, alto_lienzo - m_inf);
                s = String.Format("{0:N0}Å {1:N0}K", espectros[ind].londaMaximo, espectros[ind].temperatura);
                at = (int)g.MeasureString(s, fte).Width;
                if (px - at / 2 < 4) px = at / 2 + 4;
                if (px + at / 2 > ancho_lienzo) px = ancho_lienzo - at / 2;
                g.DrawString(s, fte, Brushes.Black, px - at / 2, alto_lienzo - m_inf + 2);
            }
            Lienzo.Image = img;
            Lienzo.Refresh();
        }
        public void RefrescaLienzo()
        {
            Lienzo.Image = img;
            Lienzo.Refresh();
        }
        private void Inicio_Click(object sender, EventArgs e)
        {
            if (principal.FilaCluster.Value > 1) principal.FilaCluster.Value = 1;
        }
        private void Atras_Click(object sender, EventArgs e)
        {
            if (principal.FilaCluster.Value > 1) principal.FilaCluster.Value--;
        }
        private void Adelante_Click(object sender, EventArgs e)
        {
            if (principal.FilaCluster.Value < principal.FilaCluster.Maximum) principal.FilaCluster.Value++;
        }
        private void Final_Click(object sender, EventArgs e)
        {
            if (principal.FilaCluster.Value < principal.FilaCluster.Maximum) principal.FilaCluster.Value = principal.FilaCluster.Maximum;
        }
    }
}
