using UnityEngine;
using System.Collections;

public class Helpers {

	public static Quaternion rotateTowards2D(Vector3 direction, float offset = 0f) {
		float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg + offset;
		return Quaternion.AngleAxis (angle, Vector3.forward);
	}

    public static float getDistance(string targetString, GameObject self)
    {
        GameObject target = GameObject.Find(targetString);

        float distBetween = Vector3.Distance(self.transform.position, target.transform.position);
        return distBetween;
    }
}
