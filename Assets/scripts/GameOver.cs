using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {
    public Text text;
	// Use this for initialization
	void Start () {

	}

    void Awake()
    {
        text = gameObject.GetComponent<Text>();
        GameObject hiscore = GameObject.Find("HiScore");
        hiscore.SendMessage("sendScore");
    }
	// Update is called once per frame
	void Update () {

	}

    void setText(string content)
    {
        text.text = content;
        
    }
}
