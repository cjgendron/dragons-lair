using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

	public int food = 30;
	public float population = 40f;
	public int gold = 10;
	public int armor = 5;


	public bool canSpawnChampions = false;
	public GameObject championType;

	public bool canAttack = false;
	public int attack = 5;

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

		population -= num;
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
		
		GUI.HorizontalSlider(new Rect(targetPos.x, Screen.height - (targetPos.y + 10), 60, 20), (float)population, 0.0F, 40.0F);
		string stats = "F: " + food.ToString () + "\n P: " + population.ToString () + "\n A: " + armor.ToString();
		GUI.Box (new Rect(targetPos.x + 60, Screen.height - targetPos.y, 50, 50), stats);

	}

}
