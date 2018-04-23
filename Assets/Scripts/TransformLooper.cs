using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Objectif :
 * Faire boucler la position de l'objet dans une zone rectangulaire.
 * Lorsque l'objet sors sur le bord droit, il revient sur à gauche.
 */

[AddComponentMenu ("SpaceShooter/Transform Looper")]
public class TransformLooper : MonoBehaviour {

	public Rect area = new Rect(0,0,10, 10);

	void Update()
	{
		Vector3 position = transform.position;

		if (area.Contains(position))
			return;

		if (position.x < area.xMin)
			position.x = area.xMax;
		else if (position.x > area.xMax)
			position.x = area.xMin;

		if (position.y < area.yMin)
			position.y = area.yMax;
		else if (position.y > area.yMax)
			position.y = area.yMin;

		transform.position = position;
	}
}
