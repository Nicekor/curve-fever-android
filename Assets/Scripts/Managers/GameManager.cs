using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : Singleton<GameManager>
{
	[SerializeField] private GameObject playerPrefab;
	[SerializeField] private GameObject instructionsPanel;
	[SerializeField] private Text countdownText;

	private List<string> leaderboard = new List<string>();
	private float currentTime = 0f;
	private float startingTime = 3f;
	private int numberOfPlayersInRoom;
	private bool hasEnded = false;

	private void Start()
    {
		numberOfPlayersInRoom = PhotonNetwork.CountOfPlayersInRooms;

		currentTime = startingTime;
		

		StartCoroutine(StartGame());
    }

	private void Update()
	{
		currentTime -= 1 * Time.deltaTime;
		countdownText.text = currentTime.ToString("0");

		if (currentTime <= 0)
		{
			countdownText.enabled = false;
		}

		// if numberOfPlayersInRoom == leadearboard.length => the game finished

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

	public void UpdateLeadearboard()
	{
		foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
		{
			print("another player");
			if (player.Value.CustomProperties["dead"] != null && (bool)player.Value.CustomProperties["dead"])
			{
				leaderboard.Add(player.Value.NickName);
			}
		}
	}
}
