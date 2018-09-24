using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sera visible en el inspector siendo SErializable
[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float speed;
	public float tilt;
	[SerializeField]
	private float speedMove;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;

	// Use this for initialization
	void Update () {
		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			//ejecutamos el sonido del disparo 
			GetComponent<AudioSource>().Play ();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		#if UNITY_ANDROID
		float moveHorizontal = Input.acceleration.x;
		float moveVertical = Input.acceleration.y;
		#else
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		#endif


		//controlamos el movimiento horizontal, vertical y velocidad
		Vector3 movement = new Vector3 (moveHorizontal * speedMove, 0.0f, moveVertical * speedMove);
		GetComponent<Rigidbody> ().velocity = movement * speed; 

		//aquí limitamos el espacio donde se movera en un rango de xMin, xMax
		//zMin y zMax
		GetComponent<Rigidbody>().position = new Vector3 (
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);

		//Ponemos el efecto de inclinación al momento del movimiento a la derecha o izquierda
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	
	}
}
