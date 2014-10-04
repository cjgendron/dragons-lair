using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

	public int food = 30;
	public int population = 40;
	public int gold = 10;
	public int armor = 5;


	public bool canSpawnChampions = false;
	public GameObject championType;

	public bool canAttack = false;
	public int attack = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
