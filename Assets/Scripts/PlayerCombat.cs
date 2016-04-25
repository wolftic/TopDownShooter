using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCombat : MonoBehaviour {
	public Weapon[] weapons;
	public int currentWeapon;
	public Text wep, bul;
	Animator anim;
	PlayerMovement pm;

	void Start () {
		anim = GetComponent<Animator> ();		
		pm = GetComponent<PlayerMovement> ();		
	}

	void Update () {
		if (Input.GetMouseButton (0)) {
			weapons [currentWeapon-1].Shoot ();
		}

		currentWeapon += (int)(Input.GetAxis ("Mouse ScrollWheel")*10);
		if (currentWeapon <= 0) {
			currentWeapon = weapons.Length;
		} else if(currentWeapon>weapons.Length) {
			currentWeapon = 1;
		}

		for (int i = 0; i < weapons.Length; i++) {
			if (currentWeapon-1 == i) {
				weapons [i].gameObject.SetActive (true);
			} else {
				weapons [i].gameObject.SetActive (false);
			}
		}

		wep.text = weapons [currentWeapon-1].name;
		bul.text = weapons [currentWeapon-1].bullets + "/" + weapons[currentWeapon-1].maxBullets;
	}
}