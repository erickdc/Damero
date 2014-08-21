using UnityEngine;
using System.Collections;

public class Pieza :Celda{

	public int Id_Jugador { get; set; }

	public Pieza (int posX,int posY,string color,string ocupado,int id_Jugador,string tipo):base(posX,posY,color,ocupado,tipo)
	{
		this.Id_Jugador = id_Jugador;
	}

	void OnMouseDown()
	{
		
		AdministracionJuego.Instance.Seleccion(this.gameObject);
	}

	public void Mover(string Colores,int Id_Jugador,string Tipo)
	{

		this.Colores = Colores;
		this.Id_Jugador = Id_Jugador;
		this.Tipo = Tipo;


	}

	void Update()
	{
		if (Input.touchCount == 1)
		{
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (collider2D == Physics2D.OverlapPoint(touchPos))
			{
				AdministracionJuego.Instance.Seleccion(this.gameObject);
			}
		}

	}
}
