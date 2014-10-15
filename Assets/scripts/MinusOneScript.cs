using UnityEngine;
using System.Collections;

public class MinusOneScript : MonoBehaviour {

    float livesFor = 1f;
    float time = 1f;
    Vector3 startPos;
    float shiftDown = 1.5f;

    Color color;
    SpriteRenderer r;
	// Use this for initialization
	void Start () {
        r = GetComponent<SpriteRenderer>();
        color = r.color;
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        r.color = new Color(color.r, color.g, color.b, time / livesFor);

        transform.position = startPos - new Vector3(0, shiftDown * (livesFor - time / livesFor), 0);

        time -= Time.deltaTime;
        if (time < 0) GameObject.Destroy(gameObject);
	}

    
}
