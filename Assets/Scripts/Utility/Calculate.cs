using UnityEngine;
using System.Collections;

public static class Calculate {

	public static Vector2 NormalizePosition(Vector2 position){
		
		return new Vector2 ( NormalizeFloat(position.x), NormalizeFloat(position.y));
	}

	public static int NormalizeFloat(float x){
		float xCalc = x - (int)x;
		int newX = 0;

		if (xCalc < -0.5f){
			newX = (int)x - 1;
		}else if (xCalc > 0.5f){
			newX = (int)x + 1;
		}else {
			newX = (int)x;
		}

		return newX;
	}
}
