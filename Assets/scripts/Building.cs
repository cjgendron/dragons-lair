using UnityEngine;
using UnityEngine.UI;
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
	public float range;

	public float armor = 1f;


	public float maxChampionSpawnRate = 30f;
	public float minChampionSpawnRate = 2f;
	float timeSinceSpawned = 999999999f;

	public GameObject championType;
	Dragon player;

	float attack;
	public float atkOffset;

	public float spawnCoeff;
	float spawnTime;
	float initSpawnTime;
	float timeLeft;
	bool paused = false;

	public GUISkin customSkin;

    Text popText;
    Text foodText;
    Text spawnTimerText;
    Slider healthBar;

	// Use this for initialization
	void Start () {
		attack = transform.position.magnitude * 1.5f - atkOffset;
		spawnTime = spawnCoeff * transform.position.magnitude * 10;
		timeLeft = spawnTime-777;
		initSpawnTime = spawnTime;
		initPopulation = population;
		player = GameObject.Find("Dragon_prefab").GetComponent<Dragon>();

        healthBar = transform.Find("UI").GetChild(0).GetComponent<Slider>();
        popText = transform.Find("UI").GetChild(1).Find("Population").Find("Number").GetComponent<Text>();
        foodText = transform.Find("UI").GetChild(1).Find("Food").Find("Number").GetComponent<Text>();
        spawnTimerText = transform.Find("UI").GetChild(1).Find("Timer").Find("Number").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		Pause();
		if (!paused){
			if (type != "farm"){
				 Attack();	
			}

			//spawn Champion
			if (timeLeft < 0 && type != "farm"){
				Instantiate(championType, transform.position, transform.rotation);
				//trySpawnChampion ();
				timeLeft = spawnTime;
			}

			updateSpawnTime();
			repair();	
		}
        	
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

	void updateSpawnTime(){
		timeLeft -= 1;
		if (spawnTime > minChampionSpawnRate * 100){
			spawnTime = initSpawnTime - 1.8f * player.GetInfamy();
		}
	}

	//currently unused
	void trySpawnChampion() {
		timeSinceSpawned += Time.deltaTime;
		if (population == initPopulation)
						return;
        if (Helpers.getDistance("Dragon", this.gameObject) < 2)
            return;
		float timeNeeded = population / initPopulation 
			* (maxChampionSpawnRate - minChampionSpawnRate) + minChampionSpawnRate;

		if (timeSinceSpawned > timeNeeded) {
			Instantiate(championType, transform.position, transform.rotation);
			timeSinceSpawned = timeSinceSpawned % timeNeeded;
		}

	}

    void Attack()
    {
        GameObject dragon = GameObject.Find("Dragon_prefab");
        if (Helpers.getDistance("Dragon_prefab", this.gameObject) < range)
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

		if (population < 0f) {
			//destroy building
			dragon.SendMessage("IncreaseHealth", food*2);
			dragon.SendMessage("LoseTarget");
			dragon.SendMessage("ObjectiveDestroyed");
			Destroy (gameObject);

		}
		if (type=="farm"){
			food -= num / 2 / armor;

			dragon.SendMessage ("IncreaseHealth", num / armor);
		}
	}

	void Pause(){
		if (Input.GetKeyUp("p")){
			if (paused){
				paused = false;
			}
			else{
				paused = true;
			}
		}
	}

	// elements are set with respect to the pivot point (I think!)
	// since our pivot points aren't always in the middle, this will be off-center for some sprites
	void OnGUI() {

        popText.text = ((int) population).ToString();
        spawnTimerText.text = ((int)(timeLeft + 100) / 100).ToString();
        foodText.text =((int)food).ToString();

        healthBar.maxValue = (float) maxPopulation;
        healthBar.value = (float)population;
        //GUI.skin = customSkin;
        //// healthbar needs to be styled, but it depicts the current health, and stays above the dragon
        //Vector2 targetPos;
        //targetPos = Camera.main.WorldToScreenPoint (transform.position);
        //int roundedPopulation = (int) population;
		


        //GUI.HorizontalSlider(new Rect(targetPos.x-30, Screen.height - (targetPos.y + 50), 60, 20), (float)population, 0.0F, initPopulation);
        //string stats = "F: " + ((int)food).ToString () + "\n P: " + roundedPopulation.ToString () + "\n S: " + ((int)(timeLeft+100)/100).ToString();
        //if (type=="farm"){
        //    stats = "F: " + ((int)food).ToString () + "\n P: " + roundedPopulation.ToString () + "\n S: " + "0";
        //}
        //GUI.Box (new Rect(targetPos.x + 60, Screen.height - targetPos.y, 50, 50), stats);

	}

}
