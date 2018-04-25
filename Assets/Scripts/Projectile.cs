using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
	public class Projectile : MonoBehaviour {

//		[HideInInspector]
		public float speed = 5.5f;

		public float range = 5;
		private float distance;

		private AudioSource _audiosrc;

		void Awake()
		{
			_audiosrc = GetComponent<AudioSource>();
		}

		void OnEnable() 
		{
			distance = 0;
			if (_audiosrc)
				_audiosrc.Play();
		}

		void FixedUpdate () 
		{
			transform.Translate(new Vector3(0, speed * Time.fixedDeltaTime, 0), Space.Self);
			distance += speed * Time.fixedDeltaTime;
			if (distance > range)
//				Destroy(gameObject);
				ObjectPool.Release (gameObject);
		}
	}
}