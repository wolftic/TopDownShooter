using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameHandler : MonoBehaviour {
	public InputField nameField;
	public string Name;
	public Transform snowman;
	public float enemycount;
	float currentWave;
	float maxcount;

	public GameObject WAVE;
	public GameObject WAVECOUNT;
	public GameObject SNOWMANLEFT;

	Coroutine game;

	void Start () {
		DontDestroyOnLoad (gameObject);
	}

	void Update () {
		if (SceneManager.GetActiveScene ().buildIndex == 1) {
			SNOWMANLEFT = GameObject.FindGameObjectWithTag("MANLEFT");
			SNOWMANLEFT.GetComponent<Text> ().text = enemycount + "/" + maxcount;
		}
	}

	IEnumerator gameWork(float wave) {
		yield return new WaitForSeconds (1f);
		enemycount = (wave * 5);
		maxcount = (wave * 5);
		currentWave = wave;
		WAVE = GameObject.FindGameObjectWithTag("WAVE");
		WAVE.GetComponent<Text> ().text = "Wave " + wave;
		WAVE.GetComponent<Animator> ().SetTrigger ("ShowText");

		WAVECOUNT = GameObject.FindGameObjectWithTag("WAVECOUNT");
		WAVECOUNT.GetComponent<Text> ().text = "" + wave;


		Debug.Log ("Wave start!");

		for (int i = 0; i < (wave * 5); i++) {
			Instantiate (snowman, new Vector3 (Random.Range(-50, 50), 0, Random.Range(-50, 50)), Quaternion.identity);
		}

		yield return new WaitUntil (() => enemycount <= 0);
		Debug.Log ("Wave won!");
		WAVE.GetComponent<Text> ().text = "Wave won!";
		WAVE.GetComponent<Animator> ().SetTrigger ("ShowText");
		enemycount = 0;
		yield return new WaitForSeconds (5f);

		StartCoroutine (gameWork (wave + 1));
	}

	public void startGame () {
		nameField = GameObject.FindGameObjectWithTag ("NF").GetComponent<InputField> ();
		Name = nameField.text;
		Debug.Log (Name);
		SceneManager.LoadScene (1);
		game = StartCoroutine (gameWork (1));
	}

	public void endGame () {
		StopCoroutine (game);
		StartCoroutine (gameEnd ());
	}

	IEnumerator gameEnd () {
		WAVE.GetComponent<Text> ().text = "You've lost";
		WAVE.GetComponent<Animator> ().SetTrigger ("ShowText");
		yield return new WaitForSeconds (2f);
		WAVE.GetComponent<Text> ().text = "Survived until wave " + currentWave + "!";
		WAVE.GetComponent<Text> ().fontSize = 45;
		WAVE.GetComponent<Animator> ().SetTrigger ("ShowText");
		yield return new WaitForSeconds (2.5f);
		SceneManager.LoadScene (0);
		Destroy (gameObject);
		yield break;
	}
}