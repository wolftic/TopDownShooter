using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float damage;
	public float speed;

	void Start () {
		Destroy (gameObject, 5);
	}

	void Update () {
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Enemy") {
			EnemyMovement em = col.GetComponent<EnemyMovement> ();
			em.health -= damage;
			if (em.health <= 0) {
				GameObject.FindGameObjectWithTag ("EH").GetComponent<Text> ().text = "";
				GameObject.FindGameObjectWithTag ("EN").GetComponent<Text> ().text = "";				
				return;
			} else {
				GameObject.FindGameObjectWithTag ("EH").GetComponent<Text> ().text = em.health + "/" + em.maxHealth;
				GameObject.FindGameObjectWithTag ("EN").GetComponent<Text> ().text = em.name;				
			}
		}
		Destroy (gameObject);
	}
}