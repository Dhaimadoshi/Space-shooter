using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter {
	public class Item : MonoBehaviour 
	{
		public enum TYPE {RepairKit, ExtraLife, Weapon};

		public TYPE type;

		public Weapon weapon;

		private AudioSource audiosrc;
		private Renderer _renderer;
		private Collider2D _collider2D;

		private void Awake()
		{
			audiosrc = GetComponent<AudioSource>();
			_renderer = GetComponent<Renderer>();
			_collider2D = GetComponent<Collider2D>();
		}

		void OnTriggerEnter2D (Collider2D other)
		{
			if (other.gameObject.tag != "Player")
				return;

			switch (type)
			{
				case TYPE.RepairKit:
					GameManager.Damage = 0;
					Debug.Log("repair kit");
					break;
				case TYPE.ExtraLife:
					TODO :
					GameManager.Lives++;	
					Debug.Log("extra life");
					break;
				case TYPE.Weapon:
					other.GetComponent<ShipControllerBase>().SwitchWeapon(weapon);
					break;
				default:
					break;
			}

			StartCoroutine(Pickup());
		}

		private IEnumerator Pickup ()
		{
			_collider2D.enabled = _renderer.enabled = false;
			audiosrc.Play();
			yield return new WaitForSeconds(audiosrc.clip.length);
//			Destroy(gameObject);
			ObjectPool.Release (gameObject);
		}
	}
}