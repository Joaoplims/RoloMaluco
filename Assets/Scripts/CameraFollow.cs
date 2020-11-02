using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public float Distancia; //distância entre a câmera e o target
	public float k; //constante elástica (> +rígido +rápido)
	public GameObject target; //objeto que a câmera deve seguir
	Rigidbody rb; //rigidbody da câmera

	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
	}

	void Update () 
	{	
		float d = target.transform.position.z - transform.position.z;

		if (Mathf.Abs(d) > Distancia)  //se distância maior que o tamanho da "mola"
		{
			float f = k * (d - Distancia); //calcular força elástica
			rb.AddForce (new Vector3 (0, 0, f)); //aplicar a força elástica em x
		}
	}
}
