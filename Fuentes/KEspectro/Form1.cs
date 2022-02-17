using System.Drawing.Imaging;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System.Reflection;
using System.Text;
using KEspectro.Properties;
using System.Runtime.Serialization.Formatters.Binary;

namespace KEspectro
{
    public partial class Form1 : Form
    {
        public const double ancho_lienzo_referencia = 2552;
        public const double alto_lienzo_referencia = 1295;
        public const double WIEN = 0.002898E+10;   // Para lomngitudes de onda en Ångstrom
        private readonly double[] CLASIFICACION_OM = { 7500, 6000, 5200, 3700, 0 };
        //private readonly double[] CLASIFICACION_OM = { 6400, 5500, 4800, 4000, 0 };
        private string VERSIONAPP;
        private const int MAX_GRADO = 11;
        private const int MAX_GRADOM1 = MAX_GRADO + 1;
        private const int NCOEFICENTES = 2 * MAX_GRADOM1;
        private const string FC_PRO = "0,75";
        private const string FC_ABS = "0,5";
        private string FICHERO_CENTROIDES;
        private bool clasificacionML;
        private readonly string[] rotulos = { "Media", "D.estándar", "Índice Davies Bouldin", "DE.Temperatura", "Media G", "D.estándar G", "Índice Davies Bouldin G", "DE.Temperatura G" };
        public string sendaApp = string.Empty;
        public readonly List<string> lista_elegibles_principal = new List<string>();
        public class EspectrosAtomicos
        {
            public int intensidad;
            public double longitud_onda;
            public string elemento;
            public string isotopo;
            public EspectrosAtomicos(int intensidad, double longitud_onda, string elemento, string isotopo)
            {
                this.intensidad = intensidad;
                this.longitud_onda = longitud_onda;
                this.elemento = elemento;
                this.isotopo = isotopo;
            }
        }
        public readonly List<EspectrosAtomicos> lineas_atomicas = new List<EspectrosAtomicos>();

        private readonly RegresionLineal linReg = new RegresionLineal();
        private double[] y;
        private double[,] x;
        private double[] w;
        public class Dato
        {
            public int n;
            public double x;
            public double y;
            public Dato(int n, double x, double y)
            {
                this.n = n;
                this.x = x;
                this.y = y;
            }
        }
        private List<Dato> picos_mas;
        private List<Dato> picos_menos;
        public class DatoEx
        {
            public double AR;
            public double DEC;
            public List<Dato> datos;
            public DatoEx(double AR, double DEC, List<Dato> datos)
            {
                this.AR = AR;
                this.DEC = DEC;
                this.datos = datos;
            }
        }

        // Se necestita esta variable global para los botones ´Reducir' y 'Calcula'

        private DatoEx datosex = null;

        private Form2 panel_img = null;
        private bool cancelar;
        private bool evitar;
        private Form3 panel_mapa = null;
        public class EspectroAnalizado
        {
            public int grado;
            public bool pro;
            public double corte;
            public double fcorte;
            public int distancia_movil;
            public double RYSQa;
            public double RYSQb;
            public double MY;
            public double MYc;
            public double AR;
            public double DEC;
            public int n;
            public byte[] pico;
            public double[] x;
            public double[] y;
            public double[] yc;
            public double[] SDVMovil;
            public double[] yn;
            public double londaMaximo;
            public double temperatura;
            public EspectroAnalizado(int grado, bool pro, double corte, double fcorte, int distancia_movil, double RYSQa, double RYSQb, double MY, double MYc, double AR, double DEC, int n, byte[] pico, double[] x, double[] y, double[] yc, double[] SDVMovil, double[] yn, double LondaMaximo, double temperatura)
            {
                this.grado = grado;
                this.pro = pro;
                this.corte = corte;
                this.fcorte = fcorte;
                this.distancia_movil = distancia_movil;
                this.RYSQa = RYSQa;
                this.RYSQb = RYSQb;
                this.MY = MY;
                this.MYc = MYc;
                this.AR = AR;
                this.DEC = DEC;
                this.n = n;
                this.pico = pico;
                this.x = x;
                this.y = y;
                this.yc = yc;
                this.SDVMovil = SDVMovil;
                this.yn = yn;
                this.londaMaximo = LondaMaximo;
                this.temperatura = temperatura;
            }
        }
        public List<EspectroAnalizado> espectros;

        private const int N_PREVIAS = 5;
        private int tipo_distancia;
        private int modo_iniciar;
        private int tipo_datosY;
        private bool normaliza_centroide;
        private bool modo_podar;
        private double pu_podar;
        private double minimo_podar;
        private bool modo_forzar;
        private int modo_centroide;
        private bool modo_genetico;
        private bool mutar;
        private double EMPEORA_MAX_MD;
        private double EMPEORA_MAX_DE;
        private double MEJORA_MIN_MD;
        private double MEJORA_MIN_DE;
        public int calcula_desde;
        public int calcula_hasta;
        private DateTime tiempo_inicio;
        private long segundos;
        public class Cluster
        {
            public int n;
            public List<int> ind_f_espectros;
            public List<double> distancias;
            public int ind_min;
            public double distancia_min;
            public int ind_max;
            public double distancia_max;
            public double distancia_media;
            public double desviacion_estandar;
            public double londaMaximo;
            public double temperatura;
            public double de_temperatura;
            public Cluster()
            {
                ind_min = -1;
                ind_max = -1;
                n = 0;
                ind_f_espectros = new List<int>();
            }
        }
        private bool omite_cambio_seleccion;
        public class IndiceDB
        {
            public double indice;
            public double indice_ponderado;
            public int[] IR;
            public double[] R;
            public IndiceDB(double indice, double indice_ponderado, int[] IR, double[] R)
            {
                this.indice = indice;
                this.indice_ponderado = indice_ponderado;
                this.IR = IR;
                this.R = R;
            }
        }

        public int[] centroides_leidos_num_esp = null;
        public double[][] centroides_leidos = null;
        public double[] x_centroides_leidos = null;
        public class Caja
        {
            public int ID;
            public int[] orden;
            public Cluster[] clusters;
            public double[][] centroides = null;
            public int[] cluster_espectro;
            public Estadistica estadistica;
            public Random az;
            public List<string> evolucion;
            public int salida;
            public Caja(int ID, int nclusters, int nespectros, int ndatos, int semilla)
            {
                this.ID = ID;
                orden = new int[nclusters];
                clusters = new Cluster[nclusters];
                for (int i = 0; i < nclusters; i++)
                {
                    clusters[i] = new Cluster();
                }
                centroides = new double[nclusters][];
                for (int i = 0; i < nclusters; i++)
                {
                    centroides[i] = new double[ndatos];
                }
                cluster_espectro = new int[nespectros];
                Array.Clear(cluster_espectro, 0, nespectros);
                estadistica = new Estadistica();
                az = new Random(semilla);
                evolucion = new List<string>();
                salida = 0;
            }
        }
        private const int N_CAJAS = 8;
        public int nclusters;
        private int ndatos;
        private readonly Caja[] mejorcaja = new Caja[N_CAJAS];
        private readonly int[] mejor_intento = new int[N_CAJAS];
        private readonly double[] mejor_estimador = new double[N_CAJAS];
        private int sel_anterior;
        private class Confianza
        {
            public double distancia;
            public int cluster;
            public Confianza(double distancia, int cluster)
            {
                this.distancia = distancia;
                this.cluster = cluster;
            }
        }
        public class Estadistica
        {
            public int ng;
            public double d_media_g;
            public double d_estandar_g;
            public double temperatura_g;
            public double de_temperatura_g;
            public IndiceDB db;
            public double si;
            public Estadistica()
            {
                ng = 0;
                d_media_g = 0;
                d_estandar_g = 0;
                temperatura_g = 0;
                de_temperatura_g = 0;
            }
        }

