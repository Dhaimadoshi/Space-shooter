using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
	public class Weapon : MonoBehaviour 
	{
		public new string name;
		public GameObject projectile;
		private int _projectileID;

		[Range (0.001f, 20)] public float firingRate = 1;
		public float firingRange = 5;

		public bool automatic = true;
		public bool cycling = true;

		public Transform[] emmitters;
		private int _current;


		private Collider2D _weaponHolderCollider2D;

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
					{
						if (automatic)
							InvokeRepeating("Fire", (1f / firingRate), (1f / firingRate));
						else
							Fire();
					}
					else
						CancelInvoke();
				}
			}
		}

		void Awake()
		{
			if (name == "")
				name = gameObject.name;
			

			_projectileID = projectile.GetInstanceID();
			ObjectPool.InitPool(projectile);
		}

		private void Fire()
		{
			if (cycling)
			{
				FireOnce();
			}
			else
			{
				for (int i = 0; i < emmitters.Length; i++)
				{
					FireOnce();
				}
			}
		}

		private void FireOnce () 
		{
			_current = (_current >= emmitters.Length - 1) ? 0 : _current+1;
			Vector3 position = emmitters[_current].TransformPoint(Vector3.up * 0.5f);
			GameObject projectileInstance = (GameObject) ObjectPool.GetInstance (_projectileID, position, emmitters[_current].rotation);
			projectileInstance.GetComponent<Projectile>().range = firingRange;

//			foreach (Collider2D c in transform.parent.GetComponentsInChildren<Collider2D>(true))
//			{
//				Physics2D.IgnoreCollision(c, projectileInstance.GetComponent<Collider2D>());
//			}
		}
	}
}