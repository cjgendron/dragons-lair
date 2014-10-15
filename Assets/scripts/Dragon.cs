using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dragon : MonoBehaviour {

	//Stats
	int level = 1;
	float levelCoeff = 1f;
	public float gold = 0;
	float goldCounter = 0;
	public float health = 100f;
	float maxHealth = 100;
	float healthCounter = 0;
	public static float infamy = 0;
	public float movementSpeed = 5f;
    float spriteOrientation = 1f;

	//Win Conditions
	public int winNum = 6;
	int winCount = 0;

	//Pausing and Leveling
	bool paused = false;
	bool levelingUp = false;


	// the flame object has to be the first child
	ParticleSystem flame;
	GameObject flameTarget;
	public float flameRange = 20f;
	public float flamePower = 20f; // damage per second
	public float baseAttack;

	//Overlays
    public GameObject minusOneHealthPrefab;
    public GameObject goldPlusOnePrefab;
    public GameObject plusOneHealthPrefab;
    public GameObject plusLotsHealthPrefab;
    public GameObject levelupPrefab;
    public GameObject pausePrefab1;
    public GameObject pausePrefab2;
    public GameObject pausePrefab3;

	public GUISkin customSkin;

    Slider healthSlider;


	// Use this for initialization
	void Start () {
        infamy = 0;
		baseAttack = flamePower;
		flame = transform.GetChild (0).GetComponent<ParticleSystem>();
		flame.Stop ();
		maxHealth=health;
        healthSlider = transform.Find("UI").GetChild(0).GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		CheckForPause ();
		if (!paused){
			if (infamy > 100 * level * levelCoeff){
				LevelUp();
			}
			Move ();
			BreatheFire ();
		}	
	}

	void OnGUI() {
        //update value

        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;

        GUI.skin = customSkin;
        //// healthbar needs to be styled, but it depicts the current health, and stays above the dragon
        Vector2 targetPos;
        targetPos = Camera.main.WorldToScreenPoint (transform.position);
        int roundedGold = (int) (gold*100);
		
        //GUI.HorizontalSlider(new Rect(targetPos.x - 20, Screen.height - (targetPos.y + 20), 40, 20), (float)health, 0.0F, maxHealth);

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
        audio.Play();
        audio.loop = true;
	}

	void LoseTarget() {
		flame.Stop ();
		flameTarget = null;
        audio.Stop();
	}

	void ReceiveGold (float amount) {
        gold += amount;
        goldCounter += amount;

        if (goldCounter > 0.5){
        	goldCounter = 0;
        	GameObject.Instantiate(goldPlusOnePrefab, transform.position + new Vector3(0, -1f, 0), transform.rotation);
        }
	}

	void ReceiveDamage(float damage)
    {
        health -= damage;
        healthCounter += damage;
        if (health < 0f)
        {
            Die();
        }
        if (healthCounter > 1){
        	healthCounter = 0;
        	GameObject.Instantiate(minusOneHealthPrefab, transform.position + new Vector3(-1.5f, 0, 0), transform.rotation);
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
    		health += amount;
    		healthCounter += amount;
    		if (healthCounter >2){
    			GameObject.Instantiate(plusOneHealthPrefab, transform.position + new Vector3(0.5f, 0.7f, 0), transform.rotation);
    			healthCounter = 0;
    		}
    	}
    }

<<<<<<< HEAD
=======
    void LevelUp(){
    	maxHealth += 50*levelCoeff;
    	health += 20*levelCoeff;
    	flamePower += 2 * levelCoeff;
    	level += 1;
    	levelCoeff += 0.1f;
    	if (level >3){
    		Pause();
    	}
    	GameObject.Instantiate(levelupPrefab, transform.position + new Vector3(1, 0, 0), transform.rotation);
    }

    void sendStats(GameObject menu){
    	int[] args = new int[5] { (int)(gold*100), (int) baseAttack, (int)level, (int) health, (int) maxHealth};
    	menu.SendMessage("setStats", args);
    }

>>>>>>> 591f963f72aeb5c901b9f661da06f3f2ddd5c275
    void ObjectiveDestroyed(int count){
    	winCount += count;
    	GameObject.Instantiate(plusLotsHealthPrefab, transform.position + new Vector3(0, 0.7f, 0), transform.rotation);
    	healthCounter = 0;
    	if (winCount == winNum){
            Win();
    	}
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

            // don't flip the healthbar
            healthSlider.transform.localScale = Vector3.Scale(transform.localScale, new Vector3(1, 1, 1));
        }



		transform.Translate (dx, Space.World);
		//if (dx.sqrMagnitude > 0.0001)
		//				prevDx = dx;
	}


    void LevelUp(){
    	maxHealth += 50*levelCoeff;
    	health += 20*levelCoeff;
    	flamePower += 2 * levelCoeff;
    	level += 1;
    	levelCoeff += 0.1f;
    	GameObject.Instantiate(levelupPrefab, new Vector3(-4.5f, 0, 0), transform.rotation);
    	paused = true;
    	levelingUp = true;
    }

    void sendStats(GameObject menu){
    	int[] args = new int[5] { (int)(gold*100), (int) flamePower, (int)level, (int) health, (int) maxHealth};
    	menu.SendMessage("setStats", args);
    }

    void BuyStuff (string type){
    	if (type == "attack"){
    		Debug.Log("attack");
    		if (gold > 10) {
    			Debug.Log("attackin");
    			gold -= 10;
	    		flamePower += 2;
    		}
    	}
    	else if (type == "health"){
    		Debug.Log("health");
    		if (gold > 2){
    			Debug.Log("healthin");
    			gold -= 2;
    			health += 5;
    		}
    	}
    	else if (type == "lair"){
    		if (gold > 3){
    			gold -= 3;
    			GameObject.Find("Lair").SendMessage("BuyStuff", "lair");
    		}
    		
    	}
    }

	void Pause(){
		if (level > 7){
			GameObject.Instantiate(pausePrefab3, new Vector3(-4, 0, 0), transform.rotation);
		}
		else if (level > 3){
			GameObject.Instantiate(pausePrefab2, new Vector3(-4, 0, 0), transform.rotation);
		}
		else if (level > 0){
			GameObject.Instantiate(pausePrefab1, new Vector3(-4, 0, 0), transform.rotation);
		}
	}


	void CheckForPause(){
		//open and close pause menu
		if (Input.GetKeyUp("p")){
			if (paused && !levelingUp){
				paused = false;
			}
			else{
				Pause();
				paused = true;
			}
		}

		//close leveling up menu
		if (levelingUp && Input.anyKeyDown && !Input.GetMouseButtonDown(0)) {
			levelingUp = false;
			paused = false;
			GameObject.FindGameObjectWithTag("Stupid").SendMessage("Done");
       }
	}

	public bool isPaused(){
		return paused;
	}

    void Die()
    {
        GameObject hiscore = GameObject.Find("HiScore");
        int[] args = new int[4] { (int)gold, (int)infamy, (int)level, 0 };
        hiscore.SendMessage("setScores", args);
        Destroy(gameObject);
    }

    void Win()
    {
        GameObject hiscore = GameObject.Find("HiScore");
        int[] args = new int[4] { (int)gold, (int)infamy, (int)level, 1 };
        Destroy(gameObject);
        hiscore.SendMessage("setScores", args);
    }

}
