using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : Singleton<GameManager>
{
	[SerializeField] private GameObject instructionsPanel;
	[SerializeField] private Text countdownText;
	[SerializeField] private GameObject leaderboardPanel;
	[SerializeField] private LeaderboardPlayer leaderboardPlayerPrefab;
	[SerializeField] private Transform leaderboardContent;
	[SerializeField] private GameObject instantiatePlayers;

	[SerializeField] private List<string> leaderboard = new List<string>();
	private float currentTime = 0f;
	private float startingTime = 3f;

	private void Start()
	{
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
	}

	IEnumerator StartGame()
	{
		yield return new WaitForSeconds(3f);

		instantiatePlayers.SetActive(true);

		Destroy(instructionsPanel);
	}

	// my attempt to the leaderboard
	/*public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
	{
		if (targetPlayer != null && changedProps.ContainsKey("dead") && (bool)changedProps["dead"] == true)
		{
			print("snake died");
			leaderboard.Add(targetPlayer.NickName);

			// the leadearboard is populated when players lose which means that the game ends when it's full
			if (leaderboard.Count == PhotonNetwork.CurrentRoom.PlayerCount)
			{
				print("game over");
				LeaderboardPlayer leaderboardPlayer = Instantiate(leaderboardPlayerPrefab, leaderboardContent);
				if (leaderboardPlayer != null)
				{
					int positionIndex = leaderboard.FindIndex(playerName => playerName.Equals(targetPlayer.NickName));
					leaderboardPlayer.SetLeaderboardPlayerInfo(positionIndex + 1, targetPlayer);
				}
				leaderboardPanel.SetActive(true);
			}
		}
	}*/

}
