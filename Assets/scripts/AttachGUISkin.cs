using UnityEngine;
using System.Collections;

public class AttachGUISkin : MonoBehaviour {

	public GUISkin customSkin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		GUI.skin = customSkin;
	}
}
