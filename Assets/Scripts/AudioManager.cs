//
//  AudioManager.cs
//
//  Author:
//		 Nicolas Gerard (Daimadoushi) <dhaimadoshi@gmail.com>
//
//  Copyright (c) 2018 Nicolas Gerard.
//
 
using UnityEngine;
using UnityEngine.Audio;

namespace Shooter
{
    public class AudioManager : MonoBehaviour
    {
        #region Public Fields
		public AudioMixer mixer;
        #endregion
     
        #region Unity Methods
        void Start()
        {
			mixer.SetFloat("MusicVolume",  PlayerSettings.MusicVolume.LinearToDecibel());
			mixer.SetFloat("SFXVolume", PlayerSettings.SFXVolume.LinearToDecibel());

			PlayerSettings.MusicVolumeChanged += delegate(float volume)
				{
					mixer.SetFloat("MusicVolume", volume.LinearToDecibel());
				};
			PlayerSettings.SFXVolumeChanged += delegate(float volume)
				{
					mixer.SetFloat("SFXVolume", volume.LinearToDecibel());
				};
        }
        #endregion
     
        #region Private Methods
        #endregion
    }
}