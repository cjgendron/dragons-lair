using UnityEngine;
using System.Collections;

public class DeadScreen : MonoBehaviour {
    float timePassed = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;

        if (timePassed > 1f && Input.anyKey)
        {
            Application.LoadLevel("StartScene");
        }
	}

    public void Initialize(string[] Args)
    {

    }
}
