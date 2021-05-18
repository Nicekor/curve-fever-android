using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : Singleton<GameManager>
{
	public GameObject playerPrefab;
	private bool hasEnded = false;
	[SerializeField] private GameObject instructionsPanel;

    private void Start()
    {
		// todo: random position
		StartCoroutine(StartGame());
    }

	IEnumerator StartGame()
	{
		int xPos = Random.Range(-8, 9);
		int yPos = Random.Range(-4, 4);

		yield return new WaitForSeconds(3f);

		PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(xPos, yPos, 0), Quaternion.identity, 0);

		Destroy(instructionsPanel);
	}

    public void EndGame()
	{
		if (hasEnded) return;
		hasEnded = true;
		StartCoroutine(PlayEndGameAnimation());
	}

	IEnumerator PlayEndGameAnimation ()
	{
		Debug.Log("Game Over");

		yield return new WaitForSeconds(1f);

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

	}
}
