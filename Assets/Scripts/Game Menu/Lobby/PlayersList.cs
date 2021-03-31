using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class PlayersList : MonoBehaviourPunCallbacks
{
	[SerializeField] Transform content;
	[SerializeField] LobbyPlayer lobbyPlayerPrefab;
	
	private List<LobbyPlayer> lobbyPlayers = new List<LobbyPlayer>();

	private void Awake()
	{
		GetCurrentRoomPlayers();
	}

	private void GetCurrentRoomPlayers()
	{
		foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
		{
			AddLobbyPlayer(playerInfo.Value);
		}
	}

	private void AddLobbyPlayer(Player player)
	{
		LobbyPlayer lobbyPlayer = Instantiate(lobbyPlayerPrefab, content);
		if (lobbyPlayer != null)
		{
			lobbyPlayer.SetPlayerInfo(player);
			lobbyPlayers.Add(lobbyPlayer);
		}
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		AddLobbyPlayer(newPlayer);
	}

	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
		int index = lobbyPlayers.FindIndex(x => x.Player == otherPlayer);
		if (index != -1)
		{
			Destroy(lobbyPlayers[index].gameObject);
			lobbyPlayers.RemoveAt(index);
		}
	}
}
