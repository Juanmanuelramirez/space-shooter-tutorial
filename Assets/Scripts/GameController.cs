using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	[SerializeField]
	private float volumen;

	public Text scoreText;
	//public Text restartText;
	public Text gameOverText;

	private bool gameOver;
	private bool restart;
	private int score;

	public Button restartButton;

	public AudioClip soundGameover;
	private AudioSource source;


	// Use this for initialization
	void Start () {
		//Reseteamos el juego a cero
		score = 0;
		UpdateScore();
		//GameOver lo ponemos falso 
		gameOver = false;
		restart = false;
		//Ocultamos el boton de comenzar un juego nuevo
		restartButton.gameObject.SetActive (false);
		//Limpiamos el GameOver
		gameOverText.text = "";
		score = 0;

		//Creamos el objeto para el GAmeover que utilizaremos cuando nos maten
		source = GetComponent<AudioSource> ();

		//Comenzamos la coroutina de los enemigos
		StartCoroutine(SpawnWaves());

	}


	IEnumerator SpawnWaves(){
		yield return new WaitForSeconds(startWait);
		//Creamos de manera infinita los hazard esperando a la siguiente hola
		while (true)
		{
			//Generamos i número de hazard 
			for (int i = 0; i < hazardCount; i++)
			{
				//Obtenemos objeto a instanciar de manera aleatoria
				//Aquí hemos definido tres tipos de asteroirdes 
				GameObject hazard = hazards[Random.Range(0, hazards.Length)];
				//Le damos una aparición aleatoria al objeto fuera de nuestra camara
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				//Obtenemos la rotación
				Quaternion spawnRotation = Quaternion.identity;
				//Instanciamos nuestro objeto
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			//esperamos la siguiente hola de ataque
			yield return new WaitForSeconds(waveWait);

			//Si ha terminado el 
			if (gameOver)
			{
				//aqui se presiona el juego 
				restartButton.gameObject.SetActive (true);
				restart = true;
				break;
			}
		}
	}
		

	public void Restart()
	{
		if (restart)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	}
	
	// Update is called once per frame
	void UpdateScore () {
		scoreText.text = "Score: " + score;
	}

	public void GameOver()
	{
		//reproducimos el game over sound
		source.PlayOneShot (soundGameover,volumen);
		gameOverText.text = "Game Over!";
		gameOver = true;
	}
}
