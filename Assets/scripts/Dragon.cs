using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dragon : MonoBehaviour {

	public float gold = 0;
	public float health = 100f;
	float maxHealth = 100;
	public static float infamy = 0;
	int level = 1;
	float levelCoeff = 1f;

	public float movementSpeed = 5f;
	//Vector3 prevDx; // used for rotation
    float spriteOrientation = 1f;

	// the flame object has to be the first child
	ParticleSystem flame;
	GameObject flameTarget;
	public float flameRange = 20f;
	public float flamePower = 20f; // damage per second
	public float baseAttack;


	public GUISkin customSkin;

	// Use this for initialization
	void Start () {
		baseAttack = flamePower;
		flame = transform.GetChild (0).GetComponent<ParticleSystem>();
		flame.Stop ();
		maxHealth=health;
	}
	
	// Update is called once per frame
	void Update () {
		if (infamy > 100 * level * levelCoeff){
			LevelUp();
		}
		Move ();
		Rotate ();

		BreatheFire ();
	}

	void OnGUI() {
		GUI.skin = customSkin;
		// healthbar needs to be styled, but it depicts the current health, and stays above the dragon
		Vector2 targetPos;
		targetPos = Camera.main.WorldToScreenPoint (transform.position);
		int roundedGold = (int) (gold*100);
		
		GUI.HorizontalSlider(new Rect(targetPos.x - 20, Screen.height - (targetPos.y + 20), 40, 20), (float)health, 0.0F, 100.0F);

		GUI.Box (new Rect(15, 15, 120, 70), "Gold: " + roundedGold.ToString() + "\n Infamy: " + ((int) infamy).ToString()
			+ "\n Health: " + ((int) health).ToString() + "/" + ((int) maxHealth).ToString()
			// + "\n Attack: " + flamePower.ToString() + "\n Armor: 20" + "\n Speed: " + ((int) movementSpeed).ToString()
			+ "\n Attack: " + ((int) flamePower).ToString());
	}

	// A function responsible for setting up the fire. Runs every frame.
	void BreatheFire() {
		GameObject target = GetClosestTarget (flame.transform.position, flameRange);
		if (flameTarget != target) {
			LoseTarget();
			if (target != null) SetTarget(target);
		}
		if (flameTarget != null) {			
			Vector3 direction = target.transform.position - flame.transform.position;
			flame.transform.rotation = Helpers.rotateTowards2D (direction, -90);

			// you can only pass one variable, so it has to be an array
			object[] vars = new object[2] {gameObject, Time.deltaTime * flamePower};
			flameTarget.SendMessage("ReceiveDamage", vars);
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

	void ReceiveGold (float amount) {
		gold += amount;
	}

	void ReceiveDamage(float damage)
    {
        health -= damage;
        if (health < 0f)
        {
            Destroy(gameObject);
            Application.LoadLevel("StartScene");
        }

    }
    public float GetInfamy(){
    	return infamy;
    }

    void IncreaseInfamy(float amount) {
    	infamy += amount;
    }

    void IncreaseHealth(float amount) {
    	if (health < maxHealth){
    		health+=amount;
    	}
    }

    void LevelUp(){
    	maxHealth += 50*levelCoeff;
    	flamePower += 2 * levelCoeff;
    	level += 1;
    	levelCoeff += 0.1f;

    }

	GameObject GetClosestTarget(Vector3 from, float maxDistance = 9999999f) {
		// there's a more efficient way to do this if the physics module is used
		// but this is good enough for now
		GameObject[] gos = GameObject.FindGameObjectsWithTag ("Damageable");
		float minDist = 999999f;
		GameObject closest = null;

		foreach (GameObject g in gos) {
			float distance = (g.transform.position - from).sqrMagnitude; 
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
        
        //flip the sprite if orientation and input are opposites
        if (spriteOrientation * Input.GetAxis("Horizontal") < 0)
        {
            spriteOrientation *= -1;
            transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
        }



		transform.Translate (dx, Space.World);
		//if (dx.sqrMagnitude > 0.0001)
		//				prevDx = dx;
	}

	void Rotate() {
		// there's no LookAt function for 2d :(
		// -90 is needed because the sprite is not rotated correctly

		//transform.rotation = Helpers.rotateTowards2D (prevDx, 0f);
			
	}

}
