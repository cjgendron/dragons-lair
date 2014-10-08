using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

	public int food = 30;
	public float population = 40f;
	
	public float goldRate = 3f; // per second

	public float armor = 1f;


	public bool canSpawnChampions = false;
	public GameObject championType;

	public bool canAttack = false;
	public float attack = 5f;

	public GUISkin customSkin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ReceiveDamage(object[] vars) {
		float num = (float) vars[1];
		GameObject dragon = vars [0] as GameObject;

		population -= num / armor;

		dragon.SendMessage ("ReceiveGold", goldRate * Time.deltaTime);

		if (population < 0f) {
			dragon.SendMessage("LoseTarget");
			Destroy (gameObject);
		}
	}

	// elements are set with respect to the pivot point (I think!)
	// since our pivot points aren't always in the middle, this will be off-center for some sprites
	void OnGUI() {
		GUI.skin = customSkin;
		// healthbar needs to be styled, but it depicts the current health, and stays above the dragon
		Vector2 targetPos;
		targetPos = Camera.main.WorldToScreenPoint (transform.position);
		int roundedPopulation = (int) population;
		
		GUI.HorizontalSlider(new Rect(targetPos.x, Screen.height - (targetPos.y + 10), 60, 20), (float)population, 0.0F, 40.0F);
		string stats = "F: " + food.ToString () + "\n P: " + roundedPopulation.ToString () + "\n A: " + armor.ToString();
		GUI.Box (new Rect(targetPos.x + 60, Screen.height - targetPos.y, 50, 50), stats);

	}

}
