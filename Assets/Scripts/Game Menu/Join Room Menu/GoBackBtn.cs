﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackBtn : MonoBehaviour
{
	public void GoToPreviousScene()
	{
		int sceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(sceneIndex - 1);
	}

	public void HideMenu(GameObject menu)
	{
		menu.SetActive(false);
	}
}
