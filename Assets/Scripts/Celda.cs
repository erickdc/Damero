using UnityEngine;
using System.Collections;

public class Celda : MonoBehaviour {

	
	public int PosX{ get; set; }
	public int PosY { get; set; }
	public string Colores { get; set; }
	public string OcupadoPor{ get; set; }
	public string Tipo { get; set; }
	public Celda (int posX, int posY, string color, string ocupado,string Tipo)
	{
		this.PosX = posX;
		this.PosY = posY;
		this.Colores = color;
		this.OcupadoPor = ocupado;
		this.Tipo = Tipo;
	}



}
