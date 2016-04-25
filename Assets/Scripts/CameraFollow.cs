using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform target;
	public Vector3 pos;
	public float zoom;

	void Start () {
	
	}

	void Update () {
		zoom = Mathf.Clamp (zoom, 0, 100);
		if(target)transform.position = Vector3.Lerp(transform.position, Vector3.Lerp (target.position, target.position + pos, zoom / 100), 15 * Time.deltaTime);	
	}
}
