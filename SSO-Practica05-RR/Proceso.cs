using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO_Practica05_RR
{
	public class Proceso
	{
		const int TIEMPO_BLOQUEADO = 8;

		public static int idProceso = 1;

		public int Id { get; set; }
		//public String Programador { get; set; }
		public String Op1 { get; set; }
		public String Op { get; set; }
		public String Op2 { get; set; }
		public string Res { get; set; }
		public bool Termino { get; set; }
		public int TME { get; set; }
		public int TR { get; set; }		
		public int TL { get; set; }
		public int TFin { get; set; }
		public int TRet { get; set; }
		public int TResp { get; set; }
		public int TEsp { get; set; }
		public int TSer { get; set; }
		public int Bloq { get; set; }
		private int servidoPrimeraVez = -1;

		public Proceso(String Programador, String Op, String Op1, String Op2, int ETA)
		{
			this.Id = idProceso;
			//this.Id = id;
			//this.Programador = Programador;
			this.Op = Op;
			this.Op1 = Op1;
			this.Op2 = Op2;
			this.TR = this.TME = ETA;
			this.Termino = false;
			idProceso++;
		}

		public void terminoError(int tiempoActual)
		{
			termino(tiempoActual);
			Termino = true;
			Res = "Error";			
		}

		public void termino(int tiempoActual)
		{
			this.Termino = true;
			this.TFin = tiempoActual;
			this.TRet = this.TFin - this.TL;
			//this.TResp = this.TL;
		}

		public void ejecuto(int tiempoActual)
		{
			if (servidoPrimeraVez == -1)
			{
				servidoPrimeraVez = tiempoActual;
				this.TResp = servidoPrimeraVez;
			}


			//this.TResp = servidoPrimeraVez - TL;
		}

		public void bloquea(int tiempoActual)
		{
			Bloq = TIEMPO_BLOQUEADO;
		}

		public void resolverEcuacion()
		{
			int op1 = int.Parse(Op1),
				op2 = int.Parse(Op2);

			switch (Op)
			{
				case "+":
					Res = (op1 + op2).ToString();
					break;
				case "-":
					Res = (op1 - op2).ToString();
					break;
				case "*":
					Res = (op1 * op2).ToString();
					break;
				case "/":
					Res = (op1 / op2).ToString();
					break;
				case "%":
					Res = (op1 % op2).ToString();
					break;
				case "^":
					Res = (op1 ^ op2).ToString();
					break;
				case "%%":
					Res = ((op1 / 100) * op2).ToString();
					break;
				default:
					Res = (-1).ToString();
					break;
			}
		}
	}
}