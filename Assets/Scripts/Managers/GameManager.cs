using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : Singleton<GameManager>
{
	public GameObject playerPrefab;
	private bool hasEnded = false;

    private void Start()
    {
		// todo: random position
		int xPos = Random.Range(-8, 9);
		int yPos = Random.Range(-4, 4);
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(xPos, yPos, 0), Quaternion.identity, 0);
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
