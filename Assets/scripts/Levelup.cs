using UnityEngine;
using System.Collections;

public class Levelup : MonoBehaviour {
    int gold;
    int attack;
    int health;
    int maxHealth;
    int lairHealth;
    int level;
    string text;

    public GameObject lair;
    public GameObject dragon;

    public GUIText GUIText;

	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void setStats (int [] args) {
        gold = args[0];
        attack = args[1];
        level = args[2];
        health = args[3];
        maxHealth = args[4];
        updateText();
    }

    void updateText () {
        text = "Gold: " + gold.ToString() + "\n Attack: " + attack.ToString()
            + "\n Health: " + health.ToString() + "/" + maxHealth.ToString();
        GUIText.text = text;
    }
}
