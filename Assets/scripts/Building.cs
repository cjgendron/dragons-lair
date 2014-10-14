using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

	public string type;
	public float food = 30f;
	public float population = 40f;
	public float maxPopulation = 100f;
	float initPopulation;
	
	public float goldRate; // per second
	public float regenRate;
	public float foodRate = 1f;

	public float armor = 1f;


	bool canSpawnChampions = false;
	public float maxChampionSpawnRate = 30f;
	public float minChampionSpawnRate = 2f;
	float timeSinceSpawned = 999999999f;

	public GameObject championType;

	bool canAttack = true;
	public float attack = 5f;

	int timer = 0;
	public float spawnCoeff;
	float spawnTime;
	float timeLeft;
	float dragonInfamy;

	public GUISkin customSkin;

	// Use this for initialization
	void Start () {
		spawnTime = spawnCoeff * transform.position.magnitude * 10;
		initPopulation = population;
		if (type == "farm"){
			canAttack = false;
		}
		if (type == "farm"){
			canSpawnChampions = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (canAttack)
            Attack();

		timeLeft = spawnTime-timer;
		timer += 1;
		if (timer > spawnTime){
			spawnChampion();
			timer=0;
		}
		if (canSpawnChampions)
			trySpawnChampion ();
		repair();
	}

	void repair() {
		if (population<maxPopulation){
			float regenCoeff = population / maxPopulation;
			population += regenCoeff * regenRate * Time.deltaTime;
			if (population > maxPopulation){
				maxPopulation = population;
			}
		}
		food += foodRate * Time.deltaTime;
	}

	void trySpawnChampion() {
		timeSinceSpawned += Time.deltaTime;
		if (population == initPopulation)
						return;
        if (Helpers.getDistance("Dragon", this.gameObject) < 2)
            return;
		float timeNeeded = population / initPopulation 
			* (maxChampionSpawnRate - minChampionSpawnRate) + minChampionSpawnRate;

		if (timeSinceSpawned > timeNeeded) {
			spawnChampion();
			timeSinceSpawned = timeSinceSpawned % timeNeeded;
		}

	}

    void Attack()
    {
        GameObject dragon = GameObject.Find("Dragon_prefab");
        if (Helpers.getDistance("Dragon_prefab", this.gameObject) < 2)
        {
            dragon.SendMessage("ReceiveDamage", Time.deltaTime * attack);
        }
    }

	void ReceiveDamage(object[] vars) {
		float num = (float) vars[1];
		GameObject dragon = vars [0] as GameObject;

		population -= num / armor;

		dragon.SendMessage ("ReceiveGold", goldRate * Time.deltaTime);
		dragon.SendMessage ("IncreaseInfamy", goldRate * Time.deltaTime * 6);

		//update spawntime
		//float coeff = dragon.SendMessage("IncreaseInfamy", goldRate * Time.deltaTime * 6);
		//spawnCoeff = 1000 / coeff;

		if (population < 0f) {
			dragon.SendMessage("IncreaseHealth", food*2);
			dragon.SendMessage("LoseTarget");
			Destroy (gameObject);
		}
		if (type=="farm"){
			food -= num / 2 / armor;

			dragon.SendMessage ("IncreaseHealth", num / armor);
		}
	}

	void spawnChampion(){
		Instantiate(championType, transform.position, transform.rotation);
	}

	// elements are set with respect to the pivot point (I think!)
	// since our pivot points aren't always in the middle, this will be off-center for some sprites
	void OnGUI() {
		GUI.skin = customSkin;
		// healthbar needs to be styled, but it depicts the current health, and stays above the dragon
		Vector2 targetPos;
		targetPos = Camera.main.WorldToScreenPoint (transform.position);
		int roundedPopulation = (int) population;
		
		GUI.HorizontalSlider(new Rect(targetPos.x, Screen.height - (targetPos.y + 10), 60, 20), (float)population, 0.0F, initPopulation);
		string stats = "F: " + ((int)food).ToString () + "\n P: " + roundedPopulation.ToString () + "\n S: " + ((int)timeLeft).ToString();
		if (type=="farm"){
			stats = "F: " + ((int)food).ToString () + "\n P: " + roundedPopulation.ToString () + "\n S: " + "0";
		}
		GUI.Box (new Rect(targetPos.x + 60, Screen.height - targetPos.y, 50, 50), stats);

	}

}
