using UnityEngine;
using System.Collections;

public class Indicator : MonoBehaviour {
    public int lifetime;
    int counter;

     void Start() {
        counter = 0;
    }
 
    void Update() {
        counter += 1;
        if (counter == lifetime){
            destroy(GameObject);
        }
    }
}
