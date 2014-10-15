using UnityEngine;
using System.Collections;

public class Levelup : MonoBehaviour {
    int gold;
    int attack;
    int health;
    int maxHealth;
    int lairHealth;
    int level;
    string text = "asdf";

    public GameObject lair;
    public GameObject dragon;
    public GameObject hardcoreLevel;
    Object hardcore;

    public GUIText GUIText;

	// Use this for initialization
	void Start () {
        hardcore = GameObject.Instantiate(hardcoreLevel, new Vector3(-4.5f, 1.8f, 0), transform.rotation);
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

    public void Done() {
        Destroy(gameObject);
        Destroy(hardcore);
    }
}
