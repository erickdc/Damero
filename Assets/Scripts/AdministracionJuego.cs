using UnityEngine;
using System.Collections;


public class AdministracionJuego : MonoBehaviour {

	public static AdministracionJuego Instance;

	Jugador Jugador1,Jugador2;
	int TurnoJugador;
	public string Ganador{ get; set; }
	bool Selected=false,TerminoPartida;
	GameObject Inicio;



	// Use this for initialization
	void Start () {
		Jugador1 = new Jugador (1,12);
		Jugador2 = new Jugador (2,12);
		Instance = this;
		TurnoJugador = 1;
		TerminoPartida = false;
		Ganador = "Ninguno";

	}
	
	// Update is called once per frame
	void Update () {
		if (Jugador1.CantidadPiezasDisponibles == 0 || Jugador2.CantidadPiezasDisponibles == 0) {
			TerminoPartida = true;
			if(Jugador1.CantidadPiezasDisponibles==0)
				Ganador="Jugador2";
			else
				Ganador="Jugador1";
		}
	}


	public void Seleccion(GameObject Seleccionado)
	{
		if (!TerminoPartida) {

			GameObject selectedObject = Seleccionado;
			var PiezaScript = (Pieza)selectedObject.GetComponent ("Pieza");

			if (!Selected && PiezaScript.Id_Jugador == TurnoJugador && PiezaScript.Tipo.Equals ("Pieza")) {
					Inicio = Seleccionado;
					Selected = true;
			} else if (Selected && PiezaScript.Tipo.Equals ("Pieza") && PiezaScript.Id_Jugador == TurnoJugador) {
					Inicio = null;
					Selected = false;

			} else if (Selected && PiezaScript.Tipo.Equals ("Celda") && PiezaScript.Colores.Equals ("Negra")) {
					var PiezaInicioScript = (Pieza)Inicio.GetComponent ("Pieza");
					int Jugada = JugadaValida (PiezaInicioScript.PosX, PiezaInicioScript.PosY, PiezaScript.PosX, PiezaScript.PosY);
					if (Jugada != -1) {
						int IdJugador = PiezaInicioScript.Id_Jugador;
						string Colores = PiezaInicioScript.Colores;
						string Tipo = PiezaInicioScript.Tipo;
						GameObject [,] Tableros = Tablero.Instance.TableroO;		
						if (Jugada == 1) {
							Moverse (IdJugador, Colores, Tipo, PiezaScript, PiezaInicioScript, Inicio, selectedObject);
							Selected = false;

						} else if (Jugada == 2) {
								
							GameObject FichaEnemiga =Tableros[PiezaInicioScript.PosX-1,PiezaInicioScript.PosY-1];
							var FichaEnemigaScript = (Pieza)FichaEnemiga.GetComponent("Pieza");
							if (FichaEnemigaScript.Id_Jugador != TurnoJugador && FichaEnemigaScript.Id_Jugador != -1) {
								   ActualizarFichaEnemiga (FichaEnemigaScript, FichaEnemiga);
								   ActualizarJugador (TurnoJugador);
								   Moverse (IdJugador, Colores, Tipo, PiezaScript, PiezaInicioScript, Inicio, selectedObject);
								   Selected = false;
							}
							
						} else if (Jugada == 3) {
							GameObject FichaEnemiga =Tableros[PiezaInicioScript.PosX-1,PiezaInicioScript.PosY+1];
							var FichaEnemigaScript = (Pieza)FichaEnemiga.GetComponent("Pieza");
							if (FichaEnemigaScript.Id_Jugador != TurnoJugador && FichaEnemigaScript.Id_Jugador != -1) {
									ActualizarFichaEnemiga (FichaEnemigaScript, FichaEnemiga);
									ActualizarJugador (TurnoJugador);
									Moverse (IdJugador, Colores, Tipo, PiezaScript, PiezaInicioScript, Inicio, selectedObject);
									Selected = false;
							}
						
						} else if (Jugada == 4) {
								GameObject FichaEnemiga =Tableros[PiezaInicioScript.PosX+1,PiezaInicioScript.PosY-1];
								var FichaEnemigaScript = (Pieza)FichaEnemiga.GetComponent ("Pieza");
								if (FichaEnemigaScript.Id_Jugador != TurnoJugador && FichaEnemigaScript.Id_Jugador != -1) {
										ActualizarFichaEnemiga (FichaEnemigaScript, FichaEnemiga);
										ActualizarJugador (TurnoJugador);
										Moverse (IdJugador, Colores, Tipo, PiezaScript, PiezaInicioScript, Inicio, selectedObject);
										Selected = false;
								}
				
								
						} else if (Jugada == 5) {
					
								GameObject FichaEnemiga =Tableros[PiezaInicioScript.PosX+1,PiezaInicioScript.PosY+1];
								var FichaEnemigaScript = (Pieza)FichaEnemiga.GetComponent("Pieza");
								if (FichaEnemigaScript.Id_Jugador != TurnoJugador && FichaEnemigaScript.Id_Jugador != -1) {
						
									ActualizarFichaEnemiga (FichaEnemigaScript, FichaEnemiga);
									ActualizarJugador (TurnoJugador);
									Moverse (IdJugador, Colores, Tipo, PiezaScript, PiezaInicioScript, Inicio, selectedObject);
									Selected = false;
								}
					
						}
					}
		
				}
			}

	}

