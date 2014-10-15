using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoldInfamyUI : MonoBehaviour {

    Text gold;
    Text infamy;
    Text goldPlusOne;
    Text infamyPlusOne;
    Color goldColor;
    Color infamyColor;
    Dragon dragon;


	// Use this for initialization
	void Start () {
        gold = transform.Find("Panel").Find("Gold").Find("Text").GetComponent<Text>();
        infamy = transform.Find("Panel").Find("Infamy").Find("Text").GetComponent<Text>();

        goldColor = gold.color;
        infamyColor = infamy.color;

        goldPlusOne = transform.Find("Panel").Find("Gold").Find("PlusOne").GetComponent<Text>();
        infamyPlusOne = transform.Find("Panel").Find("Infamy").Find("PlusOne").GetComponent<Text>();

        dragon = GameObject.FindGameObjectWithTag("Player").GetComponent<Dragon>();

        setGoldPOAlpha(0);
        setInfamyPOAlpha(0);
	}
	
	// Update is called once per frame
	void Update () {
        gold.text = ((int)dragon.gold).ToString();
        infamy.text = ((int)dragon.GetInfamy()).ToString();
	}


    void setGoldPOAlpha(float val) {
        goldPlusOne.color = new Color(goldColor.r, goldColor.g, goldColor.b, val);
    }

    void setInfamyPOAlpha(float val)
    {
        infamyPlusOne.color = new Color(infamyColor.r, infamyColor.g, infamyColor.b, val);
    }
}
