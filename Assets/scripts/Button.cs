using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
    GameObject dragon;
    GameObject lair;
    public string type;
    int timer = 0;

	// Use this for initialization
	void Start () {
        dragon = GameObject.Find("Dragon_prefab");
        lair = GameObject.Find("Lair");
	}
	
	// Update is called once per frame
	void Update () {
       if (renderer.enabled == false){
            timer += 1;
            if (timer == 5){
                renderer.enabled = true;
                timer = 0;
            }
       }
	}

    void OnMouseDown () {
        dragon.SendMessage("BuyStuff", type);
        renderer.enabled = false;
    }
}
