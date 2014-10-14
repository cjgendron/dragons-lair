using UnityEngine;
using System.Collections;

public class Champion : MonoBehaviour {

	public int attack = 5;
	public int armor = 5;
	public float health = 50f;
	public int curGold = 30;
    public int maxGold = 200;
    public float movementSpeed = 2f;
    public string goal = "dragon";
    public string home = "Village";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Look();
        Act();
        Move();
	}

    // Checks to see how the close the dragon is to the champion.  The champion will chase the dragon if it is close enough.
    void Look()
    {
        if (Helpers.getDistance("Dragon", this.gameObject) < 5)
        {
            goal = "dragon";
        }
//        else if (curGold == maxGold)
//        {
//            goal = "home";
//        }
        else
        {
            goal = "lair";
        }
    }

    void Act()
    {
        if (goal == "dragon")
        {
            Attack("Dragon");
        }
		else if (goal == "lair") {
			Attack("Lair");
		}
    }
    void Move()
    {
        if (goal == "lair")
        {
            transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Lair").transform.position, Time.deltaTime * movementSpeed);
        }
        else if (goal == "dragon")
        {
            transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Dragon").transform.position, Time.deltaTime * movementSpeed);
        }
        else if (goal == "home")
        {
            transform.position = Vector3.MoveTowards(transform.position, GameObject.Find(home).transform.position, Time.deltaTime * movementSpeed);
        }
    }

	void ReceiveDamage(object[] vars) {
		float num = (float) vars[1];
		GameObject dragon = vars [0] as GameObject;
		
		health -= num;
		if (health < 0f) {
			dragon.SendMessage("LoseTarget");
			Destroy (gameObject);
		}
	}

	void Attack(string targetString) {
		GameObject target = GameObject.Find (targetString);
		if (Helpers.getDistance(targetString, this.gameObject) < 2) {
			target.SendMessage("ReceiveDamage", Time.deltaTime * attack);
		}
	}

    void SetHome(string homeName)
    {
        home = homeName;
    }


}
