using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

	float timePassed = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;

		if (timePassed > 1f && Input.anyKey) {
			Application.LoadLevel ("GameScene");
		}
	}
}
