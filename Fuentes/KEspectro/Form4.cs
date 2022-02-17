using System.Drawing.Imaging;
using System.Text;

namespace KEspectro
{
    public partial class Form4 : Form
    {
        public Form1 principal;
        public int ind_cluster;
        public double[] x;
        public double[][] y;

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
        private Font fte_p;
        private Font fte_f;

        private int[] num_espectros;
        private double[] temperatura;
        private int nclusters;
        private int ndatos;
        private double xmin;
        private double xmax;
        private double ymin;
        private double ymax;
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
        private double fx;
        private double fy;
        public class Coordenadas
        {
            public double x;
            public double y;
            public Coordenadas(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
        }
        List<int> ind_lineas = new List<int>();
        public Form4()
        {
            InitializeComponent();
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            ind_lineas.Clear();
            LeeEspectros();
        }
        public void LeeEspectros()
        {
            lista_elegibles.Items.Clear();
            foreach (string s in principal.lista_elegibles_principal)
            {
                lista_elegibles.Items.Add(s);
            }
        }
        private void Inicio_Click(object sender, EventArgs e)
        {
            if (ind_cluster != 0)
            {
                ind_cluster = 0;
                Dibuja();
            }
        }
        private void Atras_Click(object sender, EventArgs e)
        {
            if (ind_cluster > 0)
            {
                ind_cluster--;
                Dibuja();
            }
        }
        private void Adelante_Click(object sender, EventArgs e)
        {
            if (ind_cluster < nclusters - 1)
            {
                ind_cluster++;
                Dibuja();
            }
        }
        private void Final_Click(object sender, EventArgs e)
        {
            if (ind_cluster != nclusters - 1)
            {
                ind_cluster = nclusters - 1;
                Dibuja();
            }
        }
        public void Escala(int[] num_espectros, double[] temperatura, bool leyenda_izq)
        {
            LeyendaIzq.Checked = leyenda_izq;
            int ancho = Screen.PrimaryScreen.WorkingArea.Width;
            int alto = Screen.PrimaryScreen.WorkingArea.Height;

            // Lienzo

            int ml_sup = 56;
            int ml_izq = 4;
            ancho_lienzo = ancho - ml_izq - p_raton_x.Left - p_raton_x.Width - 8;
            alto_lienzo = alto - ml_sup - 60;
            Lienzo.Location = new Point(ml_izq + p_raton_x.Left + p_raton_x.Width + 4, ml_sup);
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
            fte_p = new Font("Segoe UI", (int)Math.Round(12 * reje), FontStyle.Regular, GraphicsUnit.Point);
            fte_f = new Font("Courier New", (int)Math.Round(14 * reje), FontStyle.Bold, GraphicsUnit.Point);
            lapiz_negro = new Pen(Color.Black, (int)Math.Round(2 * reje));
            foreach (Pen p in lapiz)
            {
                p.Width = (int)Math.Round(3 * reje); ;
            }
            nclusters = principal.nclusters;
            this.num_espectros = num_espectros;
            this.temperatura = temperatura;
            IndiceMaximo.Text = nclusters.ToString();
            ndatos = x.Length;
            xmin = double.MaxValue;
            xmax = double.MinValue;
            for (int i = 0; i < ndatos; i++)
            {
                if (x[i] < xmin) xmin = x[i];
                if (x[i] > xmax) xmax = x[i];
            }
            fx = (xmax - xmin == 0) ? 1 : (ancho_lienzo - m_izq - m_dch) / (xmax - xmin);
            ymin = double.MaxValue;
            ymax = double.MinValue;
            for (int j = 0; j < nclusters; j++)
            {
                for (int i = 0; i < ndatos; i++)
                {
                    if (y[j][i] < ymin) ymin = y[j][i];
                    if (y[j][i] > ymax) ymax = y[j][i];
                }
            }
            fy = (ymax - ymin == 0) ? 1 : (alto_lienzo - m_sup - m_inf) / (ymax - ymin);
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
            string ia;
            string ne;
            if (Todos.Checked || ind_cluster == -1)
            {
                ia = string.Empty;
                int suma = 0;
                for (int i = 0; i < nclusters; i++) suma += num_espectros[i];
                ne = suma.ToString();
            }
            else
            {
                ia = (ind_cluster + 1).ToString();
                ne = num_espectros[ind_cluster].ToString();
            }
            IndiceActual.Text = ia;
            NumEspectros.Text = ne;
            img = new Bitmap(ancho_lienzo, alto_lienzo, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(img);
            g.FillRectangle(Brushes.White, 0, 0, ancho_lienzo, alto_lienzo);
            float px;
            float py;
            string s;
            int at;
            int hasta;
            if (principal.calcula_hasta == 0 || principal.calcula_hasta > ndatos) hasta = ndatos;
            else if (principal.calcula_hasta < 0) hasta = ndatos + principal.calcula_hasta;
            else hasta = principal.calcula_hasta;

            DibujaEjeX(g);
            DibujaEjeY(g);

            // Centroides

            int p_cluster;
            int u_cluster;

            if (Todos.Checked || ind_cluster == -1)
            {
                p_cluster = 0;
                u_cluster = nclusters - 1;
            }
            else
            {
                p_cluster = ind_cluster;
                u_cluster = ind_cluster;
            }
            int contador = 0;
            s = string.Format("C {0,3}. {1,7:N0} {2,6:N0}", 1, 1, 1);
            at = (int)g.MeasureString(s, fte_f).Width;
            for (int k = p_cluster; k <= u_cluster; k++)
            {
                if (num_espectros[k] == 0) continue;
                if ((Todos.Checked || ind_cluster == -1) && QuitarUnicos.Checked && num_espectros[k] < 2) continue;
                s = string.Format("C {0,3}. {1,7:N0} {2,6:N0}", k + 1, num_espectros[k], temperatura[k]);
                if (LeyendaIzq.Checked)
                {
                    g.DrawString(s, fte_f, brocha[contador % brocha.Length], m_izq, m_ly + contador * m_ily);
                }
                else
                {
                    g.DrawString(s, fte_f, brocha[contador % brocha.Length], ancho_lienzo - at - m_ry, m_ly + contador * m_ily);
                }
                for (int i = principal.calcula_desde; i < hasta; i++)
                {
                    px = m_izq + (float)((x[i] - xmin) * fx) - medio_ancho_dato;
                    py = m_sup + (float)(alto_lienzo - m_sup - m_inf - (y[k][i] - ymin) * fy) - medio_ancho_dato;
                    g.DrawEllipse(lapiz[contador % lapiz.Length], px, py, ancho_dato, ancho_dato);
                }
                contador++;
            }

            // Líneas atómicas

            if (lista_elegidas.Items.Count > 0)
            {
                py = alto_lienzo - m_inf;
                for (int i = 0; i < lista_elegidas.Items.Count; i++)
                {
                    s = string.Format("{0:f0}", principal.lineas_atomicas[ind_lineas[i]].longitud_onda);
                    px = m_izq + (float)((principal.lineas_atomicas[ind_lineas[i]].longitud_onda - xmin) * fx);
                    g.DrawLine(lapiz_negro, px, py, px, m_sup);
                    at = (int)g.MeasureString(s, fte).Width;
                    g.DrawString(s, fte, Brushes.Black, px - fte.Height, py - at, new StringFormat(StringFormatFlags.DirectionVertical));
                    s = string.Format("{0}", principal.lineas_atomicas[ind_lineas[i]].isotopo);
                    at = (int)g.MeasureString(s, fte).Width + m_ly;
                    g.DrawString(s, fte, Brushes.Black, px - fte.Height, m_sup, new StringFormat(StringFormatFlags.DirectionVertical));
                    s = string.Format("{0}", principal.lineas_atomicas[ind_lineas[i]].intensidad);
                    g.DrawString(s, fte_p, Brushes.Black, px - fte.Height, m_sup + at, new StringFormat(StringFormatFlags.DirectionVertical));
                }
            }
            Lienzo.Image = img;
            Lienzo.Refresh();
        }
        private double PixelAlongitudOnda(int px)
        {
            double lon_onda = xmin + (px - m_izq) / fx;
            return lon_onda;
        }
        private void Todos_CheckedChanged(object sender, EventArgs e)
        {
            Inicio.Enabled = !Todos.Checked;
            Atras.Enabled = !Todos.Checked;
            Adelante.Enabled = !Todos.Checked;
            Final.Enabled = !Todos.Checked;
            Dibuja();
        }
        private void QuitarUnicos_CheckedChanged(object sender, EventArgs e)
        {
            Dibuja();
        }
        private void Salvar_Click(object sender, EventArgs e)
        {
            if (nclusters == 0 || img == null) return;
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
        private void B_limpiar_Click(object sender, EventArgs e)
        {
            ind_lineas.Clear();
            lista_elegidas.Items.Clear();
            Dibuja();
        }
        private void Lienzo_MouseDown(object sender, MouseEventArgs e)
        {
            if (img == null) return;
            p_raton_x.Text = string.Format("{0}", e.X);
            p_raton_y.Text = string.Format("{0}", e.Y);
            double lon_onda = PixelAlongitudOnda(e.X);
            double lon_onda_a = lon_onda;
            double lon_onda_p = lon_onda;

            // Ajustar al punto del espectro con la X más parecida

            if (lon_onda >= xmin && lon_onda <= xmax)
            {
                for (int i = 0; i < x.Length; i++)
                {
                    if (lon_onda == x[i])
                    {
                        lon_onda = lon_onda_a = lon_onda_p = x[i];
                        break;
                    }
                    else if (lon_onda < x[i])
                    {
                        if (x[i] - lon_onda < lon_onda - x[i - 1])
                        {
                            lon_onda_a = i == 0 ? x[i] : x[i - 1];
                            lon_onda = x[i];
                            lon_onda_p = i == x.Length - 1 ? x[i] : x[i + 1];
                        }
                        else
                        {
                            lon_onda_a = i > 1 ? x[i - 2] : i > 0 ? x[i - 1] : x[i];
                            lon_onda = i > 0 ? x[i - 1] : x[i];
                            lon_onda_p = x[i];
                        }
                        break;
                    }
                }
            }
            raton_x.Text = string.Format("{0:f3}", lon_onda);
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                // Añadir la línea atómica más cercana

                AddLineaAtomica(lon_onda, lon_onda_a, lon_onda_p);
                Console.Beep();
                Dibuja();
            }
            return;
        }
        private void AddLineaAtomica(double v, double va, double vp)
        {
            int intensidad = v_i.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(v_i.Text.Trim());
            double lon_onda;
            double lon_onda_anterior = 0;
            int ind_anterior = -1;
            double cota;
            double diferencia;
            int ind_mayor;
            double mayor_intensidad;
            int ind_cerca;
            double dif_londa;
            double mas_cerca;
            for (int i = 0; i < principal.lineas_atomicas.Count; i++)
            {
                if (principal.lineas_atomicas[i].intensidad >= intensidad)
                {
                    lon_onda = principal.lineas_atomicas[i].longitud_onda;
                    if (lon_onda == v)
                    {
                        ind_lineas.Add(i);
                        lista_elegidas.Items.Add(lista_elegibles.Items[i]);
                        break;
                    }
                    else if (lon_onda > v)
                    {
                        if (ind_anterior == -1)
                        {
                            ind_lineas.Add(i);
                            lista_elegidas.Items.Add(lista_elegibles.Items[i]);
                        }
                        else if (lon_onda - v < v - lon_onda_anterior)
                        {
                            diferencia = lon_onda - v;
                            cota = vp - v;
                            if (i > 0 && diferencia > cota)
                            {
                                // Difiere más que la distancia entre dos datos consecutivos del espectro

                                // Buscar la de mayor intensidad dentro del intervalo y si no hay ninguna dentro, la más cercana

                                ind_mayor = -1;
                                mayor_intensidad = double.MinValue;

                                ind_cerca = -1;
                                mas_cerca = double.MaxValue;

                                // Retroceder en el catálogo de líneas hasta que la diferencia en longitud de onda supere la 'diferencia'

                                for (int k = i - 1; k >= 0; k--)
                                {
                                    dif_londa = Math.Abs(principal.lineas_atomicas[k].longitud_onda - v);
                                    if (dif_londa > diferencia) break;
                                    if (dif_londa < cota)
                                    {
                                        // La mayor intensidad dentro de la cota

                                        if (mayor_intensidad < principal.lineas_atomicas[k].intensidad)
                                        {
                                            ind_mayor = k;
                                            mayor_intensidad = principal.lineas_atomicas[k].intensidad;
                                        }
                                    }
                                    else if (dif_londa < mas_cerca)
                                    {
                                        // La más próxima fuera de la cota

                                        ind_cerca = k;
                                        mas_cerca = dif_londa;
                                    }
                                }
                                if (ind_mayor != -1)
                                {
                                    ind_lineas.Add(ind_mayor);
                                    lista_elegidas.Items.Add(lista_elegibles.Items[ind_mayor]);
                                }
                                else
                                {
                                    if (ind_cerca != -1)
                                    {
                                        ind_lineas.Add(ind_cerca);
                                        lista_elegidas.Items.Add(lista_elegibles.Items[ind_cerca]);
                                    }
                                    else
                                    {
                                        ind_lineas.Add(i);
                                        lista_elegidas.Items.Add(lista_elegibles.Items[i]);
                                    }
                                }
                            }
                            else
                            {
                                ind_lineas.Add(i);
                                lista_elegidas.Items.Add(lista_elegibles.Items[i]);
                            }
                        }
                        else
                        {
                            diferencia = v - lon_onda_anterior;
                            cota = v - va;
                            if (i < principal.lineas_atomicas.Count - 1 && diferencia > cota)
                            {
                                // Difiere más que la distancia entre dos datos consecutivos del espectro

                                // Buscar la de mayor intensidad dentro del intervalo y si no hay ninguna dentro, la más cercana

                                ind_mayor = -1;
                                mayor_intensidad = double.MinValue;

                                ind_cerca = -1;
                                mas_cerca = double.MaxValue;

                                // Avanzar en el catálogo de líneas hasta que la diferencia en longitud de onda supere la 'diferencia'

                                for (int k = ind_anterior + 1; k < principal.lineas_atomicas.Count; k++)
                                {
                                    dif_londa = Math.Abs(principal.lineas_atomicas[k].longitud_onda - v);
                                    if (dif_londa > diferencia) break;
                                    if (dif_londa < cota)
                                    {
                                        // La mayor intensidad dentro de la cota

                                        if (mayor_intensidad < principal.lineas_atomicas[k].intensidad)
                                        {
                                            ind_mayor = k;
                                            mayor_intensidad = principal.lineas_atomicas[k].intensidad;
                                        }
                                    }
                                    else if (dif_londa < mas_cerca)
                                    {
                                        // La más próxima fuera de la cota

                                        ind_cerca = k;
                                        mas_cerca = dif_londa;
                                    }
                                }
                                if (ind_mayor != -1)
                                {
                                    ind_lineas.Add(ind_mayor);
                                    lista_elegidas.Items.Add(lista_elegibles.Items[ind_mayor]);
                                }
                                else
                                {
                                    if (ind_cerca != -1)
                                    {
                                        ind_lineas.Add(ind_cerca);
                                        lista_elegidas.Items.Add(lista_elegibles.Items[ind_cerca]);
                                    }
                                    else
                                    {
                                        ind_lineas.Add(ind_anterior);
                                        lista_elegidas.Items.Add(lista_elegibles.Items[ind_anterior]);
                                    }
                                }
                            }
                            else
                            {
                                ind_lineas.Add(ind_anterior);
                                lista_elegidas.Items.Add(lista_elegibles.Items[ind_anterior]);
                            }
                        }
                        break;
                    }
                    ind_anterior = i;
                    lon_onda_anterior = lon_onda;
                }
            }
        }
        private void Lista_elegibles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lista_elegibles.SelectedItem != null && lista_elegibles.SelectedIndex >= 0)
            {
                ind_lineas.Add(lista_elegibles.SelectedIndex);
                lista_elegidas.Items.Add(lista_elegibles.SelectedItem);
            }
            Dibuja();
        }
        private void Lista_elegidas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lista_elegidas.SelectedItem != null && lista_elegidas.SelectedIndex >= 0)
            {
                int k = lista_elegidas.SelectedIndex;
                ind_lineas.RemoveAt(k);
                lista_elegidas.Items.RemoveAt(k);
            }
            Dibuja();
        }
        private void Lista_elegidas_MouseDown(object sender, MouseEventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < lista_elegidas.Items.Count; i++)
                {
                    sb.AppendFormat("{0,10:f3} {1} {2}", principal.lineas_atomicas[ind_lineas[i]].longitud_onda, principal.lineas_atomicas[ind_lineas[i]].isotopo, principal.lineas_atomicas[ind_lineas[i]].intensidad);
                }
                Clipboard.SetText(sb.ToString());
                Console.Beep();
                return;
            }
        }
        private void B_e_Click(object sender, EventArgs e)
        {
            string elemento = v_e.Text.Trim();
            if (elemento.Length == 0) return;
            int intensidad = v_i.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(v_i.Text.Trim());
            for (int i = 0; i < principal.lineas_atomicas.Count; i++)
            {
                if (principal.lineas_atomicas[i].longitud_onda < xmin) continue;
                if (principal.lineas_atomicas[i].longitud_onda > xmax) break;
                if (principal.lineas_atomicas[i].intensidad < intensidad) continue;
                if (principal.lineas_atomicas[i].elemento.Equals(elemento, StringComparison.OrdinalIgnoreCase))
                {
                    ind_lineas.Add(i);
                    lista_elegidas.Items.Add(lista_elegibles.Items[i]);
                }
            }
            Dibuja();
            Console.Beep();
        }
        private void Vspec_CheckedChanged(object sender, EventArgs e)
        {
            LeeEspectros();
        }
        private void LeyendaIzq_CheckedChanged(object sender, EventArgs e)
        {
            Dibuja();
        }
    }
}

