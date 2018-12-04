using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
	public bool gameActive;
	public bool paused;
	
	public GameObject completeScreen;
	public GameObject pauseScreen;

	private Player _player;
	private BlobLoader blobLoader;

	private void Start()
	{
		_player = GameObject.FindWithTag("Player").GetComponent<Player>();
		blobLoader = FindObjectOfType<BlobLoader>().GetComponent<BlobLoader>();
		completeScreen.SetActive(false);
		pauseScreen.SetActive(false);
	}

	private void Update()
	{
		if (_player == null) return;
		
		if (WinCheck())
		{
			GameComplete();
			return;
		}

		if (Input.GetButtonDown("Cancel"))
		{
			_player.isPaused = !paused;
			paused = !paused;
		}
		pauseScreen.SetActive(paused);

		if (paused) return;

		if (Input.GetMouseButtonDown(0))
		{
			blobLoader.LoadStructures();
			gameActive = !gameActive;
		}
		_player.isPaused = !gameActive;
	}

	private void GameComplete()
	{
		completeScreen.SetActive(true);
		Debug.Log("LEVEL COMPLETE");
	}

	private bool WinCheck()
	{
		return _player.hitGoal;
	}
	
	//WIN SCREEN & PAUSE SCREEN FUNCTIONS
	public void NextLevel()
	{
		var current = SceneManager.GetActiveScene().buildIndex;
		
		if (current < SceneManager.sceneCountInBuildSettings) SceneManager.LoadScene(current + 1);
		else SceneManager.LoadScene(0);
	}

	public void MainMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void ResetLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

}
