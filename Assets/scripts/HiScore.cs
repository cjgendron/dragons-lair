using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HiScore : MonoBehaviour {
    public string gold;
    public string infamy;
    public string level;
    public string text;
    public int img;

	// Use this for initialization
	void Start () {
        img = 0;
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Args[0] = gold, Args[1] = infamy, Args[2] = level
    void setScores(int[] args)
    {
        gold = args[0].ToString();
        infamy = args[1].ToString();
        level = args[2].ToString();

        if (50 > args[1])
            img = 0;

        if (100 > args[1] && args[1] > 50)
            img = 1;

        if (args[1] > 100)
            img = 2;

        Application.LoadLevel("DeadScene");

    }

    void sendScore()
    {
        GameObject scoreboard = GameObject.Find("Text");
        text = "GAME OVER\nGOLD: " + gold + "\nINFAMY: " + infamy + "\nLEVEL: " + level;
        scoreboard.BroadcastMessage("setText", text);

        if (img == 0)
        {
            GameObject picture = GameObject.Find("Baby Dragon");
            picture.renderer.enabled = true;
        }
        else if (img == 1)
        {
            GameObject picture = GameObject.Find("Young Dragon");
            picture.renderer.enabled = true;
        }

        if (img == 2)
        {
            GameObject picture = GameObject.Find("Hardcore Dragon");
            picture.renderer.enabled = true;
        }
    }
}
