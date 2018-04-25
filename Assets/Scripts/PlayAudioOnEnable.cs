//
//  PlayAudioOnEnable.cs
//
//  Author:
//		 Nicolas Gerard (Daimadoushi) <dhaimadoshi@gmail.com>
//
//  Copyright (c) 2018 Nicolas Gerard.
//
 
using UnityEngine;
 
namespace Shooter
{
	[RequireComponent (typeof (AudioSource))]
    public class PlayAudioOnEnable : MonoBehaviour
    {
        #region Private Fields
		private AudioSource _audio;
        #endregion
     
        #region Unity Methods
        void Awake()
        {
			_audio = GetComponent<AudioSource>();
        }

        void OnEnable()
        {
			_audio.Play();
        }
        #endregion
    }
}