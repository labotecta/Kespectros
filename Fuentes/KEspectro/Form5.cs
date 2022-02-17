using System.Drawing.Imaging;

namespace KEspectro
{
    public partial class Form5 : Form
    {
        public int nclusters = 0;
        public int[] nespectros = null;
        public double[] medias = null;
        public double[,] distancias = null;
        public Bitmap img = null;
        private int m_50;
        private const int ZONAS_X = 2;
        Font fteg;
        Font ftep;
        public Form5()
        {
            InitializeComponent();
        }
        public void Dibuja()
        {
            if (nclusters == 0 || nespectros == null || medias == null || distancias == null) return;
            Lienzo.Size = new Size(ClientRectangle.Width, ClientRectangle.Height - 50);
            Lienzo.Location = new Point(0, 50);
            int ancho_lienzo = Lienzo.Width;
            int alto_lienzo = Lienzo.Height;
            double rejex = ancho_lienzo / Form1.ancho_lienzo_referencia;
            double rejey = alto_lienzo / Form1.alto_lienzo_referencia;
            double reje = Math.Max(rejex, rejey);
            m_50 = (int)Math.Round(50 * reje);
            fteg = new Font("Verdana", (int)Math.Round(13 * reje), FontStyle.Regular, GraphicsUnit.Point);
            ftep = new Font("Verdana", (int)Math.Round(12 * reje), FontStyle.Regular, GraphicsUnit.Point);

            int ancho_zona = ancho_lienzo / ZONAS_X;
            int zonas_y = nclusters / ZONAS_X + nclusters % ZONAS_X;
            if (zonas_y == 0) return;
            int alto_zona = alto_lienzo / zonas_y;
            int ancho_barra = (ancho_zona - 2 * ftep.Height) / (nclusters + 1);
            double ymax = double.MinValue;
            for (int i = 0; i < nclusters; i++)
            {
                for (int j = i + 1; j < nclusters; j++)
                {
                    ymax = Math.Max(ymax, distancias[i, j]);
                }
                ymax = Math.Max(ymax, medias[i]);
            }
            if (ymax == 0) return;
            double f = (alto_zona - 2 * ftep.Height) / ymax;
            img = new Bitmap(ancho_lienzo, alto_lienzo, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(img);
            g.FillRectangle(Brushes.White, 0, 0, Lienzo.Width, Lienzo.Height);
            double y;
            int xi;
            int yi;
            int imin;
            double dmin;
            string rel;
            float ancad;
            int ic = 0;
            for (int i = 0; i < zonas_y; i++)
            {
                xi = 0;
                yi = i * alto_zona;
                for (int j = 0; j < ZONAS_X; j++)
                {
                    g.DrawRectangle(Pens.Black, xi, yi, ancho_zona - 1, alto_zona - 1);
                    g.DrawString(string.Format("{0}", ic + 1), fteg, Brushes.Black, xi, yi);
                    g.DrawString(string.Format("{0}", nespectros[ic]), fteg, Brushes.DarkRed, xi + m_50, yi);
                    imin = -1;
                    dmin = double.MaxValue;
                    for (int k = 0; k < nclusters; k++)
                    {
                        if (ic != k)
                        {
                            if (dmin > distancias[ic, k])
                            {
                                imin = k;
                                dmin = distancias[ic, k];
                            }
                        }
                    }
                    for (int k = 0; k < nclusters; k++)
                    {
                        y = alto_zona - 2 * ftep.Height - distancias[ic, k] * f;
                        g.FillRectangle(k == imin ? Brushes.Green : Brushes.Blue, ftep.Height + xi + k * ancho_barra + 1, (float)(yi + ftep.Height + y), ancho_barra - 2, (float)(distancias[ic, k] * f));
                        g.DrawString(string.Format("{0}", k + 1), ftep, Brushes.Black, ftep.Height + xi + k * ancho_barra + ancho_barra / 2 - 6, yi + alto_zona - ftep.Height);
                    }
                    y = alto_zona - 2 * ftep.Height - medias[ic] * f;
                    g.FillRectangle(Brushes.Red, ftep.Height + xi + nclusters * ancho_barra, (float)(yi + ftep.Height + y), ancho_barra, (float)(medias[ic] * f));
                    if (nespectros[ic] > 1 && dmin != 0)
                    {
                        rel = string.Format("{0:f3}", medias[ic] / dmin);
                        ancad = g.MeasureString(rel, fteg).Width;
                        g.DrawString(rel, fteg, Brushes.Red, xi + ancho_zona - ancad - 8, yi);
                    }
                    if (ic == nclusters - 1) break;
                    ic++;
                    xi += ancho_zona;
                }
                if (ic == nclusters) break;
            }
            Lienzo.Image = img;
            Lienzo.Refresh();
        }
        private void Form5_Resize(object sender, EventArgs e)
        {
            if (nclusters == 0 || nespectros == null || medias == null || distancias == null) return;
            Dibuja();
        }
        private void Salvar_Click(object sender, EventArgs e)
        {
            if (nclusters == 0 || nespectros == null || medias == null || distancias == null || img == null) return;
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
