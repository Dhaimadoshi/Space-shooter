using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter 
{
	/*
	 * Objectif :
	 * Faire boucler la position de l'objet dans une zone rectangulaire.
	 * Lorsque l'objet sors sur le bord droit, il revient sur à gauche.
	 */

	[AddComponentMenu ("Space Shooter/Transform Looper")]
	public class GameAreaLooper : MonoBehaviour 
	{

		public GameArea gameArea;
		Vector3 areaSpacePosition;

		void Update()
		{
//			Vector3 position = transform.position;
			areaSpacePosition = gameArea.transform.InverseTransformPoint(transform.position);

			if (gameArea.Area.Contains(areaSpacePosition))
				return;

			if (areaSpacePosition.x < gameArea.Area.xMin)
				areaSpacePosition.x = gameArea.Area.xMax;
			else if (areaSpacePosition.x > gameArea.Area.xMax)
				areaSpacePosition.x = gameArea.Area.xMin;

			if (areaSpacePosition.y < gameArea.Area.yMin)
				areaSpacePosition.y = gameArea.Area.yMax;
			else if (areaSpacePosition.y > gameArea.Area.yMax)
				areaSpacePosition.y = gameArea.Area.yMin;

//			transform.position = position;
			transform.position = gameArea.transform.TransformPoint(areaSpacePosition);
		}
	}
}
