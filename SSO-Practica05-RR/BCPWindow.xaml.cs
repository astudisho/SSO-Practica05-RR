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

namespace SSO_Practica05_RR
{
	/// <summary>
	/// Interaction logic for BCPWindow.xaml
	/// </summary>
	public partial class BCPWindow : Window
	{
		public BCPWindow( List<Proceso> listaProcesos = null )
		{
			InitializeComponent();

			dgvBcp.ItemsSource = listaProcesos.OrderBy( x=> x.Id);
		}
	}
}
