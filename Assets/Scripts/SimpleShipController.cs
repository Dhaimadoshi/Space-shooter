using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter 
{
	[AddComponentMenu ("Space Shooter/Simple Ship Controller")]
	[RequireComponent (typeof(Rigidbody2D))]
	public class SimpleShipController : MonoBehaviour 
	{	
		public float thrustPower = 1;
		public float steerPower = 1;

		private Rigidbody2D _rigidBody2D;

		private Vector2 _delta = Vector2.zero;
		private Vector2 _force = Vector2.zero;
		private float _torque;

		private Weapon _weapon;

		private bool _firing;
		public bool Firing
		{
			get { return _firing; }
			set
			{
				if (_firing != value)
				{
					_firing = value;

					if (_firing)
						_weapon.InvokeRepeating("Fire", (1f / _weapon.firingRate), (1f / _weapon.firingRate));
					else
						_weapon.CancelInvoke();
				}
			}
		}

		void Awake()
		{
			_rigidBody2D = GetComponent<Rigidbody2D>();
			_weapon = GetComponentInChildren<Weapon>();
		}
		void FixedUpdate ()
		{
			_delta.x = Input.GetAxis("Horizontal");
			_delta.y = Input.GetAxis("Vertical");

			Firing = Input.GetButton("Fire2");

//			transform.Translate(0, _delta.y, 0);
//			transform.Rotate(0, 0, -_delta.x);

			_force.y = _delta.y * thrustPower;
			_torque = -_delta.x * steerPower;

			_rigidBody2D.AddRelativeForce(_force);
			_rigidBody2D.AddTorque(_torque);
		}
	}
}