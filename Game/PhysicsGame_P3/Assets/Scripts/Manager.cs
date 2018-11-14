using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
	public bool gameActive;

	private Player _player;
	
	#region Singleton
	private static Manager _instance;

	public static Manager Instance { get { return _instance; } }

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(gameObject);
		} 
		else {
			_instance = this;
		}

		_player = GameObject.FindWithTag("Player").GetComponent<Player>();
	}
	#endregion

	private void Start()
	{
		GetComponent<LevelLoader>().LoadLevel();
	}

	private void Update()
	{
		if(Input.GetMouseButtonDown(0)) gameActive = !gameActive;

		if (!gameActive)
		{
			_player.isPaused = true;
		}
		else
		{
			if (WinCheck()) GameComplete();
			_player.isPaused = false;
		}
	}

	private void GameComplete()
	{
		//TODO: What to do once the game is completed
		Debug.Log("LEVEL COMPLETE");
	}

	private bool WinCheck()
	{
		return _player.hitGoal;
	}
}
