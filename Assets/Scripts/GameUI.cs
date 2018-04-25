//
//  GameUI.cs
//
//  Author:
//		 Nicolas Gerard (Daimadoushi) <dhaimadoshi@gmail.com>
//
//  Copyright (c) 2018 Nicolas Gerard.
//
 
using UnityEngine;
using UnityEngine.UI;

namespace Shooter
{
	public class GameUI : MonoBehaviour
	{
		[Header ("HUD")]
		public Text livesText;
		public Text scoreText;
		public Text highScoreText;
		public Slider damageSlider;
		private Image _damageFillArea;
		public Color damageSliderColorMin = Color.yellow;
		public Color damageSliderColorMax = Color.red;

		[Header ("NAV")]
		public Text gameStateText;
		public Button pauseButton;
		public Button resumeButton;
		public Image pauseMenu;

		[Header ("Settings")]
		public Slider musicVolumeSlider;
		public Slider sfxVolumeSlider;

		public float MusicVolume
		{
			get { return PlayerSettings.MusicVolume; }
			set { PlayerSettings.MusicVolume = value; }
		}

		public float SfxVolume
		{
			get { return PlayerSettings.SFXVolume; }
			set { PlayerSettings.SFXVolume = value; }
		}

		void Awake() 
		{
			_damageFillArea = damageSlider.fillRect.GetComponent<Image>();
			damageSlider.maxValue = GameManager.maxDamage;

			musicVolumeSlider.value = MusicVolume;
			sfxVolumeSlider.value = SfxVolume;
		}

		void Start ()
		{
			GameManager.Lives = 1;

			livesText.text = string.Format("{1} {0}", GameManager.Lives > 1 ? "Lives" : "Life", GameManager.Lives.ToString());
			scoreText.text = string.Format("Score : {0}", GameManager.Score);
			highScoreText.text = string.Format("High Score : {0}", GameManager.Highscore);
			damageSlider.value = GameManager.Damage;
			_damageFillArea.color = Color.Lerp(damageSliderColorMin, damageSliderColorMax, GameManager.Damage/damageSlider.maxValue);

			OnStateChanged(GameManager.State);

			GameManager.ScoreChanged += delegate(int score)
				{
					scoreText.text = string.Format("Score : {0}", score);
				};
			GameManager.HighScoreChanged += delegate(int score)
				{
					highScoreText.text = string.Format("High Score : {0}", score);
				};
			GameManager.LivesChanged += delegate(int lives)
				{
					livesText.text = string.Format("{1} {0}", GameManager.Lives > 1 ? "Lives" : "Life", lives);
				};
			GameManager.DamageChanged += delegate(float damage)
				{
					damageSlider.value = damage;
					_damageFillArea.color = Color.Lerp(damageSliderColorMin, damageSliderColorMax, damage/damageSlider.maxValue);

				};
			GameManager.StateChanged += OnStateChanged;
		}

		void OnStateChanged (GameManager.STATE state)
		{
			gameStateText.text = string.Format("GAME {0}", state.ToString().ToUpper());
			pauseButton.gameObject.SetActive(state == GameManager.STATE.Running);
			pauseMenu.gameObject.SetActive(state != GameManager.STATE.Running);
			resumeButton.gameObject.SetActive(state != GameManager.STATE.GameOver);
		}

		public void PauseGame ()
		{
			GameManager.State = GameManager.STATE.Paused;
		}

		public void ResumeGame ()
		{
			GameManager.State = GameManager.STATE.Running;
		}

		public void RestartGame ()
		{
			GameManager.Restart();
		}
	}

}