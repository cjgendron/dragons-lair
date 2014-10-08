using UnityEngine;
using System.Collections;

public class Lair : MonoBehaviour {

	public float health = 100f;	
	float maxHealth;
	public GUISkin customSkin;


	// Use this for initialization
	void Start () {
		maxHealth = health;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ReceiveDamage (float num) {		
		health -= num;

		
		if (health < 0f) {
			Destroy (gameObject);
		}
	}

	void OnGUI() {
		GUI.skin = customSkin;
		// healthbar needs to be styled, but it depicts the current health, and stays above the dragon
		Vector2 targetPos;
		targetPos = Camera.main.WorldToScreenPoint (transform.position);
		
		GUI.HorizontalSlider(new Rect(targetPos.x, Screen.height - (targetPos.y + 10), 60, 20), health, 0.0F, maxHealth);
//		string stats = "F: " + food.ToString () + "\n P: " + roundedPopulation.ToString () + "\n A: " + armor.ToString();
//		GUI.Box (new Rect(targetPos.x + 60, Screen.height - targetPos.y, 50, 50), stats);
		
	}
}
