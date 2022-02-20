using System.Drawing.Imaging;
using System.Text;

namespace KEspectro
{
    public partial class Form6 : Form
    {
        public Form1 principal;
        public int ind_cluster_tabla;
        public bool evitar;

        private int ancho_lienzo;
        private int alto_lienzo;
        private int m_izq;
        private int m_dch;
        private int m_sup;
        private int m_inf;
        private int m_rx;
        private int m_ly;
        private int m_ily;
        private int medio_ancho_dato;
        private int ancho_dato;
        private Font fte;
        private Font fte_f;

        private double[] x;
        private int NH_INI = 50;
        private int NH;
        private double ancho_his;
        private int h_max;
        private double fhy;
        private double[] t_his;
        private int[] histograma;
        private Form1.Caja caja;
        private int nclusters;
        private int ndatos;
        private double xmin;
        private double xmax;
        private double ymin;
        private double ymax;
        private double tmin;
        private double tmax;
        private double fx;
        private double fy;
        private readonly Brush brocha = new SolidBrush(Color.Black);
        private readonly Brush[] brochas = {
            new SolidBrush(Color.Black),
            new SolidBrush(Color.Red),
            new SolidBrush(Color.Green),
            new SolidBrush(Color.Blue),
            new SolidBrush(Color.Yellow),
            new SolidBrush(Color.Magenta),
            new SolidBrush(Color.Maroon),
            new SolidBrush(Color.Orange)
        };
        private Pen lapiz_negro;
        private readonly Pen[] lapices = new Pen[8];
        public Bitmap img;

