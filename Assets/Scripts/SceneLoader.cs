using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : Singleton<SceneLoader>
{
	public GameObject loadingScreen;
	public Text progressText;

    public void LoadScene(int sceneIndex)
	{
		StartCoroutine(LoadAsynchronously(sceneIndex));

		loadingScreen.SetActive(true);
	}

	IEnumerator LoadAsynchronously(int sceneIndex)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
		while(!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / .9f);
			progressText.text = progress * 100f + "%";
			yield return null;
		}
	}
}
