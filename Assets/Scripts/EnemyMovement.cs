using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	private NavMeshAgent agent;
	public float health;
	public float maxHealth;
	private Transform target;

	private float curTime;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		maxHealth = health;
	}

	void Update () {
		if (!target)
			return;

		if (Vector3.Distance (transform.position, target.position) > 1.5f) {
			agent.SetDestination (target.position);
		} else { //inrange
			if (curTime == 0) {
				target.GetComponent<PlayerMovement> ().Health -= 1;
				target.GetComponent<PlayerMovement> ().opacityblood += 0.25f;
				curTime = Time.time + 2;
			}
			agent.SetDestination (transform.position);
		}

		if (health <= 0) {
			GameObject.FindGameObjectWithTag ("GH").GetComponent<GameHandler> ().enemycount -= 1;
			Destroy (gameObject);
		}

		if (curTime <= Time.time && curTime != 0) {
			curTime = 0;
		}
	}
}