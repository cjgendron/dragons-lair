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
    public GameObject hardcoreLevel1;
    public GameObject hardcoreLevel2;
    public GameObject hardcoreLevel3;
    Object hardcore;

    public GUIText GUIText;

	// Use this for initialization
	void Start () {
        level = GameObject.Find("Dragon_prefab").GetComponent<Dragon>().GetLevel();
        if (level > 6){
            hardcore = GameObject.Instantiate(hardcoreLevel3, new Vector3(-5.3f, 2f, 0), transform.rotation);
        }
        else if (level > 3){
            hardcore = GameObject.Instantiate(hardcoreLevel2, new Vector3(-5.3f, 2f, 0), transform.rotation);
        }
        else if (level > 0){
            hardcore = GameObject.Instantiate(hardcoreLevel1, new Vector3(-5.3f, 2f, 0), transform.rotation);
        }
        
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
