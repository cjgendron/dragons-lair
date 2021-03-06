using UnityEngine;
using System.Collections;
 
public class GameCamera : MonoBehaviour {
 
    private Transform target;
    private float trackSpeed = 10;
 
 
    // Set target
    public void SetTarget(Transform t) {
        target = t;
    }
 
    // Track target
    void LateUpdate() {
        if (target) {
            var v = transform.position;
            v.x = target.position.x;
            transform.position = Vector3.MoveTowards (transform.position, v, trackSpeed * Time.deltaTime);
        }
    }
}