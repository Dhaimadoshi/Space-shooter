//
//  ShipControllerBase.cs
//
//  Author:
//		 Nicolas Gerard (Daimadoushi) <dhaimadoshi@gmail.com>
//
//  Copyright (c) 2018 Nicolas Gerard.
//
 
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
 
namespace Shooter
{
	[RequireComponent (typeof(Animator))]
	public abstract class ShipControllerBase : MonoBehaviour, IControllable
    {
        #region Variables
		public AnimationCurve steeringCurve;
		public AnimationCurve glideCurve;

		public float rearPowerLimit = 0.2f;

		protected Animator _animatorController;
		protected int _steeringHashID;
		protected int _thrustXHashID;
		protected int _thrustYHashID;
		protected int _shieldHashID;
	
		private List<Weapon> _weapons;
		private Weapon _weapon;
		public Weapon  Weapon
		{
			get 
			{ 
				if (!_weapon)
					_weapon = GetComponentInChildren<Weapon>();
				return _weapon; 
			}
			set
			{
				if (_weapon != value)
				{
					_weapon = value;
					if (!_weapons.Contains(_weapon))
						_weapons.Add(_weapon);
				}
			}
		}

		private bool _firing;
		protected bool Firing
		{
			get { return _weapon.Firing; }
			set { _weapon.Firing = value; }
		}

		private bool _shield;

		public bool Shield
		{
			get { return _shield; }
			set
			{
				if (_shield != value)
				{
					_shield = value;
					_animatorController.SetLayerWeight(_shieldHashID, _shield ? 1f : 0f);
				}

				if (_shield)
					Firing = false;
			}
		}

		private Vector2 _thrust;
		protected Vector2 Thrust
		{
			get { return _thrust; }
			set
			{
				if (_thrust != value)
				{
					_thrust = value;
					_animatorController.SetFloat(_thrustXHashID, _thrust.x);
					_animatorController.SetFloat(_thrustYHashID, _thrust.y);
				}
			}
		}

		private float _steering;

		public float Steering
		{
			get { return _steering; }
			set
			{
				if (_steering != value)
				{
					_steering = value;
					_animatorController.SetFloat(_steeringHashID, Steering);
				}
			}
		}
        #endregion
     
        #region Unity Methods
		protected virtual void Awake()
		{
			_animatorController = GetComponent<Animator>();
			_steeringHashID = Animator.StringToHash("Steering");
			_thrustXHashID = Animator.StringToHash("ThrustX");
			_thrustYHashID = Animator.StringToHash("ThrustY");

			_shieldHashID = _animatorController.GetLayerIndex("Shield");
			if (_shieldHashID == -1)
				Debug.LogWarning("Animator Controller must have a 'shield' Layer");

			_weapons = new List<Weapon>();
			_weapons.Add(Weapon);
		}
        #endregion

		public bool Attacking
		{
			get { return Firing; }
			set { Firing = value; }
		}

		public bool Protecting
		{
			get { return Shield; }
			set { Shield = value; }
		}

		public void SwitchWeapon (Weapon newWeapon)
		{
			if (Weapon.name == newWeapon.name)
				return;
			
			bool wasFiring = Firing;
			Firing = false;
			Weapon.gameObject.SetActive(false);

			var existingWeapon = (from item in _weapons
			                      where item.name == newWeapon.name
			                      select item).FirstOrDefault();

			if (existingWeapon != null)
			{
				existingWeapon.gameObject.SetActive(true);
				Weapon = existingWeapon;
			}
			else
			{
				GameObject newWeaponGO = Instantiate(newWeapon.gameObject);
				newWeaponGO.transform.parent = transform;
				newWeaponGO.transform.localPosition = Vector3.zero;
				newWeaponGO.transform.localRotation = Quaternion.identity;
				Weapon = newWeaponGO.GetComponent<Weapon>();
			}

			Firing = wasFiring;
		}

		public virtual void Move (Vector2 movement)
		{
			movement = Vector2.ClampMagnitude(movement, 1.0f);
			movement.y = Mathf.Clamp(movement.y, -rearPowerLimit, 1);

			Steering = steeringCurve.Evaluate(movement.y) * movement.x;

			var thrust_x = glideCurve.Evaluate(movement.y) * movement.x;
			var thrust_y = movement.y;
			Thrust = new Vector2(thrust_x, thrust_y);

			enabled = (movement != Vector2.zero);
		}

        #region Private Methods
        #endregion
	
		public virtual void ResetShip ()
		{
			Steering = 0;
			Thrust = Vector2.zero;
			Firing = Shield = false;

			transform.rotation = Quaternion.identity;
			transform.position = Vector3.zero;

			gameObject.SendMessage("Repair");

			SwitchWeapon(_weapons[0]);
		}
    }
}