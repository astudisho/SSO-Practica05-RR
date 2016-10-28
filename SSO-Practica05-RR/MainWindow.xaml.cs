using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SSO_Practica05_RR
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		readonly List<string> operaciones = new	List<string>(){ "+","-","*","/","%","^","%%" };

		private readonly int MAX_PROC_MEMORIA = 5, MAX_CUANTO = 15, MAX_PROCESOS = 30;
		private int segundos;
		private bool esBloqueado, esError, esPausa, esContinuar, esNuevo, esBCP;
		private DispatcherTimer dt;

		private void btnIniciar_Click(object sender, RoutedEventArgs e)
		{

		}

		private List<Proceso> listos, nuevos, terminados, ejecucion;

		public MainWindow()
		{
			InitializeComponent();

			listos = new List<Proceso>();
			nuevos = new List<Proceso>();
			terminados = new List<Proceso>();
			ejecucion = new List<Proceso>();

			//Inicializa dt
			dt = new DispatcherTimer();
			dt.Tick += dt_Tick;
			dt.Interval = new TimeSpan(0, 0, 1);

			actualizaGrid();
			poblarCombos();
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.E:	//Entrada salida
					break;
				case Key.W:	//Error
					break;
				case Key.P:	//Pausa
					break;
				case Key.C: //Continuar
					break;
				case Key.U: //Nuevo
					break;
				case Key.B: //BCP
					break;
				default:
					break;
			}
		}


		private void actualizaGrid()
		{
			dgvTerminados.ItemsSource = null;
			dgvNuevos.ItemsSource = null;
			dgvBloqueados.ItemsSource = null;
			dgvEjecucion.ItemsSource = null;
			dgvListos.ItemsSource = null;

			dgvBloqueados.ItemsSource = terminados;
			dgvEjecucion.ItemsSource = ejecucion;
			dgvListos.ItemsSource = listos;
			dgvNuevos.ItemsSource = nuevos;
			dgvTerminados.ItemsSource = terminados;

		}

		private void poblarCombos()
		{
			for (int i = 1; i < MAX_CUANTO; i++)
			{
				cmbCuanto.Items.Add(i);
			}

			for (int i = 1; i < MAX_PROCESOS; i++)
			{
				cmbNumeroProcesos.Items.Add(i);
			}

			cmbCuanto.SelectedIndex = 0;
			cmbNumeroProcesos.SelectedIndex = 0;
		}

		private void dt_Tick(object sender, EventArgs e)
		{
			segundos++;
		}

		private void btnIniciarCronometro_Click(object sender, EventArgs e)
		{
			dt.Start();
		}

	priv te void crearProceso(int numeroProcesos = 1)
		{
			Random rnd = new Random();
			
			Proceso nuevoProceso = new Proceso
				(
					"",
					operaciones[rnd.Next(operaciones.Count - 1) ],
					rnd.Next( 15 ).ToString(),
					rnd.Next( 15 ).ToString(),
					rnd.Next( 15 )
				);

		}

		private string segsToTime(int segs)
		{
			return (segs / 60).ToString().PadLeft(2, '0') + ":" + (segs % 60).ToString().PadLeft(2, '0');
		}
	}
}
