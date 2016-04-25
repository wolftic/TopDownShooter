using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class Weapon : MonoBehaviour {
	public Transform chamber;
	public Bullet bulletprefab;
	public bool shotgun;
	public float bullets;
	public float maxBullets;
	public float reloadTime;
	public float shootSpeed;
	public AudioClip sound;
	public AudioSource source;

	float curTime;
	float waitTime;

	void Start () {
	
	}

	void Update () {
		if (curTime <= Time.time && curTime != 0) {
			bullets = maxBullets;
			curTime = 0;
		}

		if (waitTime <= Time.time && waitTime != 0) {
			waitTime = 0;
		}
	}

	public void Shoot () {
		if (bullets <= 0) {
			Reload ();
			return;
		}
		if (waitTime != 0) {
			return;
		}
		StartCoroutine (bulletShoot ());
	}

	IEnumerator bulletShoot() {
		source.PlayOneShot (sound);
		if (shotgun) {
			for (float i = -1.5f; i < 1.5f; i++) {
				Bullet bullet = Instantiate (bulletprefab, chamber.position, chamber.rotation) as Bullet;
				bullet.transform.Rotate (0, i * 10, 0);
			}
		} else {
			Instantiate (bulletprefab, chamber.position, chamber.rotation);
		}
		curTime = 0;
		bullets--;
		waitTime = Time.time + shootSpeed;
		yield break;
	}

	public void Reload () {
		if (curTime != 0)
			return;
		
		curTime = Time.time + reloadTime;
	}
}