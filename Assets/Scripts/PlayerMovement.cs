using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float Speed;
	public float Health;
	public float maxHealth;
	public Text name;
	public Text health;
	public Text WAVE;
	public Image blood;
	public float opacityblood;
	Animator anim;
	Coroutine countdowntimer;

	float outofboundstime;

	void Start () {
		anim = GetComponent<Animator> ();
		name.text = GameObject.FindGameObjectWithTag("GH").GetComponent<GameHandler>().Name;
		maxHealth = Health;
	}

	void Update () {
		name.text = GameObject.FindGameObjectWithTag("GH").GetComponent<GameHandler>().Name;
		health.text = Health + "/" + maxHealth;
		Vector3 horizontal = Input.GetAxis("Horizontal") * Camera.main.transform.right;
		Vector3 vertical = Input.GetAxis("Vertical") * new Vector3(-0.5f, 0, 0.5f);
		Vector3 direction = horizontal + vertical;
		if (direction != Vector3.zero) {
			transform.position += (direction * Speed * Time.deltaTime);
			anim.SetFloat ("speed", direction.magnitude);
			anim.speed = direction.magnitude;
		} else {
			anim.speed = 1;
		}

		opacityblood -= 0.2f * Time.deltaTime;
		if (opacityblood > 1)
			opacityblood = 1;

		if (opacityblood < 0)
			opacityblood = 0;

		blood.color = Color.Lerp (new Color32 (255, 255, 255, 0), new Color32 (255, 255, 255, 100), opacityblood);

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		Plane plane = new Plane (Vector3.up, Vector3.zero);
		float dist;
		if (plane.Raycast (ray, out dist)) {
			Vector3 point = ray.GetPoint (dist);
			point = new Vector3 (point.x, transform.position.y, point.z);
			transform.LookAt (point);
			transform.Rotate (0, 90, 0);
		}

		if (Health <= 0) {
			GameObject.FindGameObjectWithTag ("GH").GetComponent<GameHandler> ().endGame();
			Destroy (gameObject);
		}

		if (Vector3.Distance (Vector3.zero, transform.position) > 75f && outofboundstime == 0) {
			Debug.Log ("out of bounds");
			WAVE.GetComponent<Text> ().text = "You're out of bounds!";
			WAVE.GetComponent<Animator> ().SetTrigger ("ShowText");
			outofboundstime = Time.time + 6.5f;

			countdowntimer = StartCoroutine (countdown ());
		}

		if (Vector3.Distance (Vector3.zero, transform.position) <= 75f) {
			if (outofboundstime != 0) {
				StopCoroutine (countdowntimer);
				outofboundstime = 0;
			}
		}

		if (outofboundstime != 0 && outofboundstime <= Time.time && Vector3.Distance (Vector3.zero, transform.position) > 20f) {
			Health -= Health;
		}
	}

	IEnumerator countdown() {
		yield return new WaitForSeconds (1.5f);
		WAVE.GetComponent<Text> ().text = "5";
		WAVE.GetComponent<Animator> ().SetTrigger ("ShowText2");	
		yield return new WaitForSeconds (1f);
		WAVE.GetComponent<Text> ().text = "4";
		WAVE.GetComponent<Animator> ().SetTrigger ("ShowText2");		
		yield return new WaitForSeconds (1f);
		WAVE.GetComponent<Text> ().text = "3";
		WAVE.GetComponent<Animator> ().SetTrigger ("ShowText2");	
		yield return new WaitForSeconds (1f);
		WAVE.GetComponent<Text> ().text = "2";
		WAVE.GetComponent<Animator> ().SetTrigger ("ShowText2");	
		yield return new WaitForSeconds (1f);
		WAVE.GetComponent<Text> ().text = "1";
		WAVE.GetComponent<Animator> ().SetTrigger ("ShowText2");	
	}
}