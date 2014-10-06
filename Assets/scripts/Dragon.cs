using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour {

	public int gold = 0;
	public int health = 100;
	public int infamy = 0;

	public float movementSpeed = 5f;
	Vector3 prevDx; // used for rotation

	// the flame object has to be the first child
	ParticleSystem flame;
	GameObject flameTarget;
	public float flameRange = 20f;

	// Use this for initialization
	void Start () {
		flame = transform.GetChild (0).GetComponent<ParticleSystem>();
		flame.Stop ();
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
		Rotate ();

		BreatheFire ();
	}


	// A function responsible for setting up the fire. Runs every frame.
	void BreatheFire() {
		GameObject target = GetClosestTarget (flameRange);
		if (flameTarget != target) {
			LoseTarget();
			if (target != null) SetTarget(target);
		}
		if (flameTarget != null) {			
			Vector3 direction = target.transform.position - transform.position;
			flame.transform.rotation = Helpers.rotateTowards2D (direction, -90);
		}
	}

	void SetTarget(GameObject target) {
		flameTarget = target;
		flame.Play ();
	}

	void LoseTarget() {
		flame.Stop ();
		flameTarget = null;
	}



	GameObject GetClosestTarget(float maxDistance = 9999999f) {
		// there's a more efficient way to do this if the physics module is used
		// but this is good enough for now
		GameObject[] gos = GameObject.FindGameObjectsWithTag ("Damageable");
		float minDist = 999999f;
		GameObject closest = null;

		foreach (GameObject g in gos) {
			float distance = (g.transform.position - transform.position).sqrMagnitude; 
			if (distance < minDist && distance < maxDistance) {
				closest = g;
				minDist = distance;
			}
		}
		return closest;
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

		//transform.rotation = Helpers.rotateTowards2D (prevDx, 0f);
			
	}
}
