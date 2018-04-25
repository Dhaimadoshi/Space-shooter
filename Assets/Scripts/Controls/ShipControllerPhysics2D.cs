//
//  ShipControllerPhysics2D.cs
//
//  Author:
//		 Nicolas Gerard (Daimadoushi) <dhaimadoshi@gmail.com>
//
//  Copyright (c) 2018 Nicolas Gerard.
//
 
using UnityEngine;
 
namespace Shooter
{
	[RequireComponent (typeof (Rigidbody2D))]
	public class ShipControllerPhysics2D : ShipControllerBase
    {
        #region Public Fields
		public float steeringMultiplier = 10;
		public Vector2 thrustMultiplier = new Vector2 (10f, 10f);
        #endregion

		private Vector3 thrustDirection;
		private Rigidbody2D _rigidBody2D;
     
        #region Unity Methods
		protected override void Awake()
		{
			base.Awake();
			_rigidBody2D = GetComponent<Rigidbody2D>();
		}

		private void FixedUpdate()
		{
			_rigidBody2D.AddTorque(-Steering * steeringMultiplier, ForceMode2D.Force);
			thrustDirection = (Vector3)Thrust;
			thrustDirection.Scale (thrustMultiplier);
			_rigidBody2D.AddRelativeForce(thrustDirection, ForceMode2D.Force);
		}
        #endregion
     
        #region Private Methods
        #endregion

		public override void ResetShip ()
		{
			base.ResetShip();
			_rigidBody2D.velocity = Vector2.zero;
			_rigidBody2D.angularVelocity = 0;
		}
    }
}