	void OnGUI () {
		GUI.Label (new Rect (600,500,200,150), "Es el turno del Jugador: "+ (TurnoJugador==1?"Rojo":"Azul"));
		if(TerminoPartida)
			GUI.Label (new Rect (600,530,200,150), "El Ganador es:"+ this.Ganador);
	}

	void ActualizarFichaEnemiga(Pieza PiezaEnemiga,GameObject Enemigo)
	{
			Pieza FichaEnemigaScript = PiezaEnemiga;
			GameObject FichaEnemiga=Enemigo;
			FichaEnemiga.GetComponent<SpriteRenderer> ().sprite = Tablero.Instance.CeldaNegra;
			FichaEnemigaScript.Id_Jugador=-1;
			FichaEnemigaScript.Colores="Negra";
			FichaEnemigaScript.Tipo="Celda";
	}
	int JugadaValida (int PosPiezaX,int PosPiezaY,int PosCeldaX,int PosCeldaY)
	{
		if (TurnoJugador == 1){

			if (PosCeldaY+2 == PosPiezaY && PosCeldaX+2 == PosPiezaX )
				return 2;
			
			if (PosCeldaY-2 == PosPiezaY && PosCeldaX+2 == PosPiezaX )
			    return 3;
			if ((PosCeldaY+1 == PosPiezaY  || PosCeldaY-1 == PosPiezaY) && PosCeldaX+1 == PosPiezaX )
				return 1;

		}
		if (TurnoJugador==2){

			if (PosCeldaY+2 == PosPiezaY && PosCeldaX-2 == PosPiezaX )
				return 4;
			
			if (PosCeldaY-2 == PosPiezaY && PosCeldaX-2 == PosPiezaX )
				return 5;
			if ((PosCeldaY + 1 == PosPiezaY || PosCeldaY - 1 == PosPiezaY) && PosCeldaX - 1 == PosPiezaX)
					return 1;
		}

		return -1;
	}

	void Moverse(int Id_Jugador,string Colores,string Tipo,Pieza PiezaS,Pieza PiezaSInicio,GameObject Init,GameObject selectedO)
	{
		Pieza PiezaScript = PiezaS;
		Pieza PiezaInicioScript= PiezaSInicio;
		GameObject Inicio = Init;
		GameObject selectedObject = selectedO;

		PiezaScript.Mover(Colores,Id_Jugador,Tipo);
		PiezaInicioScript.Mover ("Negra",-1,"Celda");
		Inicio.GetComponent<SpriteRenderer> ().sprite = Tablero.Instance.CeldaNegra;
		selectedObject.GetComponent<SpriteRenderer> ().sprite = (Colores.Equals("Azul")?Tablero.Instance.PiezaAzul:Tablero.Instance.PiezaRoja);
		TurnoJugador=TurnoJugador==1?2:1;
		Inicio = null;


	}
	void ActualizarJugador(int Id)
	{
		if(Id==1)
		Jugador2.CantidadPiezasDisponibles-=1;
		if(Id==2)
			Jugador1.CantidadPiezasDisponibles-=1;

	}

}
