using UnityEngine;
using System.Collections;

public class Champion : MonoBehaviour {

	public int attack = 5;
	public int armor = 5;
	public int health = 50;
	public int gold = 30;
    public float movementSpeed = 2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, Time.deltaTime * movementSpeed);
    }
}
