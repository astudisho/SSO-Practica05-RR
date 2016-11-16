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

		private static int procesosEnMemoria = 0, CUANTO_VALOR = 0;
		private readonly int MAX_PROC_MEMORIA = 5, MAX_CUANTO = 15, MAX_PROCESOS = 30, C_ZERO = 0;
		private int segundos, maximoGlobal, cuantoRestante;
		private bool esBloqueado, esError, esPausa, esContinuar, esNuevo, esBCP;
		private DispatcherTimer dt;

		private List<Proceso> listos, nuevos, terminados, ejecucion, bloqueados;

		private void btnCrear_Click(object sender, RoutedEventArgs e)
		{
			int procesosCrear = (int)cmbNumeroProcesos.SelectedValue;

			crearProceso(procesosCrear);
			CUANTO_VALOR = (int)cmbCuanto.SelectedItem;

			cmbCuanto.IsEnabled = false;
			cmbNumeroProcesos.IsEnabled = false;
			btnCrear.IsEnabled = false;
		}

		public MainWindow()
		{
			InitializeComponent();

			listos = new List<Proceso>();
			nuevos = new List<Proceso>();
			terminados = new List<Proceso>();
			ejecucion = new List<Proceso>();
			bloqueados = new List<Proceso>();

			//Inicializa dt
			dt = new DispatcherTimer();
			dt.Tick += dt_Tick;
			dt.Interval = new TimeSpan(0, 0, 1);

			actualizaGrid();
			poblarCombos();
		}

		private void ejecutarSiguiente()
		{
			if (listos.Count > C_ZERO)
				ejecucion.Add(listos[C_ZERO]);


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
					crearProceso();
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

			dgvBloqueados.ItemsSource = bloqueados;
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

			procesosEnMemoria = listos.Count + ejecucion.Count + bloqueados.Count;

			procesaEjecucion();
			procesaListos();
			procesaContadores();

			actualizaGrid();
		}

		private void resetCuanto() { cuantoRestante = CUANTO_VALOR; }

		private void procesaContadores()
		{
			txbCronometro.Text = segsToTime(segundos);
			txbMaximoGlobal.Text = segsToTime(maximoGlobal);
			txbCuanto.Text = cuantoRestante.ToString();
		}

		private void btnIniciarCronometro_Click(object sender, EventArgs e)
		{
			dt.Start();

			var procesos = (nuevos.Count < MAX_PROC_MEMORIA) ? nuevos.Count : MAX_PROC_MEMORIA;

			for (int i = 0; i < procesos; i++)
				listos.Add(nuevos[i]);

			foreach (var proceso in listos)
			{
				nuevos.Remove(proceso);
			}

			btnIniciarCronometro.IsEnabled = false;

			actualizaGrid();
		}

		private void crearProceso(int numeroProcesos = 1)
		{
			Random rnd = new Random();
			int tiempo;

			for (uint i = 0; i < numeroProcesos; i++)
			{
				Proceso nuevoProceso = new Proceso
					(
						"",
						operaciones[rnd.Next(operaciones.Count - 1)],
						rnd.Next(1,15).ToString(),
						rnd.Next(1,15).ToString(),
						tiempo = rnd.Next(1,15)
					);
				maximoGlobal += tiempo + 1;
				nuevos.Add(nuevoProceso);
			}

			actualizaGrid();
		}

		private string segsToTime(int segs)
		{
			return (segs / 60).ToString().PadLeft(2, '0') + ":" + (segs % 60).ToString().PadLeft(2, '0');
		}

		private void procesaEjecucion()
		{
			if (ejecucion.Count > C_ZERO)
			{
				ejecucion[C_ZERO].TSer++;
				ejecucion[C_ZERO].TME--;
			}
			else if (ejecucion.Count <= C_ZERO && listos.Count > C_ZERO)
			{
				ejecucion.Add(listos[C_ZERO]);
				listos.RemoveAt(C_ZERO);
			}
			else
				return;


			if (ejecucion[C_ZERO].TME <= 0)
			{
				var aux = ejecucion[C_ZERO];

				ejecucion.Remove(aux);

				//aux.TFin = segundos;

				aux.termino(segundos);
				terminados.Add(aux);
			}
				
		}

		private void procesaListos()
		{
			foreach (var proceso in listos)
			{
				proceso.TEsp++;
			}

			if (nuevos.Count > C_ZERO)
			{
				if (procesosEnMemoria < MAX_PROC_MEMORIA)
				{
					var aux = nuevos[C_ZERO];
					var auxNum = MAX_PROC_MEMORIA - procesosEnMemoria;

					auxNum = auxNum > nuevos.Count ? nuevos.Count : auxNum ;

					List<Proceso> listaAux = new List<Proceso>();

					for (int i = 0; i < auxNum; i++)
					{
						listaAux.Add(nuevos[i]);
					}

					listos.AddRange(listaAux);
					nuevos.RemoveRange(C_ZERO, auxNum);
				}
			}
		}
	}
}
