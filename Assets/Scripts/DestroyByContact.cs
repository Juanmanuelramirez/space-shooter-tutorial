using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start(){
		//Buscamos si exite el objeto GameController
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			//Instanciamos el objeto GameController
			gameController = gameControllerObject.GetComponent <GameController>();


		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter (Collider other) {

		if (other.tag == "Boundary" || other.tag == "Enemy")
		{
			return;
		}

		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}
			
		if (other.tag == "Player")
		{
			gameController._numLives--;
			gameController.UpdateLives (gameController._numLives);
			if (gameController._numLives == 0) {
				gameController.GameOver ();
				Destroy (other.gameObject);
			}
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);

		}

		gameController.AddScore (scoreValue);

		Destroy (this.gameObject);

	}
}
