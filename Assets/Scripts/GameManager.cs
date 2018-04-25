using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shooter {
	static public class GameManager 
	{
		public enum STATE
		{
			Running,
			Paused,
			GameOver
		};

		public delegate void ScoreChange(int score);
		public delegate void LivesChange(int life);
		public delegate void DamageChange(float damage);
		public delegate void stateChange(STATE state) ;

		static public event stateChange StateChanged;
		static private STATE _state;
		static public STATE State
		{
			get { return _state; }
			set
			{
				if (value != _state)
				{
					_state = value ;

					switch (_state)
					{
						case STATE.Running:
							Time.timeScale = 1;
							break;
						case STATE.Paused:
							Time.timeScale = 0;
							break;
						case STATE.GameOver:
							Time.timeScale = 0;
							break;
						default:
							break;
					}

					if (StateChanged != null)
						StateChanged(_state);
				}
			}
		}

		static public event ScoreChange HighScoreChanged;
		static public int Highscore
		{
			get { return PlayerPrefs.GetInt("HighScore", 0);}
			set 
			{ 
				PlayerPrefs.SetInt("HighScore", value); 

				if (HighScoreChanged != null)
					HighScoreChanged(value);
			}
		}

		static public event ScoreChange ScoreChanged;
		static private int _score;
		static public int Score
		{
			get { return _score; }
			set
			{
				if (_score != value)
				{
					_score = value;

					if (ScoreChanged != null)
						ScoreChanged(_score);

					if (_score > Highscore)
						Highscore = _score;
				}
			}
		}

		static public LivesChange LivesChanged;
		static private int _lives = 5;
		static public int Lives
		{
			get { return _lives; }
			set
			{
				if (_lives != value)
				{
					_lives = value;

					if (LivesChanged != null)
						LivesChanged(_lives);

					if(Lives <= 0)
					{
						State = STATE.GameOver;
					}
				}
			}
		}
			
		public const float maxDamage = 100;
		static public event DamageChange DamageChanged;
		static private float _damage;
		static public float Damage
		{
			get { return _damage; }
			set
			{
				if (_damage != value)
				{
					_damage = value;

					if (DamageChanged != null)
						DamageChanged(_damage);
					
					if (_damage >= maxDamage)
					{
						Lives --;
						_damage = 0;
					}
				}
			}
		}

		static public void Restart ()
		{
			Lives = 5;
			Damage = 0;
			Score = 0;

			LivesChanged = null;
			ScoreChanged = null;
			HighScoreChanged = null;
			DamageChanged = null;
			StateChanged = null;

			// TODO : Reset all objects instead of reloading the level
			SceneManager.LoadScene(0);	
			State = STATE.Running;
		}
	}
}