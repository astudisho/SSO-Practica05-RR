using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO_Practica05_RR.Bussines
{
	public class Marco
	{
		public int Numero_Marco { get; set; }
		public char M0 { get; set; }
		public char M1 { get; set; }
		public char M2 { get; set; }
		public char M3 { get; set; }

		public Marco(int id)
		{
			Numero_Marco = id;
			M0 = ' ';
			M1 = ' ';
			M2 = ' ';
			M3 = ' ';
		}
	}
}
