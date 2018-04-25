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
		private int _referenceID;

		[Header ("SPAWNING")]
		public int number = 5;
		[Range(0.001f, 100f)] public float minRate = 1.0f;
		[Range(0.001f, 100f)] public float maxRate = 5.0f;
		public bool infinite = false;

		[Header ("LOCATIONS")]
		public GameArea area;
		private Transform player;
		public float minDistanceFromPlayer;

		[Header ("Velocity")]
		[Range (-180f, 180f)] public float angle;
		[Range (0f, 360f)] public float spread = 30f;
		[Range (0, 10)] public float minStrength = 1f;
		[Range (0, 10)] public float maxStrength = 10f;

		[Header ("Animator")]
		public string animatorSpawningParameterName = "Spawning";
		private int spawningHashID;
		public float animatorDelayIn = 1;
		public float animatorDelayOut = 1;

		private Animator animator;

		private int _remaining;
//		private float _timeStamp;

		void Awake()
		{
			ObjectPool.InitPool(reference);
			_referenceID = reference.GetInstanceID();

			animator = GetComponent<Animator> ();
			if (animator)
				spawningHashID = Animator.StringToHash(animatorSpawningParameterName);
		}

		private IEnumerator Start()
		{
			_remaining = number;

			if (minDistanceFromPlayer > 0)
			{
				GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
				if (playerGO)
					player = playerGO.transform;
				else
					Debug.LogWarning("No player found.");
			}

			if (animator)
			{
				animator.SetBool("Spawning", true);
				yield return new WaitForSeconds(animatorDelayIn);
			}

			while (infinite || _remaining > 0)
			{
				Vector3 _position = area ? area.GetRandomPosition() : transform.position;

				if (player && Vector3.Distance(_position, player.position) < minDistanceFromPlayer)
				{
					Vector3 debugPos = _position;
					Debug.DrawLine(transform.position, debugPos);
					_position = (_position - player.position).normalized * minDistanceFromPlayer;
					Debug.DrawLine(debugPos, _position); 
//					Debug.Break();
				}

				GameObject obj = (GameObject) ObjectPool.GetInstance (_referenceID, _position, transform.rotation);
				Rigidbody2D rb2d = obj.GetComponent<Rigidbody2D>();

				if (rb2d)
				{
					float angleDelta = Random.Range(-spread * 0.5f, spread * 0.5f);
					float angle_ = angle + angleDelta;
					Vector2 direction = new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle_), Mathf.Cos(Mathf.Deg2Rad * angle_));
					direction *= Random.Range(minStrength, maxStrength);
					rb2d.velocity = direction;
				}

				_remaining --;

				yield return new WaitForSeconds(1/Random.Range(minRate, maxRate));
			}

			if (animator)
			{
				yield return new WaitForSeconds(animatorDelayOut);
				animator.SetBool(spawningHashID, false);
			}

			gameObject.SetActive(false);
		}
	}
}