        private Form1.Caja caja_s;
        private int nclusters_s;
        private Pen lapiz_s;
        private readonly Brush brocha_s = new SolidBrush(Color.Blue);
        public Form6()
        {
            InitializeComponent();
        }
        private void Inicio_Click(object sender, EventArgs e)
        {
            if (ind_cluster_tabla != 0)
            {
                ind_cluster_tabla = 0;
                Dibuja();
            }
        }
        private void Atras_Click(object sender, EventArgs e)
        {
            if (ind_cluster_tabla > 0)
            {
                ind_cluster_tabla--;
                Dibuja();
            }
        }
        private void Adelante_Click(object sender, EventArgs e)
        {
            if (ind_cluster_tabla < nclusters - 1)
            {
                ind_cluster_tabla++;
                Dibuja();
            }
        }
        private void Final_Click(object sender, EventArgs e)
        {
            if (ind_cluster_tabla != nclusters - 1)
            {
                ind_cluster_tabla = nclusters - 1;
                Dibuja();
            }
        }
        public void Escala(Form1.Caja caja, int num_histogramas)
        {
            this.caja = caja;
            int ancho = Screen.PrimaryScreen.WorkingArea.Width;
            int alto = Screen.PrimaryScreen.WorkingArea.Height;

            // Lienzo

            int ml_sup = 56;
            int ml_izq = 4;
            ancho_lienzo = ancho - ml_izq;
            alto_lienzo = alto - ml_sup - 60;
            Lienzo.Location = new Point(ml_izq, ml_sup);
            Lienzo.Size = new Size(ancho_lienzo, alto_lienzo);
            double rejex = ancho_lienzo / Form1.ancho_lienzo_referencia;
            double rejey = alto_lienzo / Form1.alto_lienzo_referencia;
            double reje = Math.Max(rejex, rejey);
            m_izq = (int)Math.Round(50 * reje);
            m_dch = (int)Math.Round(80 * reje);
            m_sup = (int)Math.Round(50 * reje);
            m_inf = (int)Math.Round(110 * reje);
            m_rx = (int)Math.Round(10 * reje);
            m_ly = (int)Math.Round(2 * reje);
            m_ily = (int)Math.Round(25 * reje);
            medio_ancho_dato = (int)Math.Round(4 * reje);
            ancho_dato = medio_ancho_dato + medio_ancho_dato;
            fte = new Font("Segoe UI", (int)Math.Round(14 * reje), FontStyle.Regular, GraphicsUnit.Point);
            fte_f = new Font("Courier New", (int)Math.Round(14 * reje), FontStyle.Bold, GraphicsUnit.Point);
            lapiz_negro = new Pen(Color.Black, (int)Math.Round(3 * reje));
            lapices[0] = new Pen(Color.Black, (int)Math.Round(2 * reje));
            lapices[1] = new Pen(Color.Red, (int)Math.Round(2 * reje));
            lapices[2] = new Pen(Color.Green, (int)Math.Round(2 * reje));
            lapices[3] = new Pen(Color.Blue, (int)Math.Round(2 * reje));
            lapices[4] = new Pen(Color.Yellow, (int)Math.Round(2 * reje));
            lapices[5] = new Pen(Color.Magenta, (int)Math.Round(2 * reje));
            lapices[6] = new Pen(Color.Maroon, (int)Math.Round(2 * reje));
            lapices[7] = new Pen(Color.Orange, (int)Math.Round(2 * reje));
            lapiz_s = new Pen(Color.Blue, (int)Math.Round(3 * reje));

            nclusters = principal.nclusters;
            IndiceMaximo.Text = nclusters.ToString();
            ndatos = principal.espectros[0].n;
            x = principal.espectros[0].x;
            xmin = x[0];
            xmax = x[ndatos - 1];
            tmin = Form1.WIEN / xmax;
            tmax = Form1.WIEN / xmin;
            fx = (tmax - tmin == 0) ? 1 : (ancho_lienzo - m_izq - m_dch) / (tmax - tmin);
            ymin = 0;
            ymax = nclusters + 1;
            fy = (ymax - ymin == 0) ? 1 : (alto_lienzo - m_sup - m_inf) / (ymax - ymin);
            PreparaHistogramas();
        }
        private void PreparaHistogramas()
        {
            if (V_n_histogramas.Text.Trim().Length == 0) V_n_histogramas.Text = "50";
            NH_INI = Convert.ToInt32(V_n_histogramas.Text.Trim());
            if (NH_INI < 1)
            {
                NH_INI = 50;
                V_n_histogramas.Text = NH_INI.ToString();
            }
            NH = NH_INI;
            t_his = new double[NH];
            histograma = new int[NH];
            ancho_his = (tmax - tmin) / NH;
            double t = tmax;
            for (int i = 0; i < NH; i++)
            {
                t -= ancho_his;
                t_his[i] = t;
            }
            t_his[NH - 1] -= 0.001; // Para asegurar que el espectro con  la máxima temeperatura entre en el último histograma

            // Buscar el máximo de todos los histogramas

            int h;
            int ind_e;
            double londa;
            h_max = int.MinValue;
            int ind_c;
            for (int ind_cluster = 0; ind_cluster < nclusters; ind_cluster++)
            {
                ind_c = caja.orden[ind_cluster];
                if (caja.clusters[ind_c].n != 0)
                {
                    Array.Clear(histograma, 0, histograma.Length);
                    for (int i = 0; i < caja.clusters[ind_c].n; i++)
                    {
                        ind_e = caja.clusters[ind_c].ind_f_espectros[i];
                        londa = principal.espectros[ind_e].londaMaximo;
                        if (londa >= xmin)
                        {
                            t = principal.espectros[ind_e].temperatura;
                            for (h = 0; h < NH; h++)
                            {
                                if (t >= t_his[h])
                                {
                                    break;
                                }
                            }
                            histograma[h]++;
                        }
                    }
                    for (int i = 0; i < NH; i++)
                    {
                        h_max = Math.Max(h_max, histograma[i]);
                    }
                }
            }
            if (h_max < 1) return;
            fhy = (double)(alto_lienzo - m_sup - m_inf) / h_max;
        }
        private int IndiceClusterTabla(Form1.Caja caja, int indice_caja)
        {
            for (int i = 0; i < caja.clusters.Length; i++)
            {
                if (indice_caja == caja.orden[i])
                {
                    return i;
                }
            }
            return -1;
        }
        private void DibujaEjeX(Graphics g)
        {
            float px;
            float py;
            string s;
            int at;

            // Eje X

            px = m_izq;
            py = alto_lienzo - 2 * (m_ly + fte.Height);
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

            s = string.Format("{0:N1}K", tmax);
            at = (int)g.MeasureString(s, fte).Width;
            px = m_izq - at / 2;
            if (px < m_rx) px = m_rx;
            py = alto_lienzo - 2 * (m_ly + fte.Height);
            g.DrawString(s, fte, Brushes.Black, px, py);

            s = string.Format("{0:N1}K", tmin);
            at = (int)g.MeasureString(s, fte).Width;
            px = ancho_lienzo - m_dch - at / 2;
            py = alto_lienzo - 2 * (m_ly + fte.Height);
            g.DrawString(s, fte, Brushes.Black, px, py);

            s = "Temperatura K";
            at = (int)g.MeasureString(s, fte).Width;
            px = m_izq + (ancho_lienzo - m_izq - m_dch - at) / 2;
            py = alto_lienzo - 2 * (m_ly + fte.Height);
            g.DrawString(s, fte, Brushes.Black, px, py);
        }
        public void Dibuja()
        {
            Superponer.Enabled = false;
            int ind_cluster_caja = caja.orden[ind_cluster_tabla];
            IndiceActual.Text = (ind_cluster_tabla + 1).ToString();
            NumEspectros.Text = caja.clusters[ind_cluster_caja].n.ToString();
            img = new Bitmap(ancho_lienzo, alto_lienzo, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(img);
            g.FillRectangle(Brushes.White, 0, 0, ancho_lienzo, alto_lienzo);
            float px;
            float py;
            string s;
            int at;

            DibujaEjeX(g);

            // Temperaturas

            int[] n_media = new int[nclusters];
            double[] londa_media = new double[nclusters];
            double[] temperatura = new double[nclusters];
            double[] de_temperatura = new double[nclusters];
            Array.Clear(n_media, 0, n_media.Length);
            Array.Clear(londa_media, 0, londa_media.Length);
            Array.Clear(temperatura, 0, temperatura.Length);
            Array.Clear(de_temperatura, 0, temperatura.Length);
            Medias(caja, n_media, londa_media, temperatura, de_temperatura, tmin, ind_cluster_caja);

            int ind_e;
            double londa;
            double t;

            // Crear histograma

            int contador = 0;
            py = (float)(alto_lienzo - m_inf - h_max * fhy - fte.Height);
            g.DrawString(string.Format("{0:N0}", h_max), fte, Brushes.Black, m_rx, py);
            int tet = 0;
            if (h_max > 0 && n_media[ind_cluster_caja] > 0)
            {
                int h;
                float anhispx;
                Array.Clear(histograma, 0, histograma.Length);
                for (int i = 0; i < caja.clusters[ind_cluster_caja].n; i++)
                {
                    ind_e = caja.clusters[ind_cluster_caja].ind_f_espectros[i];
                    londa = principal.espectros[ind_e].londaMaximo;
                    if (londa >= xmin)
                    {
                        t = principal.espectros[ind_e].temperatura;
                        for (h = 0; h < NH; h++)
                        {
                            if (t >= t_his[h])
                            {
                                break;
                            }
                        }
                        histograma[h]++;
                        tet++;
                    }
                }
                for (int i = 0; i < NH; i++)
                {
                    if (histograma[i] > 0)
                    {
                        px = m_izq + (float)(ancho_lienzo - m_izq - m_dch - (t_his[i] - tmin) * fx);
                        py = m_sup + (float)(alto_lienzo - m_sup - m_inf - histograma[i] * fhy);
                        anhispx = (float)((ancho_his) * fx - 2);
                        g.FillRectangle(brocha, px - anhispx + 1, py, anhispx, (float)(histograma[i] * fhy));
                        if (V_valores.Checked)
                        {
                            s = histograma[i].ToString();
                            at = (int)g.MeasureString(s, fte).Width;
                            g.DrawString(s, fte, Brushes.Black, px - anhispx + (anhispx - at) / 2 + 1, py - fte.Height - 2);
                        }
                    }
                }
                px = m_izq + (float)(ancho_lienzo - m_izq - m_dch - (temperatura[ind_cluster_caja] - tmin) * fx);
                g.DrawLine(lapiz_negro, px, alto_lienzo - m_inf, px, alto_lienzo - m_inf + fte.Height / 2);
                s = String.Format("{0:N0}K {1:N0}", temperatura[ind_cluster_caja], de_temperatura[ind_cluster_caja]);
                at = (int)g.MeasureString(s, fte).Width;
                if (px - at / 2 < 4) px = at / 2 + 4;
                if (px + at / 2 > ancho_lienzo) px = ancho_lienzo - at / 2;
                g.DrawString(s, fte, Brushes.Black, px - at / 2, alto_lienzo - m_inf + fte.Height / 2 + 2);
            }
            s = string.Format("G {0,3}. {1,7:N0}. {2,7:N0}", 999, 999999, 999999);
            at = (int)g.MeasureString(s, fte).Width;
            s = string.Format("G {0,3}. {1,7:N0}. {2,7:N0}", ind_cluster_tabla + 1, caja.clusters[ind_cluster_caja].n, tet);
            g.DrawString(s, fte, brocha, ancho_lienzo - m_izq - at, m_ly + contador * m_ily);
            Lienzo.Image = img;
            Lienzo.Refresh();
        }
        public void DibujaConjunto(bool leyenda)
        {
            Superponer.Enabled = true;
            int suma = 0;
            for (int i = 0; i < nclusters; i++) suma += caja.clusters[i].n;
            IndiceActual.Text = string.Empty;
            NumEspectros.Text = suma.ToString();
            img = new Bitmap(ancho_lienzo, alto_lienzo, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(img);
            g.FillRectangle(Brushes.White, 0, 0, ancho_lienzo, alto_lienzo);
            float px;
            float py;
            string s;
            int at;
            //ymax = nclusters + 1;
            //fy = (ymax - ymin == 0) ? 1 : (alto_lienzo - m_sup - m_inf) / (ymax - ymin);

            DibujaEjeX(g);

            // Temperaturas

            int[] n_media = new int[nclusters];
            double[] londa_media = new double[nclusters];
            double[] temperatura = new double[nclusters];
            double[] de_temperatura = new double[nclusters];
            Array.Clear(n_media, 0, n_media.Length);
            Array.Clear(londa_media, 0, londa_media.Length);
            Array.Clear(temperatura, 0, temperatura.Length);
            Array.Clear(de_temperatura, 0, temperatura.Length);

            for (int k = 0; k < nclusters; k++)
            {
                Medias(caja, n_media, londa_media, temperatura, de_temperatura, tmin, k);
            }

            // Ordenar por temperatura

            List<(double, int, int)> orden = new List<(double, int, int)>();
            for (int i = 0; i < nclusters; i++)
            {
                orden.Add((temperatura[i], i, IndiceClusterTabla(caja, i)));
            }
            orden.Sort();
            orden.Reverse();

            int ind_c;
            int contador = 0;
            if (leyenda)
            {
                // Leyenda

                for (int k = 0; k < nclusters; k++)
                {
                    // En el orden del número de espectros

                    ind_c = caja.orden[k];
                    if (caja.clusters[ind_c].n == 0) continue;
                    if (QuitarUnicos.Checked && caja.clusters[ind_c].n < 2) continue;
                    s = string.Format("G{0,3}. {1,7:N0}", k + 1, caja.clusters[ind_c].n);
                    g.DrawString(s, fte_f, brocha, m_izq, m_ly + contador * m_ily);
                    contador++;
                }
            }

            // Espectros

            int ind_e;
            double londa;
            double t;
            for (int k = 0; k < nclusters; k++)
            {
                // En el orden de las temperaturas (Item2)

                ind_c = orden[k].Item2;
                if (n_media[ind_c] == 0) continue;
                for (int i = 0; i < caja.clusters[ind_c].n; i++)
                {
                    ind_e = caja.clusters[ind_c].ind_f_espectros[i];
                    t = principal.espectros[ind_e].temperatura;
                    londa = principal.espectros[ind_e].londaMaximo;
                    if (londa >= xmin)
                    {
                        px = m_izq + (float)(ancho_lienzo - m_izq - m_dch - (t - tmin) * fx) - medio_ancho_dato;
                        py = m_sup + (float)(alto_lienzo - m_sup - m_inf - (k + 1 - ymin) * fy) - medio_ancho_dato;
                        g.DrawEllipse(lapiz_negro, px, py, ancho_dato, ancho_dato);
                    }
                }
            }

            // Temperatura media en cada cluster

            float x0;
            float y0;
            contador = 0;
            double horquilla = V_horquilla.Text.Trim().Length == 0 ? 0 : Convert.ToDouble(V_horquilla.Text.Trim().Replace(principal.s_millar, principal.s_decimal));
            for (int k = 0; k < nclusters; k++)
            {
                // En el orden de las temperaturas (Item2)

                ind_c = orden[k].Item2;
                y0 = m_sup + (float)(alto_lienzo - m_sup - m_inf - (k + 1 - ymin) * fy);
                x0 = m_izq + (float)(ancho_lienzo - m_izq - m_dch - (temperatura[ind_c] - tmin) * fx);

                if (n_media[ind_c] == 0) continue;
                s = (orden[k].Item3 + 1).ToString();
                py = y0 - fte.Height / 2;
                g.DrawString(s, fte, Brushes.Black, m_rx, py);

                px = x0 - ancho_dato;
                py = y0 - ancho_dato;
                g.FillRectangle(brochas[contador % brochas.Length], px, py, 2 * ancho_dato, 2 * ancho_dato);
                if (horquilla > 0 && de_temperatura[ind_c] > 0)
                {
                    px = x0 - (float)(horquilla * de_temperatura[ind_c] * fx);
                    g.DrawLine(lapices[contador % lapices.Length], px, m_sup, px, alto_lienzo - m_inf);
                    g.DrawLine(lapices[contador % lapices.Length], px, m_sup, px + ancho_dato, m_sup);
                    g.DrawLine(lapices[contador % lapices.Length], px, alto_lienzo - m_inf, px + ancho_dato, alto_lienzo - m_inf);
                    px = x0 + (float)(horquilla * de_temperatura[ind_c] * fx);
                    g.DrawLine(lapices[contador % lapices.Length], px, m_sup, px, alto_lienzo - m_inf);
                    g.DrawLine(lapices[contador % lapices.Length], px, m_sup, px - ancho_dato, m_sup);
                    g.DrawLine(lapices[contador % lapices.Length], px, alto_lienzo - m_inf, px - ancho_dato, alto_lienzo - m_inf);
                }
                contador++;
                s = String.Format("{0:N0}K {1:N0}", temperatura[ind_c], de_temperatura[ind_c]);
                at = (int)g.MeasureString(s, fte).Width;
                px = x0 - medio_ancho_dato;
                py = y0 + ancho_dato;
                if (px - at / 2 < 4) px = at / 2 + 4;
                if (px + at / 2 > ancho_lienzo) px = ancho_lienzo - at / 2;
                g.DrawString(s, fte, Brushes.Black, px - at / 2, py);
            }
            Lienzo.Image = img;
            Lienzo.Refresh();
        }
        private void DibujaSuperpuesto()
        {
            Graphics g = Graphics.FromImage(img);
            float px;
            float py;
            string s;
            int at;

            // Temperaturas

            int[] n_media = new int[nclusters_s];
            double[] londa_media = new double[nclusters_s];
            double[] temperatura = new double[nclusters_s];
            double[] de_temperatura = new double[nclusters_s];
            Array.Clear(n_media, 0, n_media.Length);
            Array.Clear(londa_media, 0, londa_media.Length);
            Array.Clear(temperatura, 0, temperatura.Length);
            Array.Clear(de_temperatura, 0, temperatura.Length);

            for (int k = 0; k < nclusters_s; k++)
            {
                Medias(caja_s, n_media, londa_media, temperatura, de_temperatura, tmin, k);
            }

            // Ordenar por temperatura

            List<(double, int, int)> orden = new List<(double, int, int)>();
            for (int i = 0; i < nclusters_s; i++)
            {
                orden.Add((temperatura[i], i, IndiceClusterTabla(caja_s, i)));
            }
            orden.Sort();
            orden.Reverse();
            int ind_c;
            int contador;

            // Leyenda

            contador = 0;
            for (int k = 0; k < Math.Max(nclusters, nclusters_s); k++)
            {
                // En el orden de temperaturas

                if (k < nclusters)
                {
                    ind_c = caja.orden[k];
                    if (caja.clusters[ind_c].n == 0) continue;
                    if (QuitarUnicos.Checked && caja.clusters[ind_c].n < 2) continue;
                    s = string.Format("G{0,3}.{1,7:N0}", k + 1, caja.clusters[ind_c].n);
                    g.DrawString(s, fte_f, brocha, m_izq, m_ly + contador * m_ily);
                    if (k < nclusters_s)
                    {
                        at = (int)g.MeasureString(s, fte_f).Width;
                        ind_c = caja_s.orden[k];
                        s = string.Format(" {0,7:N0}", caja_s.clusters[ind_c].n);
                        g.DrawString(s, fte_f, brocha_s, m_izq + at, m_ly + contador * m_ily);
                    }
                }
                else
                {
                    if (k < nclusters_s)
                    {
                        s = string.Format("G{0,3}.", k + 1);
                        g.DrawString(s, fte_f, brocha, m_izq, m_ly + contador * m_ily);
                        at = (int)g.MeasureString(string.Format("G{0,3}.{1,7:N0}", 0, 0), fte_f).Width;
                        ind_c = caja_s.orden[k];
                        s = string.Format(" {0,7:N0}", caja_s.clusters[ind_c].n);
                        g.DrawString(s, fte_f, brocha_s, m_izq + at, m_ly + contador * m_ily);
                    }
                }
                contador++;
            }

            // Espectros

            int ind_e;
            double londa;
            double t;
            for (int k = 0; k < nclusters_s; k++)
            {
                // En el orden de las temperaturas (Item2)

                ind_c = orden[k].Item2;
                if (n_media[ind_c] == 0) continue;
                for (int i = 0; i < caja_s.clusters[ind_c].n; i++)
                {
                    ind_e = caja_s.clusters[ind_c].ind_f_espectros[i];
                    t = principal.espectros[ind_e].temperatura;
                    londa = principal.espectros[ind_e].londaMaximo;
                    if (londa >= xmin)
                    {
                        px = m_izq + (float)(ancho_lienzo - m_izq - m_dch - (t - tmin) * fx) - medio_ancho_dato;
                        py = m_sup + (float)(alto_lienzo - m_sup - m_inf - (k + 1.5 - ymin) * fy) - medio_ancho_dato;
                        g.DrawEllipse(lapiz_s, px, py, ancho_dato, ancho_dato);
                    }
                }
            }

            // Temperatura media en cada cluster

            float x0;
            float y0;
            contador = 0;
            for (int k = 0; k < nclusters_s; k++)
            {
                // En el orden de las temperaturas (Item2)

                ind_c = orden[k].Item2;
                y0 = m_sup + (float)(alto_lienzo - m_sup - m_inf - (k + 1.5 - ymin) * fy);
                x0 = m_izq + (float)(ancho_lienzo - m_izq - m_dch - (temperatura[ind_c] - tmin) * fx);

                if (n_media[ind_c] == 0) continue;
                s = (orden[k].Item3 + 1).ToString();
                py = y0 - fte.Height / 2;
                g.DrawString(s, fte, Brushes.Blue, m_rx, py);

                px = x0 - ancho_dato;
                py = y0 - ancho_dato;
                g.FillRectangle(brochas[contador % brochas.Length], px, py, 2 * ancho_dato, 2 * ancho_dato);
                contador++;
                s = String.Format("{0:N0}K {1:N0}", temperatura[ind_c], de_temperatura[ind_c]);
                at = (int)g.MeasureString(s, fte).Width;
                px = x0 - medio_ancho_dato;
                py = y0 + ancho_dato;
                if (px - at / 2 < 4) px = at / 2 + 4;
                if (px + at / 2 > ancho_lienzo) px = ancho_lienzo - at / 2;
                g.DrawString(s, fte, Brushes.Blue, px - at / 2, py);
            }
            Lienzo.Image = img;
            Lienzo.Refresh();
        }
        private void Medias(Form1.Caja caja_medias, int[] n_media, double[] londa_media, double[] temperatura, double[] de_temperatura, double tmin, int ind_c_caja)
        {
            int ind_e;
            double londa;
            if (caja_medias.clusters[ind_c_caja].n == 0) return;
            for (int i = 0; i < caja_medias.clusters[ind_c_caja].n; i++)
            {
                ind_e = caja_medias.clusters[ind_c_caja].ind_f_espectros[i];
                londa = principal.espectros[ind_e].londaMaximo;
                if (londa >= tmin)
                {
                    n_media[ind_c_caja]++;
                    londa_media[ind_c_caja] += londa;
                    temperatura[ind_c_caja] += principal.espectros[ind_e].temperatura;
                }
            }
            if (n_media[ind_c_caja] == 0)
            {
                londa_media[ind_c_caja] = 0;
                temperatura[ind_c_caja] = 0;
            }
            else
            {
                londa_media[ind_c_caja] /= n_media[ind_c_caja];
                temperatura[ind_c_caja] /= n_media[ind_c_caja];
            }
            double d;
            double d2;
            if (caja_medias.clusters[ind_c_caja].n == 0) return;
            if (n_media[ind_c_caja] == 0) return;
            d2 = 0;
            for (int i = 0; i < caja_medias.clusters[ind_c_caja].n; i++)
            {
                ind_e = caja_medias.clusters[ind_c_caja].ind_f_espectros[i];
                londa = principal.espectros[ind_e].londaMaximo;
                if (londa >= tmin)
                {
                    d = principal.espectros[ind_e].temperatura - temperatura[ind_c_caja];
                    d2 += d * d;
                }
            }
            de_temperatura[ind_c_caja] = Math.Sqrt(d2 / n_media[ind_c_caja]);
        }
        private void Todos_CheckedChanged(object sender, EventArgs e)
        {
            if (!evitar)
            {
                Inicio.Enabled = !Todos.Checked;
                Atras.Enabled = !Todos.Checked;
                Adelante.Enabled = !Todos.Checked;
                Final.Enabled = !Todos.Checked;
                V_n_histogramas.Enabled = !Todos.Checked;
                ActHistogramas.Enabled = !Todos.Checked;
                V_valores.Enabled = !Todos.Checked;
                V_horquilla.Enabled = Todos.Checked;
                if (Todos.Checked)
                {
                    ymax = nclusters + 1;
                    fy = (ymax - ymin == 0) ? 1 : (alto_lienzo - m_sup - m_inf) / (ymax - ymin);
                    DibujaConjunto(true);
                }
                else
                {
                    Dibuja();
                }
            }
        }
        private void QuitarUnicos_CheckedChanged(object sender, EventArgs e)
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
        private void ActHistogramas_Click(object sender, EventArgs e)
        {
            PreparaHistogramas();
            Dibuja();
        }
        private void V_valores_CheckedChanged(object sender, EventArgs e)
        {
            if (!evitar && !Todos.Checked)
            {
                Dibuja();
            }
        }
        private void Superponer_Click(object sender, EventArgs e)
        {
            OpenFileDialog ficherolectura = new()
            {
                Filter = "CVS |*.csv|TXT |*.txt|TODO |*.*",
                FilterIndex = 1,
                RestoreDirectory = false,
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = true
            };
            if (ficherolectura.ShowDialog() == DialogResult.OK)
            {
                FileStream fsfc = new(ficherolectura.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                if (fsfc == null) return;
                StreamReader sr = new(fsfc);

                // Tres líneas de cabecera

                string linea = sr.ReadLine();
                if (!linea.Equals("Media"))
                {
                    MessageBox.Show(string.Format("El fichero {0} no tiene el formato esperado", ficherolectura.FileName), "Leer fichero de clasificación");
                    return;
                }
                sr.ReadLine();
                linea = sr.ReadLine();
                string[] ss = linea == null ? Array.Empty<string>() : linea.Split(';');
                nclusters_s = ss.Length - 1;
                caja_s = new Form1.Caja(0, nclusters_s, principal.espectros.Count, ndatos, 0);
                int ind_espectro;
                int iemax = principal.espectros.Count - 1;
                while (!sr.EndOfStream)
                {
                    linea = sr.ReadLine();
                    if (linea == null) break;
                    ss = linea.Split(';');
                    if (ss.Length == 1)
                    {
                        // Fin de caja

                        break;
                    }
                    else
                    {
                        for (int i = 0; i < nclusters_s; i++)
                        {
                            if (ss[i + 1].Trim().Length > 0)
                            {
                                ind_espectro = Convert.ToInt32(ss[i + 1]) - 1;
                                if (ind_espectro > iemax)
                                {
                                    MessageBox.Show("La clasificación coniene números de espectro mayores que espectros leidos");
                                    return;
                                }
                                caja_s.clusters[i].ind_f_espectros.Add(ind_espectro);
                            }
                        }
                    }
                }
                sr.Close();
                for (int i = 0; i < nclusters_s; i++)
                {
                    caja_s.clusters[i].n = caja_s.clusters[i].ind_f_espectros.Count;
                    caja_s.orden[i] = i;
                }
                ymax = Math.Max(nclusters, nclusters_s) + 1;
                fy = (ymax - ymin == 0) ? 1 : (alto_lienzo - m_sup - m_inf) / (ymax - ymin);
                DibujaConjunto(false);
                DibujaSuperpuesto();
            }
        }
    }
}
