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
        if (DistanceToDragon() < 5)
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
            AttackDragon();
        }
		else if (goal == "lair") {
			AttackLair();
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
            transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Dragon_prefab").transform.position, Time.deltaTime * movementSpeed);
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

	void AttackLair() {
		GameObject lair = GameObject.Find ("Lair");
		if (Vector3.Distance (lair.transform.position, transform.position) < 2) {
			lair.SendMessage("ReceiveDamage", Time.deltaTime * attack);
		}
	}

    void AttackDragon()
    {
        if (DistanceToDragon() < 1)
        {
            GameObject.Find("Dragon_prefab").SendMessage("ReceiveDamage", Time.deltaTime * attack);
        }
    }
    void SetHome(string homeName)
    {
        home = homeName;
    }

    float DistanceToDragon()
    {
        float dragonX = GameObject.Find("Dragon_prefab").transform.position.x;
        float dragonY = GameObject.Find("Dragon_prefab").transform.position.y;

        float distToDragon = Mathf.Sqrt(Mathf.Pow((dragonX - transform.position.x),2) + Mathf.Pow((dragonY - transform.position.y), 2));
        return distToDragon;
    }
}
