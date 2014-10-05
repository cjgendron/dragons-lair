using UnityEngine;
using System.Collections;

public class Helpers {

	public static Quaternion rotateTowards2D(Vector3 direction, float offset = 0f) {
		float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg + offset;
		return Quaternion.AngleAxis (angle, Vector3.forward);
	}
}
