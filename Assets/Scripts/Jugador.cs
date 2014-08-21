using UnityEngine;
using System.Collections;

public class Jugador  {

	public int Id_Jugador{ get; set; }
	public int CantidadPiezasDisponibles{ get; set; }

	public Jugador(int Id_Jugador , int CantidadPiezasDisponibles)
	{
		this.Id_Jugador = Id_Jugador;
		this.CantidadPiezasDisponibles = CantidadPiezasDisponibles;
	}
}
