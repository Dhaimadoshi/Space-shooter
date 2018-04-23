using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
	/// <summary>
	/// Spawner.
	/// </summary>
	[AddComponentMenu ("Space Shooter/Spawner")]
	public class Spawner : MonoBehaviour {

		[Header ("SPAWN")]
		public GameObject reference;

		[Header ("SPAWNING")]
		public int number = 5;
		[Range(0.001f, 100f)] public float minRate = 1.0f;
		[Range(0.001f, 100f)] public float maxRate = 5.0f;
		public bool infinite = false;

		[Header ("LOCATIONS")]
		public GameArea area;


		private int _remaining;
//		private float _timeStamp;

		void Awake()
		{
		}

//		void Start () 
//		{
////			_timeStamp = Time.time;
//			_remaining = number;
//			StartCoroutine( Spawn() );
//		}
		
//		void Update () 
//		{
//			if (Time.time <= _timeStamp + rate)
//				return;
//
//			_timeStamp = Time.time;
//
////			if (_remaining > 0)
////			{
//				Instantiate(reference, transform.position, transform.rotation);
//				_remaining--;
////			}
//
//			if (!infinite || _remaining <= 0)
//				enabled = false;
//		}

		private IEnumerator Start()
		{
			_remaining = number;

			while (infinite || _remaining > 0)
			{
				Vector3 _position = area ? area.GetRandomPosition() : transform.position;

				Instantiate(reference, _position, transform.rotation);

				_remaining --;

				yield return new WaitForSeconds(1/Random.Range(minRate, maxRate));
			}
		}
	}
}