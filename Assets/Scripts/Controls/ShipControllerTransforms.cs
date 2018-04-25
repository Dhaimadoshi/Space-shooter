//
//  ShipControllerTransforms.cs
//
//  Author:
//		 Nicolas Gerard (Daimadoushi) <dhaimadoshi@gmail.com>
//
//  Copyright (c) 2018 Nicolas Gerard.
//
 
using UnityEngine;
 
namespace Shooter
{
	public class ShipControllerTransforms : ShipControllerBase
    {
        #region Public Fields
		public float steeringMultiplier = 5;
		public Vector2 thrustMultiplier = new Vector2 (0.2f, 0.25f);
		#endregion
		
		private Vector3 thrustDirection;
     
        #region Unity Methods
        void Update()
        {
			thrustDirection = (Vector3)Thrust;
			thrustDirection.Scale(thrustMultiplier);
			transform.Translate((Vector3)thrustDirection, Space.Self);
			transform.Rotate(0, 0, -Steering * steeringMultiplier, Space.Self);
        }
        #endregion
     
        #region Private Methods
        #endregion
    }
}