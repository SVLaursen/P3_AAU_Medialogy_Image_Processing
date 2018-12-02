using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject levelSelector;
	public GameObject mainMenu;

	private void Start()
	{
		Main();
	}
	
	public void Quit()
	{
		Application.Quit();
	}

	public void LevelMenu()
	{
		levelSelector.SetActive(true);
		mainMenu.SetActive(false);
	}

	public void Main()
	{
		levelSelector.SetActive(false);
		mainMenu.SetActive(true);
	}

	public void SelectLevel(int index)
	{
		SceneManager.LoadScene(index);
	}
}
