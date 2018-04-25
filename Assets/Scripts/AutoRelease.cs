//
//  AutoRelease.cs
//
//  Author:
//		 Nicolas Gerard (Daimadoushi) <dhaimadoshi@gmail.com>
//
//  Copyright (c) 2018 Nicolas Gerard.
//
 
using UnityEngine;
using System.Collections;
 
namespace Shooter
{
    public class AutoRelease : MonoBehaviour
    {
        #region Public Fields
		public float duration;
        #endregion
     
        #region Unity Methods
        void Awake()
        {
			Animator animator = GetComponent<Animator>();
			float animatorLength = animator ? animator.GetCurrentAnimatorClipInfo(0)[0].clip.length : 0;

			AudioSource audio = GetComponent<AudioSource>();
			float audioLength = audio ? audio.clip.length : 0;

			if (duration == 0)
				duration = Mathf.Max(animatorLength, audioLength);
        }

        void OnEnable()
        {
			StartCoroutine(Release());
        }
        #endregion
     
        #region Private Methods
		private IEnumerator Release()
		{
			yield return new WaitForSeconds(duration);
			ObjectPool.Release(gameObject);
		}
        #endregion
    }
}