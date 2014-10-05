using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour {

	public int gold = 0;
	public int health = 100;
	public int infamy = 0;

	public float movementSpeed = 5f;
	Vector3 prevDx; // used for rotation

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
		Rotate ();
	}

	void Move () {
		// a basic movement system.
		// Should add boundary checking
		// (Smoothing is added by Unity by default)
		Vector3 dx = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0);
		dx = dx * Time.deltaTime * movementSpeed;
		transform.Translate (dx, Space.World);

		if (dx.sqrMagnitude > 0.0001)
						prevDx = dx;
	}

	void Rotate() {
		// there's no LookAt function for 2d :(
		// -90 is needed because the sprite is not rotated correctly
		float angle = Mathf.Atan2 (prevDx.y, prevDx.x) * Mathf.Rad2Deg - 90f;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
	}
}
