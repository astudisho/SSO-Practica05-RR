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
using System.Windows.Shapes;
using SSO_Practica05_RR.Bussines;

namespace SSO_Practica05_RR.Views
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class PagesWindow : Window
	{
		List<Marco> listaMarcos;

		public PagesWindow( )
		{
			InitializeComponent();

			listaMarcos = new List<Marco>();

			for (int i = 0; i < 32; i++)
			{
				listaMarcos.Add( new Marco(i) );
			}

			bindPages();
		}

		void bindPages()
		{
			dgvPages1.ItemsSource = listaMarcos.Select(x => x.Numero_Marco < 16);
			dgvPages2.ItemsSource = listaMarcos.Select(x => x.Numero_Marco >= 16);
		}
	}
}
