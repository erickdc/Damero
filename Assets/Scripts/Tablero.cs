using UnityEngine;
using System.Collections;

public class Tablero : MonoBehaviour {

	public static Tablero Instance;

	public static readonly int Filas =8;
	public static readonly int Columnas=8;
	public GameObject Pieza;

	public Sprite CeldaNegra, CeldaBlanca, PiezaRoja, PiezaAzul;

	public GameObject [,]TableroO;
	float PosInicialCeldaX = -3.531694f;

	float PosInicialCeldaY =3.545118f;
	bool Turno=true;
	// Use this for initialization
	void Start () {
		Instance = this;
		CrearTablero ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void CrearTablero ()
	{
		TableroO = new GameObject[Filas,Columnas];

		
		for (int i =0; i<Filas; i++) {
			for (int j = 0; j<Columnas; j++) {
				
				
				ColocarCeldasPiezas(i,j); 
				PosInicialCeldaX += 0.945499f;
			}
			
			Turno = Turno?false:true;
			PosInicialCeldaX = -3.531694f;
			PosInicialCeldaY -= 0.928534f;
			
			
		}
		
	}
	
	void ColocarCeldasPiezas(int i, int j)
	{
		// Creo la Celda con sus cordenadas en "X" y "Y"
		GameObject PiezaObject ;
		PiezaObject=(GameObject)GameObject.Instantiate (Pieza, new Vector3 (PosInicialCeldaX, PosInicialCeldaY, 0.0f), Quaternion.identity);

		//Saco la Clase de la celda
		var PiezaScript = (Pieza)PiezaObject.GetComponent("Pieza");
		//Le coloco sus valores de posiciones de filas y columnas
		PiezaScript.PosX=i;
		PiezaScript.PosY=j;
	
		if (Turno) {	
			//Cuando Turno es verdadero significa que la Celda debe ser Blanca
			Turno = false;
			PiezaObject.GetComponent<SpriteRenderer> ().sprite = CeldaBlanca;
			PiezaScript.Colores="Blanca";
			PiezaScript.OcupadoPor="Neutral";
			PiezaScript.Tipo="Celda";
			PiezaScript.Id_Jugador=-1;

		}else if(!Turno){
			// Si el turno es es falso , la celda debe ir en Negro, tambien se colocan su respectiva piezas
			Turno = true;
			bool esPieza = PonerPiezas(i,j,PiezaScript,PiezaObject);
			if(!esPieza)
			{
				PiezaObject.GetComponent<SpriteRenderer> ().sprite = CeldaNegra;
				PiezaScript.Colores="Negra";
				PiezaScript.OcupadoPor="Neutral";
				PiezaScript.Tipo="Celda";
				PiezaScript.Id_Jugador=-1;

			}
						
		}
		
		TableroO [i, j] = PiezaObject;
		 
	}
	
	bool PonerPiezas(int i,int j,Pieza piezaScript,GameObject EsPieza){

		if (i < 3 || i > 4) {
			GameObject PiezaO =EsPieza;
			Pieza PiezaScript = piezaScript;
			PiezaScript.Tipo="Pieza";
			
			if(i<3)
			{
				PiezaO.GetComponent<SpriteRenderer> ().sprite = PiezaAzul;
				PiezaScript.Colores="Azul";
				PiezaScript.Id_Jugador=2;
				PiezaScript.OcupadoPor="Player2";
				
			}else if(i>4)
			{
				PiezaO.GetComponent<SpriteRenderer> ().sprite = PiezaRoja;
				PiezaScript.Colores="Rojo";
				PiezaScript.Id_Jugador=1;
				PiezaScript.OcupadoPor="Player1";
				
			}
			return true;
		}

		return false;

		
	}


	

}
