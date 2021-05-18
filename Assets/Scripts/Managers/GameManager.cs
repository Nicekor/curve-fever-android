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

	[SerializeField] private List<string> leaderboard = new List<string>();
	private float currentTime = 0f;
	private float startingTime = 3f;
	private bool hasEnded = false;

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

	IEnumerator PlayEndGameAnimation()
	{
		Debug.Log("Game Over");

		yield return new WaitForSeconds(1f);

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

	}

	public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
	{
		if (targetPlayer != null && changedProps.ContainsKey("dead"))
		{
			leaderboard.Insert(0, targetPlayer.NickName);

			// this does penis :D
			if (leaderboard.Count == PhotonNetwork.CurrentRoom.PlayerCount)
			{
				print("game over");
			}
		}
	}
}