        private readonly object sync = new();
        private const int MAX_HILOS = 128;
        private Thread hiloclasifica = null;
        private delegate void DelegadoFinPase(Caja caja, int hilo);
        private DelegadoFinPase delegadofinpase;
        private delegate void DelegadoFinHilo(int hilo);
        private DelegadoFinHilo delegadofinhilo;
        private delegate void DelegadoActLista(Caja caja, int hilo);
        private DelegadoActLista delegadoactlista;
        private int nhilos;
        private int hilos_pendientes;
        private bool[] hilo_finalizado;
        private int maximo_iteraciones;
        private int pases_a_realizar;
        private int pases_realizados;
        private readonly ToolTip ayudarapida = new();
        private string senda_resultados;
        private bool genetico_pendiente;
        private int minimiza_genetico;
        private readonly List<string> evolucion_genetica = new();
        private int mutan_datos;
        private double muta_cuanto;
        private int generaciones_a_realizar;
        private int generaciones_realizadas;
        public class Cromosoma
        {
            public double valor;
            public double[][] genes;
            public Cromosoma(int nclusters, int ndatos)
            {
                genes = new double[nclusters][];
                for (int i = 0; i < nclusters; i++)
                {
                    genes[i] = new double[ndatos];
                }
            }
            public Cromosoma(double[][] genes, double valor)
            {
                this.genes = genes;
                this.valor = valor;
            }
        }
        private readonly List<Cromosoma> cromosomas = new();
        private bool salva_cromosomas;
        private string f_cromosomas;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            VERSIONAPP = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Text = string.Format("Análisis y clasificación de espectros v: {0}", VERSIONAPP);
            sendaApp = Application.StartupPath;
            AyudaRapida();
            evitar = false;
            cancelar = false;
            Esc.Enabled = false;
            Esc.Cursor = Cursors.No;
            Grado.Text = "5";
            IniciaTablaDatos();
            IniciaTablaCoefcientes();
            MuestraForm2();
            FilaFichero.Minimum = 1;
            FilaFichero.Maximum = 1;
            FilaFichero.Value = 1;
            R_corte.Text = "p.u.";
            R_Fichero_Centroides.Text = FICHERO_CENTROIDES = string.Empty;
            clasificacionML = false;
            delegadofinpase = new DelegadoFinPase(FinPase);
            delegadofinhilo = new DelegadoFinHilo(FinHilo);
            delegadoactlista = new DelegadoActLista(ActLista);
            senda_resultados = Settings.Default.senda_resultados;
            LeeEspectrosAtomicos();
            genetico_pendiente = false;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            salva_cromosomas = false;
            cancelar = true;
            Esc.Enabled = false;
            Esc.Cursor = Cursors.No;
            modo_genetico = false;
            Application.DoEvents();
            int contador = 0;
            while ((hilos_pendientes > 0 || genetico_pendiente) && contador < 60)
            {
                cancelar = true;
                Thread.Sleep(1000);
                contador++;
                Application.DoEvents();
            }
        }
        private void AyudaRapida()
        {
            ayudarapida.RemoveAll();
            ayudarapida.ShowAlways = true;
            ayudarapida.SetToolTip(V_modoIniciar, "0=Centroides leidos; 1=Al azar; 2=Recursivo; 3=Recursivo moderado: 4=Excluyente; 5=Kmeans++");
            ayudarapida.SetToolTip(V_tipoDistancia, "0=Euclidiana; 1=Manhattan; 2=1-Covarianza; 3=Euclidiana^2; 4=Manhattan^2");
            ayudarapida.SetToolTip(V_tipoDatosY, "0=Datos reales; 1=Datos ajustados dentro del corte; 2=Diferencias relativas; 3=Diferencias absolutas; 4=Diferencias 0,1 (si/no); 5=Datos ajustados; 6=Datos reales normalizados; 7=1 normalizado");
            ayudarapida.SetToolTip(V_normalizaCentroides, "Sólo para tipo Y = 4. Los valores de los centroides serán 0 ó 1");
            ayudarapida.SetToolTip(V_ppPoda, "% de espectros en el cluster que no se usarán para el cálculo del centroide");
            ayudarapida.SetToolTip(V_veces, "No podar el espectro si es suficientemente bueno (la distancia a su centroide es 'veces' más pequeña que al centroide más próximo)");
            ayudarapida.SetToolTip(V_podar, "No usar los espectros que menos encajan, dentro de cada cluster, para el cálculo del centroide");
            ayudarapida.SetToolTip(ListaEvolucion, "Evolución del mejor pase");
            ayudarapida.SetToolTip(R_mejorMed, "Pase con la menor media");
            ayudarapida.SetToolTip(R_mejorDE, "Pase con la menor desviación estándar");
            ayudarapida.SetToolTip(R_mejorDB, "Pase con el menor índice Davies Bouldin (DB)");
            ayudarapida.SetToolTip(R_mejorDET, "Pase con el mayor índice Silhouette (SILS)");
            ayudarapida.SetToolTip(MLKMeans, "Clasifica con la librería ML de Microsoft que usa distancias Euclidianas^2 (tipo de distancia 3)");
            ayudarapida.SetToolTip(V_minimizaGenetico, "0=Distancia; 1=D.estándar; 2=DB; 3=D.estándar Temperatura");
        }
        public void LeeEspectrosAtomicos()
        {
            FileStream fe = new FileStream(Path.Combine(sendaApp, "lineas_atomicas.txt"), FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader r = new StreamReader(fe);
            lineas_atomicas.Clear();
            lista_elegibles_principal.Clear();
            string linea;
            string[] sd;
            int intensidad;
            double longitud_onda;
            string elemento;
            string isotopo;
            while (!r.EndOfStream)
            {
                linea = r.ReadLine();
                sd = linea.Split('\t');
                intensidad = sd[2].Trim().Length == 0 ? 0 : Convert.ToInt32(sd[2]);
                longitud_onda = Convert.ToDouble(sd[3].Trim().Replace('.', ','));
                elemento = sd[4].Trim();
                isotopo = sd[4].Trim() + " " + sd[1].Trim();
                lista_elegibles_principal.Add(string.Format("{0,10:f3} {1} {2}", longitud_onda, isotopo, intensidad));
                lineas_atomicas.Add(new EspectrosAtomicos(intensidad, longitud_onda, elemento, isotopo));
            }
            r.Close();
        }
        private void MuestraForm2()
        {
            if (panel_img == null || !panel_img.Visible)
            {
                panel_img = new Form2
                {
                    principal = this
                };
                panel_img.Show();
            }
        }
        private void Habilitar(bool que)
        {
            Cursor = que ? Cursors.Default : Cursors.WaitCursor;
            R_fichero.Enabled = que;
            Vertical.Enabled = que;
            Leer.Enabled = que;
            FilaFichero.Enabled = que;
            V_reducirCada.Enabled = que;
            Reducir.Enabled = que;
            Recalcular.Enabled = que;
            ExcluirSinMaximo.Enabled = que;
            Partir.Enabled = que;
            Todo.Enabled = que;
            Grado.Enabled = que;
            Calcula.Enabled = que;
            tabla.Enabled = que;
            Coeficientes.Enabled = que;
            Recalcula.Enabled = que;
            R2.Enabled = que;
            Fcorte.Enabled = que;
            Corte.Enabled = que;
            Proporcional.Enabled = que;
            Movil.Enabled = que;
            SelPicos.Enabled = que;
            SalvaPicos.Enabled = que;
            SalvaImagen.Enabled = que;

            R_Fichero_Clusters.Enabled = que;
            R_Fichero_Centroides.Enabled = que;
            Asigna.Enabled = que;
            R_espectros.Enabled = que;
            LeerEspectrosAnalizados.Enabled = que;
            Restar.Enabled = que;
            V_max_Iteraciones.Enabled = que;
            V_nclusters.Enabled = que;
            V_semilla.Enabled = que;
            V_genetico.Enabled = que;
            V_generaciones.Enabled = que;
            V_torneo.Enabled = que;
            V_ppTorneo.Enabled = que;
            V_mutar.Enabled = que;
            V_ppMutar.Enabled = que;
            V_mutarCuanto.Enabled = que;
            V_minimizaGenetico.Enabled = que;
            V_tipoDatosY.Enabled = que;
            V_tipoDistancia.Enabled = que;
            V_modoIniciar.Enabled = que;
            V_normalizaCentroides.Enabled = que;
            V_max_empeora_md.Enabled = que;
            V_min_mejora_md.Enabled = que;
            V_max_empeora_de.Enabled = que;
            V_min_mejora_de.Enabled = que;
            V_dibujaTodo.Enabled = que;
            V_ppPoda.Enabled = que;
            V_veces.Enabled = que;
            V_podar.Enabled = que;
            V_forzar.Enabled = que;
            V_modoCentroide.Enabled = que;
            V_silhouette.Enabled = que;
            panel1.Enabled = que;
            V_hilos.Enabled = que;
            FilaCluster.Enabled = que;
            CreaClusters.Enabled = que;
            TablaClusters.Enabled = que;
            V_pases.Enabled = que;
            ListaIntentos.Enabled = que;
            ListaEvolucion.Enabled = que;
            SalvaCaso.Enabled = que;
            SalvaClasificacion.Enabled = que;
            SalvaClusters.Enabled = que;
            SalvaEspectrosAnalizados.Enabled = que;
            LeerClasificacion.Enabled = que;
            LeeCentroides.Enabled = que;
            SalvaCentroides.Enabled = que;
            V_desde.Enabled = que;
            V_hasta.Enabled = que;
            MLKMeans.Enabled = que;
            VerTemperatura.Enabled = que;
            VerCentroides.Enabled = que;
            DistanciasCentroides.Enabled = que;
            SeparaT.Enabled = que;
            Mapa.Enabled = que;
            LeerCromosomas.Enabled = que;
            SalvaCromosomas.Enabled = que;
            EjecutaGenetico.Enabled = que;

            /*panel_img.Inicio.Enabled = que;
            panel_img.Atras.Enabled = que;
            panel_img.Adelante.Enabled = que;
            panel_img.Final.Enabled = que;*/

            Application.DoEvents();
        }
        private void IniciaTablaDatos()
        {
            tabla.BackgroundColor = Color.LightGray;
            tabla.BorderStyle = BorderStyle.Fixed3D;
            tabla.ReadOnly = true;
            tabla.MultiSelect = false;
            tabla.AllowUserToAddRows = false;
            tabla.AllowUserToDeleteRows = false;
            tabla.AllowUserToOrderColumns = false;
            tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tabla.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            tabla.AllowUserToResizeRows = false;
            tabla.AllowUserToResizeColumns = false;
            tabla.ColumnHeadersVisible = true;
            tabla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            tabla.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            tabla.DefaultCellStyle.SelectionBackColor = Color.LightSalmon;
            tabla.RowHeadersVisible = false;
            int tabla_alto_fila = tabla.RowTemplate.Height;
            int tabla_alto_cabecera = tabla.ColumnHeadersHeight;
            if (tabla_alto_cabecera <= tabla_alto_fila) tabla_alto_cabecera = tabla_alto_fila + 1;
            tabla.RowTemplate.Height = tabla_alto_fila;
            tabla.ColumnHeadersHeight = tabla_alto_cabecera;
            tabla.ColumnCount = 3;
            int j = 0;
            tabla.Columns[j].FillWeight = 10;
            tabla.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tabla.Columns[j].DefaultCellStyle.BackColor = Color.Beige;
            tabla.Columns[j].Name = "N";
            j++;
            tabla.Columns[j].FillWeight = 12;
            tabla.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            tabla.Columns[j].Name = "W";
            j++;
            tabla.Columns[j].FillWeight = 12;
            tabla.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            tabla.Columns[j].Name = "F";
            Puntos.Text = string.Empty;
            tabla.RowCount = 0;
        }
        private void IniciaTablaCoefcientes()
        {
            Coeficientes.BackgroundColor = Color.LightGray;
            Coeficientes.BorderStyle = BorderStyle.Fixed3D;
            Coeficientes.ReadOnly = true;
            Coeficientes.MultiSelect = false;
            Coeficientes.AllowUserToAddRows = false;
            Coeficientes.AllowUserToDeleteRows = false;
            Coeficientes.AllowUserToOrderColumns = false;
            Coeficientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Coeficientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Coeficientes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            Coeficientes.AllowUserToResizeRows = false;
            Coeficientes.AllowUserToResizeColumns = false;
            Coeficientes.ColumnHeadersVisible = true;
            Coeficientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            Coeficientes.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            Coeficientes.DefaultCellStyle.SelectionBackColor = Color.LightSalmon;
            Coeficientes.RowHeadersVisible = false;
            int tabla_alto_fila = Coeficientes.RowTemplate.Height;
            int tabla_alto_cabecera = Coeficientes.ColumnHeadersHeight;
            if (tabla_alto_cabecera <= tabla_alto_fila) tabla_alto_cabecera = tabla_alto_fila + 1;
            Coeficientes.RowTemplate.Height = tabla_alto_fila;
            Coeficientes.ColumnHeadersHeight = tabla_alto_cabecera;
            Coeficientes.ColumnCount = 3;
            int j = 0;
            Coeficientes.Columns[j].FillWeight = 4;
            Coeficientes.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Coeficientes.Columns[j].DefaultCellStyle.BackColor = Color.Beige;
            Coeficientes.Columns[j].Name = "Grado";
            j++;
            Coeficientes.Columns[j].FillWeight = 12;
            Coeficientes.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Coeficientes.Columns[j].Name = "Coeficientes A";
            j++;
            Coeficientes.Columns[j].FillWeight = 12;
            Coeficientes.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Coeficientes.Columns[j].Name = "Coeficientes B";
            Coeficientes.RowCount = 0;
        }
        private void IniciaTablaClusters()
        {
            TablaClusters.BackgroundColor = Color.LightGray;
            TablaClusters.BorderStyle = BorderStyle.Fixed3D;
            TablaClusters.ReadOnly = true;
            TablaClusters.MultiSelect = false;
            TablaClusters.AllowUserToAddRows = false;
            TablaClusters.AllowUserToDeleteRows = false;
            TablaClusters.AllowUserToOrderColumns = false;
            TablaClusters.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            TablaClusters.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            TablaClusters.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            TablaClusters.AllowUserToResizeRows = false;
            TablaClusters.AllowUserToResizeColumns = false;
            TablaClusters.ColumnHeadersVisible = true;
            TablaClusters.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            TablaClusters.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            TablaClusters.DefaultCellStyle.SelectionBackColor = Color.LightSalmon;
            TablaClusters.RowHeadersVisible = false;
            int tabla_alto_fila = TablaClusters.RowTemplate.Height;
            int tabla_alto_cabecera = TablaClusters.ColumnHeadersHeight;
            if (tabla_alto_cabecera <= tabla_alto_fila) tabla_alto_cabecera = tabla_alto_fila + 1;
            TablaClusters.RowTemplate.Height = tabla_alto_fila;
            TablaClusters.ColumnHeadersHeight = tabla_alto_cabecera;
            TablaClusters.ColumnCount = 11;
            int j = 0;
            TablaClusters.Columns[j].FillWeight = 6;
            TablaClusters.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TablaClusters.Columns[j].DefaultCellStyle.BackColor = Color.Beige;
            TablaClusters.Columns[j].Name = "Cluster";
            j++;
            TablaClusters.Columns[j].FillWeight = 8;
            TablaClusters.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TablaClusters.Columns[j].Name = "Centroide";
            j++;
            TablaClusters.Columns[j].FillWeight = 8;
            TablaClusters.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TablaClusters.Columns[j].Name = "N";
            j++;
            TablaClusters.Columns[j].FillWeight = 12;
            TablaClusters.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TablaClusters.Columns[j].Name = "Min";
            j++;
            TablaClusters.Columns[j].FillWeight = 12;
            TablaClusters.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TablaClusters.Columns[j].Name = "Max";
            j++;
            TablaClusters.Columns[j].FillWeight = 12;
            TablaClusters.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TablaClusters.Columns[j].Name = "Media";
            j++;
            TablaClusters.Columns[j].FillWeight = 12;
            TablaClusters.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TablaClusters.Columns[j].Name = "D.Estd";
            j++;
            TablaClusters.Columns[j].FillWeight = 6;
            TablaClusters.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TablaClusters.Columns[j].DefaultCellStyle.BackColor = Color.Beige;
            TablaClusters.Columns[j].Name = "Cluster";
            j++;
            TablaClusters.Columns[j].FillWeight = 8;
            TablaClusters.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TablaClusters.Columns[j].DefaultCellStyle.BackColor = Color.Beige;
            TablaClusters.Columns[j].Name = "Ind DB";
            TablaClusters.RowCount = 0;
            j++;
            TablaClusters.Columns[j].FillWeight = 8;
            TablaClusters.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TablaClusters.Columns[j].Name = "Temp K";
            TablaClusters.RowCount = 0;
            j++;
            TablaClusters.Columns[j].FillWeight = 8;
            TablaClusters.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TablaClusters.Columns[j].Name = "DETemp";
            TablaClusters.RowCount = 0;
        }
        private void Leer_Click(object sender, EventArgs e)
        {
            OpenFileDialog ficherolectura = new()
            {
                Filter = "CVS |*.csv|TXT |*.txt|TODO |*.*",
                FilterIndex = 1,
                RestoreDirectory = false,
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false
            };
            if (ficherolectura.ShowDialog() == DialogResult.OK)
            {
                R_fichero.Items.Clear();
                foreach (string f in ficherolectura.FileNames)
                {
                    R_fichero.Items.Add(f);
                }
                Habilitar(false);
                R_filas.Enabled = true;
                R_fichero.Text = ficherolectura.FileNames[0];
                ContarFilas(R_fichero.Text);
                FilaFichero.Value = 1;
                LeeDesdeFicheroFte();
                Habilitar(true);
            }
        }
        private void ContarFilas(string fichero)
        {
            if (Vertical.Checked)
            {
                FilaFichero.Maximum = 1;
            }
            else
            {
                // Contar el número de filas

                Habilitar(false);
                FileStream fsfc = new(fichero, FileMode.Open, FileAccess.Read, FileShare.Read);
                if (fsfc == null) return;
                StreamReader sr = new(fsfc);
                sr.ReadLine();
                int contador = 0;
                R_filas.Text = string.Format("{0:N0}", contador);
                while (!sr.EndOfStream)
                {
                    sr.ReadLine();
                    contador++;
                    if (contador % 1000 == 0)
                    {
                        R_filas.Text = string.Format("{0:N0}", contador);
                        Application.DoEvents();
                    }
                }
                sr.Close();
                FilaFichero.Maximum = contador;
                R_filas.Text = string.Format("{0:N0}", contador);
                Habilitar(true);
            }
        }
        private void LeeDesdeFicheroFte()
        {
            if (!evitar)
            {
                IniciaTablaCoefcientes();
                R2.Text = string.Empty;
                F.Text = string.Empty;
                DE.Text = string.Empty;
                Corte.Text = string.Empty;
                picos_mas = new List<Dato>();
                picos_menos = new List<Dato>();
                Absorcion.Text = string.Empty;
                Emision.Text = string.Empty;
                Puntos.Text = string.Empty;
                tabla.RowCount = 0;
                Application.DoEvents();
            }
            FileStream fsfc;
            StreamReader sr;
            string linea;
            string[] ss;
            string[] fila = new string[3];
            string fichero = R_fichero.Text;
            fsfc = new FileStream(fichero, FileMode.Open, FileAccess.Read, FileShare.Read);
            sr = new StreamReader(fsfc);
            int ilo = -1;
            int ifj = -1;
            int cRA = -1;
            int cDEC = -1;
            int decalajeW = -1;
            int decalajeF = -1;
            linea = sr.ReadLine();
            ss = linea.Split(';');
            if (Vertical.Checked)
            {
                if (ss[0].Equals("W", StringComparison.OrdinalIgnoreCase))
                {
                    ilo = 0;
                    ifj = 1;
                }
                else
                {
                    ilo = 1;
                    ifj = 0;
                }
            }
            else
            {
                for (int i = 0; i < ss.Length; i++)
                {
                    if (ss[i].StartsWith("WAVE_", StringComparison.OrdinalIgnoreCase))
                    {
                        decalajeW = i;
                        break;
                    }
                }
                if (decalajeW == -1)
                {
                    MessageBox.Show(string.Format("El fichero {0} no contiene columna W", fichero), "Leer fichero de espectros fuente");
                    return;
                }
                for (int i = 0; i < ss.Length; i++)
                {
                    if (ss[i].StartsWith("FLUX_", StringComparison.OrdinalIgnoreCase))
                    {
                        decalajeF = i;
                        break;
                    }
                }
                if (decalajeF == -1)
                {
                    MessageBox.Show(string.Format("El fichero {0} no contiene columna F", fichero), "Leer fichero de espectros fuente");
                    return;
                }
                for (int i = 0; i < ss.Length; i++)
                {
                    if (ss[i].Equals("RA", StringComparison.OrdinalIgnoreCase))
                    {
                        cRA = i;
                        break;
                    }
                }
                if (cRA == -1)
                {
                    for (int i = 0; i < ss.Length; i++)
                    {
                        if (ss[i].Equals("OBJRA", StringComparison.OrdinalIgnoreCase))
                        {
                            cRA = i;
                            break;
                        }
                    }
                }
                for (int i = 0; i < ss.Length; i++)
                {
                    if (ss[i].Equals("DEC", StringComparison.OrdinalIgnoreCase))
                    {
                        cDEC = i;
                        break;
                    }
                }
                if (cDEC == -1)
                {
                    for (int i = 0; i < ss.Length; i++)
                    {
                        if (ss[i].Equals("OBJDEC", StringComparison.OrdinalIgnoreCase))
                        {
                            cDEC = i;
                            break;
                        }
                    }
                }
            }
            double AR = 0;
            double DEC = 0;
            double vx;
            double vy;
            int contador = 0;
            int pares_xy = 0;
            List<Dato> datos = new();
            while (!sr.EndOfStream)
            {
                linea = sr.ReadLine();

                // 'datos' son los pares x,y que hay en una fila

                if (Vertical.Checked)
                {
                    ss = linea.Split(';');

                    // Se supone que sólo hay dos columnas: una es la 'x' y la otra la 'y'

                    vx = Convert.ToDouble(ss[ilo]);
                    vy = Convert.ToDouble(ss[ifj]);

                    // Cada fila contiene un sólo par x,y

                    datos.Add(new Dato(contador++, vx, vy));
                    pares_xy++;
                }
                else
                {
                    // Sólo se lee una fila, la número 'contador'

                    if (contador + 1 == FilaFichero.Value)
                    {
                        ss = linea.Split(';');
                        if (cRA != -1)
                        {
                            AR = Convert.ToDouble(ss[cRA]);
                        }
                        else
                        {
                            AR = 0;
                        }
                        if (cDEC != -1)
                        {
                            DEC = Convert.ToDouble(ss[cDEC]);
                        }
                        else
                        {
                            DEC = 0;
                        }
                        pares_xy = decalajeF - decalajeW;
                        for (int i = 0; i < pares_xy; i++)
                        {
                            vx = Convert.ToDouble(ss[decalajeW + i]);
                            vy = Convert.ToDouble(ss[decalajeF + i]);
                            datos.Add(new Dato(i, vx, vy));
                        }
                        break;
                    }
                }
                contador++;
            }
            sr.Close();
            datosex = new DatoEx(AR, DEC, datos);
            int cada = Convert.ToInt32(V_reducirCada.Text.Trim());
            datosex = ReducirDatos(datosex, cada);
            double[] xd = new double[datosex.datos.Count];
            double[] yd = new double[datosex.datos.Count];
            contador = 0;
            foreach (Dato d in datosex.datos)
            {
                if (!evitar)
                {
                    fila[0] = contador.ToString();
                    fila[1] = string.Format("{0:f2}", d.x);
                    fila[2] = string.Format("{0:f2}", d.y);
                    tabla.Rows.Add(fila);
                }
                xd[contador] = d.x;
                yd[contador] = d.y;
                contador++;
            }
            if (!evitar)
            {
                MuestraForm2();
                List<EspectroAnalizado> unespectro = new();
                int grado = Convert.ToInt32(Grado.Text.Trim());
                bool pro = Proporcional.Checked;
                double corte = Corte.Text.Trim().Length == 0 ? 0 : Convert.ToDouble(Corte.Text.Trim());
                if (Fcorte.Text.Trim().Length == 0) Fcorte.Text = Proporcional.Checked ? FC_PRO : FC_ABS;
                double fcorte = Convert.ToDouble(Fcorte.Text.Trim());
                int distancia_movil = Movil.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(Movil.Text.Trim());
                double RYSQa = 0;
                double RYSQb = 0;
                double MY = 0;
                double MYc = 0;
                unespectro.Add(new EspectroAnalizado(grado, pro, corte, fcorte, distancia_movil, RYSQa, RYSQb, MY, MYc, 0, 0, contador, null, xd, yd, null, null, null, 0, 0));
                double ymin = double.MaxValue;
                double ymax = double.MinValue;
                for (int i = 0; i < unespectro[0].n; i++)
                {
                    ymin = Math.Min(ymin, unespectro[0].y[i]);
                    ymax = Math.Max(ymax, unespectro[0].y[i]);
                }
                panel_img.Dibuja(null, unespectro, 0, picos_mas, picos_menos, ymin, ymax, true);
            }
            Puntos.Text = string.Format("{0:N0}", contador);
            x = null;
            for (int i = 0; i < N_CAJAS; i++)
            {
                mejorcaja[i] = null;
            }
        }
        private void LeeFilaFicheroFte(string linea, int cRA, int cDEC, int decalajeW, int decalajeF, int cada)
        {
            double AR;
            double DEC;
            double vx;
            double vy;
            string[] ss = linea.Split(';');
            if (cRA != -1)
            {
                AR = Convert.ToDouble(ss[cRA]);
            }
            else
            {
                AR = 0;
            }
            if (cDEC != -1)
            {
                DEC = Convert.ToDouble(ss[cDEC]);
            }
            else
            {
                DEC = 0;
            }

            // Lee todas las parejas x,y en la fila y las guarda en 'datos'

            List<Dato> datos = new();
            int pares_xy = decalajeF - decalajeW;
            for (int i = 0; i < pares_xy; i++)
            {
                vx = Convert.ToDouble(ss[decalajeW + i]);
                vy = Convert.ToDouble(ss[decalajeF + i]);
                datos.Add(new Dato(i, vx, vy));
            }
            datosex = new DatoEx(AR, DEC, datos);
            datosex = ReducirDatos(datosex, cada);
            x = null;
        }
        private void Reducir_Click(object sender, EventArgs e)
        {
            if (datosex == null) return;
            IniciaTablaCoefcientes();
            R2.Text = string.Empty;
            F.Text = string.Empty;
            DE.Text = string.Empty;
            Corte.Text = string.Empty;
            picos_mas = new List<Dato>();
            picos_menos = new List<Dato>();
            Absorcion.Text = string.Empty;
            Emision.Text = string.Empty;
            Puntos.Text = string.Empty;
            tabla.RowCount = 0;
            Application.DoEvents();
            int cada = Convert.ToInt32(V_reducirCada.Text.Trim());
            datosex = ReducirDatos(datosex, cada);
            double[] xd = new double[datosex.datos.Count];
            double[] yd = new double[datosex.datos.Count];
            string[] fila = new string[3];
            int contador = 0;
            foreach (Dato d in datosex.datos)
            {
                fila[0] = contador.ToString();
                fila[1] = string.Format("{0:f2}", d.x);
                fila[2] = string.Format("{0:f2}", d.y);
                tabla.Rows.Add(fila);
                xd[contador] = d.x;
                yd[contador] = d.y;
                contador++;
            }
            Puntos.Text = string.Format("{0:N0}", contador);
            List<EspectroAnalizado> unespectro = new();
            int grado = Convert.ToInt32(Grado.Text.Trim());
            bool pro = Proporcional.Checked;
            double corte = Corte.Text.Trim().Length == 0 ? 0 : Convert.ToDouble(Corte.Text.Trim());
            if (Fcorte.Text.Trim().Length == 0) Fcorte.Text = Proporcional.Checked ? FC_PRO : FC_ABS;
            double fcorte = Convert.ToDouble(Fcorte.Text.Trim());
            int distancia_movil = Movil.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(Movil.Text.Trim());
            double RYSQa = 0;
            double RYSQb = 0;
            double MY = 0;
            double MYc = 0;
            unespectro.Add(new EspectroAnalizado(grado, pro, corte, fcorte, distancia_movil, RYSQa, RYSQb, MY, MYc, 0, 0, contador, null, xd, yd, null, null, null, 0, 0));
            picos_mas = null;
            picos_menos = null;
            double ymin = double.MaxValue;
            double ymax = double.MinValue;
            for (int i = 0; i < unespectro[0].n; i++)
            {
                ymin = Math.Min(ymin, unespectro[0].y[i]);
                ymax = Math.Max(ymax, unespectro[0].y[i]);
            }
            MuestraForm2();
            panel_img.Dibuja(null, unespectro, 0, picos_mas, picos_menos, ymin, ymax, true);
        }
        private static DatoEx ReducirDatos(DatoEx datosini, int cada)
        {
            if (cada > 1)
            {
                double vx;
                double vy;
                List<Dato> datos_tmp;
                datos_tmp = new List<Dato>();
                int hasta = datosini.datos.Count / cada;
                hasta *= cada;
                for (int i = 0; i < hasta; i += cada)
                {
                    vx = 0;
                    vy = 0;
                    for (int j = 0; j < cada; j++)
                    {
                        vx += datosini.datos[i + j].x;
                        vy += datosini.datos[i + j].y;
                    }
                    vx /= cada;
                    vy /= cada;
                    datos_tmp.Add(new Dato(i, vx, vy));
                }
                return new DatoEx(datosini.AR, datosini.DEC, datos_tmp);
            }
            return datosini;
        }
        private void Calcula_Click(object sender, EventArgs e)
        {
            if (datosex == null || datosex.datos == null || datosex.datos.Count == 0)
            {
                MessageBox.Show("No hay datos");
                return;
            }
            int grado = Convert.ToInt32(Grado.Text.Trim());
            w = new double[datosex.datos.Count];
            for (int i = 0; i < datosex.datos.Count; i++) w[i] = 1;
            if (!Regresion(linReg, grado))
            {
                MessageBox.Show("Error al efectuar la regresión");
                return;
            }
            double corte;
            if (Fcorte.Text.Trim().Length == 0) Fcorte.Text = Proporcional.Checked ? FC_PRO : FC_ABS;
            if (Proporcional.Checked)
            {
                corte = Convert.ToDouble(Fcorte.Text.Trim()) * linReg.SDVErra / (linReg.MY + linReg.MYc) * 2;
            }
            else
            {
                corte = Convert.ToDouble(Fcorte.Text.Trim()) * linReg.SDVErra;
            }
            if (x != null && x.GetLength(1) > 0) CalculaPicos(corte);
        }
        private void DatosXY(List<Dato> datos, int grado, int desde, int hasta)
        {
            y = new double[hasta - desde];
            int k = 0;
            for (int i = desde; i < hasta; i++)
            {
                y[k] = datos[i].y;
                k++;
            }
            DatosX(datos, grado, desde, hasta);
        }
        private void DatosX(List<Dato> datos, int grado, int desde, int hasta)
        {
            int terminos = grado + 1;
            x = new double[terminos, hasta - desde];
            double xi;
            double xp;
            int k = 0;
            for (int i = desde; i < hasta; i++)
            {
                x[0, k] = 1;
                xi = xp = datos[i].x;
                for (int j = 1; j < terminos; j++)
                {
                    x[j, k] = xp;
                    xp *= xi;
                }
                k++;
            }
        }
        private double[] FraccionaW(int desde, int hasta)
        {
            double[] wf = new double[hasta - desde];
            int k = 0;
            for (int i = desde; i < hasta; i++)
            {
                wf[k] = w[i];
                k++;
            }
            return wf;
        }
        private void Soldar(RegresionLineal linRegFteA, RegresionLineal linRegFteB, RegresionLineal linRegDestino, int desdeA, int hastaA, int desdeB, int hastaB)
        {
            // Unir las dos regresiones A y B

            int primer_compartido = desdeB;
            int ultimo_compartido = hastaA;
            double numero_compartidos = ultimo_compartido - primer_compartido;
            int terminos_polinomio = x.GetLength(0);
            linRegDestino.CA = new double[terminos_polinomio];
            linRegDestino.CB = new double[terminos_polinomio];
            linRegDestino.Ycalc = new double[hastaB];
            linRegDestino.SDVMovil = new double[hastaB];

            // Datos de A

            int k = 0;
            for (int i = desdeA; i < primer_compartido; i++)
            {
                linRegDestino.Ycalc[k] = linRegFteA.Ycalc[k];
                linRegDestino.SDVMovil[k] = linRegFteA.SDVMovil[k];
                k++;
            }
            linRegDestino.RYSQa = linRegFteA.RYSQa;
            linRegDestino.FRega = linRegFteA.FRega;
            linRegDestino.SDVErra = linRegFteA.SDVErra;
            for (int i = 0; i < terminos_polinomio; i++)
            {
                linRegDestino.CA[i] = linRegFteA.CA[i];
            }

            // Para los datos compartidos se pondera con peso variable de 0 (primer dato compartido por B) a 1 (último dato compartido por B), de forma lineal

            double pondera_b;
            int kb = 0;
            for (int i = primer_compartido; i < ultimo_compartido; i++)
            {
                pondera_b = kb / numero_compartidos;
                linRegDestino.Ycalc[k] = (1 - pondera_b) * linRegFteA.Ycalc[k] + pondera_b * linRegFteB.Ycalc[kb];
                linRegDestino.SDVMovil[k] = (linRegFteA.SDVMovil[k] + linRegFteB.SDVMovil[kb]) / 2;
                k++;
                kb++;
            }

            // Datos de B

            for (int i = ultimo_compartido; i < hastaB; i++)
            {
                linRegDestino.Ycalc[k] = linRegFteB.Ycalc[kb];
                linRegDestino.SDVMovil[k] = linRegFteB.SDVMovil[kb];
                k++;
                kb++;
            }
            linRegDestino.RYSQb = linRegFteB.RYSQa;
            linRegDestino.FRegb = linRegFteB.FRega;
            linRegDestino.SDVErrb = linRegFteB.SDVErra;
            for (int i = 0; i < terminos_polinomio; i++)
            {
                linRegDestino.CB[i] = linRegFteB.CA[i];
            }

            // Estadísticas del ajuste fusionado

            int distancia_movil = Movil.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(Movil.Text.Trim());
            linRegDestino.Estadisticas(y, x, w, distancia_movil);
        }
        private bool Regresion(RegresionLineal rl, int grado)
        {
            int distancia_movil = Movil.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(Movil.Text.Trim());
            if (Partir.Checked)
            {
                // Realizar dos ajustes, ambos con 2/3 de los datos:
                //  1. desde el dato 0 hasta el dato 2/3 del total
                //  2. desde el dato 1/3 hasta el último dato

                int desde_a = 0;
                int hasta_a = (2 * datosex.datos.Count) / 3;
                DatosXY(datosex.datos, grado, desde_a, hasta_a);
                RegresionLineal rlA = new RegresionLineal();
                if (!rlA.Regress(y, x, FraccionaW(desde_a, hasta_a), distancia_movil))
                {
                    return false;
                }
                int desde_b = datosex.datos.Count / 3;
                int hasta_b = datosex.datos.Count;
                DatosXY(datosex.datos, grado, desde_b, hasta_b);
                RegresionLineal rlB = new RegresionLineal();
                if (!rlB.Regress(y, x, FraccionaW(desde_b, hasta_b), distancia_movil))
                {
                    return false;
                }

                // Fusionar los dos ajustes.

                DatosXY(datosex.datos, grado, 0, datosex.datos.Count);
                Soldar(rlA, rlB, linReg, desde_a, hasta_a, desde_b, hasta_b);
            }
            else
            {
                // Un sólo ajuste con todos los datos

                DatosXY(datosex.datos, grado, 0, datosex.datos.Count);
                if (!rl.Regress(y, x, w, distancia_movil))
                {
                    return false;
                }
            }
            if (!evitar)
            {
                IniciaTablaCoefcientes();
                string[] fila = new string[3];
                if (Partir.Checked)
                {
                    for (int i = 0; i < x.GetLength(0); i++)
                    {
                        fila[0] = i.ToString();
                        fila[1] = rl.CA[i].ToString();
                        fila[2] = rl.CB[i].ToString();
                        Coeficientes.Rows.Add(fila);
                    }
                }
                else
                {
                    fila[2] = string.Empty;
                    for (int i = 0; i < x.GetLength(0); i++)
                    {
                        fila[0] = i.ToString();
                        fila[1] = rl.CA[i].ToString();
                        Coeficientes.Rows.Add(fila);
                    }
                }
                R2.Text = string.Format("{0:f6}", Math.Min(rl.RYSQa, rl.RYSQb));
                F.Text = Math.Max(rl.FRega, rl.FRegb).ToString();
                DE.Text = Math.Max(rl.SDVErra, rl.SDVErrb).ToString();
            }
            return true;
        }
        private void CalculaPicos(double corte)
        {
            if (!evitar)
            {
                Absorcion.Text = string.Empty;
                Emision.Text = string.Empty;
            }
            picos_mas = new List<Dato>();
            picos_menos = new List<Dato>();
            int distancia_movil;
            if (Movil.Text.Trim().Length == 0)
            {
                distancia_movil = 0;
                if (!evitar) Corte.Text = string.Format(Proporcional.Checked ? "{0:f5}" : "{0:f1}", corte);
            }
            else
            {
                distancia_movil = Convert.ToInt32(Movil.Text.Trim());
                if (!evitar) Corte.Text = string.Empty;
            }
            if (Fcorte.Text.Trim().Length == 0) Fcorte.Text = Proporcional.Checked ? FC_PRO : FC_ABS;
            double fcorte = Convert.ToDouble(Fcorte.Text.Trim());
            double margen = corte;
            for (int i = 0; i < x.GetLength(1); i++)
            {
                if (distancia_movil > 0)
                {
                    if (Proporcional.Checked)
                    {
                        margen = fcorte * linReg.SDVMovil[i] * 2 / (linReg.MY + linReg.MYc) * linReg.Ycalc[i];
                    }
                    else
                    {
                        margen = fcorte * linReg.SDVMovil[i];
                    }
                }
                else
                {
                    if (Proporcional.Checked)
                    {
                        margen = corte * linReg.Ycalc[i];
                    }
                }
                if (y[i] - linReg.Ycalc[i] > margen)
                {
                    picos_mas.Add(new Dato(i, x[1, i], y[i]));
                }
                else if (linReg.Ycalc[i] - y[i] > margen)
                {
                    picos_menos.Add(new Dato(i, x[1, i], y[i]));
                }
            }
            if (!evitar)
            {
                List<EspectroAnalizado> unespectro = new();
                int grado = Convert.ToInt32(Grado.Text.Trim());
                bool pro = Proporcional.Checked;
                double RYSQa = 0;
                double RYSQb = 0;
                double MY = 0;
                double MYc = 0;
                int n = x.GetLength(1);
                double[] xd = new double[n];
                for (int i = 0; i < n; i++) xd[i] = x[1, i];
                unespectro.Add(new EspectroAnalizado(grado, pro, corte, fcorte, distancia_movil, RYSQa, RYSQb, MY, MYc, 0, 0, n, null, xd, y, linReg.Ycalc, linReg.SDVMovil, null, 0, 0));
                double ymin = double.MaxValue;
                double ymax = double.MinValue;
                for (int i = 0; i < unespectro[0].n; i++)
                {
                    ymin = Math.Min(ymin, unespectro[0].y[i]);
                    ymax = Math.Max(ymax, unespectro[0].y[i]);
                }
                MuestraForm2();
                panel_img.Dibuja(null, unespectro, 0, picos_mas, picos_menos, ymin, ymax, true);
                Absorcion.Text = string.Format("{0:N0}", picos_menos.Count);
                Emision.Text = string.Format("{0:N0}", picos_mas.Count);
            }
        }
        private void CalculaPicos(int ind_cluster, int ind_espectro)
        {
            picos_mas = new List<Dato>();
            picos_menos = new List<Dato>();
            Absorcion.Text = string.Empty;
            Emision.Text = string.Empty;
            double margen = espectros[ind_espectro].corte;
            for (int i = 0; i < espectros[ind_espectro].n; i++)
            {
                if (espectros[ind_espectro].distancia_movil > 0)
                {
                    if (Proporcional.Checked)
                    {
                        margen = espectros[ind_espectro].fcorte * espectros[ind_espectro].SDVMovil[i] * 2 / (espectros[ind_espectro].MY + espectros[ind_espectro].MYc) * espectros[ind_espectro].yc[i];
                    }
                    else
                    {
                        margen = espectros[ind_espectro].fcorte * espectros[ind_espectro].SDVMovil[i];
                    }
                }
                else
                {
                    if (Proporcional.Checked)
                    {
                        margen = espectros[ind_espectro].corte * espectros[ind_espectro].yc[i];
                    }
                }
                if (espectros[ind_espectro].y[i] - espectros[ind_espectro].yc[i] > margen)
                {
                    picos_mas.Add(new Dato(i, espectros[ind_espectro].x[i], espectros[ind_espectro].y[i]));
                }
                else if (espectros[ind_espectro].yc[i] - espectros[ind_espectro].y[i] > margen)
                {
                    picos_menos.Add(new Dato(i, espectros[ind_espectro].x[i], espectros[ind_espectro].y[i]));
                }
            }
            int mejor = MejorCajaSeleccionada();
            double ymin = double.MaxValue;
            double ymax = double.MinValue;
            for (int i = 0; i < espectros[ind_espectro].n; i++)
            {
                ymin = Math.Min(ymin, mejorcaja[mejor].centroides[ind_cluster][i]);
                ymax = Math.Max(ymax, mejorcaja[mejor].centroides[ind_cluster][i]);
                ymin = Math.Min(ymin, espectros[ind_espectro].yn[i]);
                ymax = Math.Max(ymax, espectros[ind_espectro].yn[i]);
                if (V_dibujaTodo.Checked)
                {
                    ymin = Math.Min(ymin, espectros[ind_espectro].y[i]);
                    ymax = Math.Max(ymax, espectros[ind_espectro].y[i]);
                }
            }
            MuestraForm2();
            panel_img.Dibuja(mejorcaja[mejor].centroides[ind_cluster], espectros, ind_espectro, picos_mas, picos_menos, ymin, ymax, V_dibujaTodo.Checked);
            Absorcion.Text = string.Format("{0:N0}", picos_menos.Count);
            Emision.Text = string.Format("{0:N0}", picos_mas.Count);
        }
        private void SelPicos_Click(object sender, EventArgs e)
        {
            double corte = Corte.Text.Trim().Length == 0 ? 0 : Convert.ToDouble(Corte.Text.Trim());
            if (Fcorte.Text.Trim().Length == 0) Fcorte.Text = Proporcional.Checked ? FC_PRO : FC_ABS;
            if (x != null && x.GetLength(1) > 0) CalculaPicos(corte);
        }
        private void Recalcula_Click(object sender, EventArgs e)
        {
            if (datosex == null || datosex.datos == null || datosex.datos.Count == 0 || x == null || x.GetLength(1) == 0)
            {
                MessageBox.Show("No hay datos");
                return;
            }

            // Como hay un cálculo previo las matrices 'x[,] y[]' ya están cargadas

            int distancia_movil = Movil.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(Movil.Text.Trim());
            if (Fcorte.Text.Trim().Length == 0) Fcorte.Text = Proporcional.Checked ? FC_PRO : FC_ABS;
            double fcorte = Convert.ToDouble(Fcorte.Text.Trim());
            double corte;
            if (Proporcional.Checked)
            {
                corte = fcorte * linReg.SDVErra / (linReg.MY + linReg.MYc) * 2;
            }
            else
            {
                corte = fcorte * linReg.SDVErra;
            }
            int grado = x.GetLength(0) - 1;

            // Regresión excluyendo los picos

            AnulaPicos();
            if (!Regresion(linReg, grado))
            {
                MessageBox.Show("Error al efectuar la regresión");
                return;
            }
            if (distancia_movil == 0)
            {
                if (Proporcional.Checked)
                {
                    corte = fcorte * linReg.SDVErra / (linReg.MY + linReg.MYc) * 2;
                }
                else
                {
                    corte = fcorte * linReg.SDVErra;
                }
            }
            if (x != null && x.GetLength(1) > 0)
            {
                CalculaPicos(corte);
            }
            else
            {
                Corte.Text = string.Format(Proporcional.Checked ? "{0:f5}" : "{0:f1}", corte);
            }
        }
        private void AnulaPicos()
        {
            w = new double[x.GetLength(1)];
            for (int i = 0; i < x.GetLength(1); i++) w[i] = 1;
            foreach (Dato d in picos_mas) w[d.n] = 0;
            foreach (Dato d in picos_menos) w[d.n] = 0;
        }
        private void AnulaPicos(int distancia_movil, double corte, double fcorte)
        {
            if (!evitar)
            {
                Absorcion.Text = string.Empty;
                Emision.Text = string.Empty;
            }
            double margen = corte;
            int pmas = 0;
            int pmenos = 0;
            w = new double[x.GetLength(1)];
            for (int i = 0; i < x.GetLength(1); i++)
            {
                if (distancia_movil > 0)
                {
                    if (Proporcional.Checked)
                    {
                        margen = fcorte * linReg.SDVMovil[i] * 2 / (linReg.MY + linReg.MYc) * linReg.Ycalc[i];
                    }
                    else
                    {
                        margen = fcorte * linReg.SDVMovil[i];
                    }
                }
                else
                {
                    if (Proporcional.Checked)
                    {
                        margen = corte * linReg.Ycalc[i];
                    }
                }
                if (y[i] - linReg.Ycalc[i] > margen)
                {
                    pmas++;
                    w[i] = 0;
                }
                else if (linReg.Ycalc[i] - y[i] > margen)
                {
                    pmenos++;
                    w[i] = 0;
                }
                else
                {
                    w[i] = 1;
                }
            }
            if (!evitar)
            {
                Absorcion.Text = string.Format("{0:N0}", pmenos);
                Emision.Text = string.Format("{0:N0}", pmas);
            }
        }
        private void SalvaImagen_Click(object sender, EventArgs e)
        {
            if (panel_img == null || panel_img.img == null)
            {
                MessageBox.Show("No hay una imagen que salvar", "Img", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (panel_img.img == null) return;
            SaveFileDialog ficheroescritura = new()
            {
                Filter = "PNG |*.png;*.bmp|TODO |*.*",
                FilterIndex = 1
            };
            if (ficheroescritura.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    panel_img.img.Save(ficheroescritura.FileName, ImageFormat.Png);
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message, "Img", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Console.Beep();
            }
        }
        private void SalvaPicos_Click(object sender, EventArgs e)
        {
            if (x == null || x.GetLength(1) == 0) return;
            if (picos_mas == null || picos_menos == null || picos_mas.Count + picos_menos.Count == 0) return;
            SaveFileDialog ficheroescritura = new()
            {
                Filter = "CSV |*.csv|TODO |*.*",
                FilterIndex = 1
            };
            if (ficheroescritura.ShowDialog() == DialogResult.OK)
            {
                FileStream fsfc = new(ficheroescritura.FileName, FileMode.Create, FileAccess.Write, FileShare.Read);
                if (fsfc != null)
                {
                    StreamWriter sw = new StreamWriter(fsfc);
                    sw.WriteLine("Tipo;W;F;FC");
                    SalvaFila(sw);
                    sw.Close();
                    Console.Beep();
                }
            }
        }
        private void SalvaFila(StreamWriter sw)
        {
            int ipmas = 0;
            int ipmenos = 0;
            for (int i = 0; i < x.GetLength(1); i++)
            {
                if (ipmas < picos_mas.Count && i == picos_mas[ipmas].n)
                {
                    sw.WriteLine(string.Format("{0};{1};{2};{3}", 1, x[1, i], y[i], linReg.Ycalc[i]));
                    ipmas++;
                }
                if (ipmenos < picos_menos.Count && i == picos_menos[ipmenos].n)
                {
                    sw.WriteLine(string.Format("{0};{1};{2};{3}", 2, x[1, i], y[i], linReg.Ycalc[i]));
                    ipmenos++;
                }
                else
                {
                    sw.WriteLine(string.Format("{0};{1};{2};{3}", 0, x[1, i], y[i], linReg.Ycalc[i]));
                }
            }
        }
        private void FilaFichero_ValueChanged(object sender, EventArgs e)
        {
            if (!evitar) Habilitar(false);
            LeeDesdeFicheroFte();
            if (!evitar) Habilitar(true);
        }
        private void Todo_Click(object sender, EventArgs e)
        {
            SaveFileDialog ficheroescritura = new SaveFileDialog()
            {
                Filter = "CSV |*.csv|TODO |*.*",
                FilterIndex = 1
            };
            if (ficheroescritura.ShowDialog() == DialogResult.OK)
            {
                FileStream fisal = new FileStream(ficheroescritura.FileName, FileMode.Create, FileAccess.Write, FileShare.Read);
                if (fisal != null)
                {
                    StreamWriter sw = new StreamWriter(fisal);
                    string fichero = string.Format(@"{0}\{1}_des.{2}", Path.GetDirectoryName(ficheroescritura.FileName), Path.GetFileNameWithoutExtension(ficheroescritura.FileName), Path.GetExtension(ficheroescritura.FileName));
                    FileStream fisal_des = new FileStream(fichero, FileMode.Create, FileAccess.Write, FileShare.Read);
                    StreamWriter sw_des = new StreamWriter(fisal_des);
                    fichero = string.Format(@"{0}\{1}_smax.{2}", Path.GetDirectoryName(ficheroescritura.FileName), Path.GetFileNameWithoutExtension(ficheroescritura.FileName), Path.GetExtension(ficheroescritura.FileName));
                    FileStream fisal_smax = new FileStream(fichero, FileMode.Create, FileAccess.Write, FileShare.Read);
                    StreamWriter sw_smax = new StreamWriter(fisal_smax);
                    MuestraForm2();
                    int distancia_movil = Movil.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(Movil.Text.Trim());
                    if (Fcorte.Text.Trim().Length == 0) Fcorte.Text = Proporcional.Checked ? FC_PRO : FC_ABS;
                    evitar = true;
                    Habilitar(false);
                    cancelar = false;
                    Esc.Enabled = true;
                    Esc.Cursor = Cursors.Default;
                    Cursor = Cursors.Default;
                    IniciaTablaCoefcientes();
                    F.Text = string.Empty;
                    DE.Text = string.Empty;
                    Corte.Text = string.Empty;
                    picos_mas = new List<Dato>();
                    picos_menos = new List<Dato>();
                    Absorcion.Text = string.Empty;
                    Emision.Text = string.Empty;
                    Puntos.Text = string.Empty;
                    tabla.RowCount = 0;
                    tabla.Refresh();
                    if (R2.Text.Trim().Length == 0)
                    {
                        R2.Text = "0";
                    }
                    else
                    {
                        R2.Text = R2.Text.Replace('.', ',');
                    }
                    double R2_min_aceptable = Convert.ToDouble(R2.Text.Trim());
                    Application.DoEvents();

                    double R2_min = double.MaxValue;
                    double R2_max = double.MinValue;
                    double R2_med = 0;

                    double FMax;
                    double LOMax;
                    double LOMax_min = double.MaxValue;
                    double LOMax_max = double.MinValue;
                    double LOMax_med = 0;

                    double corte = 0;
                    if (Fcorte.Text.Trim().Length == 0) Fcorte.Text = Proporcional.Checked ? FC_PRO : FC_ABS;
                    double fcorte = Convert.ToDouble(Fcorte.Text.Trim());
                    int grado;
                    int grado_ini = Convert.ToInt32(Grado.Text.Trim());

                    FileStream ffte = new FileStream(R_fichero.Text, FileMode.Open, FileAccess.Read, FileShare.Read);
                    StreamReader sr = new StreamReader(ffte);

                    // Cabecera

                    int decalajeW = -1;
                    int decalajeF = -1;
                    int cRA = -1;
                    int cDEC = -1;
                    string linea = sr.ReadLine();
                    string[] ss = linea.Split(';');
                    for (int i = 0; i < ss.Length; i++)
                    {
                        if (ss[i].StartsWith("WAVE_", StringComparison.OrdinalIgnoreCase))
                        {
                            decalajeW = i;
                            break;
                        }
                    }
                    if (decalajeW == -1)
                    {
                        MessageBox.Show(string.Format("El fichero {0} no contiene columna W", R_fichero.Text), "Leer fichero de espectros fuente");
                        return;
                    }
                    for (int i = 0; i < ss.Length; i++)
                    {
                        if (ss[i].StartsWith("FLUX_", StringComparison.OrdinalIgnoreCase))
                        {
                            decalajeF = i;
                            break;
                        }
                    }
                    if (decalajeF == -1)
                    {
                        MessageBox.Show(string.Format("El fichero {0} no contiene columna F", R_fichero.Text), "Leer fichero de espectros fuente");
                        return;
                    }
                    for (int i = 0; i < ss.Length; i++)
                    {
                        if (ss[i].Equals("RA", StringComparison.OrdinalIgnoreCase))
                        {
                            cRA = i;
                            break;
                        }
                    }
                    if (cRA == -1)
                    {
                        for (int i = 0; i < ss.Length; i++)
                        {
                            if (ss[i].Equals("OBJRA", StringComparison.OrdinalIgnoreCase))
                            {
                                cRA = i;
                                break;
                            }
                        }
                    }
                    for (int i = 0; i < ss.Length; i++)
                    {
                        if (ss[i].Equals("DEC", StringComparison.OrdinalIgnoreCase))
                        {
                            cDEC = i;
                            break;
                        }
                    }
                    if (cDEC == -1)
                    {
                        for (int i = 0; i < ss.Length; i++)
                        {
                            if (ss[i].Equals("OBJDEC", StringComparison.OrdinalIgnoreCase))
                            {
                                cDEC = i;
                                break;
                            }
                        }
                    }
                    int ipmas;
                    int ipmenos;
                    int contador = 0;
                    int aceptados = 0;
                    int descartados = 0;
                    int descartados_r2 = 0;
                    int descartados_resto = 0;
                    int ul_max;
                    int sin_max = 0;
                    int cada = Convert.ToInt32(V_reducirCada.Text.Trim());
                    R_filas.Text = string.Format("{0:N0}", 0);
                    Application.DoEvents();
                    while (!sr.EndOfStream)
                    {
                        LeeFilaFicheroFte(sr.ReadLine(), cRA, cDEC, decalajeW, decalajeF, cada);

                        // Realiza la primera regresión

                        grado = grado_ini;
                        w = new double[datosex.datos.Count];
                        for (int i = 0; i < datosex.datos.Count; i++) w[i] = 1;
                        if (!Regresion(linReg, grado))
                        {
                            MessageBox.Show("Error al efectuar la regresión");
                            return;
                        }
                        if (distancia_movil == 0)
                        {
                            corte = Proporcional.Checked ? fcorte * linReg.SDVErra / (linReg.MY + linReg.MYc) * 2 : fcorte * linReg.SDVErra;
                        }
                        if (Recalcular.Checked)
                        {
                            // Realiza una segunda regresión anulando los picos de la primera regresión

                            AnulaPicos(distancia_movil, corte, fcorte);
                            if (!Regresion(linReg, grado))
                            {
                                MessageBox.Show("Error al efectuar la regresión");
                                return;
                            }
                        }

                        // Realiza nuevas regresiones, aumentando el grado, mientras R2 mejore (w no se toca)

                        double r2;
                        do
                        {
                            r2 = Math.Min(linReg.RYSQa, linReg.RYSQb);
                            grado++;
                            if (!Regresion(linReg, grado))
                            {
                                MessageBox.Show("Error al efectuar la regresión");
                                return;
                            }
                        } while (Math.Min(linReg.RYSQa, linReg.RYSQb) > r2 && grado < MAX_GRADO);
                        if (Math.Min(linReg.RYSQa, linReg.RYSQb) < r2)
                        {
                            // Retroceder un grado

                            grado--;
                            if (!Regresion(linReg, grado))
                            {
                                MessageBox.Show("Error al efectuar la regresión");
                                return;
                            }
                        }
                        if (distancia_movil == 0)
                        {
                            corte = Proporcional.Checked ? fcorte * linReg.SDVErra / (linReg.MY + linReg.MYc) * 2 : fcorte * linReg.SDVErra;
                        }
                        if (Recalcular.Checked)
                        {
                            // Realiza una última regresión anulando los picos conforme a la regresión anterior

                            AnulaPicos(distancia_movil, corte, fcorte);
                            if (!Regresion(linReg, grado))
                            {
                                MessageBox.Show("Error al efectuar la regresión");
                                return;
                            }
                            if (Math.Min(linReg.RYSQa, linReg.RYSQb) < r2)
                            {
                                // Hay que retroceder un grado

                                grado--;
                                if (!Regresion(linReg, grado))
                                {
                                    MessageBox.Show("Error al efectuar la regresión");
                                    return;
                                }
                            }
                        }
                        if (distancia_movil == 0)
                        {
                            corte = Proporcional.Checked ? fcorte * linReg.SDVErra / (linReg.MY + linReg.MYc) * 2 : fcorte * linReg.SDVErra;
                        }
                        CalculaPicos(corte);
                        FMax = double.MinValue;
                        LOMax = 0;
                        ul_max = x.GetLength(1) - 20;
                        for (int i = 0; i < x.GetLength(1); i++)
                        {
                            if (FMax < linReg.Ycalc[i])
                            {
                                FMax = linReg.Ycalc[i];
                                if (i < 20)
                                {
                                    // El máximo debe estar en el UV

                                    LOMax = -2;
                                }
                                else if (i >= ul_max)
                                {
                                    // El máximo debe estar en el IR

                                    LOMax = -1;
                                }
                                else
                                {
                                    LOMax = x[1, i];
                                }
                            }
                        }
                        if (LOMax > 0)
                        {
                            LOMax_min = Math.Min(LOMax_min, LOMax);
                            LOMax_max = Math.Max(LOMax_max, LOMax);
                            LOMax_med += LOMax;
                        }
                        else
                        {
                            sin_max++;
                        }
                        if (Math.Min(linReg.RYSQa, linReg.RYSQb) >= R2_min_aceptable && (!ExcluirSinMaximo.Checked || LOMax > 0))
                        {
                            // Salva el espectro analizado

                            aceptados++;
                            if (contador == 0)
                            {
                                // Cabecera

                                sw.Write("Num;n;g;");
                                for (int i = 0; i < MAX_GRADOM1; i++) sw.Write(string.Format("c{0}a;", i));
                                for (int i = 0; i < MAX_GRADOM1; i++) sw.Write(string.Format("c{0}b;", i));
                                sw.Write("R2a;R2b;pro;corte;fcorte;smv;My;Myc;p-;p+;AR;DEC");
                                for (int i = 0; i < x.GetLength(1); i++) sw.Write(string.Format(";tp{0};x{0};y{0};yc{0};sdv{0}", i));
                                sw.WriteLine();
                            }
                            ipmas = 0;
                            ipmenos = 0;
                            sw.Write(string.Format("{0};{1};{2};", contador, datosex.datos.Count, grado));
                            for (int i = 0; i <= grado; i++)
                            {
                                sw.Write(string.Format("{0};", linReg.CA[i]));
                            }
                            for (int i = grado + 1; i < MAX_GRADOM1; i++)
                            {
                                sw.Write(string.Format("{0};", 0));
                            }
                            for (int i = 0; i <= grado; i++)
                            {
                                sw.Write(string.Format("{0};", linReg.CB[i]));
                            }
                            for (int i = grado + 1; i < MAX_GRADOM1; i++)
                            {
                                sw.Write(string.Format("{0};", 0));
                            }
                            sw.Write(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11}", linReg.RYSQa, linReg.RYSQb, Proporcional.Checked ? 1 : 0, corte, fcorte, distancia_movil, linReg.MY, linReg.MYc, picos_menos.Count, picos_mas.Count, datosex.AR, datosex.DEC));
                            for (int i = 0; i < x.GetLength(1); i++)
                            {
                                if (ipmas < picos_mas.Count && i == picos_mas[ipmas].n)
                                {
                                    // Emisión

                                    sw.Write(string.Format(";{0};{1};{2};{3};{4}", 1, x[1, i], y[i], linReg.Ycalc[i], linReg.SDVMovil[i]));
                                    ipmas++;
                                }
                                else if (ipmenos < picos_menos.Count && i == picos_menos[ipmenos].n)
                                {
                                    // Absorción

                                    sw.Write(string.Format(";{0};{1};{2};{3};{4}", 2, x[1, i], y[i], linReg.Ycalc[i], linReg.SDVMovil[i]));
                                    ipmenos++;
                                }
                                else
                                {
                                    sw.Write(string.Format(";{0};{1};{2};{3};{4}", 0, x[1, i], y[i], linReg.Ycalc[i], linReg.SDVMovil[i]));
                                }
                            }
                            sw.WriteLine(string.Format(";{0}", LOMax));
                            R2_min = Math.Min(Math.Min(linReg.RYSQa, linReg.RYSQb), R2_min);
                            R2_max = Math.Max(Math.Min(linReg.RYSQa, linReg.RYSQb), R2_max);
                            R2_med += Math.Min(linReg.RYSQa, linReg.RYSQb);
                        }
                        else
                        {
                            descartados++;
                            if (Math.Min(linReg.RYSQa, linReg.RYSQb) < R2_min_aceptable && (!ExcluirSinMaximo.Checked || LOMax > 0))
                            {
                                // Se salvan los espectros descartados por R2 en otro fichero

                                if (descartados_r2 == 0)
                                {
                                    // Cabecera

                                    sw_des.Write("Num;n;g;");
                                    for (int i = 0; i < MAX_GRADOM1; i++) sw_des.Write(string.Format("c{0}a;", i));
                                    for (int i = 0; i < MAX_GRADOM1; i++) sw_des.Write(string.Format("c{0}b;", i));
                                    sw_des.Write("R2a;R2b;pro;corte;fcorte;smv;My;Myc;p-;p+;AR;DEC");
                                    for (int i = 0; i < x.GetLength(1); i++) sw_des.Write(string.Format(";tp{0};x{0};y{0};yc{0};sdv{0}", i));
                                    sw_des.WriteLine();
                                }
                                descartados_r2++;
                                ipmas = 0;
                                ipmenos = 0;
                                sw_des.Write(string.Format("{0};{1};{2};", contador, datosex.datos.Count, grado));
                                for (int i = 0; i <= grado; i++)
                                {
                                    sw_des.Write(string.Format("{0};", linReg.CA[i]));
                                }
                                for (int i = grado + 1; i < MAX_GRADOM1; i++)
                                {
                                    sw_des.Write(string.Format("{0};", 0));
                                }
                                sw_des.Write(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11}", linReg.RYSQa, linReg.RYSQb, Proporcional.Checked ? 1 : 0, corte, fcorte, distancia_movil, linReg.MY, linReg.MYc, picos_menos.Count, picos_mas.Count, datosex.AR, datosex.DEC));
                                for (int i = 0; i < x.GetLength(1); i++)
                                {
                                    if (ipmas < picos_mas.Count && i == picos_mas[ipmas].n)
                                    {
                                        // Emisión

                                        sw_des.Write(string.Format(";{0};{1};{2};{3};{4}", 1, x[1, i], y[i], linReg.Ycalc[i], linReg.SDVMovil[i]));
                                        ipmas++;
                                    }
                                    else if (ipmenos < picos_menos.Count && i == picos_menos[ipmenos].n)
                                    {
                                        // Absorción

                                        sw_des.Write(string.Format(";{0};{1};{2};{3};{4}", 2, x[1, i], y[i], linReg.Ycalc[i], linReg.SDVMovil[i]));
                                        ipmenos++;
                                    }
                                    else
                                    {
                                        sw_des.Write(string.Format(";{0};{1};{2};{3};{4}", 0, x[1, i], y[i], linReg.Ycalc[i], linReg.SDVMovil[i]));
                                    }
                                }
                                sw_des.WriteLine(string.Format(";{0}", LOMax));
                            }
                            else
                            {
                                // Se salvan el resto de espectros descartados en otro fichero

                                if (descartados_resto == 0)
                                {
                                    // Cabecera

                                    sw_smax.Write("Num;n;g;");
                                    for (int i = 0; i < MAX_GRADOM1; i++) sw_smax.Write(string.Format("c{0}a;", i));
                                    for (int i = 0; i < MAX_GRADOM1; i++) sw_smax.Write(string.Format("c{0}b;", i));
                                    sw_smax.Write("R2a;R2b;pro;corte;fcorte;smv;My;Myc;p-;p+;AR;DEC");
                                    for (int i = 0; i < x.GetLength(1); i++) sw_smax.Write(string.Format(";tp{0};x{0};y{0};yc{0};sdv{0}", i));
                                    sw_smax.WriteLine();
                                }
                                descartados_resto++;
                                ipmas = 0;
                                ipmenos = 0;
                                sw_smax.Write(string.Format("{0};{1};{2};", contador, datosex.datos.Count, grado));
                                for (int i = 0; i <= grado; i++)
                                {
                                    sw_smax.Write(string.Format("{0};", linReg.CA[i]));
                                }
                                for (int i = grado + 1; i < MAX_GRADOM1; i++)
                                {
                                    sw_smax.Write(string.Format("{0};", 0));
                                }
                                sw_smax.Write(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11}", linReg.RYSQa, linReg.RYSQb, Proporcional.Checked ? 1 : 0, corte, fcorte, distancia_movil, linReg.MY, linReg.MYc, picos_menos.Count, picos_mas.Count, datosex.AR, datosex.DEC));
                                for (int i = 0; i < x.GetLength(1); i++)
                                {
                                    if (ipmas < picos_mas.Count && i == picos_mas[ipmas].n)
                                    {
                                        // Emisión

                                        sw_smax.Write(string.Format(";{0};{1};{2};{3};{4}", 1, x[1, i], y[i], linReg.Ycalc[i], linReg.SDVMovil[i]));
                                        ipmas++;
                                    }
                                    else if (ipmenos < picos_menos.Count && i == picos_menos[ipmenos].n)
                                    {
                                        // Absorción

                                        sw_smax.Write(string.Format(";{0};{1};{2};{3};{4}", 2, x[1, i], y[i], linReg.Ycalc[i], linReg.SDVMovil[i]));
                                        ipmenos++;
                                    }
                                    else
                                    {
                                        sw_smax.Write(string.Format(";{0};{1};{2};{3};{4}", 0, x[1, i], y[i], linReg.Ycalc[i], linReg.SDVMovil[i]));
                                    }
                                }
                                sw_smax.WriteLine(string.Format(";{0}", LOMax));
                            }
                        }
                        contador++;
                        if (contador % 10 == 0)
                        {
                            R_filas.Text = string.Format("{0:N0}", contador);
                            Application.DoEvents();
                        }
                        if (cancelar) break;
                    }
                    sr.Close();
                    sw.Close();
                    sw_des.Close();
                    sw_smax.Close();
                    if (aceptados > 0)
                    {
                        R2_med /= aceptados;
                        LOMax_med /= aceptados;
                    }
                    else
                    {
                        R2_med = 0;
                        LOMax_med = 0;
                    }
                    R_filas.Text = string.Format("{0:N0}", contador);
                    FilaFichero.Value = 1;  // Asegura que se ejecuta el event ValueChanged
                    evitar = false;
                    FilaFichero.Value = contador;
                    R2.Text = string.Format("{0}", R2_min_aceptable);
                    string res = string.Format("R2minAcep: {0:f4}\nR2min: {1:f4} R2max: {2:f4} R2med: {3:f4}\nSin Max:{4:N0} LOMmin: {5:f1} LOMmax: {6:f1} LOMmed: {7:f1}\nAceptados: {8:N0} de {9:N0}\nTotal descartados {10:N0}; Por R2 {11:N0}", R2_min_aceptable, R2_min, R2_max, R2_med, sin_max, LOMax_min, LOMax_max, LOMax_med, aceptados, contador, descartados, descartados_r2);
                    Clipboard.SetText(res);
                    Console.Beep();
                    MessageBox.Show(res);
                    Habilitar(true);
                    cancelar = false;
                    Esc.Enabled = false;
                    Esc.Cursor = Cursors.No;
                }
            }
        }
        private void Esc_Click(object sender, EventArgs e)
        {
            cancelar = true;
            Esc.Enabled = false;
            Esc.Cursor = Cursors.No;
            Application.DoEvents();
        }
        private void Proporcional_CheckedChanged(object sender, EventArgs e)
        {
            if (Proporcional.Checked)
            {
                R_corte.Text = "p.u.";
            }
            else
            {
                R_corte.Text = string.Empty;
            }
        }
        private void CreaClusters_Click(object sender, EventArgs e)
        {
            Clasifica();
        }
        private void Clasifica()
        {
            if (espectros == null || espectros.Count == 0)
            {
                MessageBox.Show("No hay datos de espectros analizados");
                return;
            }
            if (V_pases.Text.Trim().Length == 0) V_pases.Text = "1";
            if (V_tipoDistancia.Text.Trim().Length == 0) V_tipoDistancia.Text = "0";
            if (V_modoIniciar.Text.Trim().Length == 0) V_modoIniciar.Text = "0";
            if (V_semilla.Text.Length == 0) V_semilla.Text = "1234";
            if (V_desde.Text.Trim().Length == 0) V_desde.Text = "0";
            if (V_hasta.Text.Trim().Length == 0) V_hasta.Text = "0";
            if (V_pases.Text.Trim().Length == 0) V_pases.Text = "1";
            if (V_hilos.Text.Trim().Length == 0) V_hilos.Text = "1";
            if (V_veces.Text.Trim().Length == 0) V_veces.Text = "1";
            if (V_generaciones.Text.Trim().Length == 0) V_generaciones.Text = "0";
            if (V_minimizaGenetico.Text.Trim().Length == 0) V_minimizaGenetico.Text = "0";
            nclusters = Convert.ToInt32(V_nclusters.Text.Trim());
            if (nclusters < 1)
            {
                MessageBox.Show("El número de clusters debe ser mayor que cero");
                return;
            }
            maximo_iteraciones = Convert.ToInt32(V_max_Iteraciones.Text.Trim());
            if (maximo_iteraciones < 1)
            {
                MessageBox.Show("El máximo de iteraciones debe ser mayor que cero");
                return;
            }
            pases_a_realizar = Convert.ToInt32(V_pases.Text.Trim());
            if (pases_a_realizar < 1)
            {
                MessageBox.Show("El número de pases debe ser mayor que cero");
                return;
            }
            nhilos = Convert.ToInt32(V_hilos.Text.Trim());
            if (nhilos < 1)
            {
                MessageBox.Show("El número de hilos debe ser mayor que cero");
                return;
            }
            if (nhilos > MAX_HILOS)
            {
                MessageBox.Show(string.Format("El número máximo de hilos es {0}", MAX_HILOS));
                return;
            }
            if (nhilos > pases_a_realizar)
            {
                MessageBox.Show("El número de hilos debe ser menor que el de pases");
                return;
            }
            tipo_distancia = Convert.ToInt32(V_tipoDistancia.Text.Trim());
            modo_iniciar = Convert.ToInt32(V_modoIniciar.Text.Trim());
            if (modo_iniciar == 0)
            {
                if (centroides_leidos == null)
                {
                    MessageBox.Show("No hay centroides iniciales");
                    return;
                }
                if (centroides_leidos.Length != nclusters)
                {
                    MessageBox.Show(string.Format("Los centroides iniciales no son para este número de clusters {0} != {1}", nclusters, centroides_leidos.GetLength(0)));
                    return;
                }
                if (centroides_leidos[0].Length != ndatos)
                {
                    MessageBox.Show(string.Format("Los centroides iniciales no son para este número de datos {0} != {1}", ndatos, centroides_leidos.GetLength(1)));
                    return;
                }
            }
            else
            {
                R_Fichero_Centroides.Text = FICHERO_CENTROIDES = string.Empty;
            }
            if (V_tipoDatosY.Text.Trim().Length == 0) V_tipoDatosY.Text = "0";
            tipo_datosY = Convert.ToInt32(V_tipoDatosY.Text.Trim());
            normaliza_centroide = V_normalizaCentroides.Checked;
            modo_podar = V_podar.Checked;
            if (modo_podar)
            {
                pu_podar = Convert.ToDouble(V_ppPoda.Text.Trim()) / 100;
                minimo_podar = Convert.ToDouble(V_veces.Text.Trim());
                if (minimo_podar == 0)
                {
                    MessageBox.Show("Veces no puede ser cero");
                    return;
                }
                minimo_podar = 1 / minimo_podar;
            }
            else
            {
                pu_podar = 0;
                minimo_podar = 0;
            }
            modo_forzar = V_forzar.Checked;
            modo_centroide = Convert.ToInt32(V_modoCentroide.Text.Trim());
            modo_genetico = V_genetico.Checked & Convert.ToInt32(V_generaciones.Text.Trim()) > 0;
            cromosomas.Clear();
            int semilla = Convert.ToInt32(V_semilla.Text.Trim());
            EMPEORA_MAX_MD = Convert.ToDouble(V_max_empeora_md.Text.Trim());
            EMPEORA_MAX_DE = Convert.ToDouble(V_max_empeora_de.Text.Trim());
            MEJORA_MIN_MD = Convert.ToDouble(V_min_mejora_md.Text.Trim());
            MEJORA_MIN_DE = Convert.ToDouble(V_min_mejora_de.Text.Trim());
            calcula_desde = Convert.ToInt32(V_desde.Text.Trim());
            if (calcula_desde < 0) calcula_desde = 0;
            calcula_hasta = Convert.ToInt32(V_hasta.Text.Trim());
            ndatos = espectros[0].n;
            clasificacionML = false;
            IniciaTablaClusters();
            ListaIntentos.Items.Clear();
            R_mejorMed.Text = string.Empty;
            R_mejorDE.Text = string.Empty;
            R_mejorDB.Text = string.Empty;
            R_mejorDET.Text = string.Empty;
            ListaEvolucion.Items.Clear();
            if (pases_a_realizar == 1)
            {
                ListaEvolucion.Enabled = true;
                ListaIntentos.Enabled = true;
            }
            for (int i = 0; i < N_CAJAS; i++)
            {
                mejorcaja[i] = null;
                mejor_intento[i] = 1;
                mejor_estimador[i] = double.MaxValue;
            }
            omite_cambio_seleccion = false;
            f_cromosomas = string.Empty;
            if (modo_genetico)
            {
                DialogResult res = MessageBox.Show("¿Salvar cromosomas?", "Clasificar", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    SaveFileDialog ficheroescritura = new SaveFileDialog()
                    {
                        Filter = "BIN |*.bin|TODO |*.*",
                        FilterIndex = 1
                    };
                    if (ficheroescritura.ShowDialog() == DialogResult.OK)
                    {
                        f_cromosomas = ficheroescritura.FileName;
                        salva_cromosomas = true;
                    }
                    else
                    {
                        salva_cromosomas = false;
                    }
                }
                else
                {
                    salva_cromosomas = false;
                }
            }
            else
            {
                salva_cromosomas = false;
            }
            Habilitar(false);
            cancelar = false;
            Esc.Enabled = true;
            Esc.Cursor = Cursors.Default;
            panel1.Enabled = true;
            panel1.Cursor = Cursors.Default;

            int intentos_hilo = pases_a_realizar / nhilos;
            int resto = pases_a_realizar - intentos_hilo * nhilos;
            hilo_finalizado = new bool[nhilos];
            for (int nh = 0; nh < nhilos; nh++) hilo_finalizado[nh] = false;
            hilos_pendientes = nhilos;
            R_hilosPendientes.Text = hilos_pendientes.ToString();
            tiempo_inicio = DateTime.Now;
            R_tiempo.Text = string.Format("{0:N0}", 0);
            R_iteracionActual.Text = string.Format("{0:N0}", 0);
            Application.DoEvents();

            int desde = 0;
            int hasta;
            pases_realizados = 0;
            for (int nh = 0; nh < nhilos; nh++)
            {
                hasta = desde + intentos_hilo;
                if (resto > 0)
                {
                    hasta++;
                    resto--;
                }
                hiloclasifica = new Thread(new ParameterizedThreadStart(ClasificaCaja));
                object[] parametros = new object[] { nh, semilla, desde, hasta, pases_a_realizar == 1 };
                hiloclasifica.Start(parametros);
                semilla += (hasta - desde);
                desde = hasta;
            }
        }
        private void ClasificaCaja(object parametros)
        {
            Array vector_parametros = (Array)parametros;
            int numhilo = (int)vector_parametros.GetValue(0);
            int semilla = (int)vector_parametros.GetValue(1);
            int ini = (int)vector_parametros.GetValue(2);
            int fin = (int)vector_parametros.GetValue(3);

            double[] d_media_previas = new double[N_PREVIAS];
            double[] d_estandar_previas = new double[N_PREVIAS];
            double d_media_anterior;
            double d_estandar_anterior;
            bool seguir;
            int contador;
            bool cambios;
            string linea;
            for (int pase = ini; pase < fin; pase++)
            {
                if (hilo_finalizado[numhilo] == true)
                {
                    return;
                }
                if (cancelar)
                {
                    hilo_finalizado[numhilo] = true;
                    try { Invoke(delegadofinhilo, new object[] { numhilo }); }
                    catch { }
                    Application.DoEvents();
                    return;
                }
                Caja caja = new Caja(numhilo, nclusters, espectros.Count, ndatos, semilla++);
                if (modo_iniciar == 0)
                {
                    Array.Copy(centroides_leidos, caja.centroides, centroides_leidos.Length);
                }
                if (!ClustersIniciales(caja))
                {
                    hilo_finalizado[numhilo] = true;
                    try { Invoke(delegadofinhilo, new object[] { numhilo }); }
                    catch { }
                    Application.DoEvents();
                    return;
                }
                CalculaCentroides(caja);
                EstadisticaClusters(caja);
                linea = string.Format("{0:e4} {1:e4} {2:f3} {3:f0}", caja.estadistica.d_media_g, caja.estadistica.d_estandar_g, caja.estadistica.db.indice, caja.estadistica.de_temperatura_g);
                caja.evolucion.Add(linea);
                if (nhilos == 1)
                {
                    try { Invoke(delegadoactlista, new object[] { caja, numhilo }); }
                    catch { }
                }
                for (int i = 0; i < N_PREVIAS; i++)
                {
                    d_media_previas[i] = double.MaxValue;
                    d_estandar_previas[i] = double.MaxValue;
                }
                d_media_anterior = double.MaxValue;
                d_estandar_anterior = double.MaxValue;
                seguir = true;
                contador = 0;
                while (seguir && contador < maximo_iteraciones)
                {
                    Application.DoEvents();
                    if (cancelar)
                    {
                        hilo_finalizado[numhilo] = true;
                        try { Invoke(delegadofinhilo, new object[] { numhilo }); }
                        catch { }
                        Application.DoEvents();
                        return;
                    }
                    contador++;
                    cambios = AsignaEspectros(caja);
                    CalculaCentroides(caja);
                    EstadisticaClusters(caja);
                    linea = string.Format("{0:e4} {1:e4} {2:f3} {3:f0}", caja.estadistica.d_media_g, caja.estadistica.d_estandar_g, caja.estadistica.db.indice, caja.estadistica.de_temperatura_g);
                    caja.evolucion.Add(linea);
                    if (nhilos == 1)
                    {
                        try { Invoke(delegadoactlista, new object[] { caja, numhilo }); }
                        catch { }
                    }
                    if (!cambios)
                    {
                        seguir = false;
                    }
                    else if ((caja.estadistica.d_estandar_g - d_estandar_anterior) / caja.estadistica.d_estandar_g > EMPEORA_MAX_DE)
                    {
                        seguir = false;
                    }
                    else if (caja.estadistica.d_estandar_g <= d_estandar_anterior && (d_estandar_anterior - caja.estadistica.d_estandar_g) / caja.estadistica.d_estandar_g < MEJORA_MIN_DE)
                    {
                        seguir = false;
                    }
                    else if ((caja.estadistica.d_media_g - d_media_anterior) / caja.estadistica.d_media_g > EMPEORA_MAX_MD)
                    {
                        seguir = false;
                    }
                    else if (caja.estadistica.d_media_g <= d_media_anterior && (d_media_anterior - caja.estadistica.d_media_g) / caja.estadistica.d_media_g < MEJORA_MIN_MD)
                    {
                        seguir = false;
                    }
                    if (seguir)
                    {
                        for (int i = 0; i < N_PREVIAS; i++)
                        {
                            if (Math.Abs(caja.estadistica.d_media_g - d_media_previas[i]) / caja.estadistica.d_media_g < .00001 && Math.Abs(caja.estadistica.d_estandar_g - d_estandar_previas[i]) / caja.estadistica.d_estandar_g < .00001)
                            {
                                seguir = false;
                            }
                        }
                        if (seguir)
                        {
                            d_media_anterior = caja.estadistica.d_media_g;
                            d_estandar_anterior = caja.estadistica.d_estandar_g;
                            for (int i = 1; i < N_PREVIAS; i++)
                            {
                                d_media_previas[i - 1] = d_media_previas[i];
                                d_estandar_previas[i - 1] = d_estandar_previas[i];
                            }
                            d_media_previas[N_PREVIAS - 1] = caja.estadistica.d_media_g;
                            d_estandar_previas[N_PREVIAS - 1] = caja.estadistica.d_estandar_g;
                        }
                    }
                }
                caja.estadistica.si = V_silhouette.Checked ? CalculaSilhouette(caja) : 0;
                try { Invoke(delegadofinpase, new object[] { caja, numhilo }); }
                catch { }
                Application.DoEvents();
            }
            hilo_finalizado[numhilo] = true;
            try { Invoke(delegadofinhilo, new object[] { numhilo }); }
            catch { }
            Application.DoEvents();
        }
        private void ActLista(Caja caja, int hilo)
        {
            lock (sync)
            {
                string linea = string.Format("{0:e4} {1:e4} {2:f3} {3:f0}", caja.estadistica.d_media_g, caja.estadistica.d_estandar_g, caja.estadistica.db.indice, caja.estadistica.de_temperatura_g);
                ListaEvolucion.Items.Add(linea);
                ListaEvolucion.TopIndex = ListaEvolucion.Items.Count - 1;
                Application.DoEvents();
            }
        }
        private void FinPase(Caja caja, int hilo)
        {
            lock (sync)
            {
                pases_realizados++;
                string linea = string.Format("{0:e4} {1:e4} {2:f3} {3:f0}", caja.estadistica.d_media_g, caja.estadistica.d_estandar_g, caja.estadistica.db.indice, caja.estadistica.de_temperatura_g);
                ListaIntentos.Items.Add(linea);
                ListaIntentos.TopIndex = ListaIntentos.Items.Count - 1;
                if (mejorcaja[0] == null || caja.estadistica.d_media_g < mejor_estimador[0])
                {
                    OrdenaClusters(caja);
                    mejor_intento[0] = pases_realizados;
                    mejor_estimador[0] = caja.estadistica.d_media_g;
                    mejorcaja[0] = caja;
                    R_mejorMed.Text = string.Format("{0} {1:N0}", linea, pases_realizados);
                }
                if (mejorcaja[1] == null || caja.estadistica.d_estandar_g < mejor_estimador[1])
                {
                    OrdenaClusters(caja);
                    mejor_intento[1] = pases_realizados;
                    mejor_estimador[1] = caja.estadistica.d_estandar_g;
                    mejorcaja[1] = caja;
                    R_mejorDE.Text = string.Format("{0} {1:N0}", linea, pases_realizados);
                }
                if (mejorcaja[2] == null || caja.estadistica.db.indice < mejor_estimador[2])
                {
                    OrdenaClusters(caja);
                    mejor_intento[2] = pases_realizados;
                    mejor_estimador[2] = caja.estadistica.db.indice;
                    mejorcaja[2] = caja;
                    R_mejorDB.Text = string.Format("{0} {1:N0}", linea, pases_realizados);
                }
                if (mejorcaja[3] == null || caja.estadistica.de_temperatura_g < mejor_estimador[3])
                {
                    OrdenaClusters(caja);
                    mejor_intento[3] = pases_realizados;
                    mejor_estimador[3] = caja.estadistica.de_temperatura_g;
                    mejorcaja[3] = caja;
                    R_mejorDET.Text = string.Format("{0} {1:N0}", linea, pases_realizados);
                }
                if (modo_genetico)
                {
                    double[][] genes = new double[nclusters][];
                    for (int i = 0; i < nclusters; i++)
                    {
                        genes[i] = caja.centroides[i];
                    }
                    double valor;
                    switch (minimiza_genetico)
                    {
                        case 0:
                            valor = caja.estadistica.d_media_g;
                            break;
                        case 1:
                            valor = caja.estadistica.d_estandar_g;
                            break;
                        case 2:
                            valor = caja.estadistica.db.indice;
                            break;
                        default:
                            valor = caja.estadistica.de_temperatura_g;
                            break;
                    }
                    cromosomas.Add(new Cromosoma(genes, valor));
                }
                R_iteracionActual.Text = string.Format("{0:N0}", pases_realizados);
                TimeSpan td = DateTime.Now - tiempo_inicio;
                segundos = (long)td.TotalSeconds;
                R_tiempo.Text = string.Format("{0:N0}", segundos);
            }
        }
        private void FinHilo(int hilo)
        {
            lock (sync)
            {
                hilos_pendientes--;
                R_hilosPendientes.Text = hilos_pendientes.ToString();

                // Tras una cancelación se llamará este método desde todos los hilos, por lo que 
                // no se debe llamar a 'FinClasificacion()' hasta que lo hayan hecho todos

                if (hilos_pendientes == 0)
                {
                    try
                    {
                        FinClasificacion();
                    }
                    finally
                    {
                        Habilitar(true);
                    }
                }
            }
        }
        private void FinClasificacion()
        {
            R_iteracionActual.Text = string.Format("{0:N0}", pases_realizados);
            for (int i = 0; i < N_CAJAS; i++) EstadisticaClusters(mejorcaja[i]);
            int mejor = MejorCajaSeleccionada();
            if (mejorcaja[mejor] != null)
            {
                foreach (string s in mejorcaja[mejor].evolucion)
                {
                    ListaEvolucion.Items.Add(s);
                }
                ListaEvolucion.TopIndex = ListaEvolucion.Items.Count - 1;   // Scroll para ver el último añadido
                ListaIntentos.Items.Add(string.Format("M.MEDIA: {0,4} {1:e4}", mejor_intento[0], mejor_estimador[0]));
                ListaIntentos.Items.Add(string.Format("M.DESTD: {0,4} {1:e4}", mejor_intento[1], mejor_estimador[1]));
                ListaIntentos.Items.Add(string.Format("M.IndDB: {0,4} {1:e4}", mejor_intento[2], mejor_estimador[2]));
                ListaIntentos.Items.Add(string.Format("M.DE.T : {0,4} {1:e4}", mejor_intento[3], mejor_estimador[3]));
                ListaIntentos.Items.Add(string.Format("{0:e4} {1:e4} {2:f3} {3:f0}", mejorcaja[mejor].estadistica.d_media_g, mejorcaja[mejor].estadistica.d_estandar_g, mejorcaja[mejor].estadistica.db.indice, mejorcaja[mejor].estadistica.de_temperatura_g));
                ListaIntentos.TopIndex = ListaIntentos.Items.Count - 1;
                MuestraClusters(mejorcaja[mejor]);

                // Forzar el cambio de fila en la tabla de clusters

                omite_cambio_seleccion = true;
                TablaClusters.Rows[nclusters].Selected = true;
                omite_cambio_seleccion = false;

                // Situarse en la primera fila de la tabla de clusters

                TablaClusters.Rows[0].Selected = true;
            }
            Console.Beep();
            if (modo_genetico)
            {
                if (salva_cromosomas)
                {
                    Habilitar(false);
                    EscribeCromosomas(f_cromosomas);
                    Habilitar(true);
                }
                cancelar = false;
                Genetico();
            }
            else
            {
                TimeSpan td = DateTime.Now - tiempo_inicio;
                segundos = (long)td.TotalSeconds;
                if (cancelar)
                {
                    MessageBox.Show(string.Format("Proceso cancelado. {0:N0} s", segundos));
                }
                else
                {
                    MessageBox.Show(string.Format("Proceso terminado. {0:N0} s", segundos));
                }
            }
            cancelar = false;
            Esc.Enabled = false;
            Esc.Cursor = Cursors.No;
        }
        private void Genetico()
        {
            if (V_generaciones.Text.Trim().Length == 0) V_generaciones.Text = "0";
            generaciones_a_realizar = Convert.ToInt32(V_generaciones.Text.Trim());
            if (cromosomas.Count < 2)
            {
                MessageBox.Show("Número insuficiente de cromosomas");
                return;
            }
            genetico_pendiente = true;
            minimiza_genetico = Convert.ToInt32(V_minimizaGenetico.Text.Trim());
            bool torneo = V_torneo.Checked;
            int tornean = 0;
            if (torneo)
            {
                if (V_ppTorneo.Text.Trim().Length == 0) V_ppTorneo.Text = "10";
                tornean = (int)(cromosomas.Count * Convert.ToDouble(V_ppTorneo.Text.Trim()));
            }
            mutar = V_mutar.Checked;
            if (mutar)
            {
                if (V_ppMutar.Text.Trim().Length == 0) V_ppTorneo.Text = "5";
                mutan_datos = (int)(ndatos * Convert.ToDouble(V_ppMutar.Text.Trim()) / 100);
                if (V_mutarCuanto.Text.Trim().Length == 0) V_mutarCuanto.Text = "1";
                muta_cuanto = Convert.ToDouble(V_mutarCuanto.Text.Trim()) / 100;
            }
            int semilla = Convert.ToInt32(V_semilla.Text.Trim());
            Random azg = new Random(semilla);
            Habilitar(false);
            Esc.Enabled = true;
            Esc.Cursor = Cursors.Default;
            panel1.Enabled = true;
            panel1.Cursor = Cursors.Default;
            R_iteracionActual.Text = string.Format("{0:N0}", 0);
            R_mejorMed.Text = string.Empty;
            R_mejorDE.Text = string.Empty;
            R_mejorDB.Text = string.Empty;
            R_mejorDET.Text = string.Empty;
            ListaEvolucion.Items.Clear();
            Application.DoEvents();
            evolucion_genetica.Clear();
            int nd2 = nclusters / 2;
            int[] ind_gen_padre = new int[nd2];
            int[] ind_gen_madre = new int[nclusters];
            int padre;
            int madre;
            Cromosoma hijo;
            int ind;
            int ind_gen;
            int ind_dato;
            double valor;
            string linea;
            double d;
            int jmin;
            double dmin;
            int contador;
            bool vale;
            TimeSpan td;
            for (generaciones_realizadas = 0; generaciones_realizadas < generaciones_a_realizar; generaciones_realizadas++)
            {
                // Cruza al azar

                padre = azg.Next(0, cromosomas.Count);
                do
                {
                    madre = azg.Next(0, cromosomas.Count);
                } while (padre == madre);


                // Selecciona la mitad de los genes del padre al azar

                for (int i = 0; i < nd2; i++)
                {
                    do
                    {
                        jmin = azg.Next(0, nclusters);
                        vale = true;
                        for (int j = 0; j < i; j++)
                        {
                            if (jmin == ind_gen_padre[j])
                            {
                                vale = false;
                                break;
                            }
                        }
                    } while (!vale);
                    ind_gen_padre[i] = jmin;
                }

                // Selecciona el resto de genes de la madre eliminando los más próximos a los seleccionados del padre

                Array.Clear(ind_gen_madre, 0, nclusters);
                for (int i = 0; i < nd2; i++)
                {
                    jmin = -1;
                    dmin = double.MaxValue;
                    for (int j = 0; j < nclusters; j++)
                    {
                        if (ind_gen_madre[j] == 0)
                        {
                            d = Distancia(cromosomas[padre].genes[ind_gen_padre[i]], cromosomas[madre].genes[j]);
                            if (d < dmin)
                            {
                                jmin = j;
                                dmin = d;
                            }
                        }
                    }

                    // Se excluye el más cercano

                    ind_gen_madre[jmin] = -1;
                }
                hijo = new Cromosoma(nclusters, ndatos);

                // Heredar los genes del padre

                for (int i = 0; i < nd2; i++)
                {
                    Array.Copy(cromosomas[padre].genes[ind_gen_padre[i]], hijo.genes[i], ndatos);
                }

                // Heredar los genes de la madre

                contador = nd2;
                for (int j = 0; j < nclusters; j++)
                {
                    if (ind_gen_madre[j] != -1)
                    {
                        Array.Copy(cromosomas[madre].genes[j], hijo.genes[contador++], ndatos);
                    }
                }
                if (mutar)
                {
                    // Sólo el 50% de las veces

                    if (azg.NextDouble() > 0.5)
                    {
                        ind_gen = azg.Next(0, nclusters);
                        for (int i = 0; i < mutan_datos; i++)
                        {
                            ind_dato = azg.Next(0, ndatos);
                            valor = azg.NextDouble() * muta_cuanto;
                            if (azg.NextDouble() > 0.5)
                            {
                                hijo.genes[ind_gen][ind_dato] *= (1 + valor);
                            }
                            else
                            {
                                hijo.genes[ind_gen][ind_dato] *= (1 - valor);
                            }
                        }
                    }
                }

                // Valora el nuevo cromosoma

                Caja caja = new Caja(0, nclusters, espectros.Count, ndatos, 1);
                for (int j = 0; j < nclusters; j++)
                {
                    Array.Copy(hijo.genes[j], caja.centroides[j], ndatos);
                }
                AsignaEspectros(caja);
                EstadisticaClusters(caja);
                linea = string.Format("{0:e4} {1:e4} {2:f3} {3:f0} {4:N0}", caja.estadistica.d_media_g, caja.estadistica.d_estandar_g, caja.estadistica.db.indice, caja.estadistica.de_temperatura_g, generaciones_realizadas + 1);
                ListaEvolucion.Items.Add(linea);
                switch (minimiza_genetico)
                {
                    case 0:
                        valor = caja.estadistica.d_media_g;
                        break;
                    case 1:
                        valor = caja.estadistica.d_estandar_g;
                        break;
                    case 2:
                        valor = caja.estadistica.db.indice;
                        break;
                    default:
                        valor = caja.estadistica.de_temperatura_g;
                        break;
                }
                hijo.valor = valor;
                if (torneo)
                {
                    int ipeor = -1;
                    double vpeor = double.MinValue;
                    for (int i = 0; i < tornean; i++)
                    {
                        ind = azg.Next(0, cromosomas.Count);
                        if (cromosomas[ind].valor > vpeor)
                        {
                            ipeor = ind;
                            vpeor = cromosomas[ind].valor;
                        }
                    }
                    cromosomas[ipeor] = hijo;
                }
                else
                {
                    // Sustituye al peor progenitor

                    if (cromosomas[padre].valor > cromosomas[madre].valor)
                    {
                        cromosomas[padre] = hijo;
                    }
                    else
                    {
                        cromosomas[madre] = hijo;
                    }
                }
                if (mejorcaja[4] == null || caja.estadistica.d_media_g < mejor_estimador[4])
                {
                    OrdenaClusters(caja);
                    mejor_intento[4] = pases_realizados;
                    mejor_estimador[4] = caja.estadistica.d_media_g;
                    mejorcaja[4] = caja;
                    R_mejorMed.Text = linea;
                    EspectrosExtremos(caja);
                }
                if (mejorcaja[5] == null || caja.estadistica.d_estandar_g < mejor_estimador[5])
                {
                    OrdenaClusters(caja);
                    mejor_intento[5] = pases_realizados;
                    mejor_estimador[5] = caja.estadistica.d_estandar_g;
                    mejorcaja[5] = caja;
                    R_mejorDE.Text = linea;
                    EspectrosExtremos(caja);
                }
                if (mejorcaja[6] == null || caja.estadistica.db.indice < mejor_estimador[6])
                {
                    OrdenaClusters(caja);
                    mejor_intento[6] = pases_realizados;
                    mejor_estimador[6] = caja.estadistica.db.indice;
                    mejorcaja[6] = caja;
                    R_mejorDB.Text = linea;
                    EspectrosExtremos(caja);
                }
                if (mejorcaja[7] == null || caja.estadistica.de_temperatura_g < mejor_estimador[7])
                {
                    OrdenaClusters(caja);
                    mejor_intento[7] = pases_realizados;
                    mejor_estimador[7] = caja.estadistica.de_temperatura_g;
                    mejorcaja[7] = caja;
                    R_mejorDET.Text = linea;
                    EspectrosExtremos(caja);
                }
                evolucion_genetica.Add(linea);
                if (generaciones_realizadas % 10 == 0)
                {
                    ListaEvolucion.TopIndex = ListaEvolucion.Items.Count - 1;
                    R_iteracionActual.Text = string.Format("{0:N0}", generaciones_realizadas);
                    td = DateTime.Now - tiempo_inicio;
                    segundos = (long)td.TotalSeconds;
                    R_tiempo.Text = string.Format("{0:N0}", segundos);
                    Application.DoEvents();
                }
                if (cancelar) break;
            }
            R_iteracionActual.Text = string.Format("{0:N0}", generaciones_realizadas);
            td = DateTime.Now - tiempo_inicio;
            segundos = (long)td.TotalSeconds;
            R_tiempo.Text = string.Format("{0:N0}", segundos);

            // Mostrar mejor caja en la posición del indice Silhouette

            omite_cambio_seleccion = true;
            Sel_mediaGenetico.Checked = true;
            omite_cambio_seleccion = false;
            MuestraClusters(mejorcaja[4]);

            omite_cambio_seleccion = true;
            TablaClusters.Rows[nclusters].Selected = true;
            omite_cambio_seleccion = false;
            TablaClusters.Rows[0].Selected = true;
            Console.Beep();
            if (cancelar)
            {
                MessageBox.Show(string.Format("Proceso cancelado. {0:N0} s", segundos));
            }
            else
            {
                MessageBox.Show(string.Format("Proceso terminado. {0:N0} s", segundos));
            }
            Habilitar(true);
            genetico_pendiente = false;
            cancelar = false;
        }
        private int MejorCajaSeleccionada()
        {
            if (Sel_media.Checked) return 0;
            else if (Sel_destandar.Checked) return 1;
            else if (Sel_db.Checked) return 2;
            else if (Sel_det.Checked) return 3;
            else if (Sel_mediaGenetico.Checked) return 4;
            else if (Sel_destandarGenetico.Checked) return 5;
            else if (Sel_dbGenetico.Checked) return 6;
            return 7;
        }
        private void OrdenaClusters(Caja caja)
        {
            List<(double, int)> orden = new List<(double, int)>();
            for (int i = 0; i < nclusters; i++) orden.Add((caja.clusters[i].temperatura, i));
            orden.Sort();
            orden.Reverse();
            for (int i = 0; i < nclusters; i++) caja.orden[i] = orden[i].Item2;
        }
        private int OrdenCaja(Caja caja, int ind_ordenado)
        {
            for (int i = 0; i < nclusters; i++)
            {
                if (ind_ordenado == caja.orden[i]) return i;
            }
            return -1;
        }
        private bool ClustersIniciales(Caja caja)
        {
            switch (modo_iniciar)
            {
                case 0:
                    AsignaEspectros(caja);
                    return true;
                case 1:
                    return ClustersInicialesAzar(caja, nclusters);
                case 2:
                    return ClustersInicialesRecursivo(caja, 0);
                case 3:
                    return ClustersInicialesRecursivo(caja, 1);
                case 4:
                    return ClustersInicialesExclusivo(caja);
                default:
                    return ClustersInicialesKmm(caja);
            }
        }
        private bool ClustersInicialesAzar(Caja caja, int nclusters)
        {
            // Asigna al azar los espectros a los clusters

            int ind_cluster;
            for (int i = 0; i < espectros.Count; i++)
            {
                // A que cluster

                ind_cluster = caja.az.Next(0, nclusters);
                caja.clusters[ind_cluster].n++;
                caja.clusters[ind_cluster].ind_f_espectros.Add(i);
                caja.cluster_espectro[i] = ind_cluster;
            }
            return true;
        }
        private bool ClustersInicialesRecursivo(Caja caja, int modo)
        {
            int n_espectros = espectros.Count;

            // Se asignan los [n_espectros / n_clusters] espectros con la menor distancia al centroide

            int fraccion = n_espectros / nclusters;
            if (fraccion < 2)
            {
                return ClustersInicialesAzar(caja, nclusters);
            }
            byte[] asignado = new byte[n_espectros];
            Array.Clear(asignado, 0, n_espectros);
            double[] distancias = new double[n_espectros];
            int frac_curso;
            double d;
            int i_min;
            double d_min;
            int i_max;
            double d_max;

            // Se elige un espectro al azar

            int ind_espectro = caja.az.Next(0, n_espectros);

            // Para todos los clusters menos uno, ya que al último cluster irán los espectros restantes

            int ind_cluster = 0;
            while (ind_cluster < nclusters - 1)
            {
                caja.clusters[ind_cluster].n++;
                caja.clusters[ind_cluster].ind_f_espectros.Add(ind_espectro);
                caja.cluster_espectro[ind_espectro] = ind_cluster;
                asignado[ind_espectro] = 1;
                frac_curso = fraccion - 1;
                if (frac_curso == 0) continue;

                // Distancias de los espectros, aún sin asignar, al centroide del cluster

                i_min = -1;
                d_min = double.MaxValue;
                i_max = -1;
                d_max = double.MinValue;
                for (int i = 0; i < n_espectros; i++)
                {
                    if (asignado[i] == 0)
                    {
                        distancias[i] = d = Distancia(caja, ind_cluster, espectros[i].yn);
                        if (d < d_min)
                        {
                            i_min = i;
                            d_min = d;
                        }
                        if (d > d_max)
                        {
                            i_max = i;
                            d_max = d;
                        }
                    }
                }

                // Se asignan los más cercanos al centroide, hasta completar la fracción del cluster

                while (frac_curso > 0)
                {
                    caja.clusters[ind_cluster].n++;
                    caja.clusters[ind_cluster].ind_f_espectros.Add(i_min);
                    caja.cluster_espectro[i_min] = ind_cluster;
                    asignado[i_min] = 1;
                    frac_curso--;
                    if (frac_curso == 0) break;
                    i_min = -1;
                    d_min = double.MaxValue;
                    for (int i = 0; i < n_espectros; i++)
                    {
                        if (asignado[i] == 0)
                        {
                            if (distancias[i] < d_min)
                            {
                                i_min = i;
                                d_min = distancias[i];
                            }
                        }
                    }
                }
                if (modo == 0)
                {
                    // El espectro para el siguiente cluster es el espectro más alejado del cluster actual

                    ind_espectro = i_max;
                }
                else
                {
                    // El espectro para el siguiente cluster se elige al azar

                    do
                    {
                        ind_espectro = caja.az.Next(0, n_espectros);
                    } while (asignado[ind_espectro] == 1);

                }
                ind_cluster++;
            }

            // Al último cluster, los pendientes de asignar

            for (int i = 0; i < n_espectros; i++)
            {
                if (asignado[i] == 0)
                {
                    caja.clusters[ind_cluster].n++;
                    caja.clusters[ind_cluster].ind_f_espectros.Add(i);
                    caja.cluster_espectro[i] = ind_cluster;
                }
            }
            return true;
        }
        private bool ClustersInicialesExclusivo(Caja caja)
        {
            // Se asignan todos los espectros a la mitad de los clusters

            int asignados = nclusters / 2;
            ClustersInicialesAzar(caja, asignados);

            // Se calculan los espectros más alejados en su cluster

            double d;
            int i_max = -1;
            double d_max = double.MinValue;
            int ind_espectro;
            int nuevo = asignados;
            for (int ind_cluster = 0; ind_cluster < asignados; ind_cluster++)
            {
                for (int i = 0; i < caja.clusters[ind_cluster].n; i++)
                {
                    ind_espectro = caja.clusters[ind_cluster].ind_f_espectros[i];
                    d = Distancia(caja, ind_cluster, espectros[ind_espectro].yn);
                    if (d > d_max)
                    {
                        i_max = i;
                        d_max = d;
                    }
                }

                // Se mueve el espectro más distante a un nuevo cluster

                // Se añade al nuevo cluster

                caja.clusters[nuevo].n = 1;
                ind_espectro = caja.clusters[ind_cluster].ind_f_espectros[i_max];
                caja.clusters[nuevo].ind_f_espectros.Add(ind_espectro);
                caja.cluster_espectro[ind_espectro] = nuevo;

                // Se retira del cluster anterior

                caja.clusters[ind_cluster].n--;
                caja.clusters[ind_cluster].ind_f_espectros.RemoveAt(i_max);

                nuevo++;
            }
            return true;
        }
        private bool ClustersInicialesKmm(Caja caja)
        {
            double sorteo;
            int n_espectros = espectros.Count;
            byte[] asignado = new byte[n_espectros];
            Array.Clear(asignado, 0, n_espectros);
            double[] distancias = new double[n_espectros];

            // El primer centroide al azar

            int ind_espectro = caja.az.Next(0, n_espectros);

            double d;
            double d_min;
            double d_min_absoluta;
            double d_max_absoluta;
            int ind_cluster = 0;
            while (ind_cluster < nclusters)
            {
                // Crea un centroide igual al espectro

                Array.Copy(espectros[ind_espectro].yn, caja.centroides[ind_cluster], ndatos);
                asignado[ind_espectro] = 1;
                ind_cluster++;
                if (ind_cluster == nclusters) break;

                // Calcula la distancia de los espectros aún no asignados respecto de los centroide ya creados
                // y para cada espectro se queda con la menor distancia
                // También calcula la menor y mayor de todas las distancias mínimas 'dmin_absoluta, dmax_absoluta' para normalizar

                d_min_absoluta = double.MaxValue;
                d_max_absoluta = double.MinValue;
                for (int i = 0; i < n_espectros; i++)
                {
                    if (asignado[i] == 0)
                    {
                        d_min = double.MaxValue;
                        for (int j = 0; j < ind_cluster; j++)
                        {
                            d = Distancia(caja, j, espectros[i].yn);
                            if (d < d_min) d_min = d;
                        }
                        distancias[i] = d_min;
                        if (d_min > d_max_absoluta) d_max_absoluta = d_min;
                        if (d_min < d_min_absoluta) d_min_absoluta = d_min;
                    }
                    else
                    {
                        // Para que no sea seleccionado

                        distancias[i] = -1;
                    }
                }

                // Normalizar las distancias entre 0 y 1

                d = d_max_absoluta - d_min_absoluta;
                for (int i = 0; i < n_espectros; i++) distancias[i] = (distancias[i] - d_min_absoluta) / d;

                // Elige un espectro al azar con probabilidad proporcional a su distancia registrada

                while (true)
                {
                    ind_espectro = caja.az.Next(0, n_espectros);
                    if (asignado[ind_espectro] == 0)
                    {
                        sorteo = caja.az.NextDouble();
                        if (sorteo <= distancias[ind_espectro]) break;
                    }
                }
            }
            AsignaEspectros(caja);
            return true;
        }
        private void CalculaCentroides(Caja caja)
        {
            double d;
            int ind_cluster_mas_cercano;
            double r;
            int n_peores = 0;
            int n_max_peores = 0;
            int j_peor1 = -1;
            double r_peor1 = 0;
            int[] j_peor = null;
            double[] r_peor = null;
            int ind_espectro;
            int contador;
            for (int ind_cluster = 0; ind_cluster < nclusters; ind_cluster++)
            {
                if (caja.clusters[ind_cluster].n == 0) continue;
                if (caja.clusters[ind_cluster].n == 1)
                {
                    // El centroide es el único espectro en el cluster

                    ind_espectro = caja.clusters[ind_cluster].ind_f_espectros[0];
                    Array.Copy(espectros[ind_espectro].yn, caja.centroides[ind_cluster], ndatos);
                    caja.clusters[ind_cluster].ind_min = ind_espectro;
                    caja.clusters[ind_cluster].ind_max = ind_espectro;
                }
                else
                {
                    if (modo_podar)
                    {
                        // Buscar los espectros que menos encajan, dentro de este cluster

                        n_peores = 0;
                        if (caja.clusters[ind_cluster].n > 1)
                        {
                            ind_cluster_mas_cercano = ClusterMasCercano(caja, ind_cluster);
                            if (ind_cluster_mas_cercano != -1)
                            {
                                n_max_peores = (int)(caja.clusters[ind_cluster].n * pu_podar);
                                if (n_max_peores == 0) n_max_peores = 1;
                                if (n_max_peores == 1)
                                {
                                    j_peor1 = -1;
                                    r_peor1 = double.MinValue;
                                }
                                else
                                {
                                    j_peor = new int[n_max_peores];
                                    r_peor = new double[n_max_peores];
                                    for (int ip = 0; ip < n_max_peores; ip++)
                                    {
                                        j_peor[ip] = -1;
                                        r_peor[ip] = double.MinValue;
                                    }
                                }
                                for (int j = 0; j < caja.clusters[ind_cluster].n; j++)
                                {
                                    // Mejor cuanto más pequeña

                                    r = RelacionDistanciaEspectrosEnClusterVecinos(caja, caja.clusters[ind_cluster].ind_f_espectros[j], ind_cluster, ind_cluster_mas_cercano);

                                    // No podar si es suficientemente buena (r más pequeño que el mínimo establecido)

                                    if (r > minimo_podar)
                                    {
                                        if (n_max_peores == 1)
                                        {
                                            if (r > r_peor1)
                                            {
                                                r_peor1 = r;
                                                j_peor1 = j;
                                                n_peores = 1;
                                            }
                                        }
                                        else
                                        {
                                            for (int ip = 0; ip < n_max_peores; ip++)
                                            {
                                                if (r > r_peor[ip])
                                                {
                                                    if (ip >= n_peores)
                                                    {
                                                        // Uno más a la lista de peores

                                                        n_peores++;
                                                    }
                                                    else
                                                    {
                                                        // Desplazar la lista de peores

                                                        if (n_peores < n_max_peores)
                                                        {
                                                            for (int jp = n_peores; jp > ip; jp--)
                                                            {
                                                                r_peor[jp] = r_peor[jp - 1];
                                                                j_peor[jp] = j_peor[jp - 1];
                                                            }
                                                            n_peores++;
                                                        }
                                                        else
                                                        {
                                                            for (int jp = n_max_peores - 1; jp > ip; jp--)
                                                            {
                                                                r_peor[jp] = r_peor[jp - 1];
                                                                j_peor[jp] = j_peor[jp - 1];
                                                            }
                                                        }
                                                    }
                                                    r_peor[ip] = r;
                                                    j_peor[ip] = j;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (modo_centroide == 0)
                    {
                        // El centroide es el promedio de los espectros, quitando los más desfavorables si V_podar (modo_podar) está marcado

                        contador = 0;
                        Array.Clear(caja.centroides[ind_cluster], 0, ndatos);
                        bool bueno = true;
                        for (int j = 0; j < caja.clusters[ind_cluster].n; j++)
                        {
                            if (modo_podar && n_peores > 0)
                            {
                                if (n_max_peores == 1)
                                {
                                    bueno = j != j_peor1;
                                }
                                else
                                {
                                    bueno = true;
                                    for (int ip = 0; ip < n_peores; ip++)
                                    {
                                        if (j == j_peor[ip])
                                        {
                                            bueno = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (bueno)
                            {
                                for (int k = 0; k < ndatos; k++)
                                {
                                    caja.centroides[ind_cluster][k] += espectros[caja.clusters[ind_cluster].ind_f_espectros[j]].yn[k];
                                }
                                contador++;
                            }
                        }
                        for (int k = 0; k < ndatos; k++) caja.centroides[ind_cluster][k] /= contador;
                    }
                    else
                    {
                        // El centroide es el máximo en valor absoluto (con su signo) de los espectros, quitando los más desfavorables si V_podar (modo_podar) está marcado

                        Array.Clear(caja.centroides[ind_cluster], 0, ndatos);
                        bool bueno = true;
                        for (int j = 0; j < caja.clusters[ind_cluster].n; j++)
                        {
                            if (modo_podar && n_peores > 0)
                            {
                                if (n_max_peores == 1)
                                {
                                    bueno = j != j_peor1;
                                }
                                else
                                {
                                    bueno = true;
                                    for (int ip = 0; ip < n_peores; ip++)
                                    {
                                        if (j == j_peor[ip])
                                        {
                                            bueno = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (bueno)
                            {
                                for (int k = 0; k < ndatos; k++)
                                {
                                    if (Math.Abs(espectros[caja.clusters[ind_cluster].ind_f_espectros[j]].yn[k]) > Math.Abs(caja.centroides[ind_cluster][k]))
                                    {
                                        caja.centroides[ind_cluster][k] = espectros[caja.clusters[ind_cluster].ind_f_espectros[j]].yn[k];
                                    }
                                }
                            }
                        }
                    }

                    // Buscar los espectros, dentro del cluster, más próximo y más alejado al promedio

                    int i_min = -1;
                    int i_max = -1;
                    double d_min = double.MaxValue;
                    double d_max = double.MinValue;
                    for (int j = 0; j < caja.clusters[ind_cluster].n; j++)
                    {
                        d = Distancia(ind_cluster, caja.clusters[ind_cluster].ind_f_espectros[j]);
                        if (d < d_min)
                        {
                            d_min = d;
                            i_min = caja.clusters[ind_cluster].ind_f_espectros[j];
                        }
                        if (d > d_max)
                        {
                            d_max = d;
                            i_max = caja.clusters[ind_cluster].ind_f_espectros[j];
                        }
                    }
                    caja.clusters[ind_cluster].ind_min = i_min;
                    caja.clusters[ind_cluster].ind_max = i_max;
                    if (modo_forzar)
                    {
                        // Sustituir el centroide por el espectro

                        Array.Copy(espectros[i_min].yn, caja.centroides[ind_cluster], ndatos);
                    }
                    if (tipo_datosY == 4 && normaliza_centroide)
                    {
                        // Normalizar los valores del centroide a -1/0/1

                        for (int k = 0; k < ndatos; k++)
                        {
                            if (caja.centroides[ind_cluster][k] < -0.5)
                            {
                                caja.centroides[ind_cluster][k] = -1;
                            }
                            else if (caja.centroides[ind_cluster][k] < 0.5)
                            {
                                caja.centroides[ind_cluster][k] = 0;
                            }
                            else
                            {
                                caja.centroides[ind_cluster][k] = 1;
                            }
                        }
                    }
                }
            }
        }
        private bool AsignaEspectros(Caja caja)
        {
            bool cambios = false;
            for (int ind_cluster = 0; ind_cluster < nclusters; ind_cluster++)
            {
                caja.clusters[ind_cluster].n = 0;
                caja.clusters[ind_cluster].ind_f_espectros = new List<int>();

                // 'caja.clusters[ind_cluster].distancias' se calcula en 'EstadisticaClusters'
            }

            // Aasigna los espectros al cluster con el centroide más próximo

            double d;
            int c_min;
            double d_min;
            for (int ind_espectro = 0; ind_espectro < espectros.Count; ind_espectro++)
            {
                c_min = -1;
                d_min = double.MaxValue;
                for (int ind_cluster = 0; ind_cluster < nclusters; ind_cluster++)
                {
                    d = Distancia(caja, ind_cluster, espectros[ind_espectro].yn);
                    if (d < d_min)
                    {
                        d_min = d;
                        c_min = ind_cluster;
                    }
                }
                caja.clusters[c_min].n++;
                caja.clusters[c_min].ind_f_espectros.Add(ind_espectro);
                if (caja.cluster_espectro[ind_espectro] != c_min)
                {
                    cambios = true;
                    caja.cluster_espectro[ind_espectro] = c_min;
                }
            }

            // Evitar clusters vacios. Trasladar el peor espectro del clusters más poblado

            int c_mas_poblado;
            int n_mas_poblado;
            int j_mas_alejado;
            double d_max;
            for (int ind_cluster = 0; ind_cluster < nclusters; ind_cluster++)
            {
                if (caja.clusters[ind_cluster].n == 0)
                {
                    // Cluster más poblado

                    c_mas_poblado = -1;
                    n_mas_poblado = -1;
                    for (int i = 0; i < nclusters; i++)
                    {
                        if (n_mas_poblado < caja.clusters[i].n)
                        {
                            n_mas_poblado = caja.clusters[i].n;
                            c_mas_poblado = i;
                        }
                    }

                    // Espectro más alejado

                    j_mas_alejado = -1;
                    d_max = double.MinValue;
                    for (int j = 0; j < caja.clusters[c_mas_poblado].n; j++)
                    {
                        d = Distancia(c_mas_poblado, caja.clusters[c_mas_poblado].ind_f_espectros[j]);
                        if (d_max < d)
                        {
                            d_max = d;
                            j_mas_alejado = j;
                        }
                    }

                    // Añadir el espectro al cluster vacio

                    int ind_e = caja.clusters[c_mas_poblado].ind_f_espectros[j_mas_alejado];
                    caja.clusters[ind_cluster].n = 1;
                    caja.clusters[ind_cluster].ind_f_espectros.Add(ind_e);
                    cambios = true;
                    caja.cluster_espectro[ind_e] = ind_cluster;

                    // Quitar el espectro del cluster más poblado

                    caja.clusters[c_mas_poblado].n--;
                    for (int j = j_mas_alejado; j < caja.clusters[c_mas_poblado].n; j++)
                    {
                        caja.clusters[c_mas_poblado].ind_f_espectros[j] = caja.clusters[c_mas_poblado].ind_f_espectros[j + 1];
                    }
                }
            }
            return cambios;
        }
        private void EspectrosExtremos(Caja caja)
        {
            double d;
            int i_min;
            double d_min;
            int i_max;
            double d_max;
            for (int ind_cluster = 0; ind_cluster < nclusters; ind_cluster++)
            {
                // Buscar el espectro, dentro del cluster, más próximo al centroide

                i_min = -1;
                d_min = double.MaxValue;
                i_max = -1;
                d_max = double.MinValue;
                for (int j = 0; j < caja.clusters[ind_cluster].n; j++)
                {
                    d = Distancia(ind_cluster, caja.clusters[ind_cluster].ind_f_espectros[j]);
                    if (d < d_min)
                    {
                        d_min = d;
                        i_min = caja.clusters[ind_cluster].ind_f_espectros[j];
                    }
                    if (d > d_max)
                    {
                        d_max = d;
                        i_max = caja.clusters[ind_cluster].ind_f_espectros[j];
                    }
                }
                caja.clusters[ind_cluster].ind_min = i_min;
                caja.clusters[ind_cluster].ind_max = i_max;
            }
        }
        private void EstadisticaClusters(Caja caja)
        {
            if (caja == null) return;

            double d;
            int i_min;
            double d_min;
            int i_max;
            double d_max;
            double d_media;
            double d_estandar;
            int i_es;
            double londa;
            int n_londa;
            int n_londa_g;
            double lomax;
            double t;
            double temperatura;
            double de_temperatura;

            caja.estadistica = new Estadistica();
            n_londa_g = 0;
            for (int ind_cluster = 0; ind_cluster < nclusters; ind_cluster++)
            {
                if (caja.clusters[ind_cluster].n == 0) continue;
                caja.clusters[ind_cluster].distancias = new List<double>();
                if (caja.clusters[ind_cluster].n == 1)
                {
                    i_es = caja.clusters[ind_cluster].ind_f_espectros[0];
                    d = Distancia(caja, ind_cluster, espectros[i_es].yn);
                    caja.clusters[ind_cluster].distancias.Add(d);
                    caja.clusters[ind_cluster].ind_min = i_es;
                    caja.clusters[ind_cluster].distancia_min = d;
                    caja.clusters[ind_cluster].ind_max = i_es;
                    caja.clusters[ind_cluster].distancia_max = d;
                    caja.clusters[ind_cluster].distancia_media = d;
                    caja.clusters[ind_cluster].desviacion_estandar = 0;
                    londa = espectros[caja.clusters[ind_cluster].ind_f_espectros[0]].londaMaximo;
                    if (londa > 0)
                    {
                        n_londa_g++;
                        caja.clusters[ind_cluster].londaMaximo = londa;
                        t = WIEN / londa;
                        caja.clusters[ind_cluster].temperatura = t;
                        caja.estadistica.temperatura_g += t;
                    }
                    caja.clusters[ind_cluster].de_temperatura = 0;
                    caja.estadistica.d_media_g += d;
                    caja.estadistica.ng++;
                }
                else
                {
                    i_min = -1;
                    d_min = double.MaxValue;
                    i_max = -1;
                    d_max = double.MinValue;
                    d_media = 0;
                    lomax = 0;
                    temperatura = 0;
                    n_londa = 0;
                    for (int j = 0; j < caja.clusters[ind_cluster].n; j++)
                    {
                        i_es = caja.clusters[ind_cluster].ind_f_espectros[j];
                        d = Distancia(caja, ind_cluster, espectros[i_es].yn);
                        caja.clusters[ind_cluster].distancias.Add(d);
                        if (d < d_min)
                        {
                            i_min = i_es;
                            d_min = d;
                        }
                        if (d > d_max)
                        {
                            i_max = i_es;
                            d_max = d;
                        }
                        d_media += d;
                        londa = espectros[i_es].londaMaximo;
                        if (londa > 0)
                        {
                            n_londa++;
                            n_londa_g++;
                            lomax += londa;
                            t = WIEN / londa;
                            temperatura += t;
                            caja.estadistica.temperatura_g += t;
                        }
                        caja.estadistica.d_media_g += d;
                        caja.estadistica.ng++;
                    }
                    caja.clusters[ind_cluster].ind_min = i_min;
                    caja.clusters[ind_cluster].distancia_min = d_min;
                    caja.clusters[ind_cluster].ind_max = i_max;
                    caja.clusters[ind_cluster].distancia_max = d_max;
                    caja.clusters[ind_cluster].distancia_media = d_media / caja.clusters[ind_cluster].n;
                    if (n_londa > 0)
                    {
                        caja.clusters[ind_cluster].londaMaximo = lomax / n_londa;
                        caja.clusters[ind_cluster].temperatura = temperatura / n_londa;
                    }
                    else
                    {
                        caja.clusters[ind_cluster].londaMaximo = 0;
                        caja.clusters[ind_cluster].temperatura = 0;
                    }
                    d_estandar = 0;
                    de_temperatura = 0;
                    for (int j = 0; j < caja.clusters[ind_cluster].n; j++)
                    {
                        d = Distancia(caja, ind_cluster, espectros[caja.clusters[ind_cluster].ind_f_espectros[j]].yn) - caja.clusters[ind_cluster].distancia_media;
                        d_estandar += d * d;
                        londa = espectros[caja.clusters[ind_cluster].ind_f_espectros[j]].londaMaximo;
                        if (londa > 0)
                        {
                            t = espectros[caja.clusters[ind_cluster].ind_f_espectros[j]].temperatura - caja.clusters[ind_cluster].temperatura;
                            de_temperatura += t * t;
                        }
                    }
                    caja.clusters[ind_cluster].desviacion_estandar = Math.Sqrt(d_estandar / caja.clusters[ind_cluster].n);
                    if (n_londa == 0)
                    {
                        caja.clusters[ind_cluster].de_temperatura = 0;
                    }
                    else
                    {
                        caja.clusters[ind_cluster].de_temperatura = Math.Sqrt(de_temperatura / n_londa);
                    }
                }
            }
            if (caja.estadistica.ng == 0)
            {
                caja.estadistica.d_media_g = 0;
            }
            else
            {
                caja.estadistica.d_media_g /= caja.estadistica.ng;
            }
            if (n_londa_g == 0)
            {
                caja.estadistica.temperatura_g = 0;
            }
            else
            {
                caja.estadistica.temperatura_g /= n_londa_g;
            }
            for (int ind_cluster = 0; ind_cluster < nclusters; ind_cluster++)
            {
                for (int j = 0; j < caja.clusters[ind_cluster].n; j++)
                {
                    d = Distancia(caja, ind_cluster, espectros[caja.clusters[ind_cluster].ind_f_espectros[j]].yn) - caja.clusters[ind_cluster].distancia_media;
                    caja.estadistica.d_estandar_g += d * d;
                    londa = espectros[caja.clusters[ind_cluster].ind_f_espectros[j]].londaMaximo;
                    if (londa > 0)
                    {
                        t = espectros[caja.clusters[ind_cluster].ind_f_espectros[j]].temperatura - caja.clusters[ind_cluster].temperatura;
                        caja.estadistica.de_temperatura_g += t * t;
                    }
                }
            }
            if (caja.estadistica.ng == 0)
            {
                caja.estadistica.d_estandar_g = 0;
            }
            else
            {
                caja.estadistica.d_estandar_g = Math.Sqrt(caja.estadistica.d_estandar_g / caja.estadistica.ng);
            }
            if (n_londa_g == 0)
            {
                caja.estadistica.de_temperatura_g = 0;
            }
            else
            {
                caja.estadistica.de_temperatura_g = Math.Sqrt(caja.estadistica.de_temperatura_g / n_londa_g);
            }
            caja.estadistica.db = CalculaDBI(caja);
        }
        private void MuestraClusters(Caja caja)
        {
            omite_cambio_seleccion = true;
            IniciaTablaClusters();
            string[] fila = new string[11];
            double nmedia = espectros.Count / nclusters;
            double diferencia;
            double des_estandar = 0;
            int k;
            int cmc;
            for (int ind_cluster = 0; ind_cluster < nclusters; ind_cluster++)
            {
                k = caja.orden[ind_cluster];
                fila[0] = string.Format("{0:N0}", ind_cluster + 1);
                fila[1] = string.Format("{0:N0}", caja.clusters[k].ind_min + 1);
                fila[2] = string.Format("{0:N0}", caja.clusters[k].n);
                diferencia = caja.clusters[k].n - nmedia;
                des_estandar += diferencia * diferencia;
                fila[3] = string.Format("{0:e4}", caja.clusters[k].distancia_min);
                fila[4] = string.Format("{0:e4}", caja.clusters[k].distancia_max);
                fila[5] = string.Format("{0:e4}", caja.clusters[k].distancia_media);
                fila[6] = string.Format("{0:e4}", caja.clusters[k].desviacion_estandar);
                cmc = OrdenCaja(caja, ClusterMasCercano(caja, k));
                fila[7] = string.Format("{0:N0}", cmc + 1);
                fila[8] = string.Format("{0:f4}", caja.estadistica.db.R[ind_cluster]);
                fila[9] = string.Format("{0:N0}", caja.clusters[k].temperatura);
                fila[10] = string.Format("{0:N0}", caja.clusters[k].de_temperatura);
                TablaClusters.Rows.Add(fila);
            }
            des_estandar = Math.Sqrt(des_estandar / nclusters);
            fila[0] = string.Format("{0}", "GLOB");
            fila[1] = string.Format("{0}", string.Empty);
            fila[2] = string.Format("{0:N1}", des_estandar);
            fila[3] = string.Format("{0}", string.Empty);
            fila[4] = string.Format("{0}", string.Empty);
            fila[5] = string.Format("{0:e4}", caja.estadistica.d_media_g);
            fila[6] = string.Format("{0:e4}", caja.estadistica.d_estandar_g);
            fila[7] = string.Format("{0}", string.Empty);
            fila[8] = string.Format("{0:f4}", caja.estadistica.db.indice);
            fila[9] = string.Format("{0:N0}", caja.estadistica.temperatura_g);
            fila[10] = string.Format("{0:N0}", caja.estadistica.de_temperatura_g);
            TablaClusters.Rows.Add(fila);
            omite_cambio_seleccion = false;
        }
        private double Distancia(int ind_espectro_a, int ind_espectro_b)
        {
            // espectro - espectro

            switch (tipo_distancia)
            {
                case 0:
                    return DistanciaEuclidiana(espectros[ind_espectro_a].yn, espectros[ind_espectro_b].yn);
                case 1:
                    return DistanciaManhattan(espectros[ind_espectro_a].yn, espectros[ind_espectro_b].yn);
                case 2:
                    return DistanciaCovarianza(espectros[ind_espectro_a].yn, espectros[ind_espectro_b].yn);
                case 3:
                    return DistanciaEuclidiana2(espectros[ind_espectro_a].yn, espectros[ind_espectro_b].yn);
                default:
                    return DistanciaManhattan2(espectros[ind_espectro_a].yn, espectros[ind_espectro_b].yn);
            }
        }
        private double Distancia(Caja caja, int ind_cluster, double[] b)
        {
            // centroide - espectro

            switch (tipo_distancia)
            {
                case 0:
                    return DistanciaEuclidiana(caja.centroides[ind_cluster], b);
                case 1:
                    return DistanciaManhattan(caja.centroides[ind_cluster], b);
                case 2:
                    return DistanciaCovarianza(caja.centroides[ind_cluster], b);
                case 3:
                    return DistanciaEuclidiana2(caja.centroides[ind_cluster], b);
                default:
                    return DistanciaManhattan2(caja.centroides[ind_cluster], b);
            }
        }
        private double Distancia(double[] a, double[] b)
        {
            // centroide - centroide

            switch (tipo_distancia)
            {
                case 0:
                    return DistanciaEuclidiana(a, b);
                case 1:
                    return DistanciaManhattan(a, b);
                case 2:
                    return DistanciaCovarianza(a, b);
                case 3:
                    return DistanciaEuclidiana2(a, b);
                default:
                    return DistanciaManhattan2(a, b);
            }
        }
        private double DistanciaEuclidiana(double[] a, double[] b)
        {
            // Distancia euclidiana 

            double d;
            double d2 = 0;
            int hasta;
            if (calcula_hasta == 0 || calcula_hasta > a.Length) hasta = a.Length;
            else if (calcula_hasta < 0) hasta = a.Length + calcula_hasta;
            else hasta = calcula_hasta;
            for (int k = calcula_desde; k < hasta; k++)
            {
                d = a[k] - b[k];
                d2 += d * d;
            }
            return Math.Sqrt(d2);
        }
        private double DistanciaEuclidiana2(double[] a, double[] b)
        {
            // Distancia euclidiana al cuadrado

            double d;
            double d2 = 0;
            int hasta;
            if (calcula_hasta == 0 || calcula_hasta > a.Length) hasta = a.Length;
            else if (calcula_hasta < 0) hasta = a.Length + calcula_hasta;
            else hasta = calcula_hasta;
            for (int k = calcula_desde; k < hasta; k++)
            {
                d = a[k] - b[k];
                d2 += d * d;
            }
            return d2;
        }
        private double DistanciaManhattan(double[] a, double[] b)
        {
            // Distancia en valores absolutos

            double de = 0;
            int hasta;
            if (calcula_hasta == 0 || calcula_hasta > a.Length) hasta = a.Length;
            else if (calcula_hasta < 0) hasta = a.Length + calcula_hasta;
            else hasta = calcula_hasta;
            for (int k = calcula_desde; k < hasta; k++)
            {
                de += Math.Abs(a[k] - b[k]);
            }
            return de;
        }
        private double DistanciaManhattan2(double[] a, double[] b)
        {
            // Distancia en valores absolutos al cuadrado

            double de = 0;
            int hasta;
            if (calcula_hasta == 0 || calcula_hasta > a.Length) hasta = a.Length;
            else if (calcula_hasta < 0) hasta = a.Length + calcula_hasta;
            else hasta = calcula_hasta;
            for (int k = calcula_desde; k < hasta; k++)
            {
                de += Math.Abs(a[k] - b[k]);
            }
            return de * de;
        }
        private double DistanciaCovarianza(double[] a, double[] b)
        {
            // 1/n SUMA( (Xi - Xm) * (Yi - Ym) )
            // 1/n SUMA( Xi * Yi ) - Xm * Ym

            // CV /( Dx * Dy)

            int hasta;
            if (calcula_hasta == 0 || calcula_hasta > a.Length) hasta = a.Length;
            else if (calcula_hasta < 0) hasta = a.Length + calcula_hasta;
            else hasta = calcula_hasta;

            int n = hasta - calcula_desde;
            double xmedia = 0.0d;
            double ymedia = 0.0d;
            for (int i = calcula_desde; i < hasta; i++)
            {
                xmedia += a[i];
                ymedia += b[i];
            }
            xmedia /= n;
            ymedia /= n;
            double d;
            double xdvstd = 0.0d;
            double ydvstd = 0.0d;
            double covar = 0.0d;
            for (int i = calcula_desde; i < hasta; i++)
            {
                d = a[i] - xmedia;
                xdvstd += d * d;
                d = b[i] - ymedia;
                ydvstd += d * d;
                covar += a[i] * b[i];
            }
            if (xdvstd * ydvstd == 0)
            {
                covar = 0;
            }
            else
            {
                xdvstd = Math.Sqrt(xdvstd / n);
                ydvstd = Math.Sqrt(ydvstd / n);
                covar /= n;
                covar -= xmedia * ymedia;
                covar /= (xdvstd * ydvstd);
            }
            return 1 - covar;
        }
        private void TablaClusters_SelectionChanged(object sender, EventArgs e)
        {
            if (omite_cambio_seleccion || TablaClusters.Rows.Count == 0 || TablaClusters.SelectedRows.Count == 0) return;
            if (TablaClusters.SelectedRows[0].Index >= nclusters)
            {
                // Fila global

                FilaCluster.Value = 1;
                FilaCluster.Maximum = 1;
                panel_img.IndiceActual.Text = string.Empty;
                panel_img.IndiceMaximo.Text = string.Empty;
                panel_img.IndiceEspectro.Text = IndiceEspectro.Text = string.Empty;
                panel_img.R_distancia.Text = R_Distancia.Text = string.Empty;
                panel_img.R_confianza.Text = R_confianza.Text = string.Empty;
                panel_img.R_clusterCercano.Text = R_clusterCercano.Text = string.Empty;
            }
            else
            {
                // Mostrar el primer espectro del cluster

                int mejor = MejorCajaSeleccionada();
                if (mejorcaja[mejor] == null)
                {
                    MessageBox.Show("No hay una clasificación para este concepto");
                    return;
                }
                int ind_cluster = mejorcaja[mejor].orden[TablaClusters.SelectedRows[0].Index];
                FilaCluster.Maximum = mejorcaja[mejor].clusters[ind_cluster].n;
                if (FilaCluster.Maximum == 0)
                {
                    FilaCluster.Value = 0;
                    panel_img.img = null;
                    panel_img.RefrescaLienzo();
                }
                else
                {
                    FilaCluster.Value = 1;
                    SelEspectroCluster();
                }
            }
        }
        private void SelEspectroCluster()
        {
            if (TablaClusters.Rows.Count == 0 || TablaClusters.SelectedRows.Count == 0)
            {
                panel_img.img = null;
                panel_img.RefrescaLienzo();
                return;
            }
            int mejor = MejorCajaSeleccionada();
            if (mejorcaja[mejor] == null)
            {
                MessageBox.Show("No hay una clasificación para este concepto");
                panel_img.img = null;
                panel_img.RefrescaLienzo();
                return;
            }
            int cont_cluster = (int)FilaCluster.Value - 1;
            int ind_cluster = mejorcaja[mejor].orden[TablaClusters.SelectedRows[0].Index];
            DubujaEspectro(mejor, ind_cluster, cont_cluster);
        }
        private void DubujaEspectro(int mejor, int ind_cluster, int cont_cluster)
        {
            int ind_espectro = mejorcaja[mejor].clusters[ind_cluster].ind_f_espectros[cont_cluster];
            panel_img.IndiceActual.Text = string.Format("{0:N0}", FilaCluster.Value);
            panel_img.IndiceMaximo.Text = string.Format("{0:N0}", FilaCluster.Maximum);
            panel_img.IndiceEspectro.Text = IndiceEspectro.Text = string.Format("{0:N0}", ind_espectro + 1);
            panel_img.R_distancia.Text = R_Distancia.Text = string.Format("{0:e4}", mejorcaja[mejor].clusters[ind_cluster].distancias[cont_cluster]);
            Confianza certeza = EstimaConfianza(mejorcaja[mejor], ind_cluster, ind_espectro);
            panel_img.R_confianza.Text = R_confianza.Text = string.Format("{0:f2}", certeza.distancia);
            panel_img.R_clusterCercano.Text = R_clusterCercano.Text = string.Format("{0:N0}", certeza.cluster + 1);
            Grado.Text = string.Format("{0}", espectros[ind_espectro].grado);
            R2.Text = string.Format("{0:f6}", Math.Min(espectros[ind_espectro].RYSQa, espectros[ind_espectro].RYSQb));
            Movil.Text = string.Format("{0}", espectros[ind_espectro].distancia_movil);
            Proporcional.Checked = espectros[ind_espectro].pro;
            Corte.Text = string.Format(Proporcional.Checked ? "{0:f5}" : "{0:f1}", espectros[ind_espectro].corte);
            CalculaPicos(ind_cluster, ind_espectro);
        }
        private void FilaCluster_ValueChanged(object sender, EventArgs e)
        {
            if (FilaCluster.Maximum == 0)
            {
                panel_img.img = null;
                panel_img.RefrescaLienzo();
                return;
            }
            SelEspectroCluster();
        }
        private void LeerEspectrosAnalizados_Click(object sender, EventArgs e)
        {
            OpenFileDialog ficherolectura = new OpenFileDialog()
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
                R_Fichero_Clusters.Text = ficherolectura.FileName;
                Habilitar(false);
                R_espectros.Enabled = true;
                espectros = new List<EspectroAnalizado>();
                int n = 0;
                foreach (string fichero in ficherolectura.FileNames)
                {
                    n++;
                    LeeFicheroEspectrosAnalizados(fichero, n);
                }
                Console.Beep();
                Habilitar(true);
            }
        }
        private void LeeFicheroEspectrosAnalizados(string fichero, int n_fichero)
        {
            R_espectros.Text = string.Empty;
            FileStream fsfc = new FileStream(fichero, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fsfc);
            if (V_tipoDatosY.Text.Trim().Length == 0) V_tipoDatosY.Text = "0";
            tipo_datosY = Convert.ToInt32(V_tipoDatosY.Text.Trim());
            string[] ss;
            int cdato;
            int n;
            int grado;
            bool pro;
            double corte;
            double fcorte;
            double margen;
            int distancia_movil;
            double RYSQa;
            double RYSQb;
            double MY;
            double MYc;
            double AR;
            double DEC;
            double yl;
            double yc;
            byte[] ep;
            double[] ex;
            double[] ey;
            double[] eyc;
            double[] eSDVMovil;
            double[] eyn;
            double LondaMaximo;
            int contador = 0;
            R_espectros.Text = string.Format("{0}-{1:N0}", n_fichero, contador);
            Application.DoEvents();
            string linea;

            // Ignora la cabecera

            linea = sr.ReadLine();
            if (!linea.StartsWith("Num;n;g;c0a;c1a;c2a;c3a;c4a;c5a;c6a;c7a;c8a;c9a;c10a;c11a;c0b;c1b;c2b;c3b;c4b;c5b;c6b;c7b;c8b;c9b;c10b;c11b;R2a;R2b;pro;corte;fcorte;smv;My;Myc;p-;p+;AR;DEC;"))
            {
                MessageBox.Show(string.Format("El fichero {0} no tiene el formato esperado", fichero), "Leer fichero de espectros analizados");
                return;
            }
            double rad_max_polinomio;
            double media = 0;
            while (!sr.EndOfStream)
            {
                linea = sr.ReadLine();
                ss = linea.Split(';');
                n = Convert.ToInt32(ss[1]);
                grado = Convert.ToInt32(ss[2]);

                // Se ignoran los 'MAX_GRADO + 1' coeficientes del ajuste polinómico

                RYSQa = Convert.ToDouble(ss[3 + NCOEFICENTES]);
                RYSQb = Convert.ToDouble(ss[4 + NCOEFICENTES]);
                pro = ss[5 + NCOEFICENTES].Equals("1");
                corte = Convert.ToDouble(ss[6 + NCOEFICENTES]);
                fcorte = Convert.ToDouble(ss[7 + NCOEFICENTES]);
                distancia_movil = Convert.ToInt32(ss[8 + NCOEFICENTES]);
                MY = Convert.ToDouble(ss[9 + NCOEFICENTES]);
                MYc = Convert.ToDouble(ss[10 + NCOEFICENTES]);

                // ss[10] = picos_menos.Count y ss[11] = picos_mas.Count no se usan

                AR = Convert.ToDouble(ss[13 + NCOEFICENTES]);
                DEC = Convert.ToDouble(ss[14 + NCOEFICENTES]);
                ep = new byte[n];
                ex = new double[n];
                ey = new double[n];
                eyc = new double[n];
                eSDVMovil = new double[n];
                eyn = new double[n];
                cdato = 15 + NCOEFICENTES;
                rad_max_polinomio = double.MinValue;
                for (int i = 0; i < n; i++)
                {
                    ep[i] = Convert.ToByte(ss[cdato++]);
                    ex[i] = Convert.ToDouble(ss[cdato++]);
                    ey[i] = yl = Convert.ToDouble(ss[cdato++]);
                    eyc[i] = yc = Convert.ToDouble(ss[cdato++]);
                    rad_max_polinomio = Math.Max(rad_max_polinomio, yc);
                    eSDVMovil[i] = Convert.ToDouble(ss[cdato++]);
                    switch (tipo_datosY)
                    {
                        case 0:
                            // Datos

                            eyn[i] = ey[i];
                            break;
                        case 1:
                        case 7:
                            // Valores calculados dentro del corte

                            if (distancia_movil > 0)
                            {
                                if (pro)
                                {
                                    margen = fcorte * eSDVMovil[i] * 2 / (MY + MYc) * eyc[i];
                                }
                                else
                                {
                                    margen = fcorte * eSDVMovil[i];
                                }
                            }
                            else
                            {
                                if (pro)
                                {
                                    margen = corte * eyc[i];
                                }
                                else
                                {
                                    margen = corte;
                                }
                            }
                            if (Math.Abs(ey[i] - eyc[i]) < margen)
                            {
                                eyn[i] = eyc[i];
                            }
                            else
                            {
                                eyn[i] = ey[i];
                            }
                            break;
                        case 2:
                            // Relativo

                            if (ep[i] == 0)
                            {
                                eyn[i] = 0;
                            }
                            else
                            {
                                if (yc <= 0)
                                {
                                    eyn[i] = 0;
                                }
                                else
                                {
                                    eyn[i] = (yl - yc) / yc;
                                    if (eyn[i] > 1) eyn[i] = 1;
                                    if (eyn[i] < -1) eyn[i] = -1;
                                }
                            }
                            break;
                        case 3:
                            // Absoluto

                            if (ep[i] == 0)
                            {
                                eyn[i] = 0;
                            }
                            else
                            {
                                eyn[i] = yl - yc;
                            }
                            break;
                        case 4:
                            // Binario

                            if (ep[i] == 0)
                            {
                                eyn[i] = 0;
                            }
                            else
                            {
                                if (ep[i] == 1)
                                {
                                    // Emisión

                                    eyn[i] = 1;
                                }
                                else
                                {
                                    // Absorción

                                    eyn[i] = -1;
                                }
                            }
                            break;
                        case 5:
                            // Valores calculados

                            eyn[i] = eyc[i];
                            break;
                    }
                }
                if (tipo_datosY == 6)
                {
                    for (int i = 0; i < n; i++)
                    {
                        eyn[i] = ey[i] / rad_max_polinomio;
                    }
                }
                else if (tipo_datosY == 7)
                {
                    for (int i = 0; i < n; i++)
                    {
                        eyn[i] /= rad_max_polinomio;
                    }
                }
                LondaMaximo = Convert.ToDouble(ss[cdato]);
                espectros.Add(new EspectroAnalizado(grado, pro, corte, fcorte, distancia_movil, RYSQa, RYSQb, MY, MYc, AR, DEC, n, ep, ex, ey, eyc, eSDVMovil, eyn, LondaMaximo, WIEN / LondaMaximo));
                contador++;
                if (contador % 100 == 0)
                {
                    R_espectros.Text = string.Format("{0}-{1:N0}", n_fichero, contador);
                    Application.DoEvents();
                }
                for (int i = 0; i < n; i++)
                {
                    media += eyn[i];
                }
            }
            sr.Close();
            R_espectros.Text = string.Format("{0:N0}", espectros.Count);
            ndatos = espectros[0].n;
            MessageBox.Show(string.Format("Tipo datos Y {0} Media: {1}", tipo_datosY, media / ndatos / espectros.Count));
            Clipboard.SetText(string.Format("Tipo datos Y {0} Media: {1}", tipo_datosY, media / ndatos / espectros.Count));
        }
        private void LeeCentroides_Click(object sender, EventArgs e)
        {
            OpenFileDialog ficherolectura = new OpenFileDialog()
            {
                Filter = "CVS |*.csv|TXT |*.txt|TODO |*.*",
                FilterIndex = 1,
                RestoreDirectory = false,
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false
            };
            if (ficherolectura.ShowDialog() == DialogResult.OK)
            {
                Habilitar(false);
                LeeFicheroCentroides(ficherolectura.FileName);
                Habilitar(true);
            }
        }
        private void LeeFicheroCentroides(string fichero)
        {
            FileStream fsfte = new FileStream(fichero, FileMode.Open, FileAccess.Read, FileShare.Read);
            if (fsfte != null)
            {
                R_Fichero_Centroides.Text = FICHERO_CENTROIDES = fichero;
                StreamReader sr = new StreamReader(fsfte);
                string linea;
                string[] ss;

                // Primera línea con el número de clusters y datos

                linea = sr.ReadLine();
                ss = linea.Split(';');
                if (ss.Length != 2)
                {
                    MessageBox.Show(string.Format("El fichero {0} no tiene el formato esperado", fichero), "Leer fichero de centroides");
                    return;
                }
                int ncentroides = Convert.ToInt32(ss[0]);
                V_nclusters.Text = ncentroides.ToString();
                nclusters = Convert.ToInt32(V_nclusters.Text.Trim());
                if (nclusters < 1)
                {
                    MessageBox.Show("El número de clusters debe ser mayor que cero");
                    return;
                }
                int n_datos = Convert.ToInt32(ss[1]);
                if (espectros != null && espectros.Count > 0)
                {
                    if (n_datos != ndatos)
                    {
                        MessageBox.Show("Aviso. El número de datos de los centroides no coincide con el de los espectros");
                    }
                }

                // Segunda línea con el número de espectros en cada centroide

                linea = sr.ReadLine();
                ss = linea.Split(';');
                if (ss.Length != ncentroides + 1)
                {
                    MessageBox.Show(string.Format("El fichero {0} no tiene el formato esperado", fichero), "Leer fichero de centroides");
                    return;
                }
                centroides_leidos_num_esp = new int[ncentroides];
                for (int i = 0; i < ncentroides; i++)
                {
                    centroides_leidos_num_esp[i] = Convert.ToInt32(ss[1 + i]);
                }

                // 'ndatos' filas con 'x' y tantas 'y' como 'ncentroides = nclusters'

                centroides_leidos = new double[ncentroides][];
                for (int i = 0; i < ncentroides; i++)
                {
                    centroides_leidos[i] = new double[n_datos];
                }
                x_centroides_leidos = new double[n_datos];
                int ind_dato = 0;
                while (!sr.EndOfStream)
                {
                    linea = sr.ReadLine();
                    ss = linea.Split(';');
                    x_centroides_leidos[ind_dato] = Convert.ToDouble(ss[0]);
                    for (int ind_cluster = 0; ind_cluster < ncentroides; ind_cluster++)
                    {
                        centroides_leidos[ind_cluster][ind_dato] = Convert.ToDouble(ss[1 + ind_cluster]);
                    }
                    ind_dato++;
                }
                sr.Close();
                Console.Beep();
                V_modoIniciar.Text = "0";
            }
        }
        private void LeerClasificacion_Click(object sender, EventArgs e)
        {
            if (espectros == null || espectros.Count == 0)
            {
                MessageBox.Show("No hay datos de espectros analizados");
                return;
            }
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
                ndatos = espectros[0].n;
                clasificacionML = false;
                IniciaTablaClusters();
                ListaEvolucion.Items.Clear();
                ListaIntentos.Items.Clear();
                R_mejorMed.Text = string.Empty;
                R_mejorDE.Text = string.Empty;
                R_mejorDB.Text = string.Empty;
                R_mejorDET.Text = string.Empty;
                for (int i = 0; i < N_CAJAS; i++)
                {
                    mejorcaja[i] = null;
                    mejor_intento[i] = 1;
                    mejor_estimador[i] = double.MaxValue;
                }
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
                Habilitar(false);
                sr.ReadLine();
                linea = sr.ReadLine();
                string[] ss = linea == null ? Array.Empty<string>() : linea.Split(';');
                nclusters = ss.Length - 1;
                if (nclusters < 1)
                {
                    MessageBox.Show("El número de clusters debe ser mayor que cero");
                    return;
                }
                int ncaja = 0;
                int ind_espectro;
                int iemax = espectros.Count - 1;
                while (!sr.EndOfStream)
                {
                    linea = sr.ReadLine();
                    if (linea == null) break;
                    ss = linea.Split(';');
                    if (ss.Length == 1)
                    {
                        // Fin de caja

                        for (int i = 0; i < nclusters; i++)
                        {
                            mejorcaja[ncaja].clusters[i].n = mejorcaja[ncaja].clusters[i].ind_f_espectros.Count;
                            mejorcaja[ncaja].orden[i] = i;
                        }
                        CalculaCentroides(mejorcaja[ncaja]);
                        EstadisticaClusters(mejorcaja[ncaja]);
                        EspectrosExtremos(mejorcaja[ncaja]);
                        OrdenaClusters(mejorcaja[ncaja]);
                        ncaja++;
                        sr.ReadLine();
                        sr.ReadLine();
                    }
                    else
                    {
                        if (mejorcaja[ncaja] == null) mejorcaja[ncaja] = new Form1.Caja(ncaja, nclusters, espectros.Count, ndatos, 0);
                        for (int i = 0; i < nclusters; i++)
                        {
                            if (ss[i + 1].Trim().Length > 0)
                            {
                                ind_espectro = Convert.ToInt32(ss[i + 1]) - 1;
                                if (ind_espectro > iemax)
                                {
                                    MessageBox.Show("La clasificación coniene números de espectro mayores que espectros leidos");
                                    Habilitar(true);
                                    return;
                                }
                                mejorcaja[ncaja].clusters[i].ind_f_espectros.Add(ind_espectro);
                            }
                        }
                    }
                }
                sr.Close();
                if (mejorcaja[ncaja] != null)
                {
                    for (int i = 0; i < nclusters; i++)
                    {
                        mejorcaja[ncaja].clusters[i].n = mejorcaja[ncaja].clusters[i].ind_f_espectros.Count;
                        mejorcaja[ncaja].orden[i] = i;
                    }
                    CalculaCentroides(mejorcaja[ncaja]);
                    EstadisticaClusters(mejorcaja[ncaja]);
                    EspectrosExtremos(mejorcaja[ncaja]);
                    OrdenaClusters(mejorcaja[ncaja]);
                }
                if (mejorcaja[0] != null)
                {
                    omite_cambio_seleccion = true;
                    Sel_media.Checked = true;
                    omite_cambio_seleccion = false;
                    CambiaCaja(0);
                }
                Habilitar(true);
            }
        }
        private void LeerCromosomas_Click(object sender, EventArgs e)
        {
            OpenFileDialog ficherolectura = new()
            {
                Filter = "BIN |*.bin|TODO |*.*",
                FilterIndex = 1,
                RestoreDirectory = false,
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false
            };
            if (ficherolectura.ShowDialog() == DialogResult.OK)
            {
                Habilitar(false);
                LeeCromosomas(ficherolectura.FileName);
                Console.Beep();
                Habilitar(true);
            }
        }
        private void LeeCromosomas(string fichero)
        {
            FileStream fsfc = new FileStream(fichero, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryReader sr = new BinaryReader(fsfc);
            int n;
            int n1;
            int n2;
            double valor;
            double[][] genes;
            cromosomas.Clear();
            n = sr.ReadInt32();
            n1 = sr.ReadInt32();
            V_nclusters.Text = n1.ToString();
            n2 = sr.ReadInt32();
            for (int k = 0; k < n; k++)
            {
                valor = sr.ReadDouble();
                genes = new double[n1][];
                for (int i = 0; i < n1; i++)
                {
                    genes[i] = new double[n2];
                    for (int j = 0; j < n2; j++)
                    {
                        genes[i][j] = sr.ReadDouble();
                    }
                }
                cromosomas.Add(new Cromosoma(genes, valor));
            }
            sr.Close();
        }
        private void SalvaCaso_Click(object sender, EventArgs e)
        {
            if (espectros == null || espectros.Count == 0)
            {
                MessageBox.Show("No hay datos de espectros analizados");
                return;
            }
            FolderBrowserDialog carpeta = new FolderBrowserDialog
            {
                ShowNewFolderButton = true,
                SelectedPath = senda_resultados
            };
            if (carpeta.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(carpeta.SelectedPath))
            {
                string nombre = Path.GetFileNameWithoutExtension(R_Fichero_Clusters.Text);
                string opciones = string.Format("{0,5}_{1,5}_{2,3}_{3,6}_{4,6}_{5,1}_{6,1}_{7,1}_{8,1}_{9,3}_{10,2}_{11,1}_{12,1}_{13,1}_{14,5}_{15,1}_{16,3}_{17,3}_{18,1}_{19,1}_{20,3}_{21}",
                    calcula_desde,
                    calcula_hasta,
                    nclusters,
                    V_semilla.Text.Trim(),
                    pases_realizados,
                    modo_iniciar,
                    tipo_distancia,
                    tipo_datosY,
                    normaliza_centroide ? 1 : 0,
                    pu_podar * 100,
                    minimo_podar == 0 ? 0 : 1 / minimo_podar,
                    modo_podar ? 1 : 0,
                    modo_genetico ? 1 : 0,
                    Convert.ToInt32(V_generaciones.Text.Trim()),
                    Convert.ToInt32(V_minimizaGenetico.Text.Trim()),
                    mutar ? 1 : 0,
                    Convert.ToInt32(V_ppMutar.Text.Trim()),
                    Convert.ToInt32(V_mutarCuanto.Text.Trim()),
                    modo_forzar ? 1 : 0,
                    Convert.ToInt32(V_modoCentroide.Text.Trim()),
                    nhilos,
                    nombre).Replace(' ', '_');
                string caso = Path.Combine(carpeta.SelectedPath, opciones);
                if (Directory.Exists(caso))
                {
                    if (MessageBox.Show("La carpeta ya existe y se perderá todo su contenido. ¿Sobreescribirla?", "Salvar caso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }
                    Directory.Delete(caso, true);
                }
                Directory.CreateDirectory(caso);
                Settings.Default.senda_resultados = senda_resultados = carpeta.SelectedPath;
                Settings.Default.Save();
                FileStream fsfc;
                Habilitar(false);
                string s = R_Fichero_Clusters.Text;
                R_Fichero_Clusters.ForeColor = Color.Blue;
                R_Fichero_Clusters.Text = "Centroides";
                R_Fichero_Clusters.Enabled = true;
                Application.DoEvents();
                for (int i = 0; i < N_CAJAS; i++)
                {
                    if (mejorcaja[i] != null)
                    {
                        fsfc = new FileStream(Path.Combine(caso, string.Format("Centroides_{0}.csv", i)), FileMode.Create, FileAccess.Write, FileShare.Read);
                        FicheroCentroides(fsfc, mejorcaja[i]);
                    }
                }
                R_Fichero_Clusters.Text = "Resumen";
                Application.DoEvents();
                for (int i = 0; i < N_CAJAS; i++)
                {
                    if (mejorcaja[i] != null)
                    {
                        fsfc = new FileStream(Path.Combine(caso, string.Format("Resumen_{0}.csv", i)), FileMode.Create, FileAccess.Write, FileShare.Read);
                        FicheroResumen(fsfc, i);
                    }
                }
                R_Fichero_Clusters.Text = "Imágenes distancias";
                Application.DoEvents();
                Form5 panel_distancias = new Form5();
                for (int i = 0; i < N_CAJAS; i++)
                {
                    if (mejorcaja[i] != null)
                    {
                        ImagenesDistancias(i, mejorcaja[i], panel_distancias);
                        panel_distancias.img.Save(Path.Combine(caso, string.Format("Distancias_{0}.png", i)), ImageFormat.Png);
                    }
                }
                R_Fichero_Clusters.Text = "Clasificación";
                Application.DoEvents();
                fsfc = new FileStream(Path.Combine(caso, "Clasificacion.csv"), FileMode.Create, FileAccess.Write, FileShare.Read);
                FicheroClasificacion(fsfc);
                R_Fichero_Clusters.Text = "Imágenes centroides";
                Application.DoEvents();
                Form4 panel_centroides = new Form4();
                panel_centroides.principal = this;
                for (int i = 0; i < N_CAJAS; i++)
                {
                    if (mejorcaja[i] != null)
                    {
                        for (int k = 0; k < nclusters; k++)
                        {
                            if (mejorcaja[i].clusters[k].n > 0)
                            {
                                ImagenesCentroides(mejorcaja[i], panel_centroides, k);
                                panel_centroides.img.Save(Path.Combine(caso, string.Format("Centroides_{0}_Clus_{1}.png", i, k + 1)), ImageFormat.Png);
                            }
                        }

                        // Todos superpuestos

                        ImagenesCentroides(mejorcaja[i], panel_centroides, -1);
                        panel_centroides.img.Save(Path.Combine(caso, string.Format("Centroides_{0}_Todos.png", i)), ImageFormat.Png);
                    }
                }
                R_Fichero_Clusters.Text = "Imágenes temperaturas";
                Application.DoEvents();
                Form6 panel_temperaturas = new Form6();
                panel_temperaturas.principal = this;
                panel_temperaturas.evitar = true;
                panel_temperaturas.Todos.Checked = false;
                panel_temperaturas.evitar = false;
                for (int i = 0; i < N_CAJAS; i++)
                {
                    if (mejorcaja[i] != null)
                    {
                        for (int k = 0; k < nclusters; k++)
                        {
                            if (mejorcaja[i].clusters[k].n > 0)
                            {
                                ImagenesTemperatura(mejorcaja[i], panel_temperaturas, k, 50);
                                panel_temperaturas.img.Save(Path.Combine(caso, string.Format("Temperatura_{0}_Clus_{1}.png", i, k + 1)), ImageFormat.Png);
                            }
                        }

                        // Todos superpuestos

                        ImagenesTemperatura(mejorcaja[i], panel_temperaturas, -1, 50);
                        panel_temperaturas.img.Save(Path.Combine(caso, string.Format("Temperatura_{0}_Todos.png", i)), ImageFormat.Png);
                    }
                }
                R_Fichero_Clusters.Text = "Ejemplos espectro-centroide";
                Application.DoEvents();
                MuestraForm2();
                int ind_cluster;
                double ymin;
                double ymax;
                int ind_mejor_espectro;
                int ind_peor_espectro;
                for (int i = 0; i < N_CAJAS; i++)
                {
                    if (mejorcaja[i] != null)
                    {
                        for (int k = 0; k < nclusters; k++)
                        {
                            ind_cluster = mejorcaja[i].orden[k];
                            if (mejorcaja[i].clusters[ind_cluster].n > 0)
                            {
                                ymin = double.MaxValue;
                                ymax = double.MinValue;
                                ind_mejor_espectro = mejorcaja[i].clusters[ind_cluster].ind_min;
                                ind_peor_espectro = mejorcaja[i].clusters[ind_cluster].ind_max;
                                for (int iy = 0; iy < espectros[ind_mejor_espectro].n; iy++)
                                {
                                    ymin = Math.Min(ymin, mejorcaja[i].centroides[ind_cluster][iy]);
                                    ymax = Math.Max(ymax, mejorcaja[i].centroides[ind_cluster][iy]);
                                    ymin = Math.Min(ymin, espectros[ind_mejor_espectro].yn[iy]);
                                    ymax = Math.Max(ymax, espectros[ind_mejor_espectro].yn[iy]);
                                    ymin = Math.Min(ymin, espectros[ind_peor_espectro].yn[iy]);
                                    ymax = Math.Max(ymax, espectros[ind_peor_espectro].yn[iy]);
                                }

                                // El mejor espectro

                                panel_img.Dibuja(mejorcaja[i].centroides[ind_cluster], espectros, ind_mejor_espectro, null, null, ymin, ymax, false);
                                panel_img.img.Save(Path.Combine(caso, string.Format("Ej_{0}_Clus_{1}_mejor.png", i, k + 1)), ImageFormat.Png);

                                // El peor espectro

                                if (ind_mejor_espectro != ind_peor_espectro)
                                {
                                    panel_img.Dibuja(mejorcaja[i].centroides[ind_cluster], espectros, ind_peor_espectro, null, null, ymin, ymax, false);
                                    panel_img.img.Save(Path.Combine(caso, string.Format("Ej_{0}_Clus_{1}_peor.png", i, k + 1)), ImageFormat.Png);
                                }
                            }
                        }
                    }
                }
                SelEspectroCluster();
                Console.Beep();
                R_Fichero_Clusters.ForeColor = SystemColors.ControlText;
                R_Fichero_Clusters.Text = s;
                Habilitar(true);
            }
        }
        private void SalvaClusters_Click(object sender, EventArgs e)
        {
            SalvaResumenClusters();
        }
        private void SalvaClasificacion_Click(object sender, EventArgs e)
        {
            if (espectros == null || espectros.Count == 0)
            {
                MessageBox.Show("No hay datos de espectros analizados");
                return;
            }
            SaveFileDialog ficheroescritura = new SaveFileDialog()
            {
                Filter = "CSV |*.csv|TODO |*.*",
                FilterIndex = 1
            };
            if (ficheroescritura.ShowDialog() == DialogResult.OK)
            {
                FileStream fsfc = new FileStream(ficheroescritura.FileName, FileMode.Create, FileAccess.Write, FileShare.Read);
                Habilitar(false);
                FicheroClasificacion(fsfc);
                Console.Beep();
                Habilitar(true);
            }
        }
        private void SalvaCromosomas_Click(object sender, EventArgs e)
        {
            if (cromosomas == null || cromosomas.Count == 0)
            {
                MessageBox.Show("No hay cromosomas que salvar.");
                return;
            }
            SaveFileDialog ficheroescritura = new SaveFileDialog()
            {
                Filter = "BIN |*.bin|TODO |*.*",
                FilterIndex = 1
            };
            if (ficheroescritura.ShowDialog() == DialogResult.OK)
            {
                Habilitar(false);
                EscribeCromosomas(ficheroescritura.FileName);
                Console.Beep();
                Habilitar(true);
            }
        }
        private void EscribeCromosomas(string fichero)
        {
            FileStream fsfc = new(fichero, FileMode.Create, FileAccess.Write, FileShare.Read);
            if (fsfc != null)
            {
                BinaryWriter sw = new BinaryWriter(fsfc);
                int n1 = cromosomas[0].genes.GetLength(0);
                int n2 = cromosomas[0].genes[0].Length;
                sw.Write(cromosomas.Count);
                sw.Write(n1);
                sw.Write(n2);
                foreach (Cromosoma c in cromosomas)
                {
                    sw.Write(c.valor);
                    for (int i = 0; i < n1; i++)
                    {
                        for (int j = 0; j < n2; j++)
                        {
                            sw.Write(c.genes[i][j]);
                        }
                    }
                }
                sw.Close();
            }
        }
        private void DistanciasCentroides_Click(object sender, EventArgs e)
        {
            if (espectros == null || espectros.Count == 0)
            {
                MessageBox.Show("No hay datos de espectros analizados");
                return;
            }
            int mejor = MejorCajaSeleccionada();
            Caja mc = mejorcaja[mejor];
            if (mc == null)
            {
                MessageBox.Show("No hay una clasificación para este concepto");
                return;
            }
            Form5 panel_distancias = new Form5();
            ImagenesDistancias(mejor, mc, panel_distancias);
            panel_distancias.Show();
        }
        private void VerCentroides_Click(object sender, EventArgs e)
        {
            Caja caja = null;
            if (espectros != null && espectros.Count > 0)
            {
                int mejor = MejorCajaSeleccionada();
                caja = mejorcaja[mejor];
            }
            Form4 panel_centroides = new Form4
            {
                principal = this
            };
            panel_centroides.Show();
            ImagenesCentroides(caja, panel_centroides, 0);
        }
        private void VerTemperatura_Click(object sender, EventArgs e)
        {
            if (espectros != null && espectros.Count > 0)
            {
                int mejor = MejorCajaSeleccionada();
                Caja caja = mejorcaja[mejor];
                if (caja != null)
                {
                    Form6 panel_temperaturas = new Form6
                    {
                        principal = this
                    };
                    panel_temperaturas.Show();
                    ImagenesTemperatura(caja, panel_temperaturas, 0, 50);
                }
            }
        }
        private void SalvaResumenClusters()
        {
            if (espectros == null || espectros.Count == 0)
            {
                MessageBox.Show("No hay datos de espectros analizados");
                return;
            }
            SaveFileDialog ficheroescritura = new SaveFileDialog()
            {
                Filter = "CSV |*.csv|TODO |*.*",
                FilterIndex = 1
            };
            if (ficheroescritura.ShowDialog() == DialogResult.OK)
            {
                FileStream fsfc = new FileStream(ficheroescritura.FileName, FileMode.Create, FileAccess.Write, FileShare.Read);
                Habilitar(false);
                int mejor = MejorCajaSeleccionada();
                FicheroResumen(fsfc, mejor);
                Console.Beep();
                Habilitar(true);
            }
        }
        private void TipoDatosY_TextChanged(object sender, EventArgs e)
        {
            if (espectros == null || espectros.Count == 0)
            {
                return;
            }
            Habilitar(false);
            if (V_tipoDatosY.Text.Trim().Length == 0) V_tipoDatosY.Text = "0";
            tipo_datosY = Convert.ToInt32(V_tipoDatosY.Text.Trim());
            double margen;
            double rad_max_polinomio;
            double media = 0;
            int n = espectros[0].n;
            foreach (EspectroAnalizado es in espectros)
            {
                switch (tipo_datosY)
                {
                    case 0:
                        // Datos

                        for (int i = 0; i < es.n; i++)
                        {
                            es.yn[i] = es.y[i];
                        }
                        break;
                    case 1:
                    case 7:
                        // Valores calculados dentro del corte

                        for (int i = 0; i < es.n; i++)
                        {
                            if (es.distancia_movil > 0)
                            {
                                if (es.pro)
                                {
                                    margen = es.fcorte * es.SDVMovil[i] * 2 / (es.MY + es.MYc) * es.yc[i];
                                }
                                else
                                {
                                    margen = es.fcorte * es.SDVMovil[i];
                                }
                            }
                            else
                            {
                                if (es.pro)
                                {
                                    margen = es.corte * es.yc[i];
                                }
                                else
                                {
                                    margen = es.corte;
                                }
                            }
                            if (Math.Abs(es.y[i] - es.yc[i]) < margen)
                            {
                                es.yn[i] = es.yc[i];
                            }
                            else
                            {
                                es.yn[i] = es.y[i];
                            }
                        }
                        break;
                    case 2:
                        // Relativo

                        for (int i = 0; i < es.n; i++)
                        {
                            if (es.pico[i] == 0)
                            {
                                es.yn[i] = 0;
                            }
                            else
                            {
                                if (es.yc[i] <= 0)
                                {
                                    es.yn[i] = 0;
                                }
                                else
                                {
                                    es.yn[i] = (es.y[i] - es.yc[i]) / es.yc[i];
                                    if (es.yn[i] > 1) es.yn[i] = 1;
                                    if (es.yn[i] < -1) es.yn[i] = -1;
                                }
                            }
                        }
                        break;
                    case 3:
                        // Absoluto

                        for (int i = 0; i < es.n; i++)
                        {
                            if (es.pico[i] == 0)
                            {
                                es.yn[i] = 0;
                            }
                            else
                            {
                                es.yn[i] = es.y[i] - es.yc[i];
                            }
                        }
                        break;
                    case 4:
                        // Binario

                        for (int i = 0; i < es.n; i++)
                        {
                            if (es.pico[i] == 0)
                            {
                                es.yn[i] = 0;
                            }
                            else if (es.pico[i] == 1)
                            {
                                // Emisión

                                es.yn[i] = 1;
                            }
                            else
                            {
                                // Absorción

                                es.yn[i] = -1;
                            }
                        }
                        break;
                    case 5:
                        // Valores calculados

                        for (int i = 0; i < es.n; i++)
                        {
                            es.yn[i] = es.yc[i];
                        }
                        break;
                    case 6:
                        rad_max_polinomio = double.MinValue;
                        for (int i = 0; i < es.n; i++)
                        {
                            rad_max_polinomio = Math.Max(rad_max_polinomio, es.yc[i]);
                        }
                        for (int i = 0; i < es.n; i++)
                        {
                            es.yn[i] = es.y[i] / rad_max_polinomio;
                        }
                        break;
                }
                if (tipo_datosY == 7)
                {
                    rad_max_polinomio = double.MinValue;
                    for (int i = 0; i < es.n; i++)
                    {
                        rad_max_polinomio = Math.Max(rad_max_polinomio, es.yc[i]);
                    }
                    for (int i = 0; i < es.n; i++)
                    {
                        es.yn[i] /= rad_max_polinomio;
                    }
                }
                for (int i = 0; i < es.n; i++)
                {
                    media += es.yn[i];
                }
            }
            MessageBox.Show(string.Format("Tipo datos Y {0} Media: {1}", tipo_datosY, media / n / espectros.Count));
            Clipboard.SetText(string.Format("Tipo datos Y {0} Media: {1} CC", tipo_datosY, media / n / espectros.Count));
            Habilitar(true);
        }
        private void SalvaEspectrosAnalizados_Click(object sender, EventArgs e)
        {
            if (espectros == null || espectros.Count == 0)
            {
                MessageBox.Show("No hay datos de espectros analizados");
                return;
            }
            int n = Convert.ToInt32(V_nclusters.Text.Trim());
            if (n < 1) return;
            SaveFileDialog ficheroescritura = new SaveFileDialog()
            {
                Filter = "CSV |*.csv|TODO |*.*",
                FilterIndex = 1
            };
            if (ficheroescritura.ShowDialog() == DialogResult.OK)
            {
                FileStream fsfc = new FileStream(ficheroescritura.FileName, FileMode.Create, FileAccess.Write, FileShare.Read);
                Habilitar(false);
                int mejor = MejorCajaSeleccionada();
                Caja mc = mejorcaja[mejor];
                FicheroEspectros(fsfc, mc);
                Habilitar(true);
                Console.Beep();
            }
        }
        private void SalvaCentroides_Click(object sender, EventArgs e)
        {
            if (espectros == null || espectros.Count == 0)
            {
                MessageBox.Show("No hay datos de espectros analizados");
                return;
            }
            SaveFileDialog ficheroescritura = new SaveFileDialog()
            {
                Filter = "CSV |*.csv|TODO |*.*",
                FilterIndex = 1
            };
            if (ficheroescritura.ShowDialog() == DialogResult.OK)
            {
                FileStream fsfc = new FileStream(ficheroescritura.FileName, FileMode.Create, FileAccess.Write, FileShare.Read);
                Habilitar(false);
                int mejor = MejorCajaSeleccionada();
                Caja mc = mejorcaja[mejor];
                FicheroCentroides(fsfc, mc);
                Console.Beep();
                Habilitar(true);
            }
        }
        private void FicheroClasificacion(FileStream fsfc)
        {
            if (fsfc != null)
            {
                StreamWriter sw = new StreamWriter(fsfc, Encoding.UTF8);
                int ind;
                int nmax;
                Caja mc;
                for (int k = 0; k < N_CAJAS; k++)
                {
                    mc = mejorcaja[k];
                    if (mc == null) continue;
                    sw.WriteLine(rotulos[k]);
                    sw.Write(";");
                    for (int ind_cluster = 0; ind_cluster < nclusters - 1; ind_cluster++) sw.Write(string.Format("{0};", ind_cluster + 1));
                    sw.WriteLine(string.Format("{0}", nclusters));
                    sw.Write(";");
                    for (int ind_cluster = 0; ind_cluster < nclusters - 1; ind_cluster++) sw.Write(string.Format("{0};", "------"));
                    sw.WriteLine(string.Format("{0}", "------"));
                    nmax = int.MinValue;
                    for (int ind_cluster = 0; ind_cluster < nclusters; ind_cluster++)
                    {
                        if (nmax < mc.clusters[ind_cluster].n) nmax = mc.clusters[ind_cluster].n;
                    }
                    for (int ind_dato = 0; ind_dato < nmax; ind_dato++)
                    {
                        sw.Write(";");
                        for (int ind_cluster = 0; ind_cluster < nclusters - 1; ind_cluster++)
                        {
                            ind = mc.orden[ind_cluster];
                            if (ind_dato < mc.clusters[ind].n)
                            {
                                sw.Write(string.Format("{0};", 1 + mc.clusters[ind].ind_f_espectros[ind_dato]));
                            }
                            else
                            {
                                sw.Write(string.Format("{0};", string.Empty));
                            }
                        }
                        ind = mc.orden[nclusters - 1];
                        if (ind_dato < mc.clusters[ind].n)
                        {
                            sw.WriteLine(string.Format("{0}", 1 + mc.clusters[ind].ind_f_espectros[ind_dato]));
                        }
                        else
                        {
                            sw.WriteLine(string.Format("{0}", string.Empty));
                        }
                    }
                }
                sw.Close();
            }
        }
        private void FicheroResumen(FileStream fsfc, int mejor)
        {
            if (fsfc != null)
            {
                StreamWriter sw = new StreamWriter(fsfc, Encoding.UTF8);
                int ind;
                int indb;
                Caja mc = mejorcaja[mejor];
                if (mc == null) return;

                // Parámetros

                if (clasificacionML)
                {
                    sw.WriteLine(string.Format("Clasificación ML.NET. {0:N0} s", segundos));
                }
                else
                {
                    sw.WriteLine(string.Format("Clasificación propia. {0:N0} s", segundos));
                }
                sw.WriteLine(string.Format("Fichero de espectros;{0}", R_Fichero_Clusters.Text));
                sw.WriteLine(string.Format("Número de espectros;{0:N0}", espectros.Count));
                sw.WriteLine(string.Format("Número de datos-espectro;{0:N0}", espectros[0].n));
                int mi = Convert.ToInt32(V_modoIniciar.Text.Trim());
                if (mi == 0)
                {
                    sw.WriteLine(string.Format("Fichero de centroides;{0}", R_Fichero_Centroides.Text));
                }
                else
                {
                    sw.WriteLine();
                }
                sw.WriteLine(string.Format("Número de hilos;{0}", V_hilos.Text));
                sw.WriteLine(string.Format("Máximas iteraciones;{0:N0}", V_max_Iteraciones.Text));
                sw.WriteLine(string.Format("Número de clusters;{0:N0}", V_nclusters.Text));
                sw.WriteLine(string.Format("Semilla aleatoria;{0:N0}", V_semilla.Text));
                sw.WriteLine(string.Format("Pases realizados;{0:N0}", pases_realizados));
                sw.WriteLine(string.Format("Rango datos: inical;{0:N0}", V_desde.Text));
                sw.WriteLine(string.Format("Rango datos: final;{0:N0}", V_hasta.Text));
                string s;
                switch (Convert.ToInt32(V_tipoDatosY.Text.Trim()))
                {
                    case 0:
                        s = "Datos";
                        break;
                    case 1:
                        s = "Datos ajustados dentro de la banda de corte";
                        break;
                    case 2:
                        s = "Separación relativa";
                        break;
                    case 3:
                        s = "Separación absoluta";
                        break;
                    case 4:
                        s = "Separación binaria";
                        break;
                    case 5:
                        s = "Datos ajustados (polinomio)";
                        break;
                    case 6:
                        s = "Datos normalizados al máximo del polinomio";
                        break;
                    case 7:
                        s = "Datos ajustados dentro de la banda de corte, normalizados al máximo del polinomio";
                        break;
                    default:
                        s = "Error. Desconocida";
                        break;
                }
                sw.WriteLine(string.Format("Magnitud a comparar;{0}", s));
                sw.WriteLine(Convert.ToInt32(V_tipoDatosY.Text.Trim()) == 4 && V_normalizaCentroides.Checked ? "Normalizar centroides" : string.Empty);
                switch (Convert.ToInt32(V_tipoDistancia.Text.Trim()))
                {
                    case 0:
                        s = "Euclidiana";
                        break;
                    case 1:
                        s = "Manhattan";
                        break;
                    case 2:
                        s = "1-Covarianza";
                        break;
                    case 3:
                        s = "Euclidiana**2";
                        break;
                    case 4:
                        s = "Manhattan**2";
                        break;
                    default:
                        s = "Error";
                        break;
                }
                sw.WriteLine(string.Format("Distancia;{0}", s));
                switch (mi)
                {
                    case 0:
                        s = "Centroides predefinidos";
                        break;
                    case 1:
                        s = "Espectros asignados al azar";
                        break;
                    case 2:
                        s = "Centroides al azar";
                        break;
                    case 3:
                        s = "Primer Centroides al azar";
                        break;
                    case 4:
                        s = "Extraccion del más alejado";
                        break;
                    default:
                        s = "Kmeans++";
                        break;
                }
                sw.Write(string.Format("Clusters iniciales;{0}", s));
                if (mi == 0)
                {
                    if (FICHERO_CENTROIDES.Length == 0)
                    {
                        sw.Write(". Origen indeterminado");
                    }
                    else
                    {
                        sw.Write(string.Format(". Fichero centroides;{0}", FICHERO_CENTROIDES));
                    }
                }
                sw.WriteLine();
                sw.WriteLine(modo_podar ? "Excluir los espectros más dudosos en cada cluster" : string.Empty);
                sw.WriteLine(modo_podar ? string.Format("% espectros a excluir;{0:N0}%", 100 * pu_podar) : string.Empty);
                sw.WriteLine(modo_podar ? string.Format("Veces más;{0}", V_veces.Text) : string.Empty);
                if (modo_genetico)
                {
                    sw.WriteLine("Algoritmo genético");
                    sw.WriteLine(string.Format("Número de generaciones;{0:N0}", generaciones_realizadas));
                    if (mutar)
                    {
                        sw.WriteLine("Mutaciones");
                        sw.WriteLine(string.Format("{0:f2}% de los genes", Convert.ToDouble(V_ppMutar.Text.Trim())));
                        sw.WriteLine(string.Format("{0:f2}% de su valor", Convert.ToDouble(V_mutarCuanto.Text.Trim())));
                        switch (minimiza_genetico)
                        {
                            case 0:
                                sw.WriteLine("Minimizar; media");
                                break;
                            case 1:
                                sw.WriteLine("Minimizar; desviación estándar");
                                break;
                            case 2:
                                sw.WriteLine("Minimizar; índice Davies Bouldin");
                                break;
                            default:
                                sw.WriteLine("Minimizar; des.estándar temperatura");
                                break;
                        }
                    }
                    else
                    {
                        sw.WriteLine();
                        sw.WriteLine();
                        sw.WriteLine();
                        sw.WriteLine();
                    }
                }
                else
                {
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine();
                }
                sw.WriteLine(modo_forzar ? "Centroides sobre espectros" : string.Empty);
                if (V_modoCentroide.Text.Equals("0")) sw.WriteLine("Centroides como media");
                else sw.WriteLine("Centroides como valor máximo");
                sw.WriteLine(V_silhouette.Checked ? string.Empty : "Sin calcular índice silhouette");
                sw.WriteLine(string.Format("Max retroceso en la media;{0:f2}%", 100 * Convert.ToDouble(V_max_empeora_md.Text.Trim())));
                sw.WriteLine(string.Format("Mejora mínima en la media;{0:f2}%", 100 * Convert.ToDouble(V_min_mejora_md.Text.Trim())));
                sw.WriteLine(string.Format("Max retroceso en la D.est;{0:f2}%", 100 * Convert.ToDouble(V_max_empeora_de.Text.Trim())));
                sw.WriteLine(string.Format("Mejora mínima en la D.est;{0:f2}%", 100 * Convert.ToDouble(V_min_mejora_de.Text.Trim())));
                sw.WriteLine();
                sw.WriteLine("--------------------------------");

                sw.WriteLine(rotulos[mejor]);
                sw.WriteLine(";número;distancia;;;;;desviación;grupo más;índice;L.o.Max;tenperatura K;");
                sw.WriteLine("grupo;espectros;ind_min;d_min;ind_max;d_max;media;estándar;próximo;DB;Å;media;d.estándar T;val.med;%min;%max;%media");

                // Ordenada de mayor a menor por el número de espectros en el cluster

                EstadisticaClusters(mc);
                if (nclusters > 0)
                {
                    double diferencia;
                    double des_estandar = 0;
                    int nmedia = espectros.Count / nclusters;
                    double vm_total = 0;
                    double vm_centroide;
                    int cmc;
                    for (int i = 0; i < nclusters; i++)
                    {
                        ind = mc.orden[i];
                        sw.Write(string.Format("{0};", i + 1));
                        sw.Write(string.Format("{0};", mc.clusters[ind].n));
                        diferencia = mc.clusters[ind].n - nmedia;
                        des_estandar += diferencia * diferencia;
                        sw.Write(string.Format("{0};", mc.clusters[ind].ind_min + 1));
                        sw.Write(string.Format("{0};", mc.clusters[ind].distancia_min));
                        sw.Write(string.Format("{0};", mc.clusters[ind].ind_max + 1));
                        sw.Write(string.Format("{0};", mc.clusters[ind].distancia_max));
                        sw.Write(string.Format("{0};", mc.clusters[ind].distancia_media));
                        sw.Write(string.Format("{0};", mc.clusters[ind].desviacion_estandar));
                        cmc = OrdenCaja(mc, ClusterMasCercano(mc, ind));
                        sw.Write(string.Format("{0};", cmc + 1));
                        sw.Write(string.Format("{0};", mc.estadistica.db.R[i]));
                        sw.Write(string.Format("{0};", mc.clusters[ind].londaMaximo));
                        sw.Write(string.Format("{0};", mc.clusters[ind].temperatura));
                        sw.Write(string.Format("{0};", mc.clusters[ind].de_temperatura));

                        // Radiación media del centroide

                        vm_centroide = 0;
                        for (int ic = 0; ic < ndatos; ic++)
                        {
                            vm_centroide += Math.Abs(mc.centroides[ind][ic]);
                        }
                        vm_total += vm_centroide * mc.clusters[ind].n;
                        sw.Write(string.Format("{0};", vm_centroide / ndatos));
                        vm_centroide /= 100;
                        sw.Write(string.Format("{0};", vm_centroide == 0 ? 0 : mc.clusters[ind].distancia_min / vm_centroide));
                        sw.Write(string.Format("{0};", vm_centroide == 0 ? 0 : mc.clusters[ind].distancia_max / vm_centroide));
                        sw.WriteLine(string.Format("{0}", vm_centroide == 0 ? 0 : mc.clusters[ind].distancia_media / vm_centroide));
                    }
                    des_estandar = Math.Sqrt(des_estandar / nclusters);
                    sw.WriteLine(string.Format(";{0:N1};;;;;{1};{2};;{3};;{4};{5};{6};;;{7}",
                        des_estandar,
                        mc.estadistica.d_media_g,
                        mc.estadistica.d_estandar_g,
                        mc.estadistica.db.indice,
                        mc.estadistica.temperatura_g,
                        mc.estadistica.de_temperatura_g,
                        vm_total / ndatos / espectros.Count,
                        mc.estadistica.d_media_g * 100 * espectros.Count / vm_total));
                    sw.WriteLine();
                    sw.WriteLine("DBI;DBI ponderado;Silhouette");
                    sw.WriteLine(string.Format("{0};{1};{2}", mc.estadistica.db.indice, mc.estadistica.db.indice_ponderado, mc.estadistica.si));
                }

                // Distancias entre centroides

                sw.WriteLine("Distancias entre centroides");
                sw.Write("cluster");
                for (int i = 0; i < nclusters; i++)
                {
                    sw.Write(string.Format(";{0}", i + 1));
                }
                sw.WriteLine();
                for (int i = 0; i < nclusters; i++)
                {
                    sw.Write(string.Format("{0}", i + 1));
                    ind = mc.orden[i];
                    for (int j = 0; j < nclusters; j++)
                    {
                        indb = mc.orden[j];
                        sw.Write(string.Format(";{0:e4}", Distancia(mc.centroides[ind], mc.centroides[indb])));
                    }
                    sw.WriteLine();
                }
                sw.WriteLine("--------------------------------");
                sw.WriteLine("Resultados " + rotulos[mejor]);
                sw.WriteLine("--------------------------------");
                if (ListaIntentos.Items.Count > 0)
                {
                    sw.WriteLine("Clasificación en cada pase");
                    sw.WriteLine("--------------------------------");
                    sw.WriteLine("media;destandar;indiceDB;de.temperatura");
                    for (int i = 0; i < ListaIntentos.Items.Count; i++)
                    {
                        sw.WriteLine(ListaIntentos.Items[i].ToString().Trim().Replace("    ", " ").Replace("   ", " ").Replace("  ", " ").Replace(' ', ';'));
                    }
                    sw.WriteLine("--------------------------------");
                }
                sw.WriteLine("Evolución mejor clasificación");
                sw.WriteLine("--------------------------------");
                if (mc.evolucion.Count > 0)
                {
                    sw.WriteLine(rotulos[mejor]);
                    sw.WriteLine("media;destandar;indiceDB");
                    for (int i = 0; i < mc.evolucion.Count; i++)
                    {
                        sw.WriteLine(mc.evolucion[i].ToString().Trim().Replace("    ", " ").Replace("   ", " ").Replace("  ", " ").Replace(' ', ';'));
                    }
                    sw.WriteLine("--------------------------------");
                }
                sw.WriteLine();
                sw.WriteLine("Evolución genética");
                sw.WriteLine("--------------------------------");
                sw.WriteLine("media;destandar;indiceDB");
                for (int i = 0; i < evolucion_genetica.Count; i++)
                {
                    sw.WriteLine(evolucion_genetica[i].ToString().Trim().Replace("    ", " ").Replace("   ", " ").Replace("  ", " ").Replace(' ', ';'));
                }
                sw.WriteLine("--------------------------------");
                sw.WriteLine();
                sw.WriteLine("cluster;n");
                for (int i = 0; i < nclusters; i++)
                {
                    ind = mc.orden[i];
                    sw.Write(string.Format("{0};{1};", i + 1, mc.clusters[ind].n));
                    if (mc.clusters[ind].n > 0)
                    {
                        for (int j = 0; j < mc.clusters[ind].n - 1; j++)
                        {
                            sw.Write(string.Format("{0};{1};", mc.clusters[ind].ind_f_espectros[j] + 1, mc.clusters[ind].distancias[j]));
                        }
                        sw.WriteLine(string.Format("{0};{1}", mc.clusters[ind].ind_f_espectros[mc.clusters[ind].n - 1] + 1, mc.clusters[ind].distancias[mc.clusters[ind].n - 1]));
                    }
                }

                // Centroides

                sw.WriteLine("Centroides");
                sw.WriteLine("--------------------------------");
                for (int ind_cluster = 0; ind_cluster < nclusters - 1; ind_cluster++)
                {
                    sw.Write(string.Format("{0};", ind_cluster + 1));
                }
                sw.WriteLine(string.Format("{0}", nclusters));
                for (int ind_dato = 0; ind_dato < ndatos; ind_dato++)
                {
                    for (int ind_cluster = 0; ind_cluster < nclusters - 1; ind_cluster++)
                    {
                        ind = mc.orden[ind_cluster];
                        sw.Write(string.Format("{0};", mc.centroides[ind][ind_dato]));
                    }
                    ind = mc.orden[nclusters - 1];
                    sw.WriteLine(string.Format("{0}", mc.centroides[ind][ind_dato]));
                }
                sw.WriteLine("--------------------------------");
                sw.Close();
            }
        }
        private void FicheroEspectros(FileStream fsfc, Caja mc)
        {
            if (fsfc != null)
            {
                R_espectros.Enabled = true;
                StreamWriter sw = new StreamWriter(fsfc);
                sw.WriteLine("cluster;iespectro;grado;pro;corte;fcorte;movil;R2a;R2b;mediaY;mediaYcal;AR;DEC;n;pico;x;y;yc;sdv;yn;pico;x;y;yc;sdv;yn;pico;x;y;yc;sdv;yn");
                EspectroAnalizado es;
                int ind_espectro;
                int contador = 0;
                for (int ind_cluster = 0; ind_cluster < nclusters; ind_cluster++)
                {
                    R_espectros.Text = string.Format("{0:N0}", contador);
                    Application.DoEvents();
                    for (int j = 0; j < mc.clusters[ind_cluster].n; j++)
                    {
                        ind_espectro = mc.clusters[ind_cluster].ind_f_espectros[j];
                        if (mc.cluster_espectro[ind_espectro] != ind_cluster)
                        {
                            MessageBox.Show(string.Format("Incoherencia en el cluster asignado al espectro {0} [{1} != {2}]", ind_espectro, mc.cluster_espectro[ind_espectro], ind_cluster));
                            return;
                        }
                        es = espectros[ind_espectro];
                        sw.Write(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};",
                            ind_cluster,
                            ind_espectro,
                            es.grado,
                            es.pro ? 1 : 0,
                            es.corte,
                            es.fcorte,
                            es.distancia_movil,
                            es.RYSQa,
                            es.RYSQb,
                            es.MY,
                            es.MYc,
                            es.AR,
                            es.DEC,
                            es.n
                            )); ;
                        int k;
                        for (k = 0; k < es.n - 1; k++)
                        {
                            sw.Write(string.Format("{0};{1};{2};{3};{4};{5};", es.pico[k], es.x[k], es.y[k], es.yc[k], es.SDVMovil[k], es.yn[k]));
                        }
                        if (es.n > 0) sw.WriteLine(string.Format("{0};{1};{2};{3};{4};{5}", es.pico[k], es.x[k], es.y[k], es.yc[k], es.SDVMovil[k], es.yn[k]));
                    }
                    contador += mc.clusters[ind_cluster].n;
                }
                R_espectros.Text = string.Format("{0:N0}", contador);
                sw.Close();
            }

        }
        private void FicheroCentroides(FileStream fsfc, Caja mc)
        {
            if (fsfc != null)
            {
                StreamWriter sw = new StreamWriter(fsfc);

                // Centroides

                sw.WriteLine(string.Format("{0};{1}", nclusters, ndatos));
                sw.Write(";");
                int ind_cluster;
                for (ind_cluster = 0; ind_cluster < nclusters - 1; ind_cluster++)
                {
                    sw.Write(string.Format("{0};", mc.clusters[ind_cluster].n));
                }
                sw.WriteLine(string.Format("{0}", mc.clusters[ind_cluster].n));
                for (int ind_dato = 0; ind_dato < ndatos; ind_dato++)
                {
                    sw.Write(string.Format("{0};", espectros[0].x[ind_dato]));
                    for (ind_cluster = 0; ind_cluster < nclusters - 1; ind_cluster++)
                    {
                        sw.Write(string.Format("{0};", mc.centroides[ind_cluster][ind_dato]));
                    }
                    sw.WriteLine(string.Format("{0}", mc.centroides[ind_cluster][ind_dato]));
                }
                sw.Close();
            }
        }
        private void ImagenesDistancias(int mejor, Caja mc, Form5 panel_distancias)
        {
            if (mc == null)
            {
                MessageBox.Show("No hay una clasificación para este concepto.");
                return;
            }
            int ind;
            int indb;
            int[] nespectros = new int[nclusters];
            double[] medias = new double[nclusters];
            double[,] distancias_centroides = new double[nclusters, nclusters];
            for (int i = 0; i < nclusters; i++)
            {
                ind = mc.orden[i];
                nespectros[i] = mc.clusters[ind].n;
                medias[i] = mc.clusters[ind].distancia_media;
                distancias_centroides[i, i] = 0;
                for (int j = i + 1; j < nclusters; j++)
                {
                    indb = mc.orden[j];
                    distancias_centroides[i, j] = distancias_centroides[j, i] = Distancia(mc.centroides[ind], mc.centroides[indb]);
                }
            }
            panel_distancias.Text = "Distancias entre centroides. Mejor " + rotulos[mejor];
            panel_distancias.nclusters = nclusters;
            panel_distancias.nespectros = nespectros;
            panel_distancias.medias = medias;
            panel_distancias.distancias = distancias_centroides;
            panel_distancias.Dibuja();
        }
        private void ImagenesCentroides(Caja mc, Form4 panel_centroides, int ind_cluster)
        {
            panel_centroides.ind_cluster = ind_cluster;
            int[] num_espectros_grupo = new int[nclusters];
            double[] temperatura_grupo = new double[nclusters];
            if (mc == null)
            {
                // Ver si se han leido centroides

                if (string.IsNullOrEmpty(FICHERO_CENTROIDES))
                {
                    MessageBox.Show("No hay datos que visualizar");
                    panel_centroides.Dispose();
                    return;
                }
                panel_centroides.y = centroides_leidos;
                for (int i = 0; i < nclusters; i++)
                {
                    num_espectros_grupo[i] = centroides_leidos_num_esp[i];
                    temperatura_grupo[i] = 0;
                }
                panel_centroides.x = x_centroides_leidos;
            }
            else
            {
                panel_centroides.y = new double[nclusters][];
                int ind_c;
                for (int i = 0; i < nclusters; i++)
                {
                    ind_c = mc.orden[i];
                    panel_centroides.y[i] = mc.centroides[ind_c];
                    num_espectros_grupo[i] = mc.clusters[ind_c].n;
                    temperatura_grupo[i] = mc.clusters[ind_c].temperatura;
                }
                panel_centroides.x = espectros[0].x;
            }
            panel_centroides.Escala(num_espectros_grupo, temperatura_grupo, ind_cluster != -1);
            panel_centroides.Dibuja();
        }
        private void ImagenesTemperatura(Caja mc, Form6 panel_temperaturas, int ind_cluster, int num_histogramas)
        {
            if (mc == null)
            {
                MessageBox.Show("No hay una clasificación para este concepto.");
                return;
            }
            panel_temperaturas.ind_cluster_tabla = ind_cluster;
            panel_temperaturas.Escala(mc, num_histogramas);
            if (ind_cluster == -1)
            {
                panel_temperaturas.DibujaConjunto(true);
            }
            else
            {
                panel_temperaturas.Dibuja();
            }
        }
        private IndiceDB CalculaDBI(Caja caja)
        {
            // Indice Davies Bouldin
            // Cuanto menor sea DBI mejor es la clasificación
            // El número de clusters para el que este valor es el más bajo es una buena medida del número de clústeres en
            // los que los datos podrían clasificarse idealmente.
            // Consecuentemente el número de clústeres que minimiza el índice DB se toma como el óptimo

            double suma;
            int ind_espectro;
            double[] S = new double[nclusters];
            double[,] M = new double[nclusters, nclusters];
            double[,] R = new double[nclusters, nclusters];
            double[] Rm = new double[nclusters];
            int[] IRm = new int[nclusters];
            Array.Clear(M, 0, nclusters * nclusters);
            Array.Clear(R, 0, nclusters * nclusters);

            // 'S' Dispersion intra cluster (cohesión)
            // Distancias dentro de cada cluster. Cuanto menor, mejor agrupación
            // Se dice que dos cluster son similares si tiene dipersiones similares

            // Para cada cluster

            for (int ind_cluster = 0; ind_cluster < nclusters; ind_cluster++)
            {
                suma = 0;

                // Para cada espectro en el cluster

                if (caja.clusters[ind_cluster].n > 0)
                {
                    for (int i = 0; i < caja.clusters[ind_cluster].n; i++)
                    {
                        ind_espectro = caja.clusters[ind_cluster].ind_f_espectros[i];
                        suma += Distancia(caja, ind_cluster, espectros[ind_espectro].yn);
                    }
                    suma /= caja.clusters[ind_cluster].n;
                }
                S[ind_cluster] = suma;
            }

            // 'M' Separación entre cluster
            // Distancias entre clusters (centroides). Cuanto mayor mejor agrupación

            for (int i = 0; i < nclusters; i++)
            {
                if (caja.clusters[i].n > 0)
                {
                    for (int j = i + 1; j < nclusters; j++)
                    {
                        if (caja.clusters[j].n > 0)
                        {
                            M[i, j] = M[j, i] = Distancia(caja.centroides[i], caja.centroides[j]);
                        }
                    }
                }
            }

            // 'R' Relación entre 'S' y 'M'. Rij = (Si + Sj) / Mij
            // Para cada 'i' inporta el menor de todos los pares 'ij'

            int irmax;
            double max;
            double dbi = 0;
            double dbi_ponderado = 0;
            for (int i = 0; i < nclusters; i++)
            {
                irmax = -1;
                max = double.MinValue;
                if (caja.clusters[i].n > 0)
                {
                    for (int j = 0; j < nclusters; j++)
                    {
                        if (i != j)
                        {
                            if (caja.clusters[j].n > 0)
                            {
                                if (M[i, j] > 0)
                                {
                                    R[i, j] = (S[i] + S[j]) / M[i, j];
                                    if (R[i, j] > max)
                                    {
                                        // Para cada 'i' Nos quedamos con el peor R[i, j].
                                        // Mayor para clusters con espectros alejados de su centroide (S grandes) y con centroides poco separados (M pequeña)

                                        irmax = j;
                                        max = R[i, j];
                                    }
                                }
                            }
                        }
                    }
                }
                IRm[i] = irmax;
                if (irmax == -1)
                {
                    Rm[i] = -1;
                }
                else
                {
                    Rm[i] = max;
                    dbi += max;
                    dbi_ponderado += max * caja.clusters[i].n;
                }
            }
            int n = 0;
            int np = 0;
            for (int i = 0; i < nclusters; i++)
            {
                if (IRm[i] != -1)
                {
                    n++;
                    np += caja.clusters[i].n;
                }
            }
            if (n == 0)
            {
                dbi = double.MaxValue;
            }
            else
            {
                dbi /= n;
            }
            if (np == 0)
            {
                dbi_ponderado = double.MaxValue;
            }
            else
            {
                dbi_ponderado /= np;
            }
            return new IndiceDB(dbi, dbi_ponderado, IRm, Rm);
        }
        private double CalculaSilhouette(Caja caja)
        {
            if (!V_silhouette.Checked) return -9;

            // Mejor clasificación cuanto mayor es este índice

            int ind_e1;
            int ind_e2;
            double d;
            double[] a = new double[espectros.Count];
            double[] b = new double[espectros.Count];
            Array.Clear(a, 0, a.Length);

            // Recorrer todos los clusters

            // [a] Distancia media de cada espectro respecto a los que están en su cluster

            for (int ind_cluster = 0; ind_cluster < nclusters; ind_cluster++)
            {
                // Para los espectros en el cluster 'ind_cluster'

                if (caja.clusters[ind_cluster].n == 0) continue;

                if (caja.clusters[ind_cluster].n == 1)
                {
                    ind_e1 = caja.clusters[ind_cluster].ind_f_espectros[0];

                    // Marcar como -1, para que a este espectro se le asigne una 's' de cero

                    a[ind_e1] = -1;
                }
                else
                {
                    for (int i = 0; i < caja.clusters[ind_cluster].n; i++)
                    {
                        ind_e1 = caja.clusters[ind_cluster].ind_f_espectros[i];
                        for (int j = i + 1; j < caja.clusters[ind_cluster].n; j++)
                        {
                            ind_e2 = caja.clusters[ind_cluster].ind_f_espectros[j];
                            d = Distancia(ind_e1, ind_e2);
                            a[ind_e1] += d;
                            a[ind_e2] += d;
                        }
                    }
                    for (int i = 0; i < caja.clusters[ind_cluster].n; i++)
                    {
                        ind_e1 = caja.clusters[ind_cluster].ind_f_espectros[i];
                        a[ind_e1] /= caja.clusters[ind_cluster].n - 1;
                    }
                }
            }

            // [b] Distancias medias de cada espectro respecto a los que están en otro cluster (tantas distancias como clusters menos 1)
            // Pero sólo registramos la distancia media mínima

            double d_min;
            for (int i = 0; i < nclusters; i++)
            {
                if (caja.clusters[i].n < 2) continue;

                for (int n = 0; n < caja.clusters[i].n; n++)
                {
                    // Para los espectros en el cluster 'i'

                    ind_e1 = caja.clusters[i].ind_f_espectros[n];
                    d_min = double.MaxValue;

                    // Distancias medias a los espectros en otros clusters

                    for (int j = 0; j < nclusters; j++)
                    {
                        if (i != j)
                        {
                            // Acumula distancias de 'ind_e1' con todos los espectros del cluster 'j'

                            d = 0;
                            for (int k = 0; k < caja.clusters[j].n; k++)
                            {
                                ind_e2 = caja.clusters[j].ind_f_espectros[k];
                                d += Distancia(ind_e1, ind_e2);
                            }

                            // Media

                            d /= caja.clusters[j].n;
                            if (d < d_min)
                            {
                                // Sólo nos interesa la distancia media mínima

                                d_min = d;
                            }
                        }
                    }
                    b[ind_e1] = d_min;
                }
            }

            // Calculo de Silhouette para cada espectro

            double s;
            double media = 0;
            for (int i = 0; i < espectros.Count; i++)
            {
                if (a[i] == -1)
                {
                    // Los espectros solos en un clustere no aportan a 's'
                    // así se desincentivan cluster con un sólo espectro

                    s = 0;
                }
                else if (a[i] < b[i])
                {
                    // a < b . Más cerca de los de su cluster que de los de cualquier otro cluster

                    s = 1 - a[i] / b[i];
                }
                else if (a[i] == b[i])
                {
                    s = 0;
                }
                else
                {
                    // b < a . Más cerca de de los espectros de otro cluster que de los del suyo. 's' será negativo

                    s = b[i] / a[i] - 1;
                }
                media += s;
            }
            return media / espectros.Count;
        }
        private Confianza EstimaConfianza(Caja caja, int ind_cluster, int ind_espectro)
        {
            int ind_cluster_mas_cercano = ClusterMasCercano(caja, ind_cluster);
            if (ind_cluster_mas_cercano == -1)
            {
                return new Confianza(1, ind_cluster_mas_cercano);
            }
            double relacion = RelacionDistanciaEspectrosEnClusterVecinos(caja, ind_espectro, ind_cluster, ind_cluster_mas_cercano);
            return new Confianza(1 - relacion, ind_cluster_mas_cercano);
        }
        private int ClusterMasCercano(Caja caja, int ind_cluster)
        {
            if (caja.clusters[ind_cluster].n == 0) return -1;
            double d;
            int i_min = -1;
            double d_min = double.MaxValue;

            // Distancia entre el centroide 'ind_cluster' y el resto de centroides

            for (int i = 0; i < nclusters; i++)
            {
                if (i != ind_cluster)
                {
                    d = Distancia(caja.centroides[ind_cluster], caja.centroides[i]);
                    if (d < d_min)
                    {
                        i_min = i;
                        d_min = d;
                    }
                }
            }
            return i_min;
        }
        private double RelacionDistanciaEspectrosEnClusterVecinos(Caja caja, int ind_espectro, int ind_cluster, int ind_cluster_mas_cercano)
        {
            // Distancia media a los espectros en su cluster, dividida por la distancia media a los espectros en el cluster más cercano
            // Mejor cuanto más pequeña

            int ic;
            int ie;

            ic = ind_cluster;
            if (caja.clusters[ic].n < 2) return 0;

            double da = 0;
            for (int k = 0; k < caja.clusters[ic].n; k++)
            {
                ie = caja.clusters[ic].ind_f_espectros[k];
                if (ie != ind_espectro)
                {
                    da += Distancia(espectros[ind_espectro].yn, espectros[ie].yn);
                }
            }
            da /= caja.clusters[ic].n - 1;

            ic = ind_cluster_mas_cercano;
            double db = 0;
            for (int k = 0; k < caja.clusters[ic].n; k++)
            {
                ie = caja.clusters[ic].ind_f_espectros[k];
                db += Distancia(espectros[ind_espectro].yn, espectros[ie].yn);
            }
            db /= caja.clusters[ic].n;

            return da / db;
        }
        private void Sel_media_CheckedChanged(object sender, EventArgs e)
        {
            if (omite_cambio_seleccion) return;
            if (Sel_media.Checked) CambiaCaja(0);
            else sel_anterior = 0;
        }
        private void Sel_destandar_CheckedChanged(object sender, EventArgs e)
        {
            if (omite_cambio_seleccion) return;
            if (Sel_destandar.Checked) CambiaCaja(1);
            else sel_anterior = 1;
        }
        private void Sel_db_CheckedChanged(object sender, EventArgs e)
        {
            if (omite_cambio_seleccion) return;
            if (Sel_db.Checked) CambiaCaja(2);
            else sel_anterior = 2;
        }
        private void Sel_silh_CheckedChanged(object sender, EventArgs e)
        {
            if (omite_cambio_seleccion) return;
            if (Sel_det.Checked) CambiaCaja(3);
            else sel_anterior = 3;
        }
        private void Sel_mediaGenetico_CheckedChanged(object sender, EventArgs e)
        {
            if (omite_cambio_seleccion) return;
            if (Sel_mediaGenetico.Checked) CambiaCaja(4);
            else sel_anterior = 4;
        }
        private void Sel_destandarGenetico_CheckedChanged(object sender, EventArgs e)
        {
            if (omite_cambio_seleccion) return;
            if (Sel_destandarGenetico.Checked) CambiaCaja(5);
            else sel_anterior = 5;
        }
        private void Sel_dbGenetico_CheckedChanged(object sender, EventArgs e)
        {
            if (omite_cambio_seleccion) return;
            if (Sel_dbGenetico.Checked) CambiaCaja(6);
            else sel_anterior = 6;
        }
        private void Sel_silhGenetico_CheckedChanged(object sender, EventArgs e)
        {
            if (omite_cambio_seleccion) return;
            if (Sel_detGenetico.Checked) CambiaCaja(7);
            else sel_anterior = 7;
        }
        private void CambiaCaja(int n)
        {
            if (omite_cambio_seleccion) return;
            if (mejorcaja[n] == null)
            {
                omite_cambio_seleccion = true;
                MessageBox.Show("Este cálculo no está disponible");
                switch (sel_anterior)
                {
                    case 0:
                        sel_anterior = -1;
                        Sel_media.Checked = true;
                        break;
                    case 1:
                        sel_anterior = -1;
                        Sel_destandar.Checked = true;
                        break;
                    case 2:
                        sel_anterior = -1;
                        Sel_db.Checked = true;
                        break;
                    case 3:
                        sel_anterior = -1;
                        Sel_det.Checked = true;
                        break;
                    case 4:
                        sel_anterior = -1;
                        Sel_mediaGenetico.Checked = true;
                        break;
                    case 5:
                        sel_anterior = -1;
                        Sel_destandarGenetico.Checked = true;
                        break;
                    case 6:
                        sel_anterior = -1;
                        Sel_dbGenetico.Checked = true;
                        break;
                    default:
                        sel_anterior = -1;
                        Sel_detGenetico.Checked = true;
                        break;
                }
                omite_cambio_seleccion = false;
                return;
            }
            EstadisticaClusters(mejorcaja[n]);
            ListaEvolucion.Items.Clear();
            foreach (string s in mejorcaja[n].evolucion)
            {
                ListaEvolucion.Items.Add(s);
            }
            ListaEvolucion.TopIndex = ListaEvolucion.Items.Count - 1;
            MuestraClusters(mejorcaja[n]);
            omite_cambio_seleccion = true;
            TablaClusters.Rows[nclusters].Selected = true;
            omite_cambio_seleccion = false;
            TablaClusters.Rows[0].Selected = true;
        }
        private void Mapa_Click(object sender, EventArgs e)
        {
            if (espectros == null || espectros.Count == 0)
            {
                MessageBox.Show("No hay datos de espectros analizados");
                return;
            }
            int mejor = MejorCajaSeleccionada();
            if (mejorcaja[mejor] == null)
            {
                MessageBox.Show("No hay una clasificación para este concepto");
                return;
            }
            if (panel_mapa == null || !panel_mapa.Visible)
            {
                panel_mapa = new Form3
                {
                    principal = this
                };
                panel_mapa.Show();
            }
            panel_mapa.Dibuja(mejorcaja[mejor], espectros, -1, mejor);
        }
        public void CambiaSelSel(int m, int c)
        {
            panel_mapa.Dibuja(mejorcaja[m], espectros, c, m);
        }
        private void Asigna_Click(object sender, EventArgs e)
        {
            if (!ParametrosGenerales()) return;
            if (centroides_leidos == null || centroides_leidos.GetLength(0) == 0)
            {
                DialogResult res = MessageBox.Show("No se han leido los centroides a los que asignar.¿Asignar por temperatura?", "Asignar", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    AsignaTemperatura();
                }
                return;
            }
            R_mejorMed.Text = string.Empty;
            R_mejorDE.Text = string.Empty;
            R_mejorDB.Text = string.Empty;
            R_mejorDET.Text = string.Empty;
            tipo_distancia = Convert.ToInt32(V_tipoDistancia.Text.Trim());
            modo_iniciar = Convert.ToInt32(V_modoIniciar.Text.Trim());
            Caja caja = new Caja(0, nclusters, espectros.Count, ndatos, 1);
            Array.Copy(centroides_leidos, caja.centroides, centroides_leidos.Length);
            AsignaEspectros(caja);
            CalculaCentroides(caja);
            CompletaAsignacion(caja);
        }
        private void AsignaTemperatura()
        {
            nclusters = 5;
            V_nclusters.Text = nclusters.ToString();

            int NH = nclusters;
            ndatos = espectros[0].n;
            double xmin = espectros[0].x[0];
            double xmax = espectros[0].x[ndatos - 1];
            Caja caja = new Caja(0, nclusters, espectros.Count, ndatos, 1);
            for (int ind_cluster = 0; ind_cluster < nclusters; ind_cluster++)
            {
                caja.clusters[ind_cluster].n = 0;
                caja.clusters[ind_cluster].ind_f_espectros = new List<int>();
            }
            double t;
            int h;
            double londa;
            for (int ind_e = 0; ind_e < espectros.Count; ind_e++)
            {
                londa = espectros[ind_e].londaMaximo;
                if (londa >= xmin)
                {
                    t = espectros[ind_e].temperatura;
                    for (h = 0; h < NH + 1; h++)
                    {
                        if (t >= CLASIFICACION_OM[h])
                        {
                            break;
                        }
                    }
                    caja.clusters[h].n++;
                    caja.clusters[h].ind_f_espectros.Add(ind_e);
                    caja.cluster_espectro[ind_e] = h;
                }
            }
            int ind_espectro;
            for (int ind_cluster = 0; ind_cluster < nclusters; ind_cluster++)
            {
                if (caja.clusters[ind_cluster].n == 0) continue;
                if (caja.clusters[ind_cluster].n == 1)
                {
                    // El centroide es el único espectro en el cluster

                    ind_espectro = caja.clusters[ind_cluster].ind_f_espectros[0];
                    Array.Copy(espectros[ind_espectro].yn, caja.centroides[ind_cluster], ndatos);
                }
                else
                {
                    Array.Clear(caja.centroides[ind_cluster], 0, ndatos);
                    for (int j = 0; j < caja.clusters[ind_cluster].n; j++)
                    {
                        for (int k = 0; k < ndatos; k++)
                        {
                            caja.centroides[ind_cluster][k] += espectros[caja.clusters[ind_cluster].ind_f_espectros[j]].yn[k];
                        }
                    }
                    for (int k = 0; k < ndatos; k++) caja.centroides[ind_cluster][k] /= caja.clusters[ind_cluster].n;
                }
            }
            CompletaAsignacion(caja);
        }
        private void CompletaAsignacion(Caja caja)
        {
            EspectrosExtremos(caja);
            EstadisticaClusters(caja);
            ListaEvolucion.Items.Clear();
            string linea = string.Format("{0:e4} {1:e4} {2:f3}", caja.estadistica.d_media_g, caja.estadistica.d_estandar_g, caja.estadistica.db.indice);
            ListaEvolucion.Items.Add(linea);
            ListaEvolucion.TopIndex = ListaEvolucion.Items.Count - 1;
            ListaIntentos.Items.Clear();
            linea = string.Format("{0:e4} {1:e4} {2:f3} {3:f0}", caja.estadistica.d_media_g, caja.estadistica.d_estandar_g, caja.estadistica.db.indice, caja.estadistica.de_temperatura_g);
            ListaIntentos.Items.Add(linea);
            ListaIntentos.TopIndex = ListaIntentos.Items.Count - 1;
            OrdenaClusters(caja);
            for (int i = 0; i < N_CAJAS; i++) mejorcaja[i] = null;
            mejorcaja[0] = caja;
            MuestraClusters(mejorcaja[0]);
            omite_cambio_seleccion = true;
            Sel_media.Checked = true;
            omite_cambio_seleccion = false;

            omite_cambio_seleccion = true;
            TablaClusters.Rows[nclusters].Selected = true;
            omite_cambio_seleccion = false;
            TablaClusters.Rows[0].Selected = true;
        }
        private void EjecutaGenetico_Click(object sender, EventArgs e)
        {
            if (V_generaciones.Text.Trim().Length == 0) V_generaciones.Text = "0";
            if (Convert.ToInt32(V_generaciones.Text.Trim()) < 1)
            {
                MessageBox.Show("El número de generaciones debe ser mayor que cero");
                return;
            }
            if (espectros == null || espectros.Count == 0)
            {
                MessageBox.Show("No hay datos de espectros analizados");
                return;
            }
            if (cromosomas == null || cromosomas.Count == 0)
            {
                MessageBox.Show("No hay cromosomas.");
                return;
            }
            if (!ParametrosGenerales()) return;
            tiempo_inicio = DateTime.Now;
            for (int i = 4; i < N_CAJAS; i++)
            {
                mejorcaja[i] = null;
                mejor_intento[i] = 1;
                mejor_estimador[i] = double.MaxValue;
            }
            Genetico();
        }
        private bool ParametrosGenerales()
        {
            if (Convert.ToInt32(V_nclusters.Text.Trim()) < 1)
            {
                MessageBox.Show("El número de clusters debe ser mayor que cero");
                return false;
            }
            nclusters = Convert.ToInt32(V_nclusters.Text.Trim());
            ndatos = espectros[0].n;
            calcula_desde = Convert.ToInt32(V_desde.Text.Trim());
            if (calcula_desde < 0) calcula_desde = 0;
            calcula_hasta = Convert.ToInt32(V_hasta.Text.Trim());
            tipo_distancia = Convert.ToInt32(V_tipoDistancia.Text.Trim());
            if (V_tipoDatosY.Text.Trim().Length == 0) V_tipoDatosY.Text = "0";
            tipo_datosY = Convert.ToInt32(V_tipoDatosY.Text.Trim());
            normaliza_centroide = V_normalizaCentroides.Checked;
            modo_podar = V_podar.Checked;
            if (modo_podar)
            {
                pu_podar = Convert.ToDouble(V_ppPoda.Text.Trim()) / 100;
                minimo_podar = Convert.ToDouble(V_veces.Text.Trim());
                if (minimo_podar == 0)
                {
                    MessageBox.Show("Veces no puede ser cero");
                    return false;
                }
                minimo_podar = 1 / minimo_podar;
            }
            else
            {
                pu_podar = 0;
                minimo_podar = 0;
            }
            modo_forzar = V_forzar.Checked;
            modo_centroide = Convert.ToInt32(V_modoCentroide.Text.Trim());
            clasificacionML = false;
            IniciaTablaClusters();
            return true;
        }
        private void Restar_Click(object sender, EventArgs e)
        {
            if (espectros == null || espectros.Count == 0)
            {
                MessageBox.Show("No hay datos de espectros analizados");
                return;
            }
            OpenFileDialog ficherolectura = new OpenFileDialog()
            {
                Filter = "CVS |*.csv|TXT |*.txt|TODO |*.*",
                FilterIndex = 1,
                RestoreDirectory = false,
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false
            };
            if (ficherolectura.ShowDialog() == DialogResult.OK)
            {
                Habilitar(false);
                List<int> restar_espectros = new List<int>();
                FileStream fsfc = new FileStream(ficherolectura.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamReader sr = new StreamReader(fsfc);
                Application.DoEvents();
                string linea;
                int indice;
                while (!sr.EndOfStream)
                {
                    linea = sr.ReadLine();
                    indice = Convert.ToInt32(linea);
                    restar_espectros.Add(indice);
                }
                for (int i = restar_espectros.Count - 1; i >= 0; i--)
                {
                    indice = restar_espectros[i];
                    espectros.RemoveAt(indice);
                }
                sr.Close();
                R_espectros.Text = string.Format("{0:N0}", espectros.Count);
                Habilitar(true);
            }
        }
        private void MLKMeans_Click(object sender, EventArgs e)
        {
            if (espectros == null || espectros.Count == 0)
            {
                MessageBox.Show("No hay datos de espectros analizados");
                return;
            }
            if (espectros[0].y.Length != 4563)
            {
                MessageBox.Show("Sólo funciona con espectros de 4563 datos.");
                return;
            }
            Habilitar(false);
            CreaFicheroML();
            ClasificarML(R_Fichero_Clusters.Text + "ML");
            Habilitar(true);
        }
        private void CreaFicheroML()
        {
            // Fichero auxuliar para ML KMeans

            FileStream fsal_ML = new FileStream(R_Fichero_Clusters.Text + "ML", FileMode.Create, FileAccess.Write, FileShare.Read);
            StreamWriter sw_ML = new StreamWriter(fsal_ML);
            foreach (EspectroAnalizado esa in espectros)
            {
                for (int i = 0; i < esa.n; i++)
                {
                    if (i == 0)
                    {
                        sw_ML.Write(string.Format("{0}", esa.yn[i]).Replace(',', '.'));
                    }
                    else
                    {
                        sw_ML.Write(string.Format(";{0}", esa.yn[i]).Replace(',', '.'));
                    }
                }
                sw_ML.WriteLine();
            }
            sw_ML.Close();
        }
        public class EspectroData
        {
            [ColumnName("Features")]
            [VectorType(4563)]
            [LoadColumn(0, 4563 - 1)]

            public float[]? Flujos { get; set; }
        }
        public class ClusterPrediction
        {
            [ColumnName("PredictedLabel")]
            public uint ClusterAsignado;

            [ColumnName("Score")]
            public float[]? Distancias2;
        }
        private void ClasificarML(string fichero)
        {
            nclusters = Convert.ToInt32(V_nclusters.Text.Trim());
            if (nclusters < 1) return;
            IniciaTablaClusters();
            ListaEvolucion.Items.Clear();
            Application.DoEvents();
            clasificacionML = true;

            // ML trabaja con las distancias al cuadrado

            V_tipoDistancia.Text = "3";
            tipo_distancia = 3;

            // ML trabaja con todos los datos

            V_desde.Text = "0";
            V_hasta.Text = "0";
            calcula_desde = 0;
            calcula_hasta = 0;

            try
            {
                var mlContext = new MLContext(seed: 0);
                IDataView datosParaAgrupar = mlContext.Data.LoadFromTextFile<EspectroData>(fichero, hasHeader: false, separatorChar: ';');

                // Opciones del clasificador

                var options = new KMeansTrainer.Options
                {
                    InitializationAlgorithm = KMeansTrainer.InitializationAlgorithm.KMeansPlusPlus,
                    NumberOfClusters = nclusters,
                    OptimizationTolerance = 1e-6f
                    //NumberOfThreads = 1
                };

                // Clasificador

                var pipeline = mlContext.Clustering.Trainers.KMeans(options);

                // Clasifica

                var model = pipeline.Fit(datosParaAgrupar);

                // Extraer los valores de los centroides

                KMeansModelParameters kparams = model.Model;
                VBuffer<float>[] centroids = default;
                kparams.GetClusterCentroids(ref centroids, out int k);
                float[][] fcentroides = new float[nclusters][];
                for (int i = 0; i < nclusters; i++) fcentroides[i] = centroids[i].GetValues().ToArray();
                ndatos = fcentroides[0].Length;
                omite_cambio_seleccion = true;
                Sel_media.Checked = true;
                omite_cambio_seleccion = false;
                for (int i = 0; i < N_CAJAS; i++)
                {
                    mejorcaja[i] = null;
                }
                mejorcaja[0] = new Caja(0, nclusters, espectros.Count, ndatos, 1);

                // Como 'fcentroides' es de tipo float, los copiamos a la matriz global 'centroides' que
                // es double, para su uso en otras partes de la aplicación

                for (int i = 0; i < nclusters; i++)
                {
                    //Array.Copy(fcentroides[i], mejorcaja[0].centroides[i], ndatos);

                    for (int j = 0; j < ndatos; j++)
                    {
                        mejorcaja[0].centroides[i][j] = fcentroides[i][j];
                    }
                }
                centroides_leidos = new double[nclusters][];
                for (int i = 0; i < nclusters; i++)
                {
                    centroides_leidos[i] = new double[ndatos];
                }
                Array.Copy(mejorcaja[0].centroides, centroides_leidos, centroides_leidos.Length);

                // Se transforman los datos de para incluir el espacio (columnas PredictedLabel y Score) necesario para el resultado de la evaluación

                var datosTransformados = model.Transform(datosParaAgrupar);

                // Calcula (Evaluate) las distancias a los centroides y determina el cluster asignado a cada espectro (fila) en 'datosParaAgrupar'
                // 'datosTransformados' contendrá estos calculos después de evaluar.

                var metrics = mlContext.Clustering.Evaluate(datosTransformados, scoreColumnName: "Score", featureColumnName: "Features");

                StringBuilder sb = new StringBuilder();

                // 'NormalizedMutualInformation' necesita una columna de etiquetas con el valor correcto de asignación a cluster
                //sb.AppendFormat("Información mútua normalizada: {0:F2}\n", metrics.NormalizedMutualInformation);

                sb.AppendFormat("Distancia media: {0:F3}\n", metrics.AverageDistance);

                /*
                Indice Davies Bouldin
                La relación promedio de distancias dentro de un grupo, con las distancias entre grupos.
                Cuanto más compacto sea el grupo y cuanto más separados estén los grupos, menor será este valor.
                */
                sb.AppendFormat("Indice Davies Bouldin : {0:F3}", metrics.DaviesBouldinIndex);

                Console.Beep();
                MessageBox.Show(sb.ToString());

                // Extrae del objeto 'datosTransformados' una lista (lista_predicciones) de objetos de la clase 'ClusterPrediction'
                // es decir, una lista de: ClusterAsignado y float[] Distancias2, para cada espectro

                var lista_predicciones = mlContext.Data.CreateEnumerable<ClusterPrediction>(datosTransformados, reuseRowObject: false).ToList();

                mejorcaja[0].clusters = new Cluster[nclusters];
                for (int i = 0; i < nclusters; i++)
                {
                    mejorcaja[0].clusters[i] = new Cluster
                    {
                        n = 0,
                        ind_f_espectros = new List<int>(),
                        distancias = new List<double>(),
                        ind_min = -1,
                        distancia_min = double.MaxValue,
                        ind_max = -1,
                        distancia_max = double.MinValue,
                        distancia_media = 0,
                        desviacion_estandar = 0,
                        londaMaximo = 0,
                        temperatura = 0,
                        de_temperatura = 0
                    };
                }
                uint ind_cluster;
                double d;

                /*
                // Transforma las entradas en una lista de objetos del tipo 'EspectroData'

                var entradas = mlContext.Data.CreateEnumerable<EspectroData>(datosParaAgrupar, reuseRowObject: false).ToList();
                double[] dc = new double[nclusters];
                double[] dcb = new double[nclusters];
                Console.WriteLine("----------");
                for (int i = 0; i < 5; i++) Console.WriteLine(string.Format("{0,2} {1:e4}  {2:e4}", i, espectros[0].yn[i], entradas[0].Flujos[i]));
                Console.WriteLine("----------");
                for (int i = espectros[0].n - 5; i < espectros[0].n; i++) Console.WriteLine(string.Format("{0,2} {1:e4}  {2:e4}", i, espectros[0].yn[i], entradas[0].Flujos[i]));
                Console.WriteLine("----------");
                // Una alternativa es:
                var preview = datosParaAgrupar.Preview();
                */

                int ind_espectro = 0;
                foreach (var pre in lista_predicciones)
                {
                    ind_cluster = pre.ClusterAsignado - 1;
                    mejorcaja[0].clusters[ind_cluster].n++;
                    mejorcaja[0].clusters[ind_cluster].ind_f_espectros.Add(ind_espectro);
                    d = pre.Distancias2[ind_cluster];
                    mejorcaja[0].clusters[ind_cluster].distancias.Add(d);
                    ind_espectro++;
                }
                EstadisticaClusters(mejorcaja[0]);
                mejorcaja[0].estadistica.db = CalculaDBI(mejorcaja[0]);
                mejorcaja[0].estadistica.si = V_silhouette.Checked ? CalculaSilhouette(mejorcaja[0]) : 0;
                EspectrosExtremos(mejorcaja[0]);
                OrdenaClusters(mejorcaja[0]);
                MuestraClusters(mejorcaja[0]);
                omite_cambio_seleccion = true;
                TablaClusters.Rows[nclusters].Selected = true;
                omite_cambio_seleccion = false;
                TablaClusters.Rows[0].Selected = true;
                ListaEvolucion.Items.Add(string.Format("{0:e4} {1:e4} {2:f3} {3:f0}", mejorcaja[0].estadistica.d_media_g, mejorcaja[0].estadistica.d_estandar_g, mejorcaja[0].estadistica.db.indice, mejorcaja[0].estadistica.de_temperatura_g));
                Application.DoEvents();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void SeparaT_Click(object sender, EventArgs e)
        {
            Form7 panel_separaT = new Form7();
            panel_separaT.principal = this;
            panel_separaT.Show();
            Caja caja = mejorcaja[0];
            panel_separaT.Escala(caja);
            panel_separaT.Dibuja();
        }
    }
